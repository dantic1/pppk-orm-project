using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public static class PrescriptionMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("--- PRESCRIPTIONS ---");
            System.Console.WriteLine("1. List all");
            System.Console.WriteLine("2. Add");
            System.Console.WriteLine("3. Update (end date)");
            System.Console.WriteLine("4. Delete");
            System.Console.WriteLine("0. Back");
            System.Console.Write("Choice: ");

            var input = System.Console.ReadLine();
            switch (input)
            {
                case "1": ListAll(context); break;
                case "2": Add(context); break;
                case "3": Update(context); break;
                case "4": Delete(context); break;
                case "0": return;
                default: System.Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    private static void ListAll(AppDbContext context)
    {
        var prescriptions = context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Medication)
            .Include(p => p.MedicalHistory)
                .ThenInclude(h => h!.Disease)
            .OrderBy(p => p.StartDate)
            .ToList();

        System.Console.WriteLine();
        if (prescriptions.Count == 0)
        {
            System.Console.WriteLine("No prescriptions yet.");
            return;
        }
        foreach (var p in prescriptions)
        {
            var endInfo = p.EndDate?.ToString() ?? "active";
            var forDisease = p.MedicalHistory is not null
                ? $" (for {p.MedicalHistory.Disease.Name})"
                : "";
            System.Console.WriteLine(
                $"[{p.Id}] {p.Patient.FirstName} {p.Patient.LastName} - {p.Medication.Name}{forDisease} " +
                $"| {p.Dosage}, {p.Frequency} | {p.StartDate} → {endInfo}");
        }
    }

    private static void Add(AppDbContext context)
    {
        // Patient
        var patients = context.Patients.ToList();
        if (patients.Count == 0) { System.Console.WriteLine("No patients."); return; }
        System.Console.WriteLine("--- Available patients ---");
        foreach (var pt in patients)
            System.Console.WriteLine($"[{pt.Id}] {pt.FirstName} {pt.LastName}");

        System.Console.Write("Patient ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var patientId) ||
            !context.Patients.Any(x => x.Id == patientId))
        {
            System.Console.WriteLine("Invalid patient.");
            return;
        }

        // Medication
        MedicationMenu.ListAll(context);
        if (!context.Medications.Any())
        {
            System.Console.WriteLine("No medications. Add one first.");
            return;
        }
        System.Console.Write("Medication ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var medId) ||
            !context.Medications.Any(x => x.Id == medId))
        {
            System.Console.WriteLine("Invalid medication.");
            return;
        }

        // MedicalHistory
        System.Console.Write("Link to a specific disease/history? (y/N): ");
        int? historyId = null;
        if (System.Console.ReadLine()?.Trim().ToLower() == "y")
        {
            var histories = context.MedicalHistories
                .Where(h => h.PatientId == patientId)
                .Include(h => h.Disease)
                .ToList();

            if (histories.Count == 0)
            {
                System.Console.WriteLine("No history for this patient. Skipping.");
            }
            else
            {
                System.Console.WriteLine("--- Patient's medical history ---");
                foreach (var h in histories)
                    System.Console.WriteLine($"[{h.Id}] {h.Disease.Name} ({h.StartDate} → {h.EndDate?.ToString() ?? "active"})");

                System.Console.Write("History ID: ");
                if (int.TryParse(System.Console.ReadLine(), out var hid) &&
                    histories.Any(x => x.Id == hid))
                {
                    historyId = hid;
                }
                else
                {
                    System.Console.WriteLine("Invalid or skipped.");
                }
            }
        }

        System.Console.Write("Dosage (e.g. 500mg): ");
        var dosage = System.Console.ReadLine() ?? "";

        System.Console.Write("Frequency (e.g. 3x daily): ");
        var frequency = System.Console.ReadLine() ?? "";

        System.Console.Write("Start date (yyyy-mm-dd): ");
        if (!DateOnly.TryParse(System.Console.ReadLine(), out var startDate))
        {
            System.Console.WriteLine("Invalid date.");
            return;
        }

        System.Console.Write("End date (yyyy-mm-dd, empty for ongoing): ");
        var endInput = System.Console.ReadLine();
        DateOnly? endDate = null;
        if (!string.IsNullOrWhiteSpace(endInput))
        {
            if (!DateOnly.TryParse(endInput, out var parsed))
            {
                System.Console.WriteLine("Invalid end date.");
                return;
            }
            endDate = parsed;
        }

        var prescription = new Prescription
        {
            PatientId = patientId,
            MedicationId = medId,
            MedicalHistoryId = historyId,
            Dosage = dosage,
            Frequency = frequency,
            StartDate = startDate,
            EndDate = endDate
        };

        context.Prescriptions.Add(prescription);
        context.SaveChanges();
        System.Console.WriteLine($"Prescription added with ID {prescription.Id}.");
    }

    private static void Update(AppDbContext context)
    {
        System.Console.Write("Prescription ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var prescription = context.Prescriptions.Find(id);
        if (prescription is null) { System.Console.WriteLine("Not found."); return; }

        System.Console.Write($"New end date (empty to clear) [{prescription.EndDate?.ToString() ?? "ongoing"}]: ");
        var input = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            prescription.EndDate = null;
        }
        else if (DateOnly.TryParse(input, out var parsed))
        {
            prescription.EndDate = parsed;
        }
        else
        {
            System.Console.WriteLine("Invalid date.");
            return;
        }

        context.SaveChanges();
        System.Console.WriteLine("Updated.");
    }

    private static void Delete(AppDbContext context)
    {
        System.Console.Write("Prescription ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var prescription = context.Prescriptions.Find(id);
        if (prescription is null) 
        { 
            System.Console.WriteLine("Not found."); 
            return; 
        }

        context.Prescriptions.Remove(prescription);
        context.SaveChanges();
        System.Console.WriteLine("Deleted.");
    }
}
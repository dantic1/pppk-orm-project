using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public static class MedicalHistoryMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("--- MEDICAL HISTORY ---");
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
        var histories = context.MedicalHistories
            .Include(h => h.Patient)
            .Include(h => h.Disease)
            .OrderBy(h => h.StartDate)
            .ToList();

        System.Console.WriteLine();

        if (histories.Count == 0)
        {
            System.Console.WriteLine("No medical histories found.");
            return;
        }

        foreach (var h in histories)
        {
            var endDateInfo = h.EndDate?.ToString() ?? "active";
            System.Console.WriteLine(
                $"[{h.Id}] {h.Patient.FirstName} {h.Patient.LastName} - {h.Disease.Name} " +
                $"| {h.StartDate} → {endDateInfo}");
        }
    }

     private static void Add(AppDbContext context)
    {
        //show paitents
        var patients = context.Patients.ToList();
        if (patients.Count == 0)
        {
            System.Console.WriteLine("No patients exist. Add a patient first.");
            return;
        }
        System.Console.WriteLine("--- Available patients ---");
        foreach (var p in patients)
            System.Console.WriteLine($"[{p.Id}] {p.FirstName} {p.LastName}");

        System.Console.Write("Patient ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var patientId) ||
            !context.Patients.Any(p => p.Id == patientId))
        {
            System.Console.WriteLine("Invalid patient.");
            return;
        }
        
        //show diseases
        DiseaseMenu.ListAll(context);
        var anyDisease = context.Diseases.Any();
        if (!anyDisease)
        {
            System.Console.WriteLine("No diseases exist. Add a disease first (main menu → Diseases).");
            return;
        }

        System.Console.Write("Disease ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var diseaseId) ||
            !context.Diseases.Any(d => d.Id == diseaseId))
        {
            System.Console.WriteLine("Invalid disease.");
            return;
        }

        System.Console.Write("Start date (yyyy-mm-dd): ");
        if (!DateOnly.TryParse(System.Console.ReadLine(), out var startDate))
        {
            System.Console.WriteLine("Invalid date.");
            return;
        }

        System.Console.Write("End date (yyyy-mm-dd, empty for active): ");
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

        var history = new MedicalHistory
        {
            PatientId = patientId,
            DiseaseId = diseaseId,
            StartDate = startDate,
            EndDate = endDate
        };

        context.MedicalHistories.Add(history);
        context.SaveChanges();
        System.Console.WriteLine($"Medical history added with ID {history.Id}.");
    }

    private static void Update(AppDbContext context)
    {
        System.Console.Write("Medical history ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var history = context.MedicalHistories.Find(id);
        if (history is null)
        {
            System.Console.WriteLine("Not found.");
            return;
        }

        System.Console.Write($"New end date (yyyy-mm-dd, empty to clear) [{history.EndDate?.ToString() ?? "active"}]: ");
        var input = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            history.EndDate = null;
        }
        else if (DateOnly.TryParse(input, out var parsed))
        {
            history.EndDate = parsed;
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
        System.Console.Write("Medical history ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var history = context.MedicalHistories.Find(id);
        if (history is null)
        {
            System.Console.WriteLine("Not found.");
            return;
        }

        context.MedicalHistories.Remove(history);
        context.SaveChanges();
        System.Console.WriteLine("Deleted.");
    }
}
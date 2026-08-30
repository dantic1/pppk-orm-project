using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Enums;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public static class ExaminationMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("--- EXAMINATIONS ---");
            System.Console.WriteLine("1. List all");
            System.Console.WriteLine("2. Schedule new");
            System.Console.WriteLine("3. Reschedule (change date)");
            System.Console.WriteLine("4. Cancel (delete)");
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
        var exams = context.Examinations
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
            .OrderBy(e => e.ScheduledAt)
            .ToList();

        System.Console.WriteLine();
        if (exams.Count == 0)
        {
            System.Console.WriteLine("No examinations yet.");
            return;
        }
        foreach (var e in exams)
        {
            System.Console.WriteLine(
                $"[{e.Id}] {e.ScheduledAt:yyyy-MM-dd HH:mm} | {e.ExaminationType} | " +
                $"Patient: {e.Patient.FirstName} {e.Patient.LastName} | " +
                $"Doctor: {e.Doctor.FirstName} {e.Doctor.LastName} ({e.Doctor.Specialization})");
        }
    }

    private static void Add(AppDbContext context)
    {
        // Patient
        var patients = context.Patients.ToList();
        if (patients.Count == 0)
        {
            System.Console.WriteLine("No patients.");
            return;
        }
        System.Console.WriteLine("--- Available patients ---");
        foreach (var p in patients)
            System.Console.WriteLine($"[{p.Id}] {p.FirstName} {p.LastName}");
        System.Console.Write("Patient ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var patientId) ||
            !context.Patients.Any(x => x.Id == patientId))
        {
            System.Console.WriteLine("Invalid patient.");
            return;
        }

        // Doctor
        var doctors = context.Doctors.ToList();
        System.Console.WriteLine("--- Available doctors ---");
        foreach (var d in doctors)
            System.Console.WriteLine($"[{d.Id}] {d.FirstName} {d.LastName} - {d.Specialization}");
        System.Console.Write("Doctor ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var doctorId) ||
            !context.Doctors.Any(x => x.Id == doctorId))
        {
            System.Console.WriteLine("Invalid doctor.");
            return;
        }

        // Examination type
        System.Console.WriteLine("--- Available examination types ---");
        foreach (var t in Enum.GetValues<ExaminationType>())
            System.Console.WriteLine($"  {t}");
        System.Console.Write("Type: ");
        var typeInput = (System.Console.ReadLine() ?? "").Trim().ToUpper();
        if (!Enum.TryParse<ExaminationType>(typeInput, out var examinationType))
        {
            System.Console.WriteLine("Invalid type.");
            return;
        }

        // Scheduled at
        System.Console.Write("Scheduled at (yyyy-mm-dd HH:mm): ");
        if (!DateTime.TryParse(System.Console.ReadLine(), out var scheduledAt))
        {
            System.Console.WriteLine("Invalid date/time.");
            return;
        }

        var exam = new Examination
        {
            PatientId = patientId,
            DoctorId = doctorId,
            ExaminationType = examinationType,
            ScheduledAt = scheduledAt
        };

        context.Examinations.Add(exam);
        context.SaveChanges();
        System.Console.WriteLine($"Examination scheduled with ID {exam.Id}.");
    }

    private static void Update(AppDbContext context)
    {
        System.Console.Write("Examination ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var exam = context.Examinations.Find(id);
        if (exam is null)
        {
            System.Console.WriteLine("Not found.");
            return;
        }

        System.Console.Write($"New scheduled at (yyyy-mm-dd HH:mm) [{exam.ScheduledAt:yyyy-MM-dd HH:mm}]: ");
        if (!DateTime.TryParse(System.Console.ReadLine(), out var newTime))
        {
            System.Console.WriteLine("Invalid date/time.");
            return;
        }

        exam.ScheduledAt = newTime;
        context.SaveChanges();
        System.Console.WriteLine("Rescheduled.");
    }

    private static void Delete(AppDbContext context)
    {
        System.Console.Write("Examination ID: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var exam = context.Examinations.Find(id);
        if (exam is null)
        {
            System.Console.WriteLine("Not found.");
            return;
        }

        context.Examinations.Remove(exam);
        context.SaveChanges();
        System.Console.WriteLine("Cancelled.");
    }
}
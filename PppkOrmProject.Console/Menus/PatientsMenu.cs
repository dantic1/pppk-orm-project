using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Enums;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public class PatientsMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine("--- PATIENTS ---");

            System.Console.WriteLine("1. List all");
            System.Console.WriteLine("2. Add");
            System.Console.WriteLine("3. Update");
            System.Console.WriteLine("4. Delete");
            System.Console.WriteLine("0. Back");
            System.Console.Write("Choice: ");

            var input = System.Console.ReadLine();

            switch (input)
            {
                case "1":
                    ListAll(context);
                    break;
                case "2":
                    Add(context);
                    break;
                case "3":
                    Update(context);
                    break;
                case "4":
                    Delete(context);
                    break;
                case "0":
                    return;
                default:
                    System.Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static void Delete(AppDbContext context)
    {
        System.Console.Write("Patient ID to delete: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var patient = context.Patients.Find(id);
        if (patient is null)
        {
            System.Console.WriteLine("Patient not found.");
            return;
        }

        System.Console.Write(
            $"Delete {patient.FirstName} {patient.LastName}? This also deletes their history/prescriptions/exams. (y/N): ");
        var confirm = System.Console.ReadLine();
        if (confirm?.Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Cancelled.");
            return;
        }

        context.Patients.Remove(patient);
        context.SaveChanges();
        System.Console.WriteLine("Patient deleted.");
    }

    private static void Update(AppDbContext context)
    {
        System.Console.Write("Patient ID to update: ");
        if (!int.TryParse(System.Console.ReadLine(), out var id))
        {
            System.Console.WriteLine("Invalid ID.");
            return;
        }

        var patient = context.Patients.Find(id);
        if (patient is null)
        {
            System.Console.WriteLine("Patient not found.");
            return;
        }

        System.Console.WriteLine($"Editing: {patient.FirstName} {patient.LastName}");
        System.Console.WriteLine("*Leave field empty to keep current value*");

        System.Console.Write($"First name [{patient.FirstName}]: ");
        var firstName = System.Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName)) patient.FirstName = firstName;

        System.Console.Write($"Last name [{patient.LastName}]: ");
        var lastName = System.Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName)) patient.LastName = lastName;

        System.Console.Write($"Residence [{patient.ResidenceAddress}]: ");
        var residence = System.Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(residence)) patient.ResidenceAddress = residence;

        System.Console.Write($"Permanent [{patient.PermanentAddress}]: ");
        var permanent = System.Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(permanent)) patient.PermanentAddress = permanent;
        
        try
        {
            context.SaveChanges();
            System.Console.WriteLine("Patient updated.");
        }
        catch (DbUpdateException ex)
        {
            System.Console.WriteLine($"Update failed: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    private static void Add(AppDbContext context)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("--- Add new patient ---");

        System.Console.Write("First name: ");
        var firstName = System.Console.ReadLine() ?? "";

        System.Console.Write("Last name: ");
        var lastName = System.Console.ReadLine() ?? "";

        System.Console.Write("OIB (11 digits): ");
        var oib = System.Console.ReadLine() ?? "";

        System.Console.Write("Birth date (yyyy-mm-dd): ");
        if (!DateOnly.TryParse(System.Console.ReadLine(), out var birthDate))
        {
            System.Console.WriteLine("Invalid date format.");
            return;
        }

        System.Console.Write("Gender (M/F): ");
        var genderInput = (System.Console.ReadLine() ?? "").Trim().ToUpper();
        var gender = genderInput switch
        {
            "M" => Gender.Male,
            "F" => Gender.Female,
            _ => (Gender?)null
        };
        if (gender is null)
        {
            System.Console.WriteLine("Invalid gender. Use M or F.");
            return;
        }

        System.Console.Write("Residence address: ");
        var residence = System.Console.ReadLine() ?? "";

        System.Console.Write("Permanent address: ");
        var permanent = System.Console.ReadLine() ?? "";

        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Oib = oib,
            BirthDate = birthDate,
            Gender = gender.Value,
            ResidenceAddress = residence,
            PermanentAddress = permanent,
        };

        try
        {
            context.Patients.Add(patient);
            context.SaveChanges();
            System.Console.WriteLine("Patient added - ID: {0}", patient.Id);
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Failed to add patient: {e.InnerException?.Message ?? e.Message}");
        }
    }
    
    private static void ListAll(AppDbContext context)
    {
        var patients = context.Patients.ToList();

        System.Console.WriteLine();

        if (patients.Count == 0)
        {
            System.Console.WriteLine("There are no Patients\n");
            return;
        }

        foreach (var patient in patients)
        {
            System.Console.WriteLine($"{patient.Id} - {patient.FirstName} {patient.LastName} \n" +
                                     $"Born: {patient.BirthDate} | OIB: {patient.Oib}");
        }
    }
}
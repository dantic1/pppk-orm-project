using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public static class MedicationMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("--- MEDICATIONS ---");
            System.Console.WriteLine("1. List all");
            System.Console.WriteLine("2. Add");
            System.Console.WriteLine("0. Back");
            System.Console.Write("Choice: ");

            var input = System.Console.ReadLine();
            switch (input)
            {
                case "1": ListAll(context); break;
                case "2": Add(context); break;
                case "0": return;
                default: System.Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    public static void ListAll(AppDbContext context)
    {
        var meds = context.Medications.OrderBy(m => m.Name).ToList();
        System.Console.WriteLine();
        if (meds.Count == 0)
        {
            System.Console.WriteLine("No medications yet.");
            return;
        }
        foreach (var m in meds)
        {
            System.Console.WriteLine($"[{m.Id}] {m.Name}{(m.Manufacturer is null ? "" : $" ({m.Manufacturer})")}");
        }
    }

    private static void Add(AppDbContext context)
    {
        System.Console.Write("Name: ");
        var name = System.Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(name))
        {
            System.Console.WriteLine("Name required.");
            return;
        }

        System.Console.Write("Manufacturer (optional): ");
        var manufacturer = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            manufacturer = null;
        }

        try
        {
            context.Medications.Add(new Medication
            {
                Name = name,
                Manufacturer = manufacturer
            });
            
            context.SaveChanges();
            System.Console.WriteLine("Medication added.");
        }
        catch (DbUpdateException ex)
        {
            System.Console.WriteLine($"Failed: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
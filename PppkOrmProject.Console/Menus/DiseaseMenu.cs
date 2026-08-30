using Microsoft.EntityFrameworkCore;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Menus;

public static class DiseaseMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("--- DISEASES ---");
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
        var diseases = context.Diseases.OrderBy(d => d.Name).ToList();
        System.Console.WriteLine();
        if (diseases.Count == 0)
        {
            System.Console.WriteLine("No diseases yet.");
            return;
        }
        foreach (var d in diseases)
        {
            System.Console.WriteLine($"[{d.Id}] {d.Name}{(d.Description is null ? "" : $" - {d.Description}")}");
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

        System.Console.Write("Description (optional): ");
        var description = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(description))
        {
            description = null;
        }

        try
        {
            context.Diseases.Add(new Disease
            {
                Name = name, 
                Description = description
            });
            
            context.SaveChanges();
            System.Console.WriteLine("Disease added.");
        }
        catch (DbUpdateException ex)
        {
            System.Console.WriteLine($"Failed: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
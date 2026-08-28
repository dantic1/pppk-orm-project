using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PppkOrmProject.Data;
using PppkOrmProject.Data.Migrations;

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
        throw new NotImplementedException();
    }

    private static void Update(AppDbContext context)
    {
        throw new NotImplementedException();
    }

    private static void Add(AppDbContext context)
    {
        throw new NotImplementedException();
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
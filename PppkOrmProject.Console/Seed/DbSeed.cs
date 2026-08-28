using PppkOrmProject.Data;
using PppkOrmProject.Data.Models;

namespace PppkOrmProject.Console.Seed;

public class DbSeed
{
    public static void SeedDoctors(AppDbContext context)
    {
        if (context.Doctors.Any())
        {
            return;
        }

        var doctors = new List<Doctor>
        {
            new() { FirstName = "Ana", LastName = "Anić", Specialization = "Radiolog" },
            new() { FirstName = "Marko", LastName = "Marković", Specialization = "Kardiolog" },
            new() { FirstName = "Ivan", LastName = "Ivić", Specialization = "Oftalmolog" },
            new() { FirstName = "Petra", LastName = "Perić", Specialization = "Dermatolog" },
            new() { FirstName = "Tomislav", LastName = "Tomić", Specialization = "Stomatolog" },
            new() { FirstName = "Iva", LastName = "Ivanović", Specialization = "Neurolog" }
        };

        context.Doctors.AddRange(doctors);
        context.SaveChanges();

        System.Console.WriteLine($"Seeded {doctors.Count} doctors");
    }
}
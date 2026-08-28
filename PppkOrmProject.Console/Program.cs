using PppkOrmProject.Console.Seed;
using PppkOrmProject.Data;

using var context = new AppDbContext();

if (!context.Database.CanConnect())
{
    Console.WriteLine("Can't connect to database");
    return;
}

DbSeed.SeedDoctors(context);
Console.WriteLine("Seeded completed");


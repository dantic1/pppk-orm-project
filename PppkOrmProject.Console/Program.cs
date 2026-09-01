using PppkOrmProject.Console.Menus;
using PppkOrmProject.Console.Seed;
using PppkOrmProject.Data;
using PppkOrmProject.Data.TestOrm;

using var context = new AppDbContext();

if (!context.Database.CanConnect())
{
    Console.WriteLine("Can't connect to database");
    return;
}

DbSeed.SeedDoctors(context);
Console.WriteLine("Seeded completed");

MainMenu.Show(context);

// === Mini ORM demo ===
System.Console.WriteLine();
System.Console.WriteLine("--MINI ORM DEMO --");

var connectionString = "Host=localhost;Port=5432;Database=hospital;Username=doctor;Password=password";
var testOrm= new TestOrmDbContext<PppkOrmProject.Data.Models.Disease>(connectionString);

var diseases = testOrm.GetAll();

System.Console.WriteLine($"[MiniOrm] Loaded {diseases.Count} diseases:");
foreach (var d in diseases)
{
    System.Console.WriteLine($"  [{d.Id}] {d.Name} - {d.Description ?? "(no description)"}");
}


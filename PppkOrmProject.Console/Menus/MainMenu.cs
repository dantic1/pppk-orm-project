using PppkOrmProject.Data;

namespace PppkOrmProject.Console.Menus;

public static class MainMenu
{
    public static void Show(AppDbContext context)
    {
        while (true)
        {
            System.Console.WriteLine("""
                                      
                                     ###################
                                     ###  MAIN MENU  ###
                                     ###################
                                     """);

            System.Console.WriteLine("1. Patients");
            System.Console.WriteLine("2. Medical History");
            System.Console.WriteLine("3. Examinations");
            System.Console.WriteLine("4. Prescriptions");
            System.Console.WriteLine("5. List all doctors");
            System.Console.WriteLine("0. Exit");
            System.Console.Write("Choice: ");
            
            var input = System.Console.ReadLine();

            switch (input)
            {
                case "1":
                    PatientsMenu.Show(context);
                    break;
                case "5":
                    ListAllDoctors(context);
                    break;
                case "0":
                    return;
                default:
                    System.Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
    
    private static void ListAllDoctors(AppDbContext context)
    {
        var doctors = context.Doctors.ToList();
        System.Console.WriteLine();
        System.Console.WriteLine("--- Doctors ---");
        foreach (var doctor in doctors)
        {
            System.Console.WriteLine($"{doctor.Id} - {doctor.FirstName} {doctor.LastName} - {doctor.Specialization}");
        }
    }
}
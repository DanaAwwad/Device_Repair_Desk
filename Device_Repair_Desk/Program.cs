using Device_Repair_Desk.Enums;
using Device_Repair_Desk.Modles;
using Device_Repair_Desk.Services;
using System.Diagnostics;
using System.Linq.Expressions;
//Load
FileService fileService = new FileService();
List<RapairJob> jobs;
try
{
    jobs = await fileService.LoadAsync();
}
catch (Exception e)
{
    Console.WriteLine($"File is not exist {e.Message}");
    jobs = new List<RapairJob>();
}
RapairManager rapairManager = new RapairManager(jobs);
// rapairManager.ListJob(jobs);


//Menu 
Console.WriteLine("1: Add Repair Job");
Console.WriteLine("2: List Repair Jobs");
Console.WriteLine("3: Search Rapair Job");
Console.WriteLine("4: Start Rapair");
Console.WriteLine("5: Complete Rapair");
Console.WriteLine("6: Show Estimated Cost");
Console.WriteLine("0: Exit");
int choice;
while (!int.TryParse(Console.ReadLine(), out choice))
{
    Console.WriteLine("Please enter a valid number ");
}
while (true)
{
    try
    {
        switch (choice)
        {
            case 1:
                RapairJob job = new RapairJob();

                Console.WriteLine("Enter Id:");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Please Enter number");
                }
                job.Id = id;

                Console.WriteLine("Customer Name:");
                String name = Console.ReadLine();
                job.CustomerName = name;

                Console.WriteLine("Device Name:");
                String nameDevice = Console.ReadLine();
                job.DeviceName = nameDevice;

                Console.WriteLine("if you have any nots of problem :");
                String? nots = Console.ReadLine();
                job.ProblemNotes = nots;

                int typeDevice;
                Console.WriteLine("Device Type:");
                Console.WriteLine("Enter 1 of your device is 'Laptop'");
                Console.WriteLine("Enter 2 of your device is 'Phone'");
                while (!int.TryParse(Console.ReadLine(), out typeDevice))
                {
                    Console.WriteLine("Please Enter number");
                }
                job.DeviceType = (DeviceType)typeDevice;

                rapairManager.AddJob(job);
                break;

            case 2:
                rapairManager.ListJob(jobs);
                break;
            case 3:
                Console.WriteLine("Enter customer or device name:");
                string text = Console.ReadLine();
                if (text != null)
                {
                    if (rapairManager.SearchJob(text))
                    {
                        Console.WriteLine("Job is Found");
                    }
                    else
                    {
                        Console.WriteLine("Job is not Found");
                    }
                }
                else
                {
                    Console.WriteLine("You put a null string");
                }
                break;
            case 4:
                int Id2;
                Console.WriteLine("Enter Id to start Rapair:");
                while (!int.TryParse(Console.ReadLine(), out Id2))
                {
                    Console.WriteLine("Please Enter number");
                }
                rapairManager.StartRapair(Id2);
                break;
            case 5:
                int Id4;
                Console.WriteLine("Enter Id to Complete Rapair:");
                while (!int.TryParse(Console.ReadLine(), out Id4))
                {
                    Console.WriteLine("Please Enter number");
                }
                rapairManager.CompleteRapair(Id4);
                break;
            case 6:
                int Id3;
                Console.WriteLine("Enter Id to show the Estimated Cost:");
                while (!int.TryParse(Console.ReadLine(), out Id3))
                {
                    Console.WriteLine("Please Enter number");
                }
                rapairManager.GetEstimatedCost(Id3);
                break;
            case 0:
                try
                {
                    await fileService.SaveAsync(jobs);
                    Console.WriteLine("Jobs saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save jobs: {ex.Message}");
                }
                return;
            default:
                Console.WriteLine("Invalid menu choice.");
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}


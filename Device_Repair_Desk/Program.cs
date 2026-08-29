using Device_Repair_Desk.Enums;
using Device_Repair_Desk.Models;
using Device_Repair_Desk.Services;


//Load
List<RepairJob> jobs;
FileService fileService = new FileService();

try
{
    jobs = await fileService.LoadAsync();
}
catch (Exception e)
{
    Console.WriteLine($"Failed to load {e.Message}");
    jobs = new List<RepairJob>();
}

RepairManager repairManager = new RepairManager(jobs);

while (true)
{
    Console.WriteLine("1: Add Repair Job");
    Console.WriteLine("2: List Repair Jobs");
    Console.WriteLine("3: Search Rapair Job");
    Console.WriteLine("4: Start Rapair");
    Console.WriteLine("5: Complete Rapair");
    Console.WriteLine("6: Show Estimated Cost");
    Console.WriteLine("0: Exit");

    Console.WriteLine("Choose an option: ");

    int choice;
    while (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.WriteLine("Please enter a valid number ");
    }

    try
    {
        switch (choice)
        {
            case 1:
                RepairJob job = new RepairJob();

                Console.WriteLine("Enter Id:");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Please Enter valid id");
                }
                job.Id = id;

                Console.WriteLine("Customer Name:");
                string name = Console.ReadLine();
                job.CustomerName = name;

                Console.WriteLine("Device Name:");
                string nameDevice = Console.ReadLine();
                job.DeviceName = nameDevice;

                Console.WriteLine("if you have any notes of problem :");
                string? notes = Console.ReadLine();
                job.ProblemNotes = notes;

                int typeDevice;
                Console.WriteLine("Device Type:");
                Console.WriteLine("Enter 1 of your device is 'Laptop'");
                Console.WriteLine("Enter 2 of your device is 'Phone'");
                while (!int.TryParse(Console.ReadLine(), out typeDevice))
                {
                    Console.WriteLine("Please Enter valid type");
                }
                job.DeviceType = (DeviceType)typeDevice;

                repairManager.AddJob(job);

                Console.WriteLine("Job added successfully.");
                break;

            case 2:
                repairManager.ListJob();
                break;

            case 3:
                Console.WriteLine("Enter customer or device name:");

                string text = Console.ReadLine();

                List<RepairJob> results = repairManager.SearchJobs(text);

                if (results.Count == 0)
                    Console.WriteLine("No jobs found");
                else
                {
                    foreach (RepairJob result in results)
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine($"ID: {result.Id}");
                        Console.WriteLine($"Customer Name: {result.CustomerName}");
                        Console.WriteLine($"Device Name: {result.DeviceName}");
                        Console.WriteLine($"Problem Notes: {result.ProblemNotes ?? "No notes"}");
                        Console.WriteLine($"Device Type: {result.DeviceType}");
                        Console.WriteLine($"Status: {result.Status}");
                        Console.WriteLine($"Completed At: {result.CompletedAt?.ToString() ?? "Not completed"}");
                        Console.WriteLine("------------------------------");
                    }
                }
                break;

            case 4:
                int Id2;
                Console.WriteLine("Enter Id to start Repair:");

                while (!int.TryParse(Console.ReadLine(), out Id2))
                {
                    Console.WriteLine("Please Enter number");
                }

                repairManager.StartRepair(Id2);

                Console.WriteLine("Repair started successfully.");
                break;

            case 5:
                int Id4;

                Console.WriteLine("Enter Id to Complete Repair:");

                while (!int.TryParse(Console.ReadLine(), out Id4))
                {
                    Console.WriteLine("Please Enter number");
                }

                repairManager.CompleteRepair(Id4);

                Console.WriteLine("Repair completed successfully.");
                break;

            case 6:
                int Id3;

                Console.WriteLine("Enter Id to show the Estimated Cost:");

                while (!int.TryParse(Console.ReadLine(), out Id3))
                {
                    Console.WriteLine("Please Enter number");
                }

                decimal cost = repairManager.GetEstimatedCost(Id3);
                Console.WriteLine($"Estimated Cost: ${cost}");

                break;

            case 0:
                await fileService.SaveAsync(jobs);
                Console.WriteLine("Jobs saved successfully.");

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


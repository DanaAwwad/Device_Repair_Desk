using Device_Repair_Desk.Modles;
using Device_Repair_Desk.Services;
// Use a RapairService reference to demonstrate polymorphism
RapairService service = new PhoneRapairService();
Console.WriteLine(service.GetServiceName());

//hello 

//create object of RapirJob
RapairJob job= new RapairJob();
/*Console.Write("Enter id: ");
job.Id = int.Parse(Console.ReadLine());
Console.Write("Enter Customer Name: ");
job.CustomerName = Console.ReadLine();
Console.Write("Enter Device Name: ");
job.DeviceName = Console.ReadLine();
Console.Write("if you have any problem nots: ");
job.CustomerName = Console.ReadLine() ?? null;*/


Console.WriteLine(job.ProblemNotes ?? "No notes");
RapairManager rapairManager = new RapairManager();
rapairManager.AddJob(job);
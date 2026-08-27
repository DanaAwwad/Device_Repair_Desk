using Device_Repair_Desk.Modles;
using Device_Repair_Desk.Services;

FileService fileService = new FileService();
List<RapairJob> jobs;
try
{
    jobs= await fileService.LoadAsync();
}
catch (Exception e)
{
    Console.WriteLine($"File is not exist {e.Message}");
    jobs= new List<RapairJob>();
}
RapairManager rapairManager= new RapairManager(jobs);
rapairManager.ListJob(jobs);


    


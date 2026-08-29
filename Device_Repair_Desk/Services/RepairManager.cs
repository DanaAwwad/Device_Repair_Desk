using Device_Repair_Desk.Enums;
using Device_Repair_Desk.Interface;
using Device_Repair_Desk.Models;

namespace Device_Repair_Desk.Services;

public class RepairManager
{
    private List<RepairJob> jobs;

    public RepairManager(List<RepairJob> jobs)
    {
        this.jobs = jobs;
    }

    public void AddJob(RepairJob job)
    {
        if (string.IsNullOrWhiteSpace(job.CustomerName) ||
            string.IsNullOrWhiteSpace(job.DeviceName) ||
            job.Id <= 0 ||
            ((int)job.DeviceType != 1 && (int)job.DeviceType != 2))

            throw new ArgumentException("You Have to put all the requierments : CustomerName , DeviceName, Id , DeviceType: Phone or Laptop");

        job.CustomerName = job.CustomerName.Trim().ToLower();
        job.DeviceName = job.DeviceName.Trim().ToLower();

        job.ProblemNotes = string.IsNullOrWhiteSpace(job.ProblemNotes) ? null : job.ProblemNotes;

        job.Status = RepairStatus.Recieved;
        job.CompletedAt = null;

        jobs.Add(job);
    }

    public void ListJob()
    {
        foreach (RepairJob job in jobs)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"ID: {job.Id}");
            Console.WriteLine($"Customer Name: {job.CustomerName}");
            Console.WriteLine($"Device Name: {job.DeviceName}");
            Console.WriteLine($"Problem Notes: {job.ProblemNotes ?? "No notes"}");
            Console.WriteLine($"Device Type: {job.DeviceType}");
            Console.WriteLine($"Status: {job.Status}");
            Console.WriteLine($"Completed At: {job.CompletedAt?.ToString() ?? "Not completed"}");
            Console.WriteLine("------------------------------");
        }
    }

    public List<RepairJob> SearchJobs(string text)
    {
        List<RepairJob> results = new List<RepairJob>();

        if(string.IsNullOrWhiteSpace(text))
            return results;

        text = text.Trim().ToLower();

        foreach (RepairJob job in jobs)
        { 
            if(job.DeviceName.Contains(text) || job.CustomerName.Contains(text))
            {
                results.Add(job);
            }
        }
        return results;
    }

    public RepairJob GetJobById(int id)
    {
        foreach (RepairJob job in jobs)
        {
            if (id == job.Id)
            {
                return job;
            }
        }

        throw new KeyNotFoundException("The Id is not exist");
    }

    public void StartRepair(int id) 
    {
        RepairJob job = GetJobById(id);

        if (job.Status != RepairStatus.Recieved)
            throw new InvalidOperationException("Repair job must be in Received status");

        job.Status = RepairStatus.InProgress;
    }

    public void CompleteRepair(int id)
    {
        RepairJob job = GetJobById(id);

        if (job.Status != RepairStatus.InProgress)
            throw new InvalidOperationException("Repair job must be in InProgress status");

        job.Status = RepairStatus.Completed;
        job.CompletedAt = DateTime.UtcNow;
    }

    public decimal GetEstimatedCost(int id)
    {
        RepairJob job = GetJobById(id);

        ICostEstimatable cost;

        if (job.DeviceType == DeviceType.Laptop)
        {
            cost = new LaptopRepairService();
        }
        else
        {
            cost = new PhoneRepairService();
        }

        return cost.CalculateEstimatedCost();
    }
}


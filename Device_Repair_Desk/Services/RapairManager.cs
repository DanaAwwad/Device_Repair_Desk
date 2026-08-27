using Device_Repair_Desk.Enums;
using Device_Repair_Desk.Interface;
using Device_Repair_Desk.Modles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Device_Repair_Desk.Services
{
    public class RapairManager
    {
        List<RapairJob> jobs; //private
        public void AddJob(RapairJob job)
        {
            if (string.IsNullOrWhiteSpace(job.CustomerName) || string.IsNullOrWhiteSpace(job.DeviceName)
                || job.Id <= 0 || ((int)job.DeviceType != 1 && (int)job.DeviceType != 2))

                throw new ArgumentNullException(
                    "You Have to put all the requierments : Customer_Name , Device_Name, Id , Device_Type: Phone or Laptop");

            job.CustomerName = job.CustomerName.Trim().ToLower();
            job.DeviceName = job.DeviceName.Trim().ToLower();
            job.ProblemNotes = job.ProblemNotes ?? "No notes"; // if problemNots is null put "No Nots"
            job.Status = RepairStatus.Recieved;
            job.CompletedAt = null;
            jobs.Add(job);
        }

        public void ListJob(List<RapairJob> jobs)
        {
            foreach (RapairJob job in jobs)
            {
                Console.WriteLine($"ID:{job.Id},Coustomer Name: {job.CustomerName}" +
                    $"Device Name : {job.DeviceName} , Problem Nots:{job.ProblemNotes} " +
                   $"Device Type:{job.DeviceType} , Status :{job.Status}" +
                   $"Completed At : {job.CompletedAt} ");
            }
        }

        public bool SearchJob(string text)
        {
            text = text.Trim().ToLower();
            foreach (RapairJob job in jobs)
            {
                if (job.DeviceName.Contains(text) || job.CustomerName.Contains(text))
                    return true;
            }
            return false;
        }
        public RapairJob GetJobById(int id)
        {
            foreach (RapairJob job in jobs)
            {
                if (id == job.Id)
                {
                    return job;
                }
            }
            throw new KeyNotFoundException("The Id is not exist");
        }

        public void StartRapair(int id)   //you have use GetJob method 
                                          // foreach is not required
        {
            RapairJob job = GetJobById(id);

            if (job.Status != RepairStatus.Recieved) // is better way
                throw new InvalidOperationException("Repair job must be in Received status");

            job.Status = RepairStatus.InProgress;
        }

        public void CompleteRapair(int id)
        {
            RapairJob job = GetJobById(id);

            if (job.Status != RepairStatus.InProgress)
                throw new InvalidOperationException("Repair job must be in InProgress status");

            job.Status = RepairStatus.Completed;
            job.CompletedAt = DateTime.Now;
        }


        public decimal GetEstimatedCost(int id)
        {
            RapairJob job = GetJobById(id);
            ICostEstimatable cost;
            if ((int)job.Status == 1)
            {
                cost = new LaptopRapairService();
                return cost.CalculateEstimatedCost();
            }
            else
            {
                cost = new PhoneRapairService();
                return cost.CalculateEstimatedCost();
            }
        }
    }
}


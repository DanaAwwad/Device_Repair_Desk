using Device_Repair_Desk.Enums;
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
            if (string.IsNullOrEmpty(job.CustomerName))
                throw new ArgumentNullException("The Coustomer Name is required");
            if (string.IsNullOrEmpty(job.DeviceName))
                throw new ArgumentNullException("The Device Name is required");
            jobs.Add(job);
        }
        public void ListJob()
        {

        }
        public bool SearchJob(string text)
        {
            foreach (RapairJob job in jobs)
            {
               if(job.DeviceName.Contains(text) || job.CustomerName.Contains(text))
            }
        }
        public RapairJob GetJobbyId(int id) 
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
        public void StartRapair(int id)
        {
            foreach (RapairJob job in jobs)
            {
                if (id == job.Id)
                {
                    if (job.Status == RepairStatus.Recieved)
                    {
                        job.Status = RepairStatus.InProgress;
                        return; // stop the method
                    }
                    else
                    {
                        throw new InvalidOperationException("Repair job must be in Received status");
                      
                    }
                }
            }

            throw new KeyNotFoundException("Repair job ID does not exist");
        }
        public void CompleteRapair(int id) 
        {
            foreach (RapairJob job in jobs)
            {
                if (id == job.Id)
                {
                    if (job.Status == RepairStatus.InProgress)
                    {
                        job.Status = RepairStatus.Completed;
                        job.CompletedAt= DateTime.Now;
                        return; // stop the method
                    }
                    else
                    {
                        throw new InvalidOperationException("Repair job must be in InProgress status");

                    }
                }
            }

            throw new KeyNotFoundException("Repair job ID does not exist");
        }
            
        
        public void  GetEstimatedCost(int id)
        {
            foreach(RapairJob job in jobs)
            {
                if(id == job.Id)
                { 
                    Console.WriteLine(job.CalculateEstimatedCost);
                    return;
                }
                
            }

        }


    }
}

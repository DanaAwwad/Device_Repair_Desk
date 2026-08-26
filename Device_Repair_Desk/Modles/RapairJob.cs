using Device_Repair_Desk.Enums;
using Device_Repair_Desk.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Device_Repair_Desk.Modles
{
    public class RapairJob : ICostEstimatable
    {
        public int Id {  get; set; }
        public string CustomerName { get; set; }
        public string DeviceName { get; set; }
       public  string? ProblemNotes { get; set; }
       public  DeviceType DeviceType { get; set; }
       public RepairStatus Status { get; set; }
       public DateTime? CompletedAt { get; set; }



        public decimal CalculateEstimatedCost(RapairJob job)
        {
            if ((int)job.DeviceType == 1)
                return 50;
            else 
                return 100;
        }
    }
}

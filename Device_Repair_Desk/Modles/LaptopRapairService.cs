using Device_Repair_Desk.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Device_Repair_Desk.Modles
{
    public class LaptopRapairService : RapairService, ICostEstimatable
    {
        public override string GetServiceName()
        {
            return "Laptop Rapair Service";
        }
        public decimal CalculateEstimatedCost()
        {
            return 100;
        }
    }
}

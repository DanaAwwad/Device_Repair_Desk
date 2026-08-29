using Device_Repair_Desk.Interface;

namespace Device_Repair_Desk.Models;

public class LaptopRepairService : RepairService, ICostEstimatable
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

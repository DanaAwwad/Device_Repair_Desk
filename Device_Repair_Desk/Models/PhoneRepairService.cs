using Device_Repair_Desk.Interface;

namespace Device_Repair_Desk.Models;

public class PhoneRepairService : RepairService, ICostEstimatable
{
    public override string GetServiceName()
    {
        return "Phone Rapair Service";
    }

    public decimal CalculateEstimatedCost()
    {
        return 50;
    }
}

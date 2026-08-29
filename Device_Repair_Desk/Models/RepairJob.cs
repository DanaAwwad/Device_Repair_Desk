using Device_Repair_Desk.Enums;

namespace Device_Repair_Desk.Models;

public class RepairJob
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string DeviceName { get; set; }
    public string? ProblemNotes { get; set; }
    public DeviceType DeviceType { get; set; }
    public RepairStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }
}

using Device_Repair_Desk.Models;
using System.Text.Json;

namespace Device_Repair_Desk.Services;

public class FileService
{
    private readonly string file = "DataFile.json";

    public async Task<List<RepairJob>> LoadAsync()    //Task means the result will be returned asynchronously.
    {
        if (!File.Exists(file))
            return new List<RepairJob>();

        string json = await File.ReadAllTextAsync(file);

        List<RepairJob>? jobs = JsonSerializer.Deserialize<List<RepairJob>>(json);

        return jobs ?? new List<RepairJob>();
    }

    public async Task SaveAsync(List<RepairJob> jobs)
    {
        string json = JsonSerializer.Serialize(jobs);

        await File.WriteAllTextAsync(file, json);
    }
}

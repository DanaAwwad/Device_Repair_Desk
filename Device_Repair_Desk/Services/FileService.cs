using Device_Repair_Desk.Modles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Device_Repair_Desk.Services
{
    public class FileService
    {
        string file = "DataFile.json";
        public async Task<List<RapairJob>> LoadAsync()    // Task means the result will be returned asynchronously.
        {
            if (!File.Exists(file))
                throw new Exception("File is not exist!");
            string json = await File.ReadAllTextAsync(file);
            List<RapairJob>? jobs = JsonSerializer.Deserialize<List<RapairJob>>(json);
            return jobs ?? new List<RapairJob>();
        }


        public async Task SaveAsync(List<RapairJob> jobs)
        {
            string json = JsonSerializer.Serialize(jobs);
            await File.WriteAllTextAsync(file, json);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using taskManagementModels;

namespace taskManagementDataService
{
    public class taskJsonData : ItaskDataService
    {
        private List<taskItem> taskItems = new List<taskItem>();
        private string _jsonFileName;
        public taskJsonData() { 
        _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/taskJson.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (taskItems.Count <= 0)
            {
                taskItems.Add(new taskItem { TaskId = Guid.NewGuid(), TaskName = "Codings" });
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var stream = File.Open(_jsonFileName, FileMode.Create))
            {
                JsonSerializer.Serialize(stream, taskItems, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.taskItems = JsonSerializer.Deserialize<List<taskItem>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        public void Add(taskItem taskItem)
        {
            taskItems.Add(taskItem);
            SaveDataToJsonFile();
        }
        public void UpdateTask(taskItem taskItem)
        {
            RetrieveDataFromJsonFile();

            var existingTask = taskItems.FirstOrDefault(x => x.TaskId == taskItem.TaskId);
            if (existingTask != null)
            {
                existingTask.TaskName = taskItem.TaskName;
            }
            SaveDataToJsonFile();
        }
        public void DeleteTask(Guid id)
        {
            RetrieveDataFromJsonFile();

            var existingTask = taskItems.FirstOrDefault(x => x.TaskId == id);
            if (existingTask != null)
            {
                taskItems.Remove(existingTask);
            }
            SaveDataToJsonFile();
        }
        public void TaskCompleted(Guid id){
            RetrieveDataFromJsonFile();

            var existingTask = taskItems.FirstOrDefault(x => x.TaskId == id);
            if (existingTask != null)
            {
                existingTask.TaskId = id;
                existingTask.IsCompleted = true;
                
            }
            SaveDataToJsonFile();
        }
        public List<taskItem> GetTasks()
        {
            RetrieveDataFromJsonFile();
            return taskItems;
        }
    }
}

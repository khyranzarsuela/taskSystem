using System;
using System.Collections.Generic;
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
                taskItems.Add(new taskItem { TaskId = Guid.NewGuid()});
                taskItems.Add(new taskItem { TaskName = "Codings" });
 
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<taskItem>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , taskItems);
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
        public List<taskItem> GetTasks()
        {
            RetrieveDataFromJsonFile ();
            return taskItems;
        }
        public taskItem? GetById(Guid id)
        {
            RetrieveDataFromJsonFile();
            return taskItems.FirstOrDefault(t => t.TaskId == id);
        }
        public taskItem? GetTaskItem(string taskname)
        {
            return GetTasks().FirstOrDefault(t => t.TaskName == taskname);
        }
    }
}

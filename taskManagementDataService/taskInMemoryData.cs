using System;
using System.Collections.Generic;
using System.Linq;
using taskManagementModels;

namespace taskManagementDataService
{
    public class taskInMemoryData:ItaskDataService
    {
        public List<taskItem> tasks = new List<taskItem>();

        //  private taskDBData taskDBData;

        //public taskInMemoryData() { 
        //}
        //public taskInMemoryData(taskDBData taskDBData)
        //{
        //    this.taskDBData = taskDBData;
        //}


        public void Add(taskItem task)
        {
            tasks.Add(task);
        }
        public void UpdateTask(taskItem task)
        {
            var existing = tasks.FirstOrDefault(t => t.TaskId == task.TaskId);

            if (existing != null)
            {
                existing.TaskName = task.TaskName;
                existing.IsCompleted = task.IsCompleted;
            }
        }
        public void DeleteTask(Guid id)
        {
            var task = tasks.FirstOrDefault(t => t.TaskId == id);

            if (task != null)
            {
                tasks.Remove(task);
            }

        }
        public void TaskCompleted(Guid id)
        {
            var task = tasks.FirstOrDefault(t => t.TaskId == id);

            if (task != null)
            {
                task.IsCompleted = true;
            }
        }
        public List<taskItem> GetTasks()
        {
            return tasks;
        }

    }
}
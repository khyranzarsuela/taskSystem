using System;
using System.Collections.Generic;
using System.Linq;
using taskManagementModels;

namespace taskManagementDataService
{
    public class taskDataService
    {
        public List<taskItem> tasks = new List<taskItem>();

        public void Add(taskItem task)
        {
            tasks.Add(task);
        }

        public List<taskItem> GetTasks()
        {
            return tasks;
        }

        public taskItem? GetById(Guid id)
        {
            return tasks.FirstOrDefault(t => t.TaskId == id);
        }
    }
}
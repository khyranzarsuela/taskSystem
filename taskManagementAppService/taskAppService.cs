using System;
using System.Collections.Generic;
using taskManagementModels;
using taskManagementDataService;

namespace taskManagementAppService
{
    public class taskAppService
    {
        taskDataService TaskDataService = new taskDataService();

        public void AddTask(string taskName)
        {
            var task = new taskItem
            {
                TaskId = Guid.NewGuid(),    
                TaskName = taskName
            };

            TaskDataService.Add(task);
        }

        public List<taskItem> GetTasks()
        {
            return TaskDataService.GetTasks();
        }
    }
}
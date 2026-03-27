using System;
using System.Collections.Generic;
using taskManagementModels;
using taskManagementDataService;

namespace taskManagementAppService
{
    public class taskAppService
    {
        taskDataService taskdataservice = new taskDataService(new taskDBData());
        taskInMemoryData taskinmemorydata = new taskInMemoryData();
        taskJsonData taskjsondata = new taskJsonData();
       
        public taskAppService() {
           
        }
        public void AddTask(string taskName)
        {
            var task = new taskItem
            {
                TaskId = Guid.NewGuid(),
                TaskName = taskName
            };
           
            taskdataservice.Add(task);
            taskjsondata.Add(task);
            taskinmemorydata.Add(task);
        }

        public List<taskItem> GetTasks()
        {
            
          return taskdataservice.GetTasks();
            return taskjsondata.GetTasks();
            return taskinmemorydata.GetTasks();
        }
    }
}
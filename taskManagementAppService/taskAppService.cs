using System;
using System.Collections.Generic;
using taskManagementModels;
using taskManagementDataService;

namespace taskManagementAppService
{
    public class taskAppService
    {
        taskDataService taskdataservice = new taskDataService(new taskDBData());
       // taskInMemoryData taskinmemorydata = new taskInMemoryData();

        //public taskAppService() { 
        //taskDBData taskdbdata = new taskDBData();
        //} 
        public void AddTask(string taskName)
        {
            var task = new taskItem
            {
                TaskId = Guid.NewGuid(),    
                TaskName = taskName
            };

           // taskinmemorydata.Add(task);
            taskdataservice.Add(task);
        }

        public List<taskItem> GetTasks()
        {
          //  return taskinmemorydata.GetTasks();
          return taskdataservice.GetTasks();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using taskManagementDataService;
using taskManagementModels;

namespace taskManagementAppService
{
    public class taskAppService
    {
            taskDataService taskdataservice = new taskDataService(new taskDBData());
        taskInMemoryData taskinmemorydata = new taskInMemoryData();
            taskJsonData taskjsondata = new taskJsonData();

        public taskAppService()
        {

        }
        public void AddTask(string taskName)
        {
            var task = new taskItem
            {
                TaskId = Guid.NewGuid(),
                TaskName = taskName,
                IsCompleted = false
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

        public void EditTask(Guid id, string newName)
        { 

            var tasks = taskdataservice.GetTasks();

            var task = tasks.FirstOrDefault(t => t.TaskId == id);

            if (task != null)
            {
                task.TaskName = newName;
                taskdataservice.UpdateTask(task);
                taskjsondata.UpdateTask(task);
                taskinmemorydata.UpdateTask(task);
               
            }
        }
        public void DeleteTask(Guid id) {

            var tasks = taskdataservice.GetTasks();
            var task = tasks.FirstOrDefault(t => t.TaskId == id);

            if(task != null)
            {
                tasks.Remove(task);
                taskdataservice.DeleteTask(id);
                taskjsondata.DeleteTask(id);
                taskinmemorydata.UpdateTask(task);
            }
          
        }
        public void TaskCompleted(Guid id)
        {
            var tasks = taskdataservice.GetTasks();
            var task = tasks.FirstOrDefault(t => t.TaskId == id);
        
            if (task != null)
            {
                task.IsCompleted = true;
                taskdataservice.TaskCompleted(id);
                taskjsondata.TaskCompleted(id);
                taskinmemorydata.UpdateTask(task);
            }
        }
    }
}

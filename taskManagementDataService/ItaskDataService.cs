using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagementModels;

namespace taskManagementDataService
{
    public interface ItaskDataService
    {
        void Add(taskItem task);
        void UpdateTask(taskItem taskitem);
        void DeleteTask(Guid id);
     //   void DeleteTask(taskItem taskItem);
        void TaskCompleted(Guid id);
        taskItem? GetById(Guid id);
        taskItem? GetTaskItem(string taskname);
        List<taskItem> GetTasks();
    }
}

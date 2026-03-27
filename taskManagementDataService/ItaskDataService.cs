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
        taskItem? GetById(Guid id);
        taskItem? GetTaskItem(string taskname);
        List<taskItem> GetTasks();
    }
}

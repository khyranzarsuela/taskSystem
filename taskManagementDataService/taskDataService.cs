using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagementModels;

namespace taskManagementDataService
{
    public class taskDataService
    {
        ItaskDataService _dataservice;

        public taskDataService(ItaskDataService itaskDataService)
        {
            _dataservice = itaskDataService;
        }
        public void Add(taskItem task)
        {
            _dataservice.Add(task);
        }
        public taskItem? GetById(Guid id)
        {
            return _dataservice.GetById(id);
        }
        public taskItem? GetTaskItem(string taskname)
        {
            return _dataservice.GetTaskItem(taskname);
        }
         
        public List<taskItem> GetTasks()
        {
            return _dataservice.GetTasks();
        }
    }

}
  
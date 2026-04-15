using System;

namespace taskManagementModels
{
    public class taskItem
    {
        public Guid TaskId { get; set; }

        public string TaskName { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
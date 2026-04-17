using System;
using taskManagementAppService;
using taskManagementDataService;

namespace TaskStatusProgram
{
    class Program
    {
        static taskAppService appService = new taskAppService();
        taskDBData db = new taskDBData();


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To Do List System");
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. View Pending and Completed Tasks");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Delete Task ");
                Console.WriteLine("5. Mark Task as Complete");
                Console.WriteLine("6. Exit Program");

                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                int choices;

                if (!int.TryParse(input, out choices))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choices)
                {
                    case 1:
                        AddTask();
                        break;

                    case 2:
                        ViewTasks();
                        break;

                    case 3:
                        EditTask();
                        break;

                    case 4:
                        DeleteTask();
                        break;

                    case 5:
                        MarkTaskComplete();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task: ");
            string task = Console.ReadLine();

            appService.AddTask(task);
            Console.WriteLine("Task added successfully!");
            Console.WriteLine("-------------------------");
        }

        static void ViewTasks()
        {
            var tasks = appService.GetTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            var pendingTasks = tasks.Where(t => !t.IsCompleted).ToList();
            var completedTasks = tasks.Where(t => t.IsCompleted).ToList();

            Console.WriteLine("\n--- Pending Tasks ---");

            if (pendingTasks.Count == 0)
            {
                Console.WriteLine("No pending tasks.");
                Console.WriteLine("-------------------------");
            }
            else
            {
                for (int i = 0; i < pendingTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {pendingTasks[i].TaskName}");
                 
                }
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine("\n--- Completed Tasks ---");

            if (completedTasks.Count == 0)
            {
                Console.WriteLine("No completed tasks.");
                Console.WriteLine("-------------------------");
            }
            else
            {
                for (int i = 0; i < completedTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {completedTasks[i].TaskName}");
                    
                }
                Console.WriteLine("-------------------------");
            }
        }

        static void EditTask()
        {
            var tasks = appService.GetTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                Console.WriteLine("-------------------------");
                return;
            }

          
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].TaskName}");
            }

            Console.Write("Select task number to edit: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= tasks.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.WriteLine("-------------------------");
                return;
            }

           
            Guid selectedId = tasks[index].TaskId;

            Console.Write("Enter new task name: ");
            string newName = Console.ReadLine();

            appService.EditTask(selectedId, newName);

            Console.WriteLine("Task updated!");
            Console.WriteLine("-------------------------");
        }
    
      
       static void DeleteTask()
        {
            var tasks = appService.GetTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                Console.WriteLine("-------------------------");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].TaskName}");
            }

            Console.Write("Select task number to delete: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= tasks.Count)
            {
                Console.WriteLine("Invalid selection.");

                Console.WriteLine("-------------------------");
                return;
            }

            Guid selectedId = tasks[index].TaskId;

            appService.DeleteTask(selectedId);

            Console.WriteLine("Task deleted!");
            Console.WriteLine("-------------------------");
        }
        static void MarkTaskComplete()
        {
            var tasks = appService.GetTasks();

            var pendingTasks = tasks.Where(t => !t.IsCompleted).ToList();

            if (pendingTasks.Count == 0)
            {
                Console.WriteLine("No pending tasks.");
                Console.WriteLine("-------------------------");
                return;
            }
            else
            {
                Console.WriteLine("---- Pending Task ----");

                for (int i = 0; i < pendingTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {pendingTasks[i].TaskName}");
                }
                Console.WriteLine("-------------------------");
            }


            Console.Write("Select task number to mark as complete: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= pendingTasks.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.WriteLine("-------------------------");
                return;
            }

            Guid selectedId = pendingTasks[index].TaskId;

            appService.TaskCompleted(selectedId);

            Console.WriteLine("Task marked as complete!");
            Console.WriteLine("-------------------------");
        }
    }
}

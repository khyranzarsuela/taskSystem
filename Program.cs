using System;
using taskManagementAppService;
using taskManagementDataService;

namespace TaskStatusProgram
{
    class Program
    {
        static taskAppService appService = new taskAppService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To Do List System");
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Exit Program.");

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
        }

        static void ViewTasks()
        {
            var tasks = appService.GetTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            int number = 1;

            foreach (var task in tasks)
            {
                Console.WriteLine($"{number}. {task.TaskName}");
                number++;
            }
        }
    }
}
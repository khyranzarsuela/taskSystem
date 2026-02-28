using System;
using System.Collections.Generic;

namespace TaskStatusProgram
{
    class Program
    {
        static List<string> tasks = new List<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To Do List System");
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. View aTasks");
                Console.WriteLine("3. Exit Program.");

                Console.Write("Choose a option: ");
                int choices = Convert.ToInt32(Console.ReadLine());
                

                switch (choices){
                    case 1:
                        addTask();
                        break;
                    case 2:
                        view();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Option.");
                        break;
                }

                Console.WriteLine("-------------------");
            }
        }

        static void addTask()
        {
            Console.Write("Enter task: ");
            string task = Console.ReadLine();
            tasks.Add(task);
            Console.WriteLine("Task added successfully!");
        }

        static void view()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("Tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }
    }
}
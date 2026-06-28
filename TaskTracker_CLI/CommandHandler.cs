using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class CommandHandler
    {
        public void GetList(List<ToDoItem> tasks)
        {
            foreach (var task in tasks)
                Console.WriteLine($"{task.Id}: {task.Description}\t {task.Status}");
        }

        public void AddCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Add <description>");
                return;
            }

            string description = string.Join(" ", args.Skip(1));

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Task description cannot be empty!");
                return;
            }

            taskManager.AddTask(tasks, description);
            taskStorage.SaveFile(tasks);

            Console.WriteLine("Task added succesfully!");
        }

        public void UpdateCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: update <Id> <description>");
                return;
            }

            string description = string.Join(" ", args.Skip(2));

            if (!int.TryParse(args[1], out int updateId))
            {
                Console.WriteLine("Invalid task ID.");
                return;
            }

            var updateTask = taskManager.FindTaskById(tasks, updateId);

            if (updateTask != null)
            {
                taskManager.UpdateTask(updateTask, description);
                Console.WriteLine("Task updated succesfully!");

                taskStorage.SaveFile(tasks);
            }
            else
            {
                Console.WriteLine("Task ID does not exist!");
                return;
            }
        }

        public void DeleteCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: delete <Id>");
                return;
            }

            if (!int.TryParse(args[1], out int deleteId))
            {
                Console.WriteLine("Invalid task ID.");
                return;
            }

            var deleteTask = taskManager.FindTaskById(tasks, deleteId);

            if (deleteTask != null)
            {
                taskManager.RemoveTask(tasks, deleteTask);
                Console.WriteLine("Task succesfully removed!");

                taskStorage.SaveFile(tasks);
            }
            else
            {
                Console.WriteLine("Task ID does not exist!");
                return;
            }
        }

        public void MarkCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: mark <Id> <description>");
                return;
            }

            if (!int.TryParse(args[1], out int markId))
            {
                Console.WriteLine("Invalid task ID.");
                return;
            }

            var markTask = taskManager.FindTaskById(tasks, markId);

            if (markTask != null)
            {
                string statusInput = (string)args[2];

                if (Enum.TryParse<TaskStatus>(statusInput, true, out TaskStatus status))
                {
                    taskManager.MarkTask(markTask, status);
                    Console.WriteLine("Task status succesfully changed!");
                    taskStorage.SaveFile(tasks);
                }
                else
                {
                    Console.WriteLine("Please enter \"ToDo\", \"InProgress\" or \"Done\".");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Task ID does not exist!");
                return;
            }
        }
    }
}

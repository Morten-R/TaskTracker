using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class CommandHandler
    {
        private readonly TaskManager _taskManager;
        private readonly TaskStorage _taskStorage;
        private readonly List<ToDoItem> _tasks;
        public CommandHandler(TaskManager taskManager, TaskStorage taskStorage, List<ToDoItem> tasks)
        {
            _taskManager = taskManager;
            _taskStorage = taskStorage;
            _tasks = tasks;
        }
        public void GetList()
        {
            if (!_tasks.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in _tasks)
                Console.WriteLine($"{task.Id}: {task.Description}\t {task.Status}");
        }

        public void AddCommand(string[] args)
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

            _taskManager.AddTask(_tasks, description);
            _taskStorage.SaveFile(_tasks);

            Console.WriteLine("Task added successfully!");
        }

        public void UpdateCommand(string[] args)
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

            var updateTask = _taskManager.FindTaskById(_tasks, updateId);

            if (updateTask != null)
            {
                _taskManager.UpdateTask(updateTask, description);
                Console.WriteLine("Task updated successfully!");

                _taskStorage.SaveFile(_tasks);
            }
            else
            {
                Console.WriteLine("Task ID does not exist!");
                return;
            }
        }

        public void DeleteCommand(string[] args)
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

            var deleteTask = _taskManager.FindTaskById(_tasks, deleteId);

            if (deleteTask != null)
            {
                _taskManager.RemoveTask(_tasks, deleteTask);
                Console.WriteLine("Task successfully removed!");

                _taskStorage.SaveFile(_tasks);
            }
            else
            {
                Console.WriteLine("Task ID does not exist!");
                return;
            }
        }

        public void MarkCommand(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: mark <Id> <ToDo|InProgress|Done>");
                return;
            }

            if (!int.TryParse(args[1], out int markId))
            {
                Console.WriteLine("Invalid task ID.");
                return;
            }

            var markTask = _taskManager.FindTaskById(_tasks, markId);

            if (markTask != null)
            {
                string statusInput = (string)args[2];

                if (Enum.TryParse<TaskStatus>(statusInput, true, out TaskStatus status))
                {
                    _taskManager.MarkTask(markTask, status);
                    Console.WriteLine("Task status successfully changed!");
                    _taskStorage.SaveFile(_tasks);
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

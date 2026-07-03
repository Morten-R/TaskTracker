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
                Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
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
            Save();

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

            var updateTask = GetTaskFromArgs(args);

            if (updateTask != null)
            {
                _taskManager.UpdateTask(updateTask, description);
                Console.WriteLine("Task updated successfully!");

                Save();
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

            var deleteTask = GetTaskFromArgs(args);

            if (deleteTask != null)
            {
                _taskManager.RemoveTask(_tasks, deleteTask);
                Console.WriteLine("Task successfully removed!");

                Save();
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

            var markTask = GetTaskFromArgs(args);

            if (markTask != null)
            {
                string statusInput = (string)args[2];

                if (Enum.TryParse<TaskStatus>(statusInput, true, out TaskStatus status))
                {
                    _taskManager.MarkTask(markTask, status);
                    Console.WriteLine("Task status successfully changed!");
                    Save();
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

        private void Save()
        {
            _taskStorage.SaveFile(_tasks);
        }

        private ToDoItem? GetTaskFromArgs(string[] args)
        {
            if (!int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Invalid task ID.");
            }

            return _taskManager.FindTaskById(_tasks, id);
        }

        public void ListCommand(string[] args)
        {
            if (args.Length == 1)
            {
                GetList();
                return;
            }

            if (Enum.TryParse<TaskStatus>(args[1], true, out var status))
            {
                var tasks = _taskManager.FilterByStatus(_tasks, status);

                if (!tasks.Any())
                {
                    Console.WriteLine($"No {status} tasks found.");
                    return;
                }

                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
                }
            }

            else
            {
                Console.WriteLine("Unknown status.");
            }
        }

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a command.");
                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    ListCommand(args);
                    break;

                case "add":
                    AddCommand(args);
                    break;

                case "update":
                    UpdateCommand(args);
                    break;

                case "delete":
                    DeleteCommand(args);
                    break;

                case "mark":
                    MarkCommand(args);
                    break;

                case "help":
                    Console.WriteLine("If you're stuck and don't know how to write any commands. Just enter 'dotnet run' followed by one of the following commands.\n");
                    Console.WriteLine("To get the full list of tasks: list");
                    Console.WriteLine("To get only the tasks that's done: list done");
                    Console.WriteLine("To get only the tasks that's todo: list todo");
                    Console.WriteLine("To get only the tasks that's inProgress: list inprogress");
                    Console.WriteLine("For adding a task: add <description>");
                    Console.WriteLine("To update a task: update <id> <description>");
                    Console.WriteLine("To delete a task: delete <id>");
                    Console.WriteLine("For marking a task: mark <id> <ToDo|InProgress|Done>");
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}
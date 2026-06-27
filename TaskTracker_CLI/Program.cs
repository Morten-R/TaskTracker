using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             * Requirements
             * The application should run from the command line, accept user actions and inputs as arguments, and store the tasks in a JSON file. The user should be able to:
             * Add, Update, and Delete tasks
             * Mark a task as in progress or done
             * List all tasks
             * List all tasks that are done
             * List all tasks that are not done
             * List all tasks that are in progress
             * 
             * 
             * Here are some constraints to guide the implementation:
             * You can use any programming language to build this project.
             * Use positional arguments in command line to accept user inputs.
             * Use a JSON file to store the tasks in the current directory.
             * The JSON file should be created if it does not exist.
             * Use the native file system module of your programming language to interact with the JSON file.
             * Do not use any external libraries or frameworks to build this project.
             * Ensure to handle errors and edge cases gracefully.
             * 
             * Task Properties
             * Each task should have the following properties:
             * 
             * - id: A unique identifier for the task
             * - description: A short description of the task
             * - status: The status of the task (todo, in-progress, done)
             * - createdAt: The date and time when the task was created
             * - updatedAt: The date and time when the task was last updated
             * 
             * Make sure to add these properties to the JSON file when adding a new task and update them when updating a task. 
             */


            /*TaskTracker taskTracker = new();
            taskTracker.Run();*/

            TaskStorage taskStorage = new();
            TaskManager taskManager = new();

            List<ToDoItem> tasks = taskStorage.LoadFile();



            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a command.");
                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    GetList(tasks);
                    break;

                case "add":
                    AddCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "update":
                    UpdateCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "delete":
                    DeleteCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "mark":
                    MarkCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "help":
                    Console.WriteLine("If you're stuck and don't know how to write any commands. Just enter 'dotnet run' followed by one of the commands beneath.\n");
                    Console.WriteLine("add <description>");
                    Console.WriteLine("update <id> <description>");
                    Console.WriteLine("delete <id>");
                    Console.WriteLine("mark <id> <ToDo|InProgress|Done>");
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private static void GetList(List<ToDoItem> tasks)
        {
            foreach (var task in tasks)
                Console.WriteLine($"{task.Id}: {task.Description}\t {task.Status}");
        }

        private static void AddCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
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

        private static void UpdateCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <Id> <description>");
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

        private static void DeleteCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <Id> <description>");
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

        private static void MarkCommand(string[] args, TaskManager taskManager, List<ToDoItem> tasks, TaskStorage taskStorage)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <Id> <description>");
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
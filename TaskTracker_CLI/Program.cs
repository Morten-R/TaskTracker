using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskStorage taskStorage = new();
            TaskManager taskManager = new();
            CommandHandler handler = new();

            List<ToDoItem> tasks = taskStorage.LoadFile();

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a command.");
                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    handler.GetList(tasks);
                    break;

                case "add":
                    handler.AddCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "update":
                    handler.UpdateCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "delete":
                    handler.DeleteCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "mark":
                    handler.MarkCommand(args, taskManager, tasks, taskStorage);
                    break;

                case "help":
                    Console.WriteLine("If you're stuck and don't know how to write any commands. Just enter 'dotnet run' followed by one of the following commands.\n");
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
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskStorage taskStorage = new();
            TaskManager taskManager = new();
            
            List<ToDoItem> tasks = taskStorage.LoadFile();
            CommandHandler handler = new(taskManager, taskStorage, tasks);

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a command.");
                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    handler.GetList();
                    break;

                case "add":
                    handler.AddCommand(args);
                    break;

                case "update":
                    handler.UpdateCommand(args);
                    break;

                case "delete":
                    handler.DeleteCommand(args);
                    break;

                case "mark":
                    handler.MarkCommand(args);
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
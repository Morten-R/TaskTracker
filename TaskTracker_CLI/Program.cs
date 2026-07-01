using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskStorage taskStorage = new();
            TaskManager taskManager = new();
            
            var tasks = taskStorage.LoadFile();
            CommandHandler handler = new(taskManager, taskStorage, tasks);

            handler.Execute(args);
        }
    }
}
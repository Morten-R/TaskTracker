using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class TaskTracker
    {
        public void Run()
        {
            Console.WriteLine("Welcome to the TaskTracker!\n" +
                              "Please enter what you want to do:\n" +
                              "1. List all tasks\n" +
                              "2. Add a task\n" +
                              "3. Update a task\n" +
                              "4. Remove a task\n" +
                              "5. Mark task (\"Done\", \"InProgress\" or \"ToDo\")\n" +
                              "6. List tasks that's done\n" +
                              "7. List tasks that's in-progress\n" +
                              "8. List tasks ToDo\n" + 
                              "9. Exit\n");
            GetUserOption();            
        }

        public void GetUserOption()
        {
            UserOptions options = new();
            options.GetUserInput();
        }
    }
}


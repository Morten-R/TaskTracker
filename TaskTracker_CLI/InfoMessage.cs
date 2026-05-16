using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class InfoMessage
    {
        public string Message()
        {
            return "Welcome to TaskTracker!\n" +
                   "Here you can add a task to do, update it or delete it.\n" +
                   "You can mark a task as in-progress or done!\n" +
                   "What do you want to do?:\n" +
                   "1. Add a task\n" +
                   "2. Update a task\n" +
                   "3. Delete a task\n" +
                   "4. Mark a task in-progress or as done\n" +
                   "5. Show all tasks\n" +
                   "6. Show all tasks that's done\n" +
                   "7. Show all tasks that's in-progress";
        }
    }
}

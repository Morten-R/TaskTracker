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
            Start();
            GetUserOption();            
        }

        public void Start()
        {
            InfoMessage message = new();
            Console.WriteLine(message.Message());
        }

        public void GetUserOption()
        {
            UserOptions options = new();
            options.GetUserInput();
        }


        public int GetOptionFromOptions(Options option)
        {
            return option switch
            {
                Options.Add => 1,
                Options.Update => 2,
                Options.Delete => 3,
                Options.Mark => 4,
                Options.ShowAllTasks => 5,
                Options.ShowDoneTasks => 6,
                Options.ShowInProgressTasks => 7,
                _ => 0
            };
        }
    }
}

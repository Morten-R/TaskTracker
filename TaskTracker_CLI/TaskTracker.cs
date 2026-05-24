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

            bool keepRunning = true;

            while (keepRunning)
            {
                Menu menu = new();
                int option = menu.GetOption();

                if (option == 1)
                    AddItems();

                else if (option == 5)
                    ShowingAllTasks();

                else
                    keepRunning = false;
            }
            
        }

        public void Start()
        {
            InfoMessage message = new();
            Console.WriteLine(message.Message());
        }

        public void AddItems()
        {
            Add add = new();
            add.AddItem();
        }

        public void ShowingAllTasks()
        {
            ShowAllTasks showTasks = new();
            showTasks.ShowTasks();
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

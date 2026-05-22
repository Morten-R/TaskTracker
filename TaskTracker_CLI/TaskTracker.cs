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

            Menu menu = new();
            int option = menu.GetOption();

            Add add = new();
            add.AddItem();

            ShowAllTasks showTasks = new();
            showTasks.ShowTasks();
        }

        public void Start()
        {
            InfoMessage message = new();
            Console.WriteLine(message.Message());
        }
    }
}

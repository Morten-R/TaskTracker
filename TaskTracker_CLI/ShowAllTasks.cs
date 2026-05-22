using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class ShowAllTasks
    {
        public void ShowTasks()
        {
            Lists list = new();
            
            if (list.toDoList.Count == 0)
            {
                Console.WriteLine("The list is empty!\n");
            }

            else
            {
                for (int i = 0; i < list.toDoList.Count; i++)
                {
                    Console.WriteLine(list.toDoList[i]);
                }
            }
        }
    }
}

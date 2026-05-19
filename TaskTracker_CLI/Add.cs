using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Add
    {
        public string AddItem()
        {
            Console.WriteLine("Add a task: ");
            string? description = Console.ReadLine();



            return description;
        }
    }
}

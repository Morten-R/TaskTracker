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
            InfoMessage message = new();
            
            Console.WriteLine(message.Message());
        }
    }
}

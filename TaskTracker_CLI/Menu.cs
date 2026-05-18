using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Menu
    {
        public int GetOption()
        {
            UserInput input = new();

            int option = input.Input("\nPlease choose the option you want to do:");

            switch (option)
            {
                case 1:
                    Console.WriteLine("You want to add a task!");
                    break;

                case 2:
                    Console.WriteLine("You want to update a task!");
                    break;

                default:
                    break;
            }
            return option;
        }
    }
}

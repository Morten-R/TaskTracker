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

            int option = input.Input("\nPlease choose what you want to do:");

            switch (option)
            {
                case 1:
                    Console.WriteLine("You chose to add a task!");
                    break;

                case 2:
                    Console.WriteLine("You chose to update an existing task!");
                    break;

                case 3:
                    Console.WriteLine("You chose to delete a task!");
                    break;

                case 4:
                    Console.WriteLine("You chose to mark a task!");
                    break;

                case 5:
                    Console.WriteLine("You want to show all tasks!");
                    break;

                case 6:
                    Console.WriteLine("You want to show all tasks that's done!");
                    break;

                case 7:
                    Console.WriteLine("You want to show all tasks that's in-progress!");
                    break;

                default:
                    break;
            }
            return option;
        }
    }
}

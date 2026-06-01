using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class UserInput
    {
        public int Input(string message)
        {
            Console.WriteLine(message);
            
            while (true)
            {
                if (Validator.InputValidator(Console.ReadLine(), 1, 8, out int result))
                    return result;

                Console.WriteLine("\nPlease enter a number 1 - 8");
            }
        }

        public int InputTaskID(string message)
        {
            UserOptions options = new();

            Console.WriteLine(message);

            while (true)
            {
                if (Validator.InputTaskIdValidator(Console.ReadLine(), 1, options.toDoList.Count(), out int result))
                    return result;

                Console.WriteLine("\nTask does not exist! Please enter a valid task ID.");
            }
        }
    }
}

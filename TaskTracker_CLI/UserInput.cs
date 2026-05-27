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
            Console.WriteLine(message);

            while (true)
            {
                if (Validator.InputTaskIdValidator(Console.ReadLine(), out int result))
                    return result;

                Console.WriteLine("\nPlease enter task ID you want to update.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Validator
    {
        public static bool InputValidator(string? input, int min, int max, out int result)
        {
            result = -1;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            return int.TryParse(input, out result) && result >= min && result <= max;
        }

        public static bool InputTaskIdValidator(string? input, out int result)
        {
            result = -1;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            return int.TryParse(input, out result) && result > 0;
        }
    }
}

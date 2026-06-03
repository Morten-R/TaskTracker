using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public enum Options
    {
        ListAllTasks = 1,
        Add = 2,
        Update = 3,
        Remove = 4,
        Mark = 5,
        ShowDoneTasks = 6,
        ShowInProgressTasks = 7,
        ShowToDoTasks = 8,
        Exit = 9
    }
}

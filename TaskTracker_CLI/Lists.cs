using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Lists
    {
        public List<ToDoItem> toDoList = [];

        public void AddItemToList(ToDoItem toDoItem)
        {
            toDoList.Add(toDoItem);
        }
    }
}

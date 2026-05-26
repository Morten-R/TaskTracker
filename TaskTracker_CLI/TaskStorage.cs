using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TaskTracker_CLI
{
    public class TaskStorage
    {
        public void SaveFile(List<ToDoItem> toDoList)
        {
            string jsonString = JsonSerializer.Serialize(toDoList);
            File.WriteAllText("tasks.json", jsonString);
        }
    }
}

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
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(toDoList, options);
            File.WriteAllText("tasks.json", jsonString);
        }

        public List<ToDoItem> LoadFile()
        {
            string jsonString = File.ReadAllText("tasks.json");
            var desString = JsonSerializer.Deserialize<List<ToDoItem>>(jsonString);

            if (desString != null)
            {
                return desString;
            }

            else
            {
                return new List<ToDoItem>();
            }
        }
    }
}

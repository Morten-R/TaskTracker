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
        public void SaveFile(List<ToDoItem> tasks)
        {
            var prettyPrint = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(tasks, prettyPrint);
            File.WriteAllText("tasks.json", jsonString);
        }

        public List<ToDoItem> LoadFile()
        {
            if (!File.Exists("tasks.json"))
            {
                return new List<ToDoItem>();
            }

            string jsonString = File.ReadAllText("tasks.json");

            if (string.IsNullOrEmpty(jsonString))
                return new List<ToDoItem>();

            var desString = JsonSerializer.Deserialize<List<ToDoItem>>(jsonString);

            if (desString != null)
            {
                return desString;   
            }

            return new List<ToDoItem>();
        }
    }
}

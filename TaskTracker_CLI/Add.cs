using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Add
    {
        public Lists list = new();
        public string AddItem()
        {
            Console.WriteLine("Add a task: ");
            string? description = Console.ReadLine();

            list.AddItemToList(new ToDoItem
            {
                Id = 0,
                Description = description,
                Status = "",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            Console.WriteLine("Task added succesfully!\n");

            return description;
        }
    }
}

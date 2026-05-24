using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class Add
    {
        Lists list = new();
        public string AddItem()
        {
            Console.WriteLine("Write the name of the task you want to add: ");
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

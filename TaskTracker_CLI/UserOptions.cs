using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class UserOptions
    {
        public bool isValid { get; set; } = true;
        public int Choice { get; set; }

        public List<ToDoItem> toDoList = [];

        public void GetUserInput()
        {
            UserInput userInput = new();

            while (isValid)
            {
                Choice = userInput.Input("\nEnter your choice: ");

                switch (Choice)
                {
                    case 1:
                        // add tasks
                        Console.WriteLine("Add a task: ");
                        string? description = Console.ReadLine();

                        toDoList.Add(new ToDoItem { Description = description });
                        Console.WriteLine("Task added succesfully!");
                        break;

                    case 2:
                        Console.WriteLine("Coming soon!");
                        break;

                    case 3:
                        Console.WriteLine("Coming soon!");
                        break;

                    case 4:
                        Console.WriteLine("Coming soon!");
                        break;

                    case 5:
                        // show all tasks
                        if (toDoList.Count == 0)
                            Console.WriteLine("The list is empty!");

                        else
                        {
                            Console.WriteLine("Showing all tasks:\n");
                            for (int i = 0; i < toDoList.Count; i++)
                                Console.WriteLine($"{toDoList[i].Description}");
                        }
                            break;

                    default:
                        break;
                }
            }
        }
    }
}

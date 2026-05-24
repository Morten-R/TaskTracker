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

        private readonly List<ToDoItem> toDoList = [];

        public void GetUserInput()
        {
            UserInput userInput = new();

            while (isValid)
            {
                Choice = userInput.Input("\nEnter your choice: ");

                switch (Choice)
                {
                    case 1:
                        // show all tasks
                        if (toDoList.Count == 0)
                            Console.WriteLine("The list is empty!");

                        else
                        {
                            Console.WriteLine("Showing all tasks:\n");
                            
                            foreach (var task in toDoList)
                            {
                                Console.WriteLine($"{task.Id}: {task.Description}, Status: {task.Status}, Created: {task.CreatedAt}, Last updated: {task.UpdatedAt}");
                            }
                        }
                        break;

                    case 2:
                        // add tasks
                        Console.WriteLine("Add a task: ");
                        string? description = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(description))
                        {
                            Console.WriteLine("Task description cannot be empty.");
                            break;
                        }

                        toDoList.Add(new ToDoItem
                        {
                            Id = toDoList.Count == 0 ? 1 : toDoList.Max(t => t.Id) + 1,
                            Description = description,
                            Status = TaskStatus.ToDo,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        });
                        Console.WriteLine("Task added succesfully!");
                        break;

                    case 3:
                        // update a task
                        Console.WriteLine("Coming soon!");
                        break;

                    case 4:
                        // remove a task
                        Console.WriteLine("Enter task ID to remove: ");
                        int taskID = Convert.ToInt32(Console.ReadLine());

                        bool found = false;

                        for (int i = 0; i < toDoList.Count; i++)
                        {
                            if (taskID == toDoList[i].Id)
                            {
                                toDoList.Remove(toDoList[i]);
                                Console.WriteLine("Task succesfully removed!");
                                found = true;
                                break;
                            }                           
                        }

                        if (!found)
                        {
                            Console.WriteLine("Task does not exist!");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Coming soon!");
                            break;

                    case 6:
                        Console.WriteLine("Coming soon!");
                        break;

                    case 7:
                        Console.WriteLine("Coming soon!");
                        break;

                    case 8:
                        isValid = false;
                        break;
                        

                    default:
                        break;
                }
            }
        }
    }
}

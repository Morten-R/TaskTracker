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


        public List<ToDoItem> toDoList = GetLoadFile();

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
                                Console.WriteLine($"{task.Id}: {task.Description}\t\tStatus: {task.Status}\tCreated: {task.CreatedAt}\tLast updated: {task.UpdatedAt}");
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
                        Console.WriteLine($"Task added succesfully!");

                        TaskStorage taskStorage = new();
                        taskStorage.SaveFile(toDoList);

                        break;

                    case 3:
                        // update a task
                        int id = userInput.InputTaskID("Enter task ID for the task you want to update: ");

                        bool updTaskIdFound = false;
                        for (int i = 0; i < toDoList.Count; i++)
                        {
                            if (id == toDoList[i].Id)
                            {
                                Console.WriteLine("Please enter new task: ");
                                string? newTask = Console.ReadLine();
                                toDoList[i].Description = newTask;
                                toDoList[i].UpdatedAt = DateTime.Now;

                                Console.WriteLine("Task succesfully updated!");
                                updTaskIdFound = true;

                                TaskStorage taskStorage1 = new();
                                taskStorage1.SaveFile(toDoList);

                                break;
                            }
                            
                        }

                        if (!updTaskIdFound)
                        {
                            Console.WriteLine("Task does not exist!");
                        }
                            break;

                    case 4:
                        // remove a task
                        int taskID = userInput.InputTaskID("Enter the ID for the task you want to remove: ");

                        bool found = false;

                        for (int i = 0; i < toDoList.Count; i++)
                        {
                            if (taskID == toDoList[i].Id)
                            {
                                toDoList.Remove(toDoList[i]);
                                Console.WriteLine("Task succesfully removed!");
                                found = true;

                                TaskStorage taskStorage1 = new();
                                taskStorage1.SaveFile(toDoList);

                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("Task does not exist!");
                        }
                        break;

                    case 5:
                        // mark task as done or in-progress
                        int markID = userInput.InputTaskID("Enter the task ID for the task you want to change status: ");
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
        public static List<ToDoItem> GetLoadFile()
        {
            TaskStorage file = new();
            var loadedTasks = file.LoadFile();

            return loadedTasks;
        }
    }
}

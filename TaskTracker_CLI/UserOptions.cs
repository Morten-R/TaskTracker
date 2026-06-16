using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class UserOptions
    {
        private bool isValid { get; set; } = true;
        private int Choice { get; set; }


        public List<ToDoItem> toDoList = GetLoadFile();

        public void GetUserInput()
        {
            UserInput userInput = new();
            TaskStorage taskStorage = new();
            TaskManager taskManager = new();

            while (isValid)
            {
                Choice = userInput.Input("\nEnter your choice: ");

                switch (Choice)
                {
                    case 1:
                        // show all tasks
                        taskManager.ShowAllTasks(toDoList);
                        break;

                    case 2:
                        // add tasks
                        //taskManager.AddTask(toDoList, taskStorage);

                        bool addingTask = true;

                        while (addingTask)
                        {
                            Console.WriteLine("Add a task: ");
                            string? description = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(description))
                            {
                                Console.WriteLine("Task description cannot be empty.");
                                continue;
                            }

                            taskManager.AddTask(toDoList, description);
                            Console.WriteLine($"Task added succesfully!");

                            taskManager.SaveTasks(taskStorage, toDoList);

                            addingTask = false;
                        }

                        break;

                    case 3:
                        // update a task                        
                        bool isUpdateValid = true;

                        while (isUpdateValid)
                        {
                            int id = userInput.InputTaskID("\nEnter task ID for the task you want to update: ");

                            var task = taskManager.FindTaskById(toDoList, id);

                            if (task != null)
                            {
                                Console.WriteLine("Please enter new task: ");
                                string? newDescription = Console.ReadLine();

                                taskManager.UpdateTask(task, newDescription);
                                
                                Console.WriteLine("Task succesfully updated!");
                                taskManager.SaveTasks(taskStorage, toDoList);
                                isUpdateValid = false;
                            }

                            else
                                Console.WriteLine("Task ID does not exist!");
                        }
                        break;

                    case 4:
                        // remove a task
                        bool isRemoveIdValid = true;

                        while (isRemoveIdValid)
                        {
                            int taskID = userInput.InputTaskID("\nEnter the ID for the task you want to remove: ");

                            var remTask = taskManager.FindTaskById(toDoList, taskID);

                            if (remTask != null)
                            {
                                taskManager.RemoveTask(toDoList, remTask);
                                Console.WriteLine("Task succesfully removed!");

                                taskManager.SaveTasks(taskStorage, toDoList);
                                isRemoveIdValid = false;
                            }

                            else
                                Console.WriteLine("Task ID does not exist!");
                        }
                        break;

                    case 5:
                        // mark task as done or in-progress
                        bool isMarkIdValid = true;

                        while (isMarkIdValid)
                        {
                            int markID = userInput.InputTaskID("\nEnter the task ID for the task's status you want to change: ");

                            var markTask = taskManager.FindTaskById(toDoList, markID);

                            if (markTask != null)
                            {
                                Console.WriteLine("\nPlease enter what status you want to give the task: ");

                                bool isStatusValid = true;
                                while (isStatusValid)
                                {
                                    string? statusInput = Console.ReadLine();

                                    if (statusInput == "ToDo")
                                        taskManager.MarkTask(markTask, TaskStatus.ToDo);

                                    else if (statusInput == "InProgress")
                                        taskManager.MarkTask(markTask, TaskStatus.InProgress);

                                    else if (statusInput == "Done")
                                        taskManager.MarkTask(markTask, TaskStatus.Done);

                                    else
                                    {
                                        Console.WriteLine("\nPlease enter \"ToDo\", \"InProgress\" or \"Done\".");
                                        continue;
                                    }

                                    Console.WriteLine("\nStatus succesfully changed!");
                                    taskManager.SaveTasks(taskStorage, toDoList);
                                    isStatusValid = false;
                                    isMarkIdValid = false;
                                }
                            }
                            else
                                Console.WriteLine("Task ID does not exist!");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Showing tasks that's done:");
                        taskManager.FilterByStatus(toDoList, TaskStatus.Done);
                        break;

                    case 7:
                        Console.WriteLine("Showing tasks that's in-progress:");
                        taskManager.FilterByStatus(toDoList, TaskStatus.InProgress);
                        break;

                    case 8:
                        Console.WriteLine("Showing tasks ToDo:");
                        taskManager.FilterByStatus(toDoList, TaskStatus.ToDo);
                        break;

                    case 9:
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

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
                        var tasks = taskManager.ShowAllTasks(toDoList);

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

                            taskStorage.SaveFile(toDoList);

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
                                taskStorage.SaveFile(toDoList);
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

                                taskStorage.SaveFile(toDoList);
                                isRemoveIdValid = false;
                            }

                            else
                                Console.WriteLine("Task ID does not exist!");
                        }
                        break;

                    case 5:
                        // mark task as todo, done or in-progress
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

                                    if (Enum.TryParse<TaskStatus>(statusInput, true, out var status))
                                    {
                                        taskManager.MarkTask(markTask, status);
                                    }

                                    else
                                    {
                                        Console.WriteLine("\nPlease enter \"ToDo\", \"InProgress\" or \"Done\".");
                                        continue;
                                    }

                                    Console.WriteLine("\nStatus succesfully changed!");
                                    taskStorage.SaveFile(toDoList);
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
                        var doneTasks = taskManager.FilterByStatus(toDoList, TaskStatus.Done);

                        foreach (var task in doneTasks)
                        {
                            Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
                        }
                        break;

                    case 7:
                        Console.WriteLine("Showing tasks that's in-progress:");
                        var inProgTasks = taskManager.FilterByStatus(toDoList, TaskStatus.InProgress);

                        foreach (var task in inProgTasks)
                        {
                            Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
                        }
                        break;

                    case 8:
                        Console.WriteLine("Showing tasks ToDo:");
                        var toDoTasks = taskManager.FilterByStatus(toDoList, TaskStatus.ToDo);

                        foreach (var task in toDoTasks)
                        {
                            Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
                        }
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

        public void AddTask(TaskManager taskManager)
        {
            var tasks = taskManager.ShowAllTasks(toDoList);

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
        }
    }
}
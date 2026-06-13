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

                        taskStorage.SaveFile(toDoList);

                        break;

                    case 3:
                        // update a task
                        bool isUpdateValid = true;
                        while (isUpdateValid)
                        {
                            int id = userInput.InputTaskID("\nEnter task ID for the task you want to update: ");

                            var task = FindTaskById(id);

                            if (task != null)
                            {
                                Console.WriteLine("Please enter new task: ");
                                string? newTask = Console.ReadLine();
                                task.Description = newTask;
                                task.UpdatedAt = DateTime.Now;

                                Console.WriteLine("Task succesfully updated!");

                                taskStorage.SaveFile(toDoList);

                                isUpdateValid = false;

                                break;
                            }

                            if (task == null)
                                Console.WriteLine("Task ID does not exist!");
                        }

                        break;

                    case 4:
                        // remove a task
                        bool isRemoveValid = true;
                        while (isRemoveValid)
                        {
                            int taskID = userInput.InputTaskID("\nEnter the ID for the task you want to remove: ");

                            var remTask = FindTaskById(taskID);

                                if (remTask != null)
                                {
                                    toDoList.Remove(remTask);
                                    Console.WriteLine("Task succesfully removed!");

                                    taskStorage.SaveFile(toDoList);

                                    isRemoveValid = false;

                                    break;
                                }

                            if (remTask == null)
                                Console.WriteLine("Task ID does not exist!");
                        }
                        break;

                    case 5:
                        // mark task as done or in-progress
                        bool isMarkValid = true;
                        while (isMarkValid)
                        {
                            int markID = userInput.InputTaskID("\nEnter the task ID for the task's status you want to change: ");

                            var markTask = FindTaskById(markID);

                            if (markTask != null)
                            {
                                 Console.WriteLine("\nPlease enter what status you want to give the task: ");

                                 bool isStatusValid = true;
                                 while (isStatusValid)
                                 {
                                    string? statusInput = Console.ReadLine();
                                    if (statusInput == "ToDo")
                                        markTask.Status = TaskStatus.ToDo;

                                    else if (statusInput == "InProgress")
                                        markTask.Status = TaskStatus.InProgress;

                                    else if (statusInput == "Done")
                                        markTask.Status = TaskStatus.Done;

                                    else
                                    {
                                        Console.WriteLine("\nPlease enter \"ToDo\", \"InProgress\" or \"Done\".");
                                        continue;
                                    }

                                    Console.WriteLine("\nStatus succesfully changed!");
                                        

                                    markTask.UpdatedAt = DateTime.Now;
                                    taskStorage.SaveFile(toDoList);
                                    isStatusValid = false;

                                    isMarkValid = false;
                                 }
                                 break;
                            }

                            if (markTask == null)
                                Console.WriteLine("Task ID does not exist!");

                        }
                        break;

                    case 6:
                        Console.WriteLine("Showing tasks that's done:");
                        FilterByStatus(TaskStatus.Done);
                        break;

                    case 7:
                        Console.WriteLine("Showing tasks that's in-progress:");
                        FilterByStatus(TaskStatus.InProgress);
                        break;

                    case 8:
                        Console.WriteLine("Showing tasks ToDo:");
                        FilterByStatus(TaskStatus.ToDo);
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

        public void FilterByStatus(TaskStatus status)
        {
            var filteredTasks = toDoList
                .Where(task => task.Status == status);

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
            }
        }

        public ToDoItem? FindTaskById(int id)
        {
            for (int i = 0; i < toDoList.Count; i++)
            {
                if (toDoList[i].Id == id)
                    return toDoList[i];
            }

            return null;
        }
    }
}

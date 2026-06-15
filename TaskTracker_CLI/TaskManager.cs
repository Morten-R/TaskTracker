using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class TaskManager
    {
        public ToDoItem? FindTaskById(List<ToDoItem> toDoList, int id)
        {
            for (int i = 0; i < toDoList.Count; i++)
            {
                if (toDoList[i].Id == id)
                    return toDoList[i];
            }

            return null;
        }

        public void FilterByStatus(List<ToDoItem> toDoList, TaskStatus status)
        {
            var filteredTasks = toDoList
                .Where(task => task.Status == status);

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"ID: {task.Id}\t Name: {task.Description}\t\t Status: {task.Status}\t\t Created: {task.CreatedAt}\t Last updated: {task.UpdatedAt}");
            }
        }

        public void AddTask(List<ToDoItem> toDoList, TaskStorage taskStorage)
        {
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

                addingTask = false;
            }
        }

        public void UpdateTask(ToDoItem task, string? newDescription)
        {
            task.Description = newDescription;
            task.UpdatedAt = DateTime.Now;
        }

        /*public void UpdateTask(List<ToDoItem> toDoList, UserInput userInput, TaskStorage taskStorage)
        {
            bool isUpdateValid = true;
            while (isUpdateValid)
            {
                int id = userInput.InputTaskID("\nEnter task ID for the task you want to update: ");

                var task = FindTaskById(toDoList, id);

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
        }*/

        public void RemoveTask(List<ToDoItem> toDoList, UserInput userInput, TaskStorage taskStorage)
        {
            bool isRemoveValid = true;
            while (isRemoveValid)
            {
                int taskID = userInput.InputTaskID("\nEnter the ID for the task you want to remove: ");

                var remTask = FindTaskById(toDoList, taskID);

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
        }

        public void MarkTask(List<ToDoItem> toDoList, UserInput userInput, TaskStorage taskStorage)
        {
            bool isMarkValid = true;
            while (isMarkValid)
            {
                int markID = userInput.InputTaskID("\nEnter the task ID for the task's status you want to change: ");

                var markTask = FindTaskById(toDoList, markID);

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
        }

        public void ShowAllTasks(List<ToDoItem> toDoList)
        {
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

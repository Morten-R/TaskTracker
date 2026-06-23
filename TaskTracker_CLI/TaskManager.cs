using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI
{
    public class TaskManager
    {
        public ToDoItem? FindTaskById(List<ToDoItem> tasks, int id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                    return tasks[i];
            }

            return null;
        }

        public IEnumerable<ToDoItem> FilterByStatus(List<ToDoItem> tasks, TaskStatus status)
        {
            return tasks.Where(task => task.Status == status);
        }

        public void AddTask(List<ToDoItem> tasks, string description)
        {
            tasks.Add(new ToDoItem
            {
            Id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1,
            Description = description,
            Status = TaskStatus.ToDo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            });
        }

        public void UpdateTask(ToDoItem task, string? newDescription)
        {
            task.Description = newDescription;
            task.UpdatedAt = DateTime.Now;
        }

        

        public void RemoveTask(List<ToDoItem> tasks, ToDoItem remTask)
        {
            tasks.Remove(remTask);
        }

        public void MarkTask(ToDoItem markTask, TaskStatus userChoice)
        {
            markTask.Status = userChoice;
            markTask.UpdatedAt = DateTime.Now; 
        }

        public IEnumerable<ToDoItem> ShowAllTasks(List<ToDoItem> tasks)
        {
            return tasks;
        }
    }
}
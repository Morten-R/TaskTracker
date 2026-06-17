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

        public IEnumerable<ToDoItem> FilterByStatus(List<ToDoItem> toDoList, TaskStatus status)
        {
            return toDoList.Where(task => task.Status == status);
        }

        public void AddTask(List<ToDoItem> toDoList, string description)
        {
            toDoList.Add(new ToDoItem
            {
            Id = toDoList.Count == 0 ? 1 : toDoList.Max(t => t.Id) + 1,
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

        

        public void RemoveTask(List<ToDoItem> toDoList, ToDoItem remTask)
        {
            toDoList.Remove(remTask);
        }

        public void MarkTask(ToDoItem markTask, TaskStatus userChoice)
        {
            markTask.Status = userChoice;
            markTask.UpdatedAt = DateTime.Now; 
        }

        public IEnumerable<ToDoItem> ShowAllTasks(List<ToDoItem> toDoList)
        {
            return toDoList;
        }
    }
}
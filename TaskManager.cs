using System.Collections.Generic;

namespace CyberBot
{
    public class TaskManager
    {
        // Storage helper
        private readonly TaskStorageHelper storage;

        // Constructor
        public TaskManager()
        {
            storage = new TaskStorageHelper();
        }

        // ============================
        // Add Task
        // ============================
        public string AddTask(string title, string description, string reminder)
        {
            storage.AddTask(title, description, reminder);

            ActivityLogger.Log(
                $"Task added: '{title}'" +
                (string.IsNullOrWhiteSpace(reminder)
                    ? ""
                    : $" (Reminder: {reminder})"));

            return $"✅ Task '{title}' has been added successfully.";
        }

        // ============================
        // Get All Tasks
        // ============================
        public List<CyberTask> GetAllTasks()
        {
            return storage.LoadTasks();
        }

        // ============================
        // Mark Complete
        // ============================
        public string MarkAsComplete(int id)
        {
            List<CyberTask> tasks = storage.LoadTasks();

            CyberTask task = tasks.Find(t => t.Id == id);

            if (task == null)
            {
                return "❌ Task not found.";
            }

            storage.MarkAsComplete(id);

            ActivityLogger.Log(
                $"Task completed: '{task.Title}'");

            return $"✅ Task '{task.Title}' marked as complete.";
        }

        // ============================
        // Delete Task
        // ============================
        public string DeleteTask(int id)
        {
            List<CyberTask> tasks = storage.LoadTasks();

            CyberTask task = tasks.Find(t => t.Id == id);

            if (task == null)
            {
                return "❌ Task not found.";
            }

            storage.DeleteTask(id);

            ActivityLogger.Log(
                $"Task deleted: '{task.Title}'");

            return $"🗑️ Task '{task.Title}' deleted successfully.";
        }
    }
}
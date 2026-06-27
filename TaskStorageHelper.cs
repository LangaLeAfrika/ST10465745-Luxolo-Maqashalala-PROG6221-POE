using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CyberBot
{
    public class TaskStorageHelper
    {
        // JSON file name
        private const string FilePath = "tasks.json";

        // ============================
        // Load Tasks
        // ============================
        public List<CyberTask> LoadTasks()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return new List<CyberTask>();
                }

                string json = File.ReadAllText(FilePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<CyberTask>();
                }

                List<CyberTask>? tasks =
                    JsonConvert.DeserializeObject<List<CyberTask>>(json);

                return tasks ?? new List<CyberTask>();
            }
            catch
            {
                return new List<CyberTask>();
            }
        }

        // ============================
        // Save Tasks
        // ============================
        public void SaveTasks(List<CyberTask> tasks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(FilePath, json);
            }
            catch
            {
                // Prevent application crash
            }
        }

        // ============================
        // Add Task
        // ============================
        public void AddTask(
            string title,
            string description,
            string reminder)
        {
            List<CyberTask> tasks = LoadTasks();

            int nextId = 1;

            if (tasks.Count > 0)
            {
                nextId = tasks.Max(t => t.Id) + 1;
            }

            CyberTask newTask = new CyberTask
            {
                Id = nextId,
                Title = title,
                Description = description,
                Reminder = reminder,
                IsComplete = false,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };

            tasks.Add(newTask);

            SaveTasks(tasks);
        }

        // ============================
        // Mark Complete
        // ============================
        public void MarkAsComplete(int id)
        {
            List<CyberTask> tasks = LoadTasks();

            CyberTask? task =
                tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsComplete = true;

                SaveTasks(tasks);
            }
        }

        // ============================
        // Delete Task
        // ============================
        public void DeleteTask(int id)
        {
            List<CyberTask> tasks = LoadTasks();

            CyberTask? task =
                tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                tasks.Remove(task);

                SaveTasks(tasks);
            }
        }
    }
}
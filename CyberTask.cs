using System;

namespace CyberBot
{
    public class CyberTask
    {
        // Unique task ID
        public int Id { get; set; }

        // Task title
        public string Title { get; set; }

        // Task description
        public string Description { get; set; }

        // Optional reminder
        public string Reminder { get; set; }

        // Completion status
        public bool IsComplete { get; set; }

        // Date and time created
        public string CreatedAt { get; set; }

        // Constructor
        public CyberTask()
        {
            Title = "";
            Description = "";
            Reminder = "";
            IsComplete = false;
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        // Display nicely in a ListBox
        public override string ToString()
        {
            string status = IsComplete ? "✅ Completed" : "⏳ Pending";

            string reminderText = string.IsNullOrWhiteSpace(Reminder)
                ? "No Reminder"
                : Reminder;

            return $"[{Id}] {Title}\n" +
                   $"Description: {Description}\n" +
                   $"Reminder: {reminderText}\n" +
                   $"Status: {status}\n" +
                   $"Created: {CreatedAt}";
        }
    }
}
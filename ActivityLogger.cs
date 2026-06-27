using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBot
{
    public static class ActivityLogger
    {
        // Stores all activities
        private static readonly List<string> logEntries = new List<string>();

        // ============================
        // Add a new log entry
        // ============================
        public static void Log(string action)
        {
            string entry =
                $"[{DateTime.Now:HH:mm}] {action}";

            logEntries.Add(entry);
        }

        // ============================
        // Get the last X entries
        // Default = 10
        // ============================
        public static string GetRecentLog(int count = 10)
        {
            if (logEntries.Count == 0)
            {
                return "No activity has been recorded yet.";
            }

            var recent =
                logEntries
                .TakeLast(count)
                .ToList();

            string output = "Here's a summary of recent actions:\n\n";

            for (int i = 0; i < recent.Count; i++)
            {
                output += $"{i + 1}. {recent[i]}\n";
            }

            if (logEntries.Count > count)
            {
                output +=
                    "\nType 'show more' to view the complete activity history.";
            }

            return output;
        }

        // ============================
        // Get the complete log
        // ============================
        public static string GetFullLog()
        {
            if (logEntries.Count == 0)
            {
                return "No activity has been recorded yet.";
            }

            string output = "Complete Activity History\n\n";

            for (int i = 0; i < logEntries.Count; i++)
            {
                output += $"{i + 1}. {logEntries[i]}\n";
            }

            return output;
        }

        // ============================
        // Total number of entries
        // ============================
        public static int GetCount()
        {
            return logEntries.Count;
        }

        // ============================
        // Clear log
        // ============================
        public static void Clear()
        {
            logEntries.Clear();
            
        }
    }
}
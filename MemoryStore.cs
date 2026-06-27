using System;

namespace CyberBot
{
    public class MemoryStore
    {
        //===========================================
        // User Information
        //===========================================

        public string UserName { get; set; } = "";

        public string FavoriteTopic { get; set; } = "";

        //===========================================
        // Conversation Memory
        //===========================================

        public string CurrentTopic { get; private set; } = "";

        public string LastTopic { get; private set; } = "";

        //===========================================
        // Stores the current topic
        //===========================================

        public void SetCurrentTopic(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return;

            LastTopic = CurrentTopic;
            CurrentTopic = topic;
        }

        //===========================================
        // Returns the current topic
        //===========================================

        public string GetCurrentTopic()
        {
            return CurrentTopic;
        }

        //===========================================
        // Returns the previous topic
        //===========================================

        public string GetLastTopic()
        {
            return LastTopic;
        }

        //===========================================
        // Returns true if user has introduced themselves
        //===========================================

        public bool HasUserName()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }

        //===========================================
        // Returns true if favourite topic exists
        //===========================================

        public bool HasFavoriteTopic()
        {
            return !string.IsNullOrWhiteSpace(FavoriteTopic);
        }

        //===========================================
        // Clears chatbot memory
        //===========================================

        public void ResetMemory()
        {
            UserName = "";
            FavoriteTopic = "";
            CurrentTopic = "";
            LastTopic = "";
        }
    }
}
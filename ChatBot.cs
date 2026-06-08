using System;

namespace CyberBot
{
    public class ChatBot
    {
        private KeywordResponder responder;
        private SentimentDetector sentiment;
        private MemoryStore memory;

        public ChatBot()
        {
            responder = new KeywordResponder();
            sentiment = new SentimentDetector();
            memory = new MemoryStore();
        }

        public string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            // Detect name in multiple ways
            if (input.StartsWith("my name is") ||
                input.StartsWith("i am") ||
                input.StartsWith("i'm"))
            {
                string name = input
                    .Replace("my name is", "")
                    .Replace("i am", "")
                    .Replace("i'm", "")
                    .Trim();

                memory.UserName = name;
                return $"Nice to meet you, {name}! ";
            }

            // If user just types a single word → assume it's their name
            if (!input.Contains(" ") && memory.UserName == null)
            {
                memory.UserName = input;
                return $"Nice to meet you, {input}! ";
            }

            // Mood detection
            string mood = sentiment.DetectMood(input);
            if (mood != null)
                return mood;

            // Keyword response
            string response = responder.GetResponse(input);
            if (response != null)
                return response;

            return "I'm not sure I understand. Try asking about cybersecurity.";
        }
    }
}
using System;

namespace CyberBot
{
    public class ChatBot
    {
        //===========================================
        // Objects
        //===========================================

        private readonly MemoryStore memory;
        private readonly KeywordResponder keywordResponder;
        private readonly SentimentDetector sentimentDetector;
        private readonly QuizManager quizManager; // ✅ FIX

        //===========================================
        // Constructor
        //===========================================

        public ChatBot()
        {
            memory = new MemoryStore();
            keywordResponder = new KeywordResponder();
            sentimentDetector = new SentimentDetector();
            quizManager = new QuizManager(); // ✅ FIX
        }

        //===========================================
        // Main Chat Method
        //===========================================

        public string GetReply(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type a message.";

            input = input.Trim();

            //------------------------------------------------
            // Save user's name
            //------------------------------------------------

            if (input.ToLower().StartsWith("my name is"))
            {
                string name = input.Substring(10).Trim();

                memory.UserName = name;

                ActivityLogger.Log($"User identified as {name}");

                return $"Nice to meet you, {name}!";
            }

            //------------------------------------------------
            // Sentiment Detection (POE requirement)
            //------------------------------------------------

            string sentimentResponse = sentimentDetector.DetectSentiment(input);

            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                ActivityLogger.Log("Sentiment detected and response given.");
                return sentimentResponse;
            }

            //------------------------------------------------
            // NLP Intent Detection
            //------------------------------------------------

            string intent = keywordResponder.DetectIntent(input);

            switch (intent)
            {
                case "addtask":

                    ActivityLogger.Log("NLP detected Add Task intent.");

                    return "Sure. What task would you like me to add?";

                case "reminder":

                    ActivityLogger.Log("NLP detected Reminder intent.");

                    return "No problem. What would you like me to remind you about?";

                case "quiz":

                    ActivityLogger.Log("Quiz started.");

                    quizManager.ResetQuiz(); // ✅ FIXED

                    return "Cybersecurity Quiz started! Type 'next question' to begin.";

                case "activitylog":

                    ActivityLogger.Log("Viewed activity log.");

                    return ActivityLogger.GetRecentLog();

                default:
                    break;
            }


            //------------------------------------------------
            // Keyword Response (Part 1 & 2 requirement)
            //------------------------------------------------

            string keywordResponse = keywordResponder.GetResponse(input);

            if (!string.IsNullOrEmpty(keywordResponse))
            {
                ActivityLogger.Log("Keyword matched: response delivered.");
                return keywordResponse;
            }

            //------------------------------------------------
            // FINAL FALLBACK (CRITICAL - fixes your error)
            //------------------------------------------------

            return "I did not quite understand that. Can you rephrase?";

        }
    }
}
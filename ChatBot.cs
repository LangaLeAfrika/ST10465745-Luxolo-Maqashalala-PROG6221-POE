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
            string lowerInput = input.ToLower();

            //------------------------------------------------
            // Save user's name
            //------------------------------------------------

            if (lowerInput.StartsWith("my name is"))
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
            // Follow-up conversation
            //------------------------------------------------

            if (lowerInput.Contains("tell me more") ||
                lowerInput.Contains("more information") ||
                lowerInput.Contains("explain more") ||
                lowerInput.Contains("continue") ||
                lowerInput.Contains("go on"))
            {
                string topic = memory.GetCurrentTopic();

                if (string.IsNullOrEmpty(topic))
                {
                    return "Which cybersecurity topic would you like to know more about?";
                }

                ActivityLogger.Log($"Follow-up requested for {topic}");

                switch (topic)
                {
                    case "password":
                        return "Strong passwords should be long, unique and contain uppercase letters, lowercase letters, numbers and symbols. A password manager can help keep them secure.";

                    case "phishing":
                        return "Phishing attacks often use fake emails or websites to steal personal information. Always verify the sender before clicking links.";

                    case "malware":
                        return "Malware includes viruses, worms, spyware and trojans. Keeping your antivirus software updated helps reduce the risk.";

                    case "ransomware":
                        return "Regular backups are your best defence against ransomware because they allow you to restore your files.";

                    case "privacy":
                        return "Review your privacy settings regularly and avoid sharing sensitive information publicly.";

                    case "vpn":
                        return "A VPN encrypts your internet traffic, especially when using public Wi-Fi.";

                    case "firewall":
                        return "Firewalls monitor incoming and outgoing network traffic and help block unauthorized access.";

                    case "antivirus":
                        return "Antivirus software detects, blocks and removes malicious software from your computer.";

                    case "email":
                        return "Always check the sender's email address carefully and avoid opening unexpected attachments.";

                    case "identity theft":
                        return "Protect your personal information and use strong authentication to reduce the risk of identity theft.";
                }
            }
            //------------------------------------------------
            // Remember the cybersecurity topic
            //-----------------------------------------------

            if (lowerInput.Contains("password"))
                memory.SetCurrentTopic("password");

            else if (lowerInput.Contains("phishing"))
                memory.SetCurrentTopic("phishing");

            else if (lowerInput.Contains("malware"))
                memory.SetCurrentTopic("malware");

            else if (lowerInput.Contains("ransomware"))
                memory.SetCurrentTopic("ransomware");

            else if (lowerInput.Contains("privacy"))
                memory.SetCurrentTopic("privacy");

            else if (lowerInput.Contains("vpn"))
                memory.SetCurrentTopic("vpn");

            else if (lowerInput.Contains("firewall"))
                memory.SetCurrentTopic("firewall");

            else if (lowerInput.Contains("antivirus"))
                memory.SetCurrentTopic("antivirus");

            else if (lowerInput.Contains("email"))
                memory.SetCurrentTopic("email");

            else if (lowerInput.Contains("identity"))
                memory.SetCurrentTopic("identity theft");
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
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

        //===========================================
        // Constructor
        //===========================================

        public ChatBot()
        {
            memory = new MemoryStore();
            keywordResponder = new KeywordResponder();
            sentimentDetector = new SentimentDetector();
        }

        //===========================================
        // Main Chat Method
        //===========================================

        public string GetReply(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please type a cybersecurity question.";
            }

            input = input.ToLower().Trim();

            //-------------------------------------------------
            // Greetings
            //-------------------------------------------------

            if (input.Contains("hello") ||
                input.Contains("hi") ||
                input.Contains("hey") ||
                input.Contains("good morning") ||
                input.Contains("good afternoon") ||
                input.Contains("good evening"))
            {
                if (!string.IsNullOrEmpty(memory.UserName))
                {
                    return $"Hello {memory.UserName}! How can I help you stay cyber safe today?";
                }

                return "Hello! Welcome to CyberShield Assistant.";
            }

            //-------------------------------------------------
            // User introduces name
            //-------------------------------------------------

            if (input.StartsWith("my name is "))
            {
                string name = input.Replace("my name is", "").Trim();

                if (!string.IsNullOrEmpty(name))
                {
                    memory.UserName =
                        char.ToUpper(name[0]) + name.Substring(1);

                    return $"Nice to meet you, {memory.UserName}! I'll remember your name.";
                }
            }

            //-------------------------------------------------
            // User asks name
            //-------------------------------------------------

            if (input.Contains("what is my name") ||
                input.Contains("remember my name"))
            {
                if (!string.IsNullOrEmpty(memory.UserName))
                {
                    return $"Your name is {memory.UserName}.";
                }

                return "I don't know your name yet. Tell me by typing 'My name is ...'";
            }

            //-------------------------------------------------
            // Favourite Topic
            //-------------------------------------------------

            if (input.StartsWith("my favourite topic is"))
            {
                string topic =
                    input.Replace("my favourite topic is", "").Trim();

                memory.FavoriteTopic = topic;

                return $"Great! I'll remember that your favourite cybersecurity topic is {topic}.";
            }

            //-------------------------------------------------
            // Ask Favourite Topic
            //-------------------------------------------------

            if (input.Contains("what is my favourite topic") ||
                input.Contains("favorite topic"))
            {
                if (!string.IsNullOrEmpty(memory.FavoriteTopic))
                {
                    return $"Your favourite cybersecurity topic is {memory.FavoriteTopic}.";
                }

                return "You haven't told me your favourite topic yet.";
            }
            //-------------------------------------------------
            // Sentiment Detection
            //-------------------------------------------------

            string sentimentResponse = sentimentDetector.DetectSentiment(input);

            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                return sentimentResponse;
            }

            //-------------------------------------------------
            // Follow-up Questions
            //-------------------------------------------------

            if (input.Contains("tell me more") ||
                input.Contains("more information") ||
                input.Contains("explain more"))
            {
                if (!string.IsNullOrEmpty(memory.CurrentTopic))
                {
                    return keywordResponder.GetResponse(memory.CurrentTopic);
                }

                return "Please ask about a cybersecurity topic first, then I can tell you more.";
            }

            //-------------------------------------------------
            // Thank You
            //-------------------------------------------------

            if (input.Contains("thank you") ||
                input.Equals("thanks") ||
                input.Contains("thanks"))
            {
                if (!string.IsNullOrEmpty(memory.UserName))
                {
                    return $"You're welcome, {memory.UserName}! Stay cyber safe.";
                }

                return "You're welcome! Stay cyber safe.";
            }

            //-------------------------------------------------
            // Goodbye
            //-------------------------------------------------

            if (input.Equals("bye") ||
                input.Equals("goodbye") ||
                input.Equals("exit") ||
                input.Contains("see you"))
            {
                if (!string.IsNullOrEmpty(memory.UserName))
                {
                    return $"Goodbye {memory.UserName}! Remember to stay safe online and think before you click.";
                }

                return "Goodbye! Stay cyber safe.";
            }

            //-------------------------------------------------
            // Detect Cybersecurity Topics
            //-------------------------------------------------

            string[] topics =
            {
                "password",
                "phishing",
                "virus",
                "malware",
                "privacy",
                "scam",
                "safe browsing",
                "social engineering",
                "suspicious link",
                "two-factor authentication",
                "firewall",
                "antivirus",
                "encryption",
                "backup",
                "update",
                "authentication",
                "vpn",
                "email",
                "data breach",
                "identity theft",
                "spyware",
                "ransomware",
                "public wi-fi",
                "cloud security",
                "cyberbullying"
            };

            foreach (string topic in topics)
            {
                if (input.Contains(topic))
                {
                    memory.SetCurrentTopic(topic);

                    return keywordResponder.GetResponse(input);
                }
            }

            //-------------------------------------------------
            // Unknown Question
            //-------------------------------------------------

            return
            @"I'm not sure how to answer that yet.

            You can ask me about topics such as:

            • Password Security
            • Phishing
            • Malware
            • Ransomware
            • Firewall
            • Antivirus
            • Encryption
            • VPN
            • Email Security
            • Public Wi-Fi
            • Cloud Security
            • Identity Theft

            I'm always happy to help you improve your cybersecurity awareness.";
                    }
    }
}
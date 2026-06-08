using System;

namespace CyberBot
{
    public class SentimentDetector
    {
        public string DetectMood(string input)
        {
            if (input.Contains("worried") || input.Contains("scared"))
                return "It's okay to feel that way. Cybersecurity can be confusing, but I'm here to help!";

            if (input.Contains("happy"))
                return "Glad you're feeling good! Stay safe online! ";

            return null;
        }
    }
}
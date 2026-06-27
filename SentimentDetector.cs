namespace CyberBot
{
    public class SentimentDetector
    {
        public string DetectSentiment(string input)
        {
            input = input.ToLower();

            // Worried or scared
            if (input.Contains("worried") ||
                input.Contains("scared") ||
                input.Contains("afraid"))
            {
                return "I understand your concern. Staying informed and following good cybersecurity practices can help keep you safe online.";
            }

            // Curious
            if (input.Contains("curious"))
            {
                return "That's great! Curiosity is an important part of learning cybersecurity.";
            }

            // Frustrated
            if (input.Contains("frustrated"))
            {
                return "Cybersecurity can sometimes feel overwhelming, but don't worry. We can work through it together.";
            }

            // Confused
            if (input.Contains("confused"))
            {
                return "No problem. I'll do my best to explain cybersecurity concepts in a simpler way.";
            }

            // Happy
            if (input.Contains("happy"))
            {
                return "I'm glad you're enjoying learning about cybersecurity!";
            }

            // Excited
            if (input.Contains("excited"))
            {
                return "That's wonderful! Enthusiasm makes learning easier and more enjoyable.";
            }

            // Sad
            if (input.Contains("sad"))
            {
                return "I'm sorry you're feeling down. Remember that improving your cybersecurity knowledge can help you feel more confident online.";
            }

            // Angry
            if (input.Contains("angry"))
            {
                return "Take your time. Cybersecurity issues can be frustrating, but there are always ways to solve them.";
            }

            // No sentiment detected
            return "";
        }
    }
}
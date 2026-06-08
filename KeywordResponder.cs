using System;

namespace CyberBot
{
    public class KeywordResponder
    {
        public string GetResponse(string input)
        {
            if (input.Contains("password"))
                return "Use strong passwords with letters, numbers, and symbols.";

            if (input.Contains("phishing"))
                return "Phishing is when attackers trick you into giving personal info. Always check links!";

            if (input.Contains("virus"))
                return "Install antivirus software and avoid suspicious downloads.";

            if (input.Contains("safe browsing"))
                return "Only visit secure websites (https) and avoid unknown links.";

            return null;
        }
    }
}
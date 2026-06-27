using System;
using System.Collections.Generic;

namespace CyberBot
{
public class KeywordResponder
{
private readonly Random random = new Random();

private readonly Dictionary<string, List<string>> responses =
    new Dictionary<string, List<string>>
{
    // ============================================
    // 1. Password
    // ============================================

    {
        "password",
        new List<string>
        {
            "Use strong passwords that combine uppercase letters, lowercase letters, numbers and symbols.",
            "Avoid using personal information such as birthdays in your passwords.",
            "Consider using a password manager to generate and store strong passwords.",
            "Never share your passwords with anyone."
        }
    },

    // ============================================
    // 2. Phishing
    // ============================================

    {
        "phishing",
        new List<string>
        {
            "Avoid clicking suspicious email links.",
            "Always verify the sender before opening attachments.",
            "Banks will never ask for your password through email.",
            "Watch out for spelling mistakes and urgent requests."
        }
    },

    // ============================================
    // 3. Virus
    // ============================================

    {
        "virus",
        new List<string>
        {
            "Keep your antivirus software updated.",
            "Run regular virus scans on your computer.",
            "Avoid downloading files from unknown websites.",
            "Viruses can damage files and slow down your computer."
        }
    },

    // ============================================
    // 4. Malware
    // ============================================

    {
        "malware",
        new List<string>
        {
            "Malware is malicious software designed to damage systems or steal information.",
            "Only install software from trusted websites.",
            "Keep your operating system updated to reduce malware risks.",
            "Use antivirus software to detect and remove malware."
        }
    },

    // ============================================
    // 5. Privacy
    // ============================================

    {
        "privacy",
        new List<string>
        {
            "Review your privacy settings regularly.",
            "Avoid sharing sensitive personal information online.",
            "Limit what you post on social media.",
            "Think carefully before granting app permissions."
        }
    },

    // ============================================
    // 6. Scam
    // ============================================

    {
        "scam",
        new List<string>
        {
            "Be cautious of offers that seem too good to be true.",
            "Never send money to strangers online.",
            "Always verify messages before taking action.",
            "Scammers often create a false sense of urgency."
        }
    },

    // ============================================
    // 7. Safe Browsing
    // ============================================

    {
        "safe browsing",
        new List<string>
        {
            "Visit trusted websites whenever possible.",
            "Keep your web browser updated.",
            "Avoid entering personal information on unsecured websites.",
            "Look for HTTPS before entering sensitive information."
        }
    },

    // ============================================
    // 8. Social Engineering
    // ============================================

    {
        "social engineering",
        new List<string>
        {
            "Cybercriminals often manipulate people instead of computers.",
            "Never reveal sensitive information to unknown individuals.",
            "Always verify someone's identity before sharing confidential information.",
            "Think carefully before responding to unexpected requests."
        }
    },

    // ============================================
    // 9. Suspicious Links
    // ============================================

    {
        "suspicious link",
        new List<string>
        {
            "Avoid clicking links from unknown senders.",
            "Hover over links to inspect the destination before clicking.",
            "Suspicious links are commonly used in phishing attacks.",
            "If a link looks unusual, don't click it."
        }
    },
                // ============================================
    // 10. Two-Factor Authentication
    // ============================================

    {
        "two-factor authentication",
        new List<string>
        {
            "Two-factor authentication adds an extra layer of security to your accounts.",
            "Enable 2FA on your email, banking and social media accounts.",
            "Even if someone steals your password, 2FA helps keep your account secure.",
            "Authenticator apps are generally safer than SMS verification."
        }
    },

    // ============================================
    // 11. Firewall
    // ============================================

    {
        "firewall",
        new List<string>
        {
            "Firewalls help block unauthorized access to your computer.",
            "A firewall acts as a protective barrier between your device and the internet.",
            "Keeping your firewall enabled improves network security.",
            "Both hardware and software firewalls help defend against cyber threats."
        }
    },

    // ============================================
    // 12. Antivirus
    // ============================================

    {
        "antivirus",
        new List<string>
        {
            "Antivirus software helps detect and remove malicious programs.",
            "Regular antivirus updates improve protection against new threats.",
            "Schedule regular scans to keep your computer protected.",
            "Always use trusted antivirus software."
        }
    },

    // ============================================
    // 13. Encryption
    // ============================================

    {
        "encryption",
        new List<string>
        {
            "Encryption protects information by converting it into unreadable data.",
            "Encrypted information can only be accessed with the correct key.",
            "Most secure websites use encryption to protect your data.",
            "Encryption is essential for protecting sensitive information."
        }
    },

    // ============================================
    // 14. Backup
    // ============================================

    {
        "backup",
        new List<string>
        {
            "Regular backups help protect your files from accidental loss.",
            "Cloud backups provide extra protection if your device fails.",
            "Backups are your best defence against ransomware attacks.",
            "Follow the 3-2-1 backup rule whenever possible."
        }
    },

    // ============================================
    // 15. Updates
    // ============================================

    {
        "update",
        new List<string>
        {
            "Keeping software updated helps close security vulnerabilities.",
            "Updates often include important security improvements.",
            "Enable automatic updates whenever possible.",
            "Outdated software can expose your computer to cyberattacks."
        }
    },

    // ============================================
    // 16. Authentication
    // ============================================

    {
        "authentication",
        new List<string>
        {
            "Authentication verifies a user's identity before granting access.",
            "Strong authentication methods improve cybersecurity.",
            "Multi-factor authentication offers better protection than passwords alone.",
            "Authentication helps prevent unauthorized access."
        }
    },

    // ============================================
    // 17. VPN
    // ============================================

    {
        "vpn",
        new List<string>
        {
            "A VPN encrypts your internet traffic and improves online privacy.",
            "Use a VPN when connecting to public Wi-Fi networks.",
            "Choose a trusted VPN provider for better security.",
            "VPNs help reduce the risk of data interception."
        }
    },

    // ============================================
    // 18. Email Security
    // ============================================

    {
        "email",
        new List<string>
        {
            "Be cautious when opening email attachments from unknown senders.",
            "Email is one of the most common ways cybercriminals spread malware.",
            "Never click suspicious email links without verifying the sender.",
            "Strong passwords and two-factor authentication improve email security."
        }
    },
                // ============================================
    // 19. Data Breach
    // ============================================

    {
        "data breach",
        new List<string>
        {
            "A data breach occurs when sensitive information is accessed without permission.",
            "Strong passwords and encryption help reduce the risk of data breaches.",
            "Always change your password if a service reports a data breach.",
            "Monitor your accounts after a reported data breach."
        }
    },

    // ============================================
    // 20. Identity Theft
    // ============================================

    {
        "identity theft",
        new List<string>
        {
            "Identity theft happens when criminals steal personal information to impersonate you.",
            "Never share your ID number or banking details with untrusted sources.",
            "Monitor your bank statements for suspicious transactions.",
            "Protect your personal information both online and offline."
        }
    },

    // ============================================
    // 21. Spyware
    // ============================================

    {
        "spyware",
        new List<string>
        {
            "Spyware secretly monitors your activities without your permission.",
            "Install software only from trusted websites to reduce spyware risks.",
            "Keep your antivirus software updated to detect spyware.",
            "Avoid clicking unknown pop-ups or advertisements."
        }
    },

    // ============================================
    // 22. Ransomware
    // ============================================

    {
        "ransomware",
        new List<string>
        {
            "Ransomware encrypts your files and demands payment to unlock them.",
            "Maintain regular backups to recover your files if infected.",
            "Never open unexpected email attachments from unknown senders.",
            "Keep your operating system updated to reduce ransomware risks."
        }
    },

    // ============================================
    // 23. Public Wi-Fi
    // ============================================

    {
        "public wi-fi",
        new List<string>
        {
            "Avoid accessing online banking while connected to public Wi-Fi.",
            "Use a trusted VPN when using public Wi-Fi networks.",
            "Public Wi-Fi can expose your personal information if not secured.",
            "Turn off automatic Wi-Fi connections when travelling."
        }
    },

    // ============================================
    // 24. Cloud Security
    // ============================================

    {
        "cloud security",
        new List<string>
        {
            "Enable two-factor authentication on your cloud accounts.",
            "Only store sensitive files with trusted cloud providers.",
            "Review file sharing permissions regularly.",
            "Use strong passwords to protect cloud storage accounts."
        }
    },

    // ============================================
    // 25. Cyberbullying
    // ============================================

    {
        "cyberbullying",
        new List<string>
        {
            "Report cyberbullying to the platform where it occurs.",
            "Never respond aggressively to online harassment.",
            "Block abusive users and keep evidence if necessary.",
            "Speak to a trusted adult, teacher or guardian if cyberbullying continues."
        }
    }
};

// ============================================
// Returns a random response for a topic
// ============================================

public string GetResponse(string input)
{
    input = input.ToLower();

    foreach (var topic in responses.Keys)
    {
        if (input.Contains(topic))
        {
            List<string> topicResponses = responses[topic];

            return topicResponses[random.Next(topicResponses.Count)];
        }
    }

    return
@"I'm still learning about that topic.

Try asking me about:

• Password Security
• Phishing
• Malware
• Ransomware
• Firewall
• Antivirus
• Encryption
• VPN
• Safe Browsing
• Email Security
• Public Wi-Fi
• Cloud Security
• Identity Theft

I'm always happy to help you stay cyber safe!";
}
}
}
using System.Collections.Generic;

namespace CyberBot
{
    public class QuizManager
    {
        // ============================
        // Fields
        // ============================

        private readonly List<QuizQuestion> questions;

        private int currentQuestionIndex;

        private int score;

        // ============================
        // Constructor
        // ============================

        public QuizManager()
        {
            questions = new List<QuizQuestion>();

            currentQuestionIndex = 0;

            score = 0;

            LoadQuestions();
        }

        // ============================
        // Load Quiz Questions
        // ============================

        private void LoadQuestions()
        {
            questions.Add(new QuizQuestion
            {
                Question = "What should you do if you receive an email asking for your password?",

                Options = new List<string>
                {
                    "A. Reply with your password",
                    "B. Ignore it",
                    "C. Report it as phishing",
                    "D. Forward it to friends"
                },

                CorrectAnswer = "C",

                Explanation = "Reporting phishing emails helps prevent scams and protects other users.",

                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Using the same password for every account is safe.",

                Options = new List<string>
                {
                    "True",
                    "False"
                },

                CorrectAnswer = "False",

                Explanation = "Each account should have its own unique password.",

                IsTrueFalse = true
            });

            questions.Add(new QuizQuestion
            {
                Question = "Which of these is the strongest password?",

                Options = new List<string>
                {
                    "A. password123",
                    "B. 12345678",
                    "C. P@55w0rd!",
                    "D. qwerty"
                },

                CorrectAnswer = "C",

                Explanation = "Strong passwords combine uppercase, lowercase, numbers and symbols.",

                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Public Wi-Fi is always safe for online banking.",

                Options = new List<string>
                {
                    "True",
                    "False"
                },

                CorrectAnswer = "False",

                Explanation = "Avoid banking on public Wi-Fi unless you are using a trusted VPN.",

                IsTrueFalse = true
            });
            // ============================
            // Question 5
            // ============================

            questions.Add(new QuizQuestion
            {
                Question = "Which type of software is designed to damage or steal data from a computer?",

                Options = new List<string>
    {
        "A. Antivirus",
        "B. Malware",
        "C. Firewall",
        "D. VPN"
    },

                CorrectAnswer = "B",

                Explanation = "Malware is malicious software that can damage systems or steal information.",

                IsTrueFalse = false
            });

            // ============================
            // Question 6
            // ============================

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Ransomware encrypts your files and demands payment to unlock them.",

                Options = new List<string>
    {
        "True",
        "False"
    },

                CorrectAnswer = "True",

                Explanation = "Ransomware locks or encrypts files and demands a ransom for recovery.",

                IsTrueFalse = true
            });

            // ============================
            // Question 7
            // ============================

            questions.Add(new QuizQuestion
            {
                Question = "What is the main benefit of Two-Factor Authentication (2FA)?",

                Options = new List<string>
    {
        "A. Makes your internet faster",
        "B. Adds an extra layer of account security",
        "C. Removes the need for passwords",
        "D. Blocks all viruses"
    },

                CorrectAnswer = "B",

                Explanation = "Two-Factor Authentication adds another verification step before login.",

                IsTrueFalse = false
            });

            // ============================
            // Question 8
            // ============================

            questions.Add(new QuizQuestion
            {
                Question = "Which website is generally safer to enter personal information on?",

                Options = new List<string>
    {
        "A. A website beginning with HTTP",
        "B. A website beginning with HTTPS",
        "C. Any public Wi-Fi login page",
        "D. Any website with advertisements"
    },

                CorrectAnswer = "B",

                Explanation = "HTTPS encrypts communication between your browser and the website.",

                IsTrueFalse = false
            });
        }
        // ============================
        // Questions 9 - 12
        // ============================

        private void AddRemainingQuestions()
        {
            questions.Add(new QuizQuestion
            {
                Question = "True or False: You should regularly review your privacy settings on social media.",

                Options = new List<string>
                {
                    "True",
                    "False"
                },

                CorrectAnswer = "True",

                Explanation = "Regularly reviewing your privacy settings helps protect your personal information.",

                IsTrueFalse = true
            });

            questions.Add(new QuizQuestion
            {
                Question = "Social engineering attacks mainly target:",

                Options = new List<string>
                {
                    "A. Computer hardware",
                    "B. Human behaviour",
                    "C. Internet cables",
                    "D. Printers"
                },

                CorrectAnswer = "B",

                Explanation = "Social engineering manipulates people into revealing sensitive information.",

                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is Identity Theft?",

                Options = new List<string>
                {
                    "A. Stealing someone's personal information",
                    "B. Hacking a Wi-Fi router",
                    "C. Installing antivirus software",
                    "D. Creating strong passwords"
                },

                CorrectAnswer = "A",

                Explanation = "Identity theft occurs when someone uses another person's personal information without permission.",

                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Regular backups help protect against ransomware attacks.",

                Options = new List<string>
                {
                    "True",
                    "False"
                },

                CorrectAnswer = "True",

                Explanation = "Backups allow you to restore your files without paying a ransom.",

                IsTrueFalse = true
            });
        }

        // ============================
        // Quiz Methods
        // ============================

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count)
                return questions[currentQuestionIndex];

            return null;
        }

        public bool SubmitAnswer(string answer)
        {
            if (currentQuestionIndex >= questions.Count)
                return false;

            bool correct = answer.Trim().ToUpper() ==
                           questions[currentQuestionIndex].CorrectAnswer.Trim().ToUpper();

            if (correct)
                score++;

            currentQuestionIndex++;

            return correct;
        }

        public bool IsFinished()
        {
            return currentQuestionIndex >= questions.Count;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetTotalQuestions()
        {
            return questions.Count;
        }

        public string GetFinalScore()
        {
            return $"You scored {score} out of {questions.Count}.";
        }

        public string GetFinalMessage()
        {
            double percentage = (double)score / questions.Count * 100;

            if (percentage >= 80)
                return "Excellent! Your cybersecurity knowledge is outstanding.";

            if (percentage >= 60)
                return "Well done! You have a solid understanding of cybersecurity.";

            if (percentage >= 40)
                return "Good effort. Keep learning to strengthen your cybersecurity skills.";

            return "Keep practising. Cybersecurity awareness improves with continuous learning.";
        }

        public void ResetQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
        }
    }
}
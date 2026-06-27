using System.Collections.Generic;

namespace CyberBot
{
    public class QuizQuestion
    {
        // Question text
        public string Question { get; set; }

        // Answer options (A, B, C, D)
        public List<string> Options { get; set; }

        // Correct answer (A, B, C or D)
        public string CorrectAnswer { get; set; }

        // Explanation shown after answering
        public string Explanation { get; set; }

        // True if this is a True/False question
        public bool IsTrueFalse { get; set; }

        // Constructor
        public QuizQuestion()
        {
            Question = "";
            Options = new List<string>();
            CorrectAnswer = "";
            Explanation = "";
            IsTrueFalse = false;
        }
    }
}
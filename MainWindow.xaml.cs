using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CyberBot
{
    public partial class MainWindow : Window
    {
        //=========================================================
        // Objects
        //=========================================================
        private readonly ChatBot bot = new ChatBot();
        private readonly TaskManager taskManager = new TaskManager();
        private QuizManager quizManager = new QuizManager();
        private string selectedAnswer = ""; // kept for UI, but handlers will use quizManager for logic

        //=========================================================
        // User Information
        //=========================================================
        private bool waitingForName = true;
        private string userName = "";

        //=========================================================
        // Constructor
        //=========================================================
        public MainWindow()
        {
            InitializeComponent();
            // initial LoadQuestion moved to Loaded handler to avoid deferred TabItem issues
        }

        private void RefreshTasks()
        {
            TaskList.ItemsSource = null;
            TaskList.ItemsSource = taskManager.GetAllTasks();
        }

        //=========================================================
        // Window Loaded
        //=========================================================
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserInput.Focus();
            await StartupSequence();
            RefreshTasks();

            // ensure quiz UI populated after layout
            Dispatcher.BeginInvoke(new Action(LoadQuestion), DispatcherPriority.Loaded);
        }

        //=========================================================
        // Startup Sequence
        //=========================================================
        private async Task StartupSequence()
        {
            PlayGreeting();

            await Task.Delay(400);

            DisplayAsciiArt();

            await Task.Delay(600);

            ChatDisplay.AppendText("Initializing CyberShield Assistant...\n");
            ChatDisplay.ScrollToEnd();

            await Task.Delay(450);

            ChatDisplay.AppendText("Loading Cybersecurity Knowledge Base...\n");
            ChatDisplay.ScrollToEnd();

            await Task.Delay(450);

            ChatDisplay.AppendText("Loading Threat Intelligence...\n");
            ChatDisplay.ScrollToEnd();

            await Task.Delay(450);

            ChatDisplay.AppendText("Checking Secure Environment...\n");
            ChatDisplay.ScrollToEnd();

            await Task.Delay(450);

            ChatDisplay.AppendText("✔ System Secure\n");
            ChatDisplay.AppendText("✔ Knowledge Base Loaded\n");
            ChatDisplay.AppendText("✔ CyberShield Ready\n\n");

            ChatDisplay.ScrollToEnd();

            await Task.Delay(400);

            DisplayWelcome();

            AskUserName();
            LoadTasks();
        }

        //=========================================================
        // Greeting Audio
        //=========================================================
        private void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "greeting.wav");

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch
            {
                // Continue silently if audio fails
            }
        }

        //=========================================================
        // ASCII Banner
        //=========================================================
        private void DisplayAsciiArt()
        {
            ChatDisplay.AppendText(@"

                   _____      _               _____ _     _      _     _
                  / ____|    | |             / ____| |   (_)    | |   | |
                 | |    _   _| |__   ___ _ _| (___ | |__  _  ___| | __| |
                 | |   | | | | '_ \ / _ \ '__\___ \| '_ \| |/ _ \ |/ _` |
                 | |___| |_| | |_) |  __/ |  ____) | | | | |  __/ | (_| |
                  \_____\__, |_.__/ \___|_| |_____/|_| |_|_|\___|_|\__,_|
                          __/ |
                         |___|

                ============================================================

");
        }

        //=========================================================
        // Welcome Message
        //=========================================================
        private void DisplayWelcome()
        {
            ChatDisplay.AppendText(
                @"Welcome to CyberShield Assistant!

                Your AI Cybersecurity Awareness Assistant.

                I can help you learn about:
                            
      🔐 Passwords       🛡 Firewall        🌐 VPN             📧 Email Security 
      🎣 Phishing        🦠 Antivirus       🔒 Encryption      ☁ Cloud Security  
      💀 Malware         🛑 Ransomware      📶 Public Wi-Fi    👤 Identity Theft 
      🌍 Safe Browsing   🎭 Social Eng.     🔑 Privacy         ⚠ Scams          

");
        }

        //=========================================================
        // Ask User Name
        //=========================================================
        private void AskUserName()
        {
            ChatDisplay.AppendText(
                @"👤 Before we begin...

                What is your name?

                ------------------------------------------------------------

                ");

            ChatDisplay.ScrollToEnd();

            UserInput.Focus();
        }

        //=========================================================
        // Enter Key
        //=========================================================
        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                SendMessage();
            }
        }

        //=========================================================
        // Load Questions
        //=========================================================
        private void LoadQuestion()
        {
            var question = quizManager.GetCurrentQuestion();
            ResultText.Text = "";

            if (question == null)
                return;

            QuestionText.Text = question.Question;
            OptionsPanel.Children.Clear();

            foreach (var option in question.Options)
            {
                RadioButton rb = new RadioButton
                {
                    Content = option,
                    FontSize = 16,
                    Foreground = Brushes.White,
                    Margin = new Thickness(5)
                };

                rb.Checked += (s, e) =>
                {
                    selectedAnswer = option.Substring(0, 1);
                };

                OptionsPanel.Children.Add(rb);
            }
        }

        //=========================================================
        // Send Button
        //=========================================================
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        //=========================================================
        // Send Message
        //=========================================================
        private async void SendMessage()
        {
            string message = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;
            ActivityLogger.Log($"User asked: {message}");

            UserInput.Clear();

            //-----------------------------------------------------
            // First message = User's Name
            //-----------------------------------------------------
            if (waitingForName)
            {
                userName = message;

                // Store the name inside the chatbot
                bot.GetReply("my name is " + userName);

                waitingForName = false;

                ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 👤 {userName}

------------------------------------------------------------

");

                await Task.Delay(300);

                ChatDisplay.AppendText(
        $@"[{DateTime.Now:HH:mm}] 🤖 CyberShield

        Hello {userName}!

        It's great to meet you.

        What can I assist you with today:

            AVAILABLE CYBERSECURITY TOPICS   

      🔐 Passwords       🛡 Firewall        🌐 VPN             📧 Email Security 
      🎣 Phishing        🦠 Antivirus       🔒 Encryption      ☁ Cloud Security  
      💀 Malware         🛑 Ransomware      📶 Public Wi-Fi    👤 Identity Theft 
      🌍 Safe Browsing   🎭 Social Eng.     🔑 Privacy         ⚠ Scams          

");

                ChatDisplay.ScrollToEnd();
                UserInput.Focus();

                return;
            }

            //-----------------------------------------------------
            // Normal Chat Flow (AFTER name is set)
            //-----------------------------------------------------
            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 👤 {userName}: {message}

");

            ActivityLogger.Log($"[{userName}] said: {message}");

            string reply = bot.GetReply(message);

            await Task.Delay(200);

            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 🤖 CyberShield: {reply}

------------------------------------------------------------

");

            ActivityLogger.Log($"Bot replied: {reply}");
            ChatDisplay.ScrollToEnd();

            //-----------------------------------------------------
            // Display User Message
            //-----------------------------------------------------
            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 👤 {userName}

            {message}

            ");

            ChatDisplay.ScrollToEnd();

            //-----------------------------------------------------
            // Professional Typing Animation
            //---------------------------------------------------------
            ChatDisplay.AppendText(
$@"[{DateTime.Now:HH:mm}] 🤖 CyberShield

            Typing");

            ChatDisplay.ScrollToEnd();

            for (int i = 0; i < 3; i++)
            {
                await Task.Delay(250);
                ChatDisplay.AppendText(".");
                ChatDisplay.ScrollToEnd();
            }

            await Task.Delay(250);

            //-----------------------------------------------------
            // Remove Typing Text
            //-----------------------------------------------------
            int typingIndex = ChatDisplay.Text.LastIndexOf("Typing");

            if (typingIndex >= 0)
            {
                ChatDisplay.Text =
                    ChatDisplay.Text.Remove(
                        typingIndex,
                        ChatDisplay.Text.Length - typingIndex);
            }

            // 🔹 NLP Task Detection
            if (message.ToLower().Contains("add task") ||
                message.ToLower().Contains("remind me") ||
                message.ToLower().Contains("i need to"))
            {
                string taskTitle = message
                    .Replace("add task", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("remind me to", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("i need to", "", StringComparison.OrdinalIgnoreCase)
                    .Trim();

                if (!string.IsNullOrWhiteSpace(taskTitle))
                {
                    taskManager.AddTask(
                        taskTitle,
                        "Created from chat",
                        "");

                    // Refresh UI list
                    RefreshTasks();

                    // Log it
                    ActivityLogger.Log($"Task created from chat: {taskTitle}");
                    ChatDisplay.ScrollToEnd();
                    return;
                }
            }

            //-----------------------------------------------------
            // Get ChatBot Response
            //-----------------------------------------------------
            ActivityLogger.Log("Bot responded to user");
            ChatDisplay.ScrollToEnd();

            UserInput.Focus();
        }

        //=========================================================
        // Clear Chat
        //=========================================================
        private async void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Clear the current conversation?",
                "CyberShield Assistant",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            ChatDisplay.Clear();

            waitingForName = true;
            userName = "";

            UserInput.Clear();

            await StartupSequence();

            UserInput.Focus();
        }

        //=========================================================
        // Exit Button
        //=========================================================
       private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit CyberShield Assistant?",
                "Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void LoadTasks()
        {
            TaskList.ItemsSource = taskManager.GetAllTasks();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string description = TaskDescriptionBox.Text.Trim();
            string reminder = ReminderBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            string result = taskManager.AddTask(
                title,
                description,
                reminder);

            MessageBox.Show(result);

            TaskTitleBox.Clear();
            TaskDescriptionBox.Clear();
            ReminderBox.Clear();

            RefreshTasks();
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            CyberTask task = (CyberTask)TaskList.SelectedItem;

            MessageBox.Show(
                taskManager.MarkAsComplete(task.Id));

            RefreshTasks();
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = OptionsPanel.Children
                .OfType<RadioButton>()
                .FirstOrDefault(r => r.IsChecked == true);

            if (selectedOption == null)
            {
                ResultText.Text = "⚠ Please select an answer.";
                return;
            }

            var currentQuestion = quizManager.GetCurrentQuestion();
            if (currentQuestion == null)
                return;

            // Determine the answer text to submit:
            // - For True/False questions submit "True"/"False"
            // - For multiple-choice submit the leading letter (A/B/C/D)
            string raw = selectedOption.Content.ToString();
            string answerToSubmit = currentQuestion.IsTrueFalse
                ? raw.Trim()
                : raw.Length > 0 ? raw.Substring(0, 1).ToUpper() : raw.Trim();

            bool correct = quizManager.SubmitAnswer(answerToSubmit);

            if (correct)
            {
                ResultText.Text = "✅ Correct!";
                ActivityLogger.Log("Quiz: Correct answer");
            }
            else
            {
                ResultText.Text = $"❌ Wrong! Correct answer: {currentQuestion.CorrectAnswer}";
                ActivityLogger.Log("Quiz: Wrong answer");
            }

            if (!quizManager.IsFinished())
            {
                LoadQuestion();
            }
            else
            {
                ShowFinalScore();
            }
        }

        private void ShowFinalScore()
        {
            QuestionText.Text = "🎉 Quiz Completed!";
            OptionsPanel.Children.Clear();

            ResultText.Text = quizManager.GetFinalScore();

            ActivityLogger.Log($"Quiz Finished. {quizManager.GetFinalScore()}");

            // Reset manager for next run
            quizManager.ResetQuiz();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            CyberTask task = (CyberTask)TaskList.SelectedItem;

            MessageBox.Show(
                taskManager.DeleteTask(task.Id));

            RefreshTasks();
        }

        private void RestartQuiz_Click(object sender, RoutedEventArgs e)
        {
            quizManager.ResetQuiz();
            ResultText.Text = "";
            LoadQuestion();
            ActivityLogger.Log("Quiz restarted");
        }

        private void RefreshTasks_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }

        private void RefreshLog_Click(object sender, RoutedEventArgs e)
        {
            LogDisplay.Text = ActivityLogger.GetRecentLog();
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogger.Clear();
            LogDisplay.Text = "Log cleared.";
        }
    }
}
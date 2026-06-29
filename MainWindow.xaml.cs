using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CyberBot
{
    public partial class MainWindow : Window
    {
        //=========================================================
        // Objects
        //=========================================================

        private readonly ChatBot bot = new ChatBot();
        private readonly TaskManager taskManager = new TaskManager();
        private ActivityLogger logger = new ActivityLogger();

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

            Loaded += MainWindow_Loaded;

            UserInput.KeyDown += UserInput_KeyDown;
        }

        //=========================================================
        // Window Loaded
        //=========================================================

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserInput.Focus();

            await StartupSequence();
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

            // 🔹 Show user message
            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 👤 {userName}: {message}

");

            // 🔹 LOG user message
            logger.Log($"[{userName}] said: {message}");

            // 🔹 Get bot reply
            string reply = bot.GetReply(message);

            // 🔹 Small delay for realism
            await Task.Delay(200);

            // 🔹 Show bot reply
            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 🤖 CyberShield: {reply}

------------------------------------------------------------

");

            // 🔹 LOG bot reply
            logger.Log($"Bot replied: {reply}");

            // 🔹 Auto scroll
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
            //-----------------------------------------------------

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
                    taskManager.AddTask(taskTitle);

                    // Refresh UI list
                    RefreshTaskList();

                    // Log it
                    logger.Log($"Task created from chat: {taskTitle}");

                    ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 🤖 CyberShield:

✅ Task added: {taskTitle}

------------------------------------------------------------

");

                    ChatDisplay.ScrollToEnd();
                    return;
                }
            }
            //-----------------------------------------------------
            // Get ChatBot Response
            //-----------------------------------------------------

            string reply = bot.GetReply(message);

            //-----------------------------------------------------
            // Display Bot Reply
            //-----------------------------------------------------

            ChatDisplay.AppendText(
            $@"[{DateTime.Now:HH:mm}] 🤖 CyberShield

            {reply}

            ------------------------------------------------------------

");

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
            TaskList.Items.Clear();

            foreach (CyberTask task in taskManager.GetAllTasks())
            {
                TaskList.Items.Add(task);
            }
        }
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string description = TaskDescriptionBox.Text.Trim();
            string reminder = ReminderBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show(
                    "Please enter a task title.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            string result =
                taskManager.AddTask(title, description, reminder);

            MessageBox.Show(result);

            TaskTitleBox.Clear();
            TaskDescriptionBox.Clear();
            ReminderBox.Clear();

            LoadTasks();
        }
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");

                return;
            }

            CyberTask selectedTask =
                (CyberTask)TaskList.SelectedItem;

            MessageBox.Show(
                taskManager.MarkAsComplete(selectedTask.Id));

            LoadTasks();
        }
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");

                return;
            }

            CyberTask selectedTask =
                (CyberTask)TaskList.SelectedItem;

            MessageBox.Show(
                taskManager.DeleteTask(selectedTask.Id));

            LoadTasks();
        }
        private void RefreshTaskList()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = taskManager.GetTasks();
        }
    }
}
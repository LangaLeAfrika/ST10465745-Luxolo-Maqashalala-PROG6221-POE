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

                • Password Security
                • Phishing
                • Malware
                • Ransomware
                • Safe Browsing
                • VPN
                • Encryption
                • Firewall
                • Email Security
                • Identity Theft
                • Cloud Security
                • Social Engineering

                ------------------------------------------------------------

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
    }
}
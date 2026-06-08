using CyberBot;
using System.Windows;

namespace CyberSecurityChatbot   //MUST match XAML namespace
{
    public partial class MainWindow : Window
    {
        private ChatBot bot;

        public MainWindow()
        {
            InitializeComponent();
            bot = new ChatBot();

            ChatDisplay.AppendText("Bot: Hello! Welcome to the Cybersecurity Chatbot.\n");
            ChatDisplay.AppendText("Bot: What's your name?\n\n");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text;

            if (string.IsNullOrWhiteSpace(input))
                return;

            // Show user message
            ChatDisplay.AppendText("You: " + input + "\n");

            // Get bot response
            string response = bot.GetResponse(input);

            // Show bot response
            ChatDisplay.AppendText("Bot: " + response + "\n\n");

            // Clear input
            UserInput.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cybersecurity_ChatBot_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatBot chat;
        private readonly ReminderParser _reminderParser = new ReminderParser();

        // Task creation flow state
        private bool _isAddingTask = false;
        private bool _awaitingTaskTitle = false;
        private bool _awaitingTaskDescription = false;
        private bool _awaitingTaskReminder = false;

        private bool _awaitingQuizResponse = false;

        private string _pendingTaskTitle = "";
        private string _pendingTaskDescription = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadAsciiArt();

            string connectionString = "server=localhost;port=3306;database=ChatbotDB;uid=root;pwd=;";
            TaskLibrary taskLibrary = new TaskLibrary(connectionString);

            chat = new ChatBot(taskLibrary);

            chat.PlayIntroLogic();
            AddBotMessage(chat.GetGreeting());
        }

        private void LoadAsciiArt()
        {
            string asciiArt = @" _______  _______           _______    _______           ______   _______  _______  _______  _______  _______           _______ __________________            ______   _______ _________  
(       )(  ___  )|\     /|(  ____ \  (  ____ \|\     /|(  ___ \ (  ____ \(  ____ )(  ____ \(  ____ \(  ____ \|\     /|(  ____ )\__   __/\__   __/|\     /|  (  ___ \ (  ___  )\__   __/  
| () () || (   ) || )   ( || (    \/  | (    \/( \   / )| (   ) )| (    \/| (    )|| (    \/| (    \/| (    \/| )   ( || (    )|   ) (      ) (   ( \   / )  | (   ) )| (   ) |   ) (     
| || || || |   | || |   | || (__      | |       \ (_) / | (__/ / | (__    | (____)|| (_____ | (__    | |      | |   | || (____)|   | |      | |    \ (_) /   | (__/ / | |   | |   | |     
| |(_)| || |   | || |   | ||  __)     | |        \   /  |  __ (  |  __)   |     __)(_____  )|  __)   | |      | |   | ||     __)   | |      | |     \   /    |  __ (  | |   | |   | |     
| |   | || | /\| || |   | || (        | |         ) (   | (  \ \ | (      | (\ (         ) || (      | |      | |   | || (\ (      | |      | |      ) (     | (  \ \ | |   | |   | |     
| )   ( || (_\ \ || (___) || (____/\  | (____/\   | |   | )___) )| (____/\| ) \ \__/\____) || (____/\| (____/\| (___) || ) \ \_____) (___   | |      | |     | )___) )| (___) |   | |     
|/     \|(____\/_)(_______)(_______/  (_______/   \_/   |/ \___/ (_______/|/   \__/\_______)(_______/(_______/(_______)|/   \__/\_______/   )_(      \_/     |/ \___/ (_______)   )_(     
";
            AsciiBox.Text = asciiArt;
        }

        private void OfferQuiz()
        {
            AddBotMessage("Would you like to test your cyber security knowledge by playing a short quiz? (Yes/No)");
            _awaitingQuizResponse = true;
        }

        private void AddBotMessage(string text)
        {
            Border bubble = new Border
            {
                Background = Brushes.ForestGreen,
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(15),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 400
            };

            TextBlock message = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = message;
            ChatPanel.Children.Add(bubble);
        }

        private void AddUserMessage(string text)
        {
            Border bubble = new Border
            {
                Background = Brushes.DodgerBlue,
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(15),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 400
            };

            TextBlock message = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = message;
            ChatPanel.Children.Add(bubble);
        }

        public void SendMessage()
        {
            string userMessage = UserInputTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
                return;

            AddUserMessage(userMessage);
            UserInputTextBox.Clear();

            string lowerInput = userMessage.ToLower();

            if (_awaitingQuizResponse)
            {
                if (lowerInput == "yes" || lowerInput == "y")
                {
                    _awaitingQuizResponse = true;

                    AddBotMessage("Great! Let's start the quiz.");

                    QuizWindow quiz = new QuizWindow();

                    this.Hide();

                    quiz.ShowDialog();

                    this.Show();

                    AddBotMessage("Welcome back! How else can I help you today?");

                    ChatScrollViewer.ScrollToEnd();
                    return;
                }

                if (lowerInput == "no" || lowerInput == "n")
                {
                    _awaitingQuizResponse = false;

                    AddBotMessage("No problem! What cyber security topic would you like to learn about?");

                    ChatScrollViewer.ScrollToEnd();
                    return;
                }
            }

            // ==========================
            // Show tasks
            // ==========================
            if (lowerInput == "show my tasks" || lowerInput == "show tasks")
            {
                if (string.IsNullOrWhiteSpace(chat.CurrentUserName))
                {
                    AddBotMessage("Please tell me your name first before viewing tasks.");
                    ChatScrollViewer.ScrollToEnd();
                    return;
                }

                string taskList = chat.ShowTasks(chat.CurrentUserName);
                AddBotMessage(taskList);
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            // ==========================
            // Start add task flow
            // ==========================
            if (lowerInput == "add task")
            {
                if (string.IsNullOrWhiteSpace(chat.CurrentUserName))
                {
                    AddBotMessage("Please tell me your name first before adding tasks.");
                    ChatScrollViewer.ScrollToEnd();
                    return;
                }

                _isAddingTask = true;
                _awaitingTaskTitle = true;
                _awaitingTaskDescription = false;
                _awaitingTaskReminder = false;

                _pendingTaskTitle = "";
                _pendingTaskDescription = "";

                AddBotMessage("Sure. What is the task title?");
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            // ==========================
            // Continue add task flow
            // ==========================
            if (_isAddingTask)
            {
                HandleTaskCreationFlow(userMessage);
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            // ==========================
            // Normal chatbot conversation
            // ==========================
            string response = chat.ProcessesInput(userMessage);
            AddBotMessage(response);
            ChatScrollViewer.ScrollToEnd();
        }

        private void HandleTaskCreationFlow(string userMessage)
        {
            if (_awaitingTaskTitle)
            {
                _pendingTaskTitle = userMessage;
                _awaitingTaskTitle = false;
                _awaitingTaskDescription = true;

                AddBotMessage("Got it. Now enter a description for the task.");
                return;
            }

            if (_awaitingTaskDescription)
            {
                _pendingTaskDescription = userMessage;
                _awaitingTaskDescription = false;
                _awaitingTaskReminder = true;

                AddBotMessage("When should I remind you?\nExamples:\n- tomorrow at 18:00\n- today at 7pm\n- in 3 days");
                return;
            }

            if (_awaitingTaskReminder)
            {
                ReminderParser.ReminderParseResult result = _reminderParser.ParseReminder(userMessage);

                if (!result.Success)
                {
                    AddBotMessage(result.DisplayMessage);
                    return;
                }

                string saveMessage = chat.AddTask(
                    chat.CurrentUserName,
                    _pendingTaskTitle,
                    _pendingTaskDescription,
                    result.ReminderDate
                );

                AddBotMessage(saveMessage + "\n" + result.DisplayMessage);

                // Reset flow
                _isAddingTask = false;
                _awaitingTaskTitle = false;
                _awaitingTaskDescription = false;
                _awaitingTaskReminder = false;
                _pendingTaskTitle = "";
                _pendingTaskDescription = "";
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }
    }
}
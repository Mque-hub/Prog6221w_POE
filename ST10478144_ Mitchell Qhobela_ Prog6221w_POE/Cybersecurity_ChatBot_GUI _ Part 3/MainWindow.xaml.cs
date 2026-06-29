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
        private readonly Reminder_Intent_NLP_Parser _intentParser= new Reminder_Intent_NLP_Parser();
        public ChatLogger Logger => _logger;
        private readonly ChatLogger _logger = new ChatLogger();

        // Task creation flow state
        private bool _isAddingTask = false;
        private bool _awaitingTaskTitle = false;
        private bool _awaitingTaskDescription = false;
        private bool _awaitingTaskReminder = false;

        private bool _awaitingQuizResponse = false;

        private string _pendingTaskTitle = "";
        private string _pendingTaskDescription = "";
        private bool _isUpdatingTask = false;
        private bool _awaitingUpdateChoice = false;
        private bool _awaitingNewValue = false;

        private int _taskToUpdate;
        private string _updateField = "";



        public MainWindow()
        {
            InitializeComponent();
            LoadAsciiArt();

            // Opening the Database on MySQL for interaction between Visual Studio and MySQL
            string connectionString = "server=localhost;port=3306;database=ChatbotDB;uid=root;pwd=Student@ST10478144;";
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

            ReminderIntentModel intent = _intentParser.Parse(userMessage);

           
            string lowerInput = userMessage.ToLower();

            if (intent.IsReminder)
            {
                // Store the extracted information
                _pendingTaskTitle = intent.Title;
                _pendingTaskDescription = intent.Description;

                // If no reminder date was found, ask for one
                if (string.IsNullOrWhiteSpace(intent.ReminderText))
                {
                    _isAddingTask = true;

                    _awaitingTaskTitle = false;
                    _awaitingTaskDescription = false;
                    _awaitingTaskReminder = true;

                    AddBotMessage(
                        "Sure! When should I remind you?\n\n" +
                        "Examples:\n" +
                        "- tomorrow\n" +
                        "- today at 7pm\n" +
                        "- next Monday\n" +
                        "- in 3 days");

                    ChatScrollViewer.ScrollToEnd();
                    return;
                }

                // A reminder date was already included
                ReminderParser.ReminderParseResult result =
                    _reminderParser.ParseReminder(intent.ReminderText);

                if (!result.Success)
                {
                    AddBotMessage(result.DisplayMessage);
                    return;
                }

                string message = chat.AddTask(
                    chat.CurrentUserName,
                    intent.Title,
                    intent.Description,
                    result.ReminderDate);

                AddBotMessage(message);

                ChatScrollViewer.ScrollToEnd();
                return;
            }

            if (_awaitingQuizResponse)
            {
                chat.Logger.Record($"{chat.CurrentUserName} started the Cyber Security Quiz.");
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

            
            //If condition to Show tasks
            
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

            
            // beginning a add task flow
           
            if (lowerInput.Contains("add a task")||
                 lowerInput.Contains("create a task") ||
                 lowerInput.Contains("create a new task")||
                 lowerInput.Contains("add a new task")||
                  lowerInput.Contains("setup a new task")||
                   lowerInput.Contains("setup a task"))
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

            // Continuing add task flow
            
            if (_isAddingTask)
            {
                HandleTaskCreationFlow(userMessage);
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            if (_isUpdatingTask)
            {
                HandleTaskUpdateFlow(userMessage);
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            if (lowerInput.StartsWith("delete task"))
            {
                string[] parts = lowerInput.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[2], out int taskId))
                {
                    AddBotMessage(chat.DeleteTask(taskId));
                }
                else
                {
                    AddBotMessage("Please enter the task ID. Example: delete task 3");
                }

                ChatScrollViewer.ScrollToEnd();
                return;
            }

            if (lowerInput.StartsWith("complete task"))
            {
                string[] parts = lowerInput.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[2], out int taskId))
                {
                    AddBotMessage(chat.CompleteTask(taskId));
                }
                else
                {
                    AddBotMessage("Please enter a valid task ID. Example: complete task 2");
                }

                ChatScrollViewer.ScrollToEnd();
                return;
            }

            if (lowerInput.StartsWith("update task"))
            {
                string[] parts = lowerInput.Split(' ');

                if (parts.Length == 3 && int.TryParse(parts[2], out int taskId))
                {
                    _isUpdatingTask = true;
                    _awaitingUpdateChoice = true;
                    _taskToUpdate = taskId;

                    AddBotMessage(@"What would you like to update?

                    1. Title
                    2. Description
                    3. Reminder
                     ");
                }
                else
                {
                    AddBotMessage("Example: update task 4, remember to use task IDs");
                }

                return;
            }

            if (lowerInput == "activity log" ||
                lowerInput == "show activity log")
            {
                AddBotMessage(chat.Logger.DisplayLog());
                ChatScrollViewer.ScrollToEnd();
                return;
            }

            
            // Normal chatbot conversation
            string response = chat.ProcessesInput(userMessage);
            AddBotMessage(response);
            if (response.Contains("Cyber Security Quiz"))
            {
                _awaitingQuizResponse = true;
            }
            ChatScrollViewer.ScrollToEnd();
        }

        // Method on maintaining user interaction whilst creating a task
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

        //// Method on maintaining user interaction whilst updating a task title, description, and reminder
        private void HandleTaskUpdateFlow(string userMessage)
        {
            if (_awaitingUpdateChoice)
            {
                _updateField = userMessage.ToLower();

                _awaitingUpdateChoice = false;
                _awaitingNewValue = true;

                switch (_updateField)
                {
                    case "1":
                    case "title":
                        AddBotMessage("Enter the new title:");
                        break;

                    case "2":
                    case "description":
                        AddBotMessage("Enter the new description:");
                        break;

                    case "3":
                    case "reminder":
                        AddBotMessage("Enter the new reminder date:");
                        break;

                    default:
                        AddBotMessage("Please choose 1-4.");
                        _awaitingUpdateChoice = true;
                        _awaitingNewValue = false;
                        break;
                }

                return;
            }

            if (_awaitingNewValue)
            {
                if (_updateField == "1" || _updateField == "title")
                {
                    AddBotMessage(chat.UpdateTitle(_taskToUpdate, userMessage));
                }
                else if (_updateField == "2" || _updateField == "description")
                {
                    AddBotMessage(chat.UpdateDescription(_taskToUpdate, userMessage));
                }

                else if (_updateField == "3" || _updateField == "reminder")
                    {
                        ReminderParser.ReminderParseResult result =
                            _reminderParser.ParseReminder(userMessage);

                        if (!result.Success)
                        {
                            AddBotMessage(result.DisplayMessage);
                            return;
                        }

                        AddBotMessage(chat.UpdateReminder(_taskToUpdate, result.ReminderDate));

                        _isUpdatingTask = false;
                        _awaitingUpdateChoice = false;
                        _awaitingNewValue = false;

                        _taskToUpdate = 0;
                        _updateField = "";

                        return;
                    }
            }
        }

        // chatbot Send button Method 
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        // Method for using enter to send a message
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }
    }
}
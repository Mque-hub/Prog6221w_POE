using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cybersecurity_ChatBot_GUI
{
    public class ChatBot
    {
        
            // Creating an object(instance) for the SoundPlayer() class that will play the greeting welcoming sound

            SoundPlayer player = new SoundPlayer("Greeting.wav");
            public void PlayIntroLogic()
            {

                // Calling the object "player". This object will play the greeting sound 
                player.Play();

                
            }

        // Creating constructor of external classes
        private KeywordResponder _keywords=new KeywordResponder();
        private SentimentDetector _sentiment = new SentimentDetector();
        private MemoryStore _memory=new MemoryStore();

        private ChatLogger _logger = new ChatLogger();

        public string CurrentUserName => _memory.UserName;

        // Declarations
        private bool _awaitingName = true;
        private string _lastTopic;

        // Creating a greeting and getting the users name
        public string GetGreeting()
        {
            return "Hello! Welcome to MQue Cybersecurity Bot.\n what's your name";
            
        }

        //Creating a processing class to process the input of the user and give responses based on the methods or atrributes of the external classes
        public string ProcessesInput( string input) 
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter something";

            string originalInput = input.Trim();
            string normalizedInput = originalInput.ToLower();

            Sentiment sentiment = _sentiment.Detect(normalizedInput);
            string emotionalReply = _sentiment.GetSentimentResponse(sentiment);

            if (_awaitingName)
            {
                _memory.UserName = originalInput;   // keep real formatting
                _awaitingName = false;

                return $"Nice to meet you {_memory.UserName}. How are you doing today and what can I do for you?";
            }

            bool topicDetected =_memory.DetectFavouriteTopic(normalizedInput);

            if (topicDetected)
            {
                _lastTopic = _memory.FavouriteTopic;

                return _memory.GetPersonalisedOpener();
            }

            if (normalizedInput.Contains("tell me more") ||
                normalizedInput.Contains("explain more") ||
                normalizedInput.Contains("more info") ||
                normalizedInput.Contains("continue"))
            {

                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return _keywords.GetFollowUpResponse(_lastTopic);
                }
            }

            if (normalizedInput.Contains("how are you")|| normalizedInput.Contains("what is your purpose"))
                {
                return $"Sorry {_memory.UserName} I am only a chatbot, unfortunately I don't have feelings";
                }

            if (normalizedInput.Contains("what can i ask you") || normalizedInput.Contains("topics"))
            {
                return "You can ask me about topics such as:\n- " + string.Join("\n- ", _keywords.GetAllKeywords());
            }

            if (normalizedInput.Contains("cybersecurity"))
            {
                _lastTopic = "cybersecurity";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("cybersecurity");
            }

            if (normalizedInput.Contains("phishing"))
            {
                _lastTopic = "phishing";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("phishing");
            }

            if (normalizedInput.Contains("password"))
            {
                _lastTopic = "password";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("password");
            }

            if (normalizedInput.Contains("scam"))
            {
                _lastTopic = "scam";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("scam");
            }

            if (normalizedInput.Contains("privacy"))
            {
                _lastTopic = "privacy";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("privacy");
            }

            if (normalizedInput.Contains("identity"))
            {
                _lastTopic = "identity";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("identity");
            }

            return $"What can I help you with {_memory.UserName} \n would you like to know about cybersecurity, or passwords, or phishing? \n \n Or would you like to play a Cyber Security Quiz? (Yes/No)";
        }

        private readonly TaskLibrary _taskLibrary;

        public ChatBot(TaskLibrary taskLibrary)
        {
            _taskLibrary = taskLibrary;
        }
        public string AddTask(string username, string title, string description, DateTime reminderDate)
        {
            TaskItem task = new TaskItem
            {
                TaskTitle = title,
                Description = description,
                ReminderDate = reminderDate,
                Username = username
            };

            _taskLibrary.AddTask(task);

            _logger.Record($"{username} added task '{title}'.");

            return $"Task saved.\n" +
                   $"Task: {task.TaskTitle}\n" +
                   $"Description: {task.Description}\n" +
                   $"Reminder: {task.ReminderDate:dd MMM yyyy HH:mm}";
        }

        public string DeleteTask(int taskId)
        {
            _taskLibrary.DeleteTask(taskId);
            return "Task deleted successfully.";
        }

        public string CompleteTask(int taskId)
        {
            _taskLibrary.CompleteTask(taskId);
            return $"Task {taskId} marked as completed.";
        }

        public string UpdateTitle(int taskId, string newTitle)
        {
            _taskLibrary.UpdateTitle(taskId, newTitle);

            return "Task title updated successfully.";
        }

        public string UpdateDescription(int taskId, string newDescription)
        {
            _taskLibrary.UpdateDescription(taskId, newDescription);

            return "Task description updated successfully.";
        }

        public string UpdateReminder(int taskId, DateTime newReminder)
        {
            _taskLibrary.UpdateReminder(taskId, newReminder);

            return $"Reminder for task {taskId} updated successfully.";
        }

        public string ShowTasks(string username)
        {
            _logger.Record($"{username} viewed their task list.");

            List<TaskItem> tasks = _taskLibrary.GetTasksByUser(username);

            if (tasks.Count == 0)
                return "You have no tasks saved.";



            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Your Tasks:");
            sb.AppendLine("Use the task ID to make any changes (e.g. 'complete task 4' or 'delete task 4').");
            sb.AppendLine();

            int displayNumber = 1;

            foreach (TaskItem task in tasks)
            {
                sb.AppendLine($"{displayNumber}. {task.TaskTitle}");
                sb.AppendLine($"   Task ID: {task.Id}");
                sb.AppendLine($"   Due: {task.ReminderDate:g}");
                sb.AppendLine($"   Status: {(task.IsCompleted ? "Completed " : "Pending")}");
                sb.AppendLine();

                displayNumber++;
            }

            return sb.ToString();
        }
    }

}
    




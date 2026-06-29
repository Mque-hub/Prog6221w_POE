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
        public ChatLogger Logger => _logger;
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

            _logger.Record($"{_memory.UserName} asked: \"{input}\"");

            if (_awaitingName)
            {
                _memory.UserName = originalInput;   // To keep real name format without converting it completely to lower cases
                _awaitingName = false;

                return $"Nice to meet you {_memory.UserName}. How are you doing today and what can I do for you?";
            }

            bool topicDetected =_memory.DetectFavouriteTopic(normalizedInput);

            if (topicDetected)
            {
                _lastTopic = _memory.FavouriteTopic;

                return _memory.GetPersonalisedOpener();
            }

            // IF conditions of user input regarding cyber security 
            if (normalizedInput.Contains("tell me more") ||
                normalizedInput.Contains("explain more") ||
                normalizedInput.Contains("more info") ||
                normalizedInput.Contains("continue"))
            {

                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return _keywords.GetFollowUpResponse(_lastTopic) +

               $"\n\nWhat else would you like to learn about, {_memory.UserName}?" +
               "\nYou can also ask about passwords, phishing, privacy or identity theft." +
           "\n\n Or would you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
                }
            }

            if (normalizedInput.Contains("how are you")|| normalizedInput.Contains("what is your purpose"))
                {
                return $"Sorry {_memory.UserName} I am only a chatbot, unfortunately I don't have feelings";
                }

            if (normalizedInput.Contains("what can i ask you") || normalizedInput.Contains("topics"))
            {
                return "Ask me about topics such as:\n- " + string.Join("\n- ", _keywords.GetAllKeywords());
            }

            if (normalizedInput.Contains("cybersecurity"))
            {
                _lastTopic = "cybersecurity";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("cybersecurity") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            if (normalizedInput.Contains("phishing"))
            {
                _lastTopic = "phishing";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("phishing") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            if (normalizedInput.Contains("password"))
            {
                _lastTopic = "password";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("password") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            if (normalizedInput.Contains("scam"))
            {
                _lastTopic = "scam";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("scam") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            if (normalizedInput.Contains("privacy"))
            {
                _lastTopic = "privacy";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("privacy") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            if (normalizedInput.Contains("identity"))
            {
                _lastTopic = "identity";

                return emotionalReply + "\n\n" +
           _keywords.ChatbotResponds("identity") +
           "\n\nWould you like to test your knowledge with a Cyber Security Quiz? (Yes/No)"; 
            }

            return $"What else would you like to learn about, {_memory.UserName}?";
        }

        // Calling the Task Library class
        private readonly TaskLibrary _taskLibrary;
        
        // Secondary ChatBot constructor 
        public ChatBot(TaskLibrary taskLibrary)
        {
            _taskLibrary = taskLibrary;
        }

        // Method to Adding tasks to the database
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

        // Method for deleting the task
        public string DeleteTask(int taskId)
        {
            _taskLibrary.DeleteTask(taskId);
            _logger.Record($"{CurrentUserName} deleted task {taskId}");
            return "Task deleted successfully.";
        }

        // Method for indicating if the task is completed
        public string CompleteTask(int taskId)
        {
            _taskLibrary.CompleteTask(taskId);
            _logger.Record($"{CurrentUserName} completed task {taskId}");
            return $"Task {taskId} marked as completed.";
        }

        // Method for updating the task title
        public string UpdateTitle(int taskId, string newTitle)
        {
            _taskLibrary.UpdateTitle(taskId, newTitle);
            _logger.Record($"{CurrentUserName} updated the title of task {taskId}");
            return "Task title updated successfully.";
        }

        // Method for updating the task description
        public string UpdateDescription(int taskId, string newDescription)
        {
            _taskLibrary.UpdateDescription(taskId, newDescription);
            _logger.Record($"{CurrentUserName} updated the description of task {taskId}");
            return "Task description updated successfully.";
        }

        // Method for updating the task reminder
        public string UpdateReminder(int taskId, DateTime newReminder)
        {
            _taskLibrary.UpdateReminder(taskId, newReminder);
            _logger.Record($"{CurrentUserName} updated the reminder of task {taskId}");
            return $"Reminder for task {taskId} updated successfully.";
        }

        // Method for displaying the tasks
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
    




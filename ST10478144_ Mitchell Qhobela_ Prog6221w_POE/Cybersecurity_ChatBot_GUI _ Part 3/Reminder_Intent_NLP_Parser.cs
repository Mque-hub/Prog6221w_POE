using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class Reminder_Intent_NLP_Parser
    {
        public ReminderIntentModel Parse(string input)
        {
            ReminderIntentModel result = new ReminderIntentModel();

            string text = input.Trim().ToLower();
            
            //Condition to recognize the keywords for adding tasks  

            if (!(text.Contains("remind") ||
                  text.Contains("remember to")))
                  
            {
                result.IsReminder = false;
                return result;
            }

            result.IsReminder = true;

            // Removing the command phrases
            text = text.Replace("remind me to", "");
            text = text.Replace("remember to", "");
            

            // Finding reminder words
            List<string> reminderWords = new List<string>
                {
                    "today",
                    "tomorrow",
                    "next week",
                    "next month",
                    "in 2 days",
                    "in 3 days",
                    "next monday",
                    "next tuesday",
                    "next wednesday",
                    "next thursday",
                    "next friday",
                    "next saturday",
                    "next sunday"
                };

            foreach (string word in reminderWords)
            {
                if (text.Contains(word))
                {
                    result.ReminderText = word;

                    text = text.Replace(word, "").Trim();

                    break;
                }
            }

            // Description
            result.Description = text;

            // Generating a title automatically
            result.Title = GenerateTitle(text);

            return result;
        }

        // Method for generating a title
        private string GenerateTitle(string description)
        {
            string[] words = description.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return "Reminder";

            // Taking the first 3 words
            int count = Math.Min(3, words.Length);

            string title = string.Join(" ", words.Take(count));

            // Capitalizing each word
            title = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(title);

            return title;
        }
    }
}

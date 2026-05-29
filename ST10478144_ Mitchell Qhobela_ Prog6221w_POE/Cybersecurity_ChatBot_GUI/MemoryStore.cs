using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class MemoryStore
    {
        // Creating a name getter and setter 
        public string UserName
        { get; set; }

        // creating a favourite topic getter and setter
        public string FavouriteTopic { get; set; }

        // creating a memory storage to remember the user's name and favourite topic
        private Dictionary<string, string> _memory =
        new Dictionary<string, string>();

        // creating a storing method 
        public void Store(string key, string value)
        {
            _memory[key] = value;
        }

        // creating a recalling method 
        public string Recall(string key)
        {
            if (_memory.ContainsKey(key))
            {
                return _memory[key];
            }

            return null;
        }

        // creating a favourite topic detector using key phrases/words
        public bool DetectFavouriteTopic(string input)
        {
            input = input.ToLower();

            List<string> triggers = new List<string>()
    {
        "i love",
        "i enjoy",
        "i'm interested in",
        "i have an interest in",
        "i like"
    };

            foreach (string trigger in triggers)
            {
                if (input.Contains(trigger))
                {
                    int start =
                        input.IndexOf(trigger) + trigger.Length;

                    string topic =
                        input.Substring(start).Trim();

                    FavouriteTopic = topic;

                    Store("topic", topic);

                    return true;
                }
            }

            return false;
        }

        // Creating a method that recalls that the user mentioned the topic and gives a leading respose based on that topic
        public string GetPersonalisedOpener()
        {
            string topic = Recall("topic");

            if (!string.IsNullOrEmpty(topic))
            {
                return $"For a person who is interested in {topic},data security is important and I understand you. Do you want me to tell you more or do you want you want me to show you the topics";
            }

            return "Tell me more about your interests.";
        }

    }
}

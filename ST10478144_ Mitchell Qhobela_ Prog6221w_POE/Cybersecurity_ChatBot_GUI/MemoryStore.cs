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
        public string UserName
        { get; set; }
        public string FavouriteTopic { get; set; }

        private Dictionary<string, string> _memory =
        new Dictionary<string, string>();

        public void Store(string key, string value)
        {
            _memory[key] = value;
        }

        public string Recall(string key)
        {
            if (_memory.ContainsKey(key))
            {
                return _memory[key];
            }

            return null;
        }

        public void DetectFavouriteTopic(string input)
        {
            input = input.ToLower();

            List<string> triggers = new List<string>()
            {
                "i love",
                "i enjoy",
                "i'm interested in",
                "I have an interest in",
                "i like"
            };

                foreach (string trigger in triggers)
                {
                    if (input.Contains(trigger))
                    {
                        int start = input.IndexOf(trigger) + trigger.Length;

                        string topic = input.Substring(start).Trim();

                        FavouriteTopic = topic;

                        Store("topic", topic);

                        break;
                    }
                }
        }

        public string GetPersonalisedOpener()
        {
            string topic = Recall("topic");

            if (!string.IsNullOrEmpty(topic))
            {
                return $"Because you mentioned your love for {topic}, a person who loves {topic} usually enjoys creative activities.";
            }

            return "Tell me more about your interests.";
        }

    }
}

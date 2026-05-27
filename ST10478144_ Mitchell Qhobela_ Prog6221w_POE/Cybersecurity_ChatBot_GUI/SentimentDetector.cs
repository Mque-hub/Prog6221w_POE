using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cybersecurity_ChatBot_GUI
{

    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Happy
    }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, List<string>> _triggers =
            new Dictionary<Sentiment, List<string>>()
            {
            {
                Sentiment.Happy,
                new List<string>()
                {
                    "happy",
                    "great",
                    "awesome",
                    "thank you",
                    "thanks",
                    "Helpful"
                }
            }
            };

        public Sentiment Detect(string input)
        {
            input = input.ToLower();

            foreach (var pair in _triggers)
            {
                foreach (string word in pair.Value)
                {
                    if (input.Contains(word))
                    {
                        return pair.Key;
                    }
                }
            }

            return Sentiment.Neutral;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cybersecurity_ChatBot_GUI
{
    // Creating a sentiment enum for the different sentiment categories
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Happy
    }

    // Creating a sentiment detector keywords using List<> so that all categories are stored in a dictionary
    
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
                },
                {
                       Sentiment.Worried,
                     new List<string>()
                    {
                        "worried",
                        "scared",
                        "afraid",
                        "anxious",
                        "nervous",
                        "unsafe"
                    }
                },
                {
                       Sentiment.Curious,
                     new List<string>()
                    {
                        "curious",
                        "wondering",
                        "interested",
                        "want to know",
                        "how does",
                        "how do I"
                    }
                },
                {
                       Sentiment.Frustrated,
                     new List<string>()
                    {
                        "frustrated",
                        "annoyed",
                        "confused",
                        "don't understand",
                        "what do you mean",
                        
                    }
                }
            };

        //Creating the sentiment detector that looks through the dictionary
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
        // Once the keywords are identified, the following is the responses to each sentiment keyword, including neutral 
        public string GetSentimentResponse(Sentiment sentiment)
        {
            switch (sentiment)
            {
                case Sentiment.Happy:
                    return "I'm glad you're feeling happy!";

                case Sentiment.Worried:
                    return "I can tell you are worried, don't worry you'll be sorted after this conversation.";

                case Sentiment.Curious:
                    return "I see that you are very interested in this topic!";

                case Sentiment.Frustrated:
                    return "I can tell this frastrates you, let's try a different approach";

                default:
                    return "Thanks for sharing.";
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class KeywordResponder
    {
        private Dictionary<string, List<string>> _responses =
    new Dictionary<string, List<string>>()
    {
        { "cybersecurity", new List<string>()

            {
                "Would you like to learn about is cybersecurity? ",
                "In today's society, cybersecurity is important"
            }

        }

    };
        private Random _random = new Random();

        public void ChatbotResponds(string keyword)
        {
            if (_responses.ContainsKey(keyword))
            {
                List<string> responses = _responses[keyword];
                int index = _random.Next(responses.Count);
                Console.WriteLine(responses[index]);
            }

            else 
            {
                Console.WriteLine("we are currently updating our system");
            
            }
        }
    }
}

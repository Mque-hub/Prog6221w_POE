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
        private KeywordResponder _keywords=new KeywordResponder();
        private SentimentDetector _sentiment = new SentimentDetector();
        private MemoryStore _memory=new MemoryStore();
        private bool _awaitingName = true;
        public string GetGreeting()
        {
            return "Hello! Welcome to MQue Cybersecurity Bot.\n what's your name";
            
        }

        public string ProcessesInput( string input) 
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter something";

            input = input.Trim().ToLower();

            if (_awaitingName) 
            {
                _memory.UserName = input;
                _awaitingName= false;

                return $"Nice to meet you {_memory.UserName}. How are you doing today and what can I do for you?";
            }
            

            if (input.Contains("how are you"))
                {
                return $"Sorry {_memory.UserName} I am only a chatbot, unfortunately I don't have feelings";
                }

            if (input.Contains("what can i ask you"))
            {
                return "You can ask my about topics such as:\n- " + string.Join("\n- ", _keywords.GetAllKeywords());
            }

            if (input.Contains("what is cybersecurity"))
            {
                return _keywords.ChatbotResponds("cybersecurity");
            }

            return "Please repeat that";
        }



    }
    }




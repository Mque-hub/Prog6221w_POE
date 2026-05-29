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

        private bool _awaitingName = true;
        public string GetGreeting()
        {
            return "Hello! Welcome to MQue Cybersecurity Bot.\n what's your name";
            
        }



    }
    }




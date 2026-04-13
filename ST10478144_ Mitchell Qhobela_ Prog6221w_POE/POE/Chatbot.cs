using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;


namespace POE
{
     public class Chatbot
    {
        // Creating an object(instance) for the SoundPlayer() class that will play the greeting welcoming sound

        SoundPlayer player = new SoundPlayer("Greeting.wav");
        public void PlayIntroLogic() 
        {
            
        // Calling the object "player". This object will play the greeting sound 
            player.Play();

        // ASCII art work for the cybersecurity bot
            Console.WriteLine(@"  _______  _______           _______                                                                                  
                                    (       )(  ___  )|\     /|(  ____ \                                                                                 
                                    | () () || (   ) || )   ( || (    \/                                                                                 
                                    | || || || |   | || |   | || (__                                                                                     
                                    | |(_)| || |   | || |   | ||  __)                                                                                    
                                    | |   | || | /\| || |   | || (                                                                                       
                                    | )   ( || (_\ \ || (___) || (____/\                                                                                 
                                    |/     \|(____\/_)(_______)(_______/                                                                                 
                                                                                                                     
 _______           ______   _______  _______  _______  _______  _______           _______ __________________         
(  ____ \|\     /|(  ___ \ (  ____ \(  ____ )(  ____ \(  ____ \(  ____ \|\     /|(  ____ )\__   __/\__   __/|\     /|
| (    \/( \   / )| (   ) )| (    \/| (    )|| (    \/| (    \/| (    \/| )   ( || (    )|   ) (      ) (   ( \   / )
| |       \ (_) / | (__/ / | (__    | (____)|| (_____ | (__    | |      | |   | || (____)|   | |      | |    \ (_) / 
| |        \   /  |  __ (  |  __)   |     __)(_____  )|  __)   | |      | |   | ||     __)   | |      | |     \   /  
| |         ) (   | (  \ \ | (      | (\ (         ) || (      | |      | |   | || (\ (      | |      | |      ) (   
| (____/\   | |   | )___) )| (____/\| ) \ \__/\____) || (____/\| (____/\| (___) || ) \ \_____) (___   | |      | |   
(_______/   \_/   |/ \___/ (_______/|/   \__/\_______)(_______/(_______/(_______)|/   \__/\_______/   )_(      \_/   
                                                                                                                     
                                           ______   _______ _________                                                                                        
                                          (  ___ \ (  ___  )\__   __/                                                                                        
                                          | (   ) )| (   ) |   ) (                                                                                           
                                          | (__/ / | |   | |   | |                                                                                           
                                          |  __ (  | |   | |   | |                                                                                           
                                          | (  \ \ | |   | |   | |                                                                                           
                                          | )___) )| (___) |   | |                                                                                           
                                          |/ \___/ (_______)   )_(                                                                                           
                                                                                                                     ");
            // Cybersecurity Bot title that will display after the ASCII artwork

            Console.WriteLine("==============================================================================================");
            Console.WriteLine("                             MQue Cybersecurity Bot                                           ");
            Console.WriteLine("==============================================================================================");


        }

        

    }
}

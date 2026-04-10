using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    public  class User
    {
        public void UserInfo() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hi There! Welcome to Mque Cybersecurity Bot! I will be assisting you today.");
            Console.WriteLine("What's your name? ");

            Console.ForegroundColor = ConsoleColor.Blue;
            string name= Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hi " + name + "! Nice to meet you.");

            string input = Console.ReadLine().ToLower();

            if (input == "how are you") 
            {
                Console.WriteLine("I'm only just a bot. Unfortunetly I don't have feelings.");
                Console.WriteLine("How can I help! ");
            }
            else if ( input == "what is cybersecurity")
                    {
                        Console.WriteLine("Cybersecurity is the practice of defending computers, servers, mobile devices, electronic systems, networks, and data from malicious attacks.");
                    }

        }
    }
}

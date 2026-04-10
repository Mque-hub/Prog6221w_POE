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

            else if (input == "what is your purpose")
            {
                Console.WriteLine("My purpose is to give you a better understanding of cybersecurity. How can I help you today?");
            }

            else if (input == "what can I ask you about") 
            {
                Console.WriteLine("Ask me what is cybersecurity or types of cybersecurity attacks");
            }

            else if (input == "what is cybersecurity")
            {
                Console.WriteLine("Cybersecurity is the practice of defending computers, servers, mobile devices, electronic systems, networks, and data from dangerous malicious attacks.");
            }

            else if (input.Contains("types of cybersecurity attacks"))
            {
                Console.WriteLine(" 1. malware - a software that a cybercriminal or hacker has created to disrupt or damage a legitimate your computer" +
                    "\n 2. Phishing - when a cybercriminal targets victims with emails that appear to be from a legitimate company asking for sensitive information."
                    + "\n 3. A man-in-the-middle attack - when a cybercriminal intercepts communication between two individuals in order to steal data" +
                    "\n 4. A denial-of-service attack - when a cybercriminal prevents a computer system from executing legitimate requests by overwhelming the networks and servers with traffic." +
                    "\n 5. Identity threats - involves malicious attempts to steal or misuse personal or organizational identities that allow the attacker to retrieve sensitive information or move laterally within the network.");
            }
            else if (input.Contains("malware"))
            {
                Console.WriteLine("To prevent a cybercriminal from injecting malware in your machines, use Anti-Virus softwares such as: " +
                    "\n -Kaspersky Antivirus Protection \n - Avast Antivirus Protection \n - Norton Antivirus Protection \n - McAfee Antivirus Protection \n - Bitdefender Antivirus Protection");
            }

            else if (input.Contains("phising"))
            {
                Console.WriteLine("Do not open emails and / or attachments from unknown senders. Do not click on links from emails that come from unknown senders or links to unfimilar websites");
            }

            else if (input.Contains("man-in-the-middle attack"))
            {
                Console.WriteLine("Avoid using unsafe WiFi networks in public environments");
            }

            else if (input.Contains("A denial-of-service attack"))
            {
                Console.WriteLine("Have a web application firewall (WAF) which will assist in blocking attacks by enabling customizable policies to filter, scan, and block malicious HTTP traffic between web applications and the internet. ");
            }

            else if (input.Contains("Identity threats"))
            {
                Console.WriteLine("Secure your accounts and use strong and complex passwords. Keep any identity documents such as IDs or passports in a safe place. Avoid phishing");
            }
        }
    }
}

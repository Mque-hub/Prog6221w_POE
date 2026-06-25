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
            //Foreach loop will be used throughout the logic for text animation. This brings a typing effect. 
           
            // Welcoming Message 
            foreach (char c in "\n Hi There! Welcome to Mque Cybersecurity Bot! I'm MQue and I will be assisting you today.\n ")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c);
                Thread.Sleep(10);
            }

            // Prompt the user to enter their name
            foreach (char c in "\n What's your name? ")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c);
                Thread.Sleep(50);
            }
            


            Console.ForegroundColor = ConsoleColor.Blue;
            string name= Console.ReadLine();

            // this "...." statement in the foreach loop gives a thinking effect before answering
            foreach (char c in " ......... \n")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c);
                Thread.Sleep(120);
            }

            // Response has the name included. Friendly response for a welcome feeling
            foreach (char c in "\n MQue: Hi " + name + "! Nice to meet you.\n") 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c);
                Thread.Sleep(20);
            }

            string response;

            // Do-while loop is used with the control loop variable being a boolean expression of yes or no (Y/N)

            do
            {
                foreach (char c in "\n MQue: What can I help you with? ")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c);
                    Thread.Sleep(50);
                }


                
                Console.ForegroundColor = ConsoleColor.Blue;
                string input = Console.ReadLine().ToLower();
                Console.ResetColor();
                Console.WriteLine("--------------------------------------------------------------------------------------\n ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine( name + ": " + input + "\n");

                // The following logic is made up of a variant of if else conditions accomodating certain types of possible questions the user might ask regarding cybersecurity

                // Should the input be null a message will display to notify the user of an empty response
                if (string.IsNullOrWhiteSpace(input))
                {
                    foreach (char c in "MQue: Please enter something")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    

                }

                // The following else if conditions will consist of Contains() class in case the user might type additional words and the logic should pick it up and give a valid response

                else if (input.Contains( "how are you"))
                {
                    foreach (char c in "MQue: I'm only just a bot. Unfortunetly I don't have feelings.")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Thread.Sleep(30);
                    }

                }

                else if (input.Contains("what is your purpose"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: My purpose is to give you a better understanding of cybersecurity. Ask me what is cybersecurity?");
                }

                else if (input.Contains ( "what can I ask you"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: You can ask me: \n -what is cybersecurity \n -types of cybersecurity attacks \n -difference between cybersecurity attack and cybersecurity threat");
                    
                }


                else if (input.Contains("what is cybersecurity"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Cybersecurity is the practice of defending computers, servers, mobile devices, electronic systems, networks, and data from dangerous malicious attacks.");
                }

                else if (input.Contains("types of cybersecurity attacks") || input.Contains("types of cybersecurity threats"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: 1. malware - a software that a cybercriminal or hacker has created to disrupt or damage a legitimate your computer" +
                        "\n 2. Phishing - when a cybercriminal targets victims with emails that appear to be from a legitimate company asking for sensitive information."
                        + "\n 3. A man-in-the-middle attack - when a cybercriminal intercepts communication between two individuals in order to steal data" +
                        "\n 4. A denial-of-service attack - when a cybercriminal prevents a computer system from executing legitimate requests by overwhelming the networks and servers with traffic." +
                        "\n 5. Identity threats - involves malicious attempts to steal or misuse personal or organizational identities that allow the attacker to retrieve sensitive information or move laterally within the network.");
                }


                else if (input.Contains("malware") || input.Contains("antivirus") || input.Contains("virus")|| input.Contains("viruses"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: To prevent a cybercriminal from injecting malware in your machines, use Anti-Virus softwares such as: " +
                        "\n -Kaspersky Antivirus Protection \n - Avast Antivirus Protection \n - Norton Antivirus Protection \n - McAfee Antivirus Protection \n - Bitdefender Antivirus Protection");
                }

                else if (input.Contains("phishing"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Do not open emails and / or attachments from unknown senders. Do not click on links from emails that come from unknown senders or links to unfimilar websites");
                }

                else if (input.Contains("man-in-the-middle attack") || input.Contains("mitm"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Avoid using unsafe WiFi networks in public environments");
                }

                else if (input.Contains("a denial-of-service attack"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Have a web application firewall (WAF) which will assist in blocking attacks by enabling customizable policies to filter, scan, and block malicious HTTP traffic between web applications and the internet. ");
                }

                else if (input.Contains("identity threats"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Secure your accounts and use strong and complex passwords. Keep any identity documents such as IDs or passports in a safe place. Avoid phishing");
                }

                else if (input.Contains("password")) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: To always ensure that your password is strong it must contain at least: " +
                        "\n - 1 number" +
                        "\n - 1 Capital letter" +
                        "\n - 1 Special character" +
                        "\n - must not be a simple name or your birthdate ");
                }


                else if (input.Contains("What is a firewall") || input.Contains("firewall"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: A firewall controls the incoming and outgoing network traffick by sitting between a computer, i.e., your local computer and another network such as the internet. In other words it is a traffic officer that controls what goes in your local machine.");
                }


                else if (input.Contains("difference between cybersecurity attack and cybersecurity threat") || input.Contains(" difference between attack and threats"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" MQue: Cybersecurity attack is when your computer system firewall has been hacked and you become effected by it because you have lost access or your private data has been leaked");
                    Console.WriteLine(" \n Cybersecurity threat is when there is chance of a malicious attemt to damage or disrupt your computer system or network. ");
                }

                else if (input.Contains("ransomware "))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Ransomeware is a type of malware designed to hold something such as sensitive data to randsom. A randsome attacker may block access to your computer system entirely.  ");
                }

                else if (input.Contains("saftey") || input.Contains("safe") || input.Contains("protect"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Keeping your computer system safe from cybersecurity attacks or cybersecurity threats is called Cyber Hygiene");
                    Console.WriteLine("\n Best Cyber Hyigene Practises:" +
                        "\n 1. Use strong and complex passwords " +
                        "\n 2. Use a Two-Factor Authentication (2FA)" +
                        "\n 3. Update your software regularly" +
                        "\n 4. Avoid phising" +
                        "\n 5. Backup crucial Data" +
                        "\n 6. Use antivirus softwares" +
                        "\n 7. Avoid oversharing paersonal data" +
                        "\n 8. Always monitor your accounts and devices " +
                        "\n 9. Stay away from unsecure public wifis" +
                        "\n 10. Invest in learning more about cybersecurity ");
                }

                else if (input.Contains("two-factor authentication") || input.Contains("2fa")) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: A Two-Factor Authentication (2FA) is a security procedure that adds another layer of identity verificaion.");
                    Console.WriteLine("MQue: This inculdes: " +
                        "\n - A pin, password or passphrase" +
                        "\n - Boimetrics identifiers such as fingerprints, facial recognition or voice match" +
                        "\n - A security token, passkey, or smart card  ");
                    Console.WriteLine("Ask me the types of two-factor authentication");
                    
                }

                else if (input.Contains("types of two-factor authentication") || input.Contains("types of 2fa") || input.Contains("two-factor authentication methods") || input.Contains("2fa methods")) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MQue: Two-Factor Authentication (2FA) " +
                        "\n - SMS code: A sms will be sent to your smart phone device contain a pin code in it" +
                        "\n - Authentication app messages: A prompt from the authenticator app will appear in the notifications for you to 'Approve' or 'Deny' access to your accounts or devices" +
                        "\n - Tockens: You will recieve time-based tokens that generate within the Authenticator app frequently" +
                        "\n - Voice calls: An automated system will call a user and deliver the pin code by voice" +
                        "\n - Biometrics factors: This option is common on mobile phones. This is when a fingerprint, facial recognition or voice match is required for authentication verification");
                }

                else
                {

                    foreach (char c in " ........ \n")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Thread.Sleep(120);
                    }

                    foreach (char c in "MQue: Sorry!! We are currently in maintainance. I will answer that question in our upcoming update.")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        Thread.Sleep(20);
                    }

                }

                // Prompt the user if they want to continue or not
                foreach (char c in " \n MQue: Anything else I can help with. (Y/N)")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c);
                    Thread.Sleep(30);
                }
                   
                Console.ForegroundColor = ConsoleColor.Blue;
                response = Console.ReadLine().ToLower();
            } while (response != "n");

            // Closing message to notify the user that the application has come to the end
            Console.ResetColor();
            Console.WriteLine("==============================================================================================");
            Console.WriteLine("                         Goodbye "+ name+ " ! See you next time");
            Console.WriteLine("==============================================================================================");
        }
    }
}

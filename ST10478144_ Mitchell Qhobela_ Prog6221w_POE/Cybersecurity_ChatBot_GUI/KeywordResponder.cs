using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    // Setting up key topics in cybersecurity and storing them in a dictionary list<>
    public class KeywordResponder
    {
        private Dictionary<string, List<string>> _responses =
    new Dictionary<string, List<string>>()
        {
        { "cybersecurity", new List<string>()

            {
                "Secure your digital life by enabling Multi-Factor Authentication (MFA) on all accounts, using long and unique passphrases via a dedicated password manager, and promptly installing all software updates. Additionally, never click unexpected links in emails, and avoid sensitive browsing over unencrypted public Wi-Fi without a VPN ",
                "In today's society, cybersecurity is important and here's why? \n Cybersecurity is the practice of defending computers, servers, mobile devices, electronic systems, networks, and data from dangerous malicious attacks."
            }

        },
        { "phishing", new List<string>()

            {
                "Phishing is a deceptive social engineering attack where cybercriminals impersonate trusted entities—such as banks, government agencies, or tech companies—to trick victims into revealing sensitive information, such as login credentials, credit card numbers, or personal data ",
                "Do not open emails and / or attachments from unknown senders. Do not click on links from emails that come from unknown senders or links to unfimilar websites. This is what makes you vulnerable to cybercriminals unaware."
            }

        },
        { "man-in-the-middle attack", new List<string>()

            {
                "A man-in-the-middle (MitM) attack is a form of cyberattack in which criminals exploiting weak web-based protocols insert themselves between entities in a communication channel to steal data ",
                "Avoid using unsafe WiFi networks in public environments, cybercriminals exploit exploiting weak web-based protocols by inserting themselves between entities in a communication channel to steal your data"
            }

        },
        { "password", new List<string>()

            {
                "Having a strong password will protect you devices from cybercriminals and also any pesons who has access to you physical device location",
                " To always ensure that your password is strong it must contain at least: " +
                        "\n - 1 number" +
                        "\n - 1 Capital letter" +
                        "\n - 1 Special character" +
                        "\n - must not be a simple name or your birthdate"
            }

        },
        { "scam", new List<string>()

            {
                " Protect your computer by using security software. Set the software to update automatically so it will deal with any new security threats",
                " Protect your accounts by using multi-factor authentication. Some accounts offer extra security by requiring two or more credentials to log in to your account, such requiring a sms one time pin, or auto generated codes",
                " Protect your data by backing it up. Back up the data on your computer to an external hard drive or in the cloud. Back up the data on your phone, too. By having back up you will be able to retrive and regain control of your data when you have been scammed"
            }

        },
        { "malware", new List<string>()

            {
                " To prevent a cybercriminal from injecting malware in your machines, use Anti-Virus softwares such as:\n -Kaspersky Antivirus Protection \n - Avast Antivirus Protection \n - Norton Antivirus Protection \n - McAfee Antivirus Protection \n - Bitdefender Antivirus Protection",
                
            }

        },
        { "identity", new List<string>()

            {
                " Secure your accounts and use strong and complex passwords. Keep any identity documents such as IDs or passports in a safe place. Avoid phishing",
                "Restrict your privacy settings on your social media accounts – the more private, the better. Try not to share your personal details online, such as your date of birth, address, and contact details. Remember, this information can be used by fraudsters to piece together your identity.\r\n\r\n"
            }

        },

        { "privacy", new List<string>()

            {
                "Privacy in cybersecurity refers to the fundamental right of individuals to control how their personal data—such as financial records, medical history, or online behavior—is collected, stored, and shared",
                "Focus on securing the most sensitive accounts (email, banking) first with unique passwords, turn off unneeded location or app permissions, and use end-to-end encrypted messaging like Signal or ProtonMail"
            }

        }



    };

        // Suggecting topics for a user that cover what is included in the dictionary
        private static Dictionary<string, List<string>> _topics =
        new Dictionary<string, List<string>>() 
        {
            { "Passwords", new List<string>{ "password", "strong password", } },
            { "Phishing", new List<string>{ "anti-virus", "virus", "protection from viruses" } },
            { "Malware", new List<string>{ "protection from malware", "malware",  } },
            { "Cybersecurity", new List<string>{ "what is cybersecurity", "information security",  } },
            { "Identity Theft", new List<string>{ "Identity", "theft",  } }
        };

        // printing all the topics in the _topics list
        public List<string> GetAllKeywords()
        {
            return KeywordResponder._topics.Keys.ToList();
        }
        // the following is the random selection requirement logic
        private Random _random = new Random();

        public string ChatbotResponds(string keyword)
        {
            if (_responses.ContainsKey(keyword))
            {
                List<string> responses = _responses[keyword];
                int index = _random.Next(responses.Count);
                return responses[index];
            }

            else 
            {
                return "I am still developing that information...";
            
            }
        }

        public string GetFollowUpResponse(string keyword)
        {
            if (_responses.ContainsKey(keyword))
            {
                List<string> responses = _responses[keyword];

                return responses[0];
            }

            return "I don't have more information on that yet.";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class Quiz
    {
        public List<QuizQuestions> Questions = new List<QuizQuestions>();

        public List<QuizQuestions> CorrectQuestions = new List<QuizQuestions>();

        public List<QuizQuestions> WrongQuestions = new List<QuizQuestions>();

        public int CurrentQuestion = 0;

        public int Score = 0;

        private readonly ChatLogger _logger;

        public Quiz()
        {
            LoadQuestions();
        }

        public Quiz(ChatLogger logger)
        {
            _logger = logger;

            LoadQuestions();

            _logger.Record("Quiz started.");
        }

        private void LoadQuestions()
        {
            Questions.Add(new QuizQuestions
            {
                QuestionText = " What should you do when you recieve suspicious email to with a link to reset your password? ",
                Options = new List<string>
                {
                    "A) Click the link ",
                    "B) Delete the email",
                    "C) Report the email as phishing",
                    "D) Ignore the email"
                },
                CorrectAnswer = 2,
                Explanation = "Your correct! Reporting phishing helps prevent scams"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "Why is MFA (multi-factor authentication) recommended?",
                Options = new List<string>
                {
                    "A) It provides an extra layer of security",
                    "B) it encrypts the data",
                    "C) it makes IT the administrator",
                    "D) None of the above",
                },
                CorrectAnswer = 0,
                Explanation = "Correct! MFA (multi-factor authentication) provides an extra layer of protection, good job"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "The purpose of cyber security is learn how to hack people",
                Options = new List<string>
                {
                    "True",
                    "False"
                   
                },
                CorrectAnswer = 1,
                Explanation = "The purpose of cyber sceurity is to prevent unauthorized access to data."
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "How do you make a strong password?",
                Options = new List<string>
                {
                    "A) I use my pet's name",
                    "B) I use special characters, numbers and letters",
                    "C) I use my name and birthday",
                    "D) All of the above"
                },
                CorrectAnswer = 1,
                Explanation = "Correct! A strong and complex password will make it hard for cybercriminals to hack you"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "What is the biggest cause of cyber attacks?",
                Options = new List<string>
                {
                    "A) Bad IT department",
                    "B) Human error",
                    "C) system failure",
                                    
                },
                CorrectAnswer = 1,
                Explanation = "Correct! Cybercriminals take advantage of ignorant, and vulnerable users"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "The most commonly known strategy of cybercriminals is social engineering",
                Options = new List<string>
                {
                    "True",
                    "False"
                    

                },
                CorrectAnswer = 0,
                Explanation = "Good Job!"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "The purpose of encrypting data is to lock the data",
                Options = new List<string>
                {
                    "True",
                    "False"


                },
                CorrectAnswer = 1,
                Explanation = " Encryption makes the data unreadable to unauthorized people "
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "What is a ransomware attack?",
                Options = new List<string>
                {
                    "A) The hacker demands a ransom",
                    "B) The hacker offers money for the data",
                    "C) The hacker shares the data with his colleagues",
                    "D) When you hack a bank",


                },
                CorrectAnswer = 0,
                Explanation = " Correct! Hackers sometimes locks the victim out of the computer or device interface entirely, blocking basic access and demanding money!"
            });


            Questions.Add(new QuizQuestions
            {
                QuestionText = "The malicious software that hackers install is called a spyware",
                Options = new List<string>
                {
                    "False",
                    "True",
                   


                },
                CorrectAnswer = 0,
                Explanation = "The malicious software that hackers install is called a malware"
            });

            Questions.Add(new QuizQuestions
            {
                QuestionText = "What best equips you to fight the cyber threat?",
                Options = new List<string>
                {
                    "A) New work computers",
                    "B) Training on hacking methods",
                    "C) Awareness training",
                    
                },
                CorrectAnswer = 2,
                Explanation = "Correct! Awareness training helps prevents social engineering , a common cybercriminal strategy"
            });
        }

        public QuizQuestions GetCurrentQuestion()
        {
            return Questions[CurrentQuestion];
        }

        public bool CheckAnswer(int selectedAnswer)
        {
            QuizQuestions q = Questions[CurrentQuestion];

            if (selectedAnswer == q.CorrectAnswer)
            {
                Score++;
                CorrectQuestions.Add(q);
                return true;
            }

            WrongQuestions.Add(q);
            return false;
        }

        public bool NextQuestion()
        {
            CurrentQuestion++;

            return CurrentQuestion < Questions.Count;
        }

        public string ShowResults()
        {
            string feedback;

            if (Score == Questions.Count)
            {
                feedback = " Perfect! You are becoming a cyber security master";
            }
            else if (Score >= 7)
            {
                feedback = " Excellent, you are a fast learner ";
            }
            else if (Score >= 5)
            {
                feedback = " Good job! Keep practicing.";
            }
            else if (Score >= 3)
            {
                feedback = " Don't give up. Keep learning!";
            }
            else
            {
                feedback = " Time to lock in and study";
            }

            _logger.Record($"Quiz completed. Score: {Score}/{Questions.Count}");

            return
                "Quiz Finished!\n\n" +
                "Score: " + Score + " / " + Questions.Count +
                "\n\nCorrect Answers: " + CorrectQuestions.Count +
                "\nIncorrect Answers: " + WrongQuestions.Count +
                "\n\n" + feedback;
        }
    }
}

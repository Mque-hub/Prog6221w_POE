using POE;
using System;
using System.Media;

class Program 
{
    static void Main(string[] args)
    {
        // Create an object(instance) for the other classes within the program
        Chatbot chat=new Chatbot();
        User user=new User();

        // Calling the "chat" object. In this Chatbot() class is a audio output of a greeting message, ASCII artwork and a display message
        
        chat.PlayIntroLogic();

        // Calling the "user" object. In the User() class is the main logic for the program, i.e., Prompting user information, giving reponses based on questions, loops and if else conditions etc.
        user.UserInfo();
    }
        
}

using POE;
using System;
using System.Media;

class Program 
{
    static void Main(string[] args)
    {
        Chatbot chat=new Chatbot();
        User user=new User();

        chat.PlayIntroLogic();
        user.UserInfo();
    }
        
}

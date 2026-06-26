using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class QuizQuestions
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswer { get; set; }
        public string Explanation { get; set; }

    }

}

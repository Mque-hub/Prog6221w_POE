using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cybersecurity_ChatBot_GUI
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private Quiz quiz = new Quiz();

        RadioButton[] buttons;

        public QuizWindow()
        {
            InitializeComponent();

            buttons = new RadioButton[]
            {
                rb1,
                rb2,
                rb3,
                rb4
            };

            DisplayQuestion();
        }

        //-----------------------------------------------------

        private void DisplayQuestion()
        {
            QuizQuestions q = quiz.GetCurrentQuestion();

            txtQuestionNumber.Text =
                $"Question {quiz.CurrentQuestion + 1} of {quiz.Questions.Count}";

            txtQuestion.Text = q.QuestionText;

            foreach (RadioButton rb in buttons)
            {
                rb.Visibility = Visibility.Collapsed;
                rb.IsChecked = false;
            }

            for (int i = 0; i < q.Options.Count; i++)
            {
                buttons[i].Content = q.Options[i];
                buttons[i].Visibility = Visibility.Visible;
            }

            txtFeedback.Text = "";

            btnSubmit.IsEnabled = true;

            btnNext.Visibility = Visibility.Collapsed;
        }

        //-----------------------------------------------------

        private int GetSelectedAnswer()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsChecked == true)
                    return i;
            }

            return -1;
        }

        //-----------------------------------------------------

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int answer = GetSelectedAnswer();

            if (answer == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            QuizQuestions q = quiz.GetCurrentQuestion();

            bool correct = quiz.CheckAnswer(answer);

            if (correct)
            {
                txtFeedback.Text = "✔ Correct!\n" + q.Explanation;
            }
            else
            {
                txtFeedback.Text = "✘ Incorrect!\n" + q.Explanation;
            }

            btnSubmit.IsEnabled = false;

            btnNext.Visibility = Visibility.Visible;
        }

        //-----------------------------------------------------

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (quiz.NextQuestion())
            {
                DisplayQuestion();
            }
            else
            {
                MessageBox.Show(quiz.ShowResults(), "Quiz Results");

                Close();
            }
        }
    }
}

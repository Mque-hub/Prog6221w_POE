using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cybersecurity_ChatBot_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAsciiArt();

            ChatBot chat = new ChatBot();

            chat.PlayIntroLogic();

            AddBotMessage(chat.GetGreeting());


        }

        private void LoadAsciiArt()
        {
            string asciiArt = @" _______  _______           _______    _______           ______   _______  _______  _______  _______  _______           _______ __________________            ______   _______ _________  
(       )(  ___  )|\     /|(  ____ \  (  ____ \|\     /|(  ___ \ (  ____ \(  ____ )(  ____ \(  ____ \(  ____ \|\     /|(  ____ )\__   __/\__   __/|\     /|  (  ___ \ (  ___  )\__   __/  
| () () || (   ) || )   ( || (    \/  | (    \/( \   / )| (   ) )| (    \/| (    )|| (    \/| (    \/| (    \/| )   ( || (    )|   ) (      ) (   ( \   / )  | (   ) )| (   ) |   ) (     
| || || || |   | || |   | || (__      | |       \ (_) / | (__/ / | (__    | (____)|| (_____ | (__    | |      | |   | || (____)|   | |      | |    \ (_) /   | (__/ / | |   | |   | |     
| |(_)| || |   | || |   | ||  __)     | |        \   /  |  __ (  |  __)   |     __)(_____  )|  __)   | |      | |   | ||     __)   | |      | |     \   /    |  __ (  | |   | |   | |     
| |   | || | /\| || |   | || (        | |         ) (   | (  \ \ | (      | (\ (         ) || (      | |      | |   | || (\ (      | |      | |      ) (     | (  \ \ | |   | |   | |     
| )   ( || (_\ \ || (___) || (____/\  | (____/\   | |   | )___) )| (____/\| ) \ \__/\____) || (____/\| (____/\| (___) || ) \ \_____) (___   | |      | |     | )___) )| (___) |   | |     
|/     \|(____\/_)(_______)(_______/  (_______/   \_/   |/ \___/ (_______/|/   \__/\_______)(_______/(_______/(_______)|/   \__/\_______/   )_(      \_/     |/ \___/ (_______)   )_(     
                                                                                                                                                                                          ";

            AsciiBox.Text = asciiArt;
        }

        private void AddBotMessage(string text)
        {
            Border bubble = new Border
            {
                Background = Brushes.ForestGreen,
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(15),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 400
            };

            TextBlock message = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = message;

            ChatPanel.Children.Add(bubble);
        }

        private void AddUserMessage(string text)
        {
            Border bubble = new Border
            {
                Background = Brushes.DodgerBlue,
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(15),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 400
            };

            TextBlock message = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = message;

            ChatPanel.Children.Add(bubble);
        }


        public void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInputTextBox.Text;

            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                AddUserMessage(userMessage);

                UserInputTextBox.Clear();

                AddBotMessage("Message received.");

                ChatScrollViewer.ScrollToEnd();
            }
        }
    }
}
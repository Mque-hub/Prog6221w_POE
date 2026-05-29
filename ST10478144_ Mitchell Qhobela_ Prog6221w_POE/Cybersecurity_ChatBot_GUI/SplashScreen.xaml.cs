using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace Cybersecurity_ChatBot_GUI
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();
            LoadAsciiArt();

            timer.Interval = TimeSpan.FromSeconds(4);

            timer.Tick += Timer_Tick;

            timer.Start();
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            this.Close();
        }
    }
}


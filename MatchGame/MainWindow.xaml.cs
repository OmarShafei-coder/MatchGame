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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed = 0;
        int matchesFound = 0;
        bool IsFirstClick = true;
        TextBlock lastTextBlockClicked;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if(matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text += " - Play again?";
            }
        }

        private void SetUpGame()
        {
            Random random = new Random();
            List<string> AnimalEmojis = new List<string>()
            {
                "🐒", "🐒",
                "🐕", "🐕",
                "🐴", "🐴",
                "🐷", "🐷",
                "🐏", "🐏",
                "🐪", "🐪",
                "🦒", "🦒",
                "🐘", "🐘",
            };

            foreach (TextBlock textBlock in MainGrid.Children.OfType<TextBlock>())
            {
                textBlock.Visibility = Visibility.Visible;
                if (AnimalEmojis.Count == 0) break;
                int Index = random.Next(AnimalEmojis.Count);
                string NextEmoji = AnimalEmojis[Index];
                textBlock.Text = NextEmoji;
                AnimalEmojis.RemoveAt(Index);
            }
            matchesFound = 0;
            tenthsOfSecondsElapsed = 0;
            timer.Start();
        }

        /* If it's the first in the
         * pair being clicked, keep
         * track of which TextBlock
         * was clicked and make the
         * animal disappear. If
         * it's the second one,
         * either make it disappear
         * (if it's a match) or
         * bring back the first one
         * (if it's not).
         */
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(IsFirstClick)
            {
                textBlock.Visibility = Visibility.Hidden;
                IsFirstClick = false;
            }
            else
            {
                if(lastTextBlockClicked.Text == textBlock.Text)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    matchesFound++;
                }
                else
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                }
                IsFirstClick = true;
            }
            lastTextBlockClicked = textBlock;
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetUpGame();
        }
    }
}

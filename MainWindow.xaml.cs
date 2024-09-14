using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PrisonersRiddle
{

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random r)
        {
            // slightly modified from https://stackoverflow.com/questions/273313/randomize-a-listt
            int n = list.Count;
            while (n > 1)
            {
                int k = r.Next(n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToGlobalizationString(this double input, string format)
        {    // from https://stackoverflow.com/questions/24910873/forcing-four-decimal-places-for-double
            return input.ToString(format, CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name));
        }

    }





    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MakeResultsInvisable();
            tbResultsText.Visibility = Visibility.Hidden;
            tbResults.Visibility = Visibility.Hidden;
        }

        private async void btnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            string hldButtonContents = DisablePlayButton();

            MakeResultsInvisable();
            int players;
            int attempts;
            int iterations;

            if (int.TryParse(tbNumOfPlayers.Text, out players) == false
                || int.TryParse(tbNumOfAttempts.Text, out attempts) == false
                || int.TryParse(tbRunIterations.Text, out iterations) == false
                )
            {
                tbNumOfPlayers.Text = "100";
                tbNumOfAttempts.Text = "50";
                tbRunIterations.Text = "1000";
                MakeResultsVisable();
                EnablePlayButton(hldButtonContents);
                tbResults.Text = "Enter numbers only";
                return;
            }


            int cntSuccess = 0;

            await Task.Run(() => { cntSuccess = PlaytimeWorker.Playtime(iterations, players, attempts, new Random()); });


            MakeResultsVisable();


            string results = (((double)cntSuccess / (double)iterations) * (double)100.0).ToGlobalizationString("F4");
            tbResults.Text = $"{results}%";

            EnablePlayButton(hldButtonContents);

        }

        private void EnablePlayButton(string hldButtonContents)
        {
            btnPlayGame.Content = hldButtonContents;
            btnPlayGame.IsEnabled = true;
        }

        private string DisablePlayButton()
        {
            btnPlayGame.IsEnabled = false;
            string hld = btnPlayGame.Content.ToString();
            btnPlayGame.Content = "Working";
            return hld;
        }

        private void MakeResultsInvisable()
        {
            tbResultsText.Visibility = Visibility.Hidden;
            tbResults.Visibility = Visibility.Hidden;
        }
        private void MakeResultsVisable()
        {
            tbResultsText.Visibility = Visibility.Visible;
            tbResults.Visibility = Visibility.Visible;
        }

        private void genericNumbersOnly(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0] < '0' || e.Text[0] > '9')
            {
                e.Handled = true;
            }

        }



        /* 
                private int Playtime(int playtimeIterations, int numPlayers, int numAttemptsAllowed, Random randomizer)
                {
                    int cntSuccess = 0;
                    for (int idx = 0; idx < playtimeIterations; idx++)
                    {
                        List<int> boxes = GetBoxList(numPlayers);
                        RandomizeBoxes(boxes, randomizer);
                        if (GroupsPlaytime(numPlayers, numAttemptsAllowed, boxes)) cntSuccess++;
                    }
                    return cntSuccess;
                }


                private bool GroupsPlaytime(int numPlayers, int numAttemptsAllowed, List<int> boxes)
                {
                    int cntPlayerFoundTheirNumber = 0;
                    for (int playerNumber = 0; playerNumber < numPlayers; playerNumber++)
                    {
                        if (PlayerFindYourNumber(playerNumber, numAttemptsAllowed, boxes)) cntPlayerFoundTheirNumber++;
                    }
                    // $"Number of players: {numPlayers} found their box {cntPlayerFoundTheirNumber} times".Dump(); 
                    if (cntPlayerFoundTheirNumber == numPlayers)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                private bool PlayerFindYourNumber(int playerNumber, int numAttemptsAllowed, List<int> boxes)
                {
                    int currentBoxAttempt = playerNumber;
                    while (numAttemptsAllowed > 0)
                    {
                        if (boxes[currentBoxAttempt] == playerNumber)
                        {
                            return true;
                        }
                        currentBoxAttempt = boxes[currentBoxAttempt];
                        numAttemptsAllowed--;
                    }
                    return false;
                }




                private List<int> GetPlayers(int cnt)
                {
                    // Enumerable is oddly sdlightly slower than a for loop. 
                    return new List<int>(Enumerable.Range(0, cnt));
                }


                private List<int> GetBoxList(int cnt)
                {
                    // for loop is oddly slightly faster than using Enumerable 
                    List<int> l = new List<int>();
                    for (int idx = 0; idx < cnt; idx++)
                    {
                        l.Add(idx);
                    }
                    return l;
                }


                private void RandomizeBoxes(List<int> boxes, Random r)
                {
                    // in .Net 8 explore random Random.shuffle 
                    boxes.Shuffle(r);

                }

                private void genericNumbersOnly(object sender, TextCompositionEventArgs e)
                {
                    if ( e.Text[0] < '0' || e.Text[0] > '9' )
                    {
                        e.Handled= true;
                    }

                }
        */
    }
}

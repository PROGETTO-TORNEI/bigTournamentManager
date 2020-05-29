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

namespace bigTournamentManager
{
    /// <summary>
    /// Logica di interazione per TurnsWindow.xaml
    /// </summary>
    public partial class TurnsWindow : Window
    {
        public Tournament tournament;
        public MainWindow mainWindow;

        public TurnsWindow(Tournament t, MainWindow mw)
        {
            InitializeComponent();

            this.tournament = t;
            this.mainWindow = mw;

            //bool finalphase = false;
        }

        private void btnAddTurn_Click(object sender, RoutedEventArgs e)
        {
            int tablePlayersNumber = Int32.Parse(txbPlayersNumber.Text);
            bool italianRound = (bool)chb1.IsChecked;

            SingletonDBMS.GetInstance().GetPartialRanking(this.tournament.ListPlayers);

            Turn turn = new Turn(italianRound, tablePlayersNumber, this.tournament.ListPlayers, this.tournament.nextRoundNumber());
            turn.svizzera();
            ltb1.Items.Add(turn);
            this.tournament.setTurn(turn);

            SingletonDBMS.GetInstance().InsertTurn(this.tournament);

            this.resetPointsPlayers();
        }

        private void btnShowTurn_Click(object sender, RoutedEventArgs e)
        {
            if (ltb1.SelectedItem != null)
            {
                Turn selectedTurn = (Turn)ltb1.SelectedItem;

                SingletonDBMS.GetInstance().GetTurnRanking(this.tournament.ListPlayers, selectedTurn.RoundNumber);
                
                TablesWindow win = new TablesWindow(this.tournament, (Turn)ltb1.SelectedItem, this);
                this.Hide();
                win.Show();
            }
        }

        private void resetPointsPlayers() 
        {
            IEnumerator<Player> en = this.tournament.ListPlayers.GetEnumerator();
            while (en.MoveNext())
            {
                Player p = en.Current;
                p.Points = 0;
            }
        }

    }
}
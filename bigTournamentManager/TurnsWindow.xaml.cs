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
            this.loadLastTurn();

            int tablePlayersNumber = Int32.Parse(txbPlayersNumber.Text);
            bool italianRound = (bool)chb1.IsChecked;            

            Turn turn = new Turn(italianRound, tablePlayersNumber, this.tournament.getListPlayers(), this.tournament.nextRoundNumber());
            turn.svizzera();
            ltb1.Items.Add(turn);
            this.tournament.setCurrentTurn(turn);

            SingletonDBMS.GetInstance().InsertTurn(this.tournament);
        }

        private void btnShowTurn_Click(object sender, RoutedEventArgs e)
        {
            if (ltb1.SelectedItem != null)
            {
                TablesWindow win = new TablesWindow(this.tournament, (Turn)ltb1.SelectedItem, this);                
                this.Close();
                win.Show();
            }
        }

        private bool loadLastTurn()
        {

            return true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.mainWindow.Show();
        }
    }
}
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

namespace bigTournamentManager
{
    public partial class MainWindow : Window
    {
        private LinkedList<Player> listPlayers = new LinkedList<Player>();
        public Tournament tournament;
        private String name;
        private String address;
        private String game;
        private DateTime date;
        private bool teams;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //predo dati da form
            this.name = txbName.Text;
            this.address = txbAddress.Text;
            this.game = txbGame.Text;
            this.date = dpkData.DisplayDate;

            this.teams = (bool) chb1.IsChecked;

            this.tournament = new Tournament(name, address, game, date, teams, listPlayers);           

            TurnsWindow win = new TurnsWindow(tournament);
            win.Show();

        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            String player = txbPlayer.Text;
            listPlayers.AddLast(new Player(player));
        }
    }
}

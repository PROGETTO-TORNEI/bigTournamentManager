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
            SingletonDBMS.GetInstance().CreateDb();
            //MessageBox.Show(Environment.MachineName);
            
            List<String> games = SingletonDBMS.GetInstance().GetGamesFromDB();
            for (int i = 0; i < games.Count; i++)
                this.cmBoxGame.Items.Add(games.ElementAt(i));
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.name = txbName.Text;
            this.address = txbAddress.Text;
            this.game = cmBoxGame.Text;
            this.date = dpkData.DisplayDate;

            this.teams = (bool) chb1.IsChecked;            

            this.tournament = new Tournament(this.name, this.game, this.address, this.date, this.teams, this.listPlayers.ToList<Player>());

            bool b = SingletonDBMS.GetInstance().InsertTournament(this.tournament);

            TurnsWindow win = new TurnsWindow(this.tournament, this);
            win.Show();
            this.Hide();
        }


        private void OnKeyDownHandlerTxbPlayer(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Player player = SingletonDBMS.GetInstance().GetPlayerFromDB(txbPlayer.Text);
                if (player != null) {
                    this.listPlayers.AddLast(new Player(player.Nickname));
                    ltb1.Items.Add(player);
                    txbPlayer.Text = "";
                } else {
                    MessageBox.Show("GIOCATORE INESISTENTE");
                }
                txbPlayer.Focus();
            }
        }

        private void OnKeyDownHandlerTxbGame(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                dpkData.Focus();
            }
        }

        private void OnKeyDownHandlerTxbAddress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                cmBoxGame.Focus();
            }
        }

        private void OnKeyDownHandlerTxbName(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txbAddress.Focus();
            }
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            String player = txbPlayer.Text;
            txbPlayer.Clear();
            this.listPlayers.AddLast(new Player(player));
            ltb1.Items.Add(player);
        }
    }
}

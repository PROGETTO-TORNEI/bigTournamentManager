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
        public MainWindow()
        {
            InitializeComponent();
            //predo dati da form
            String name = txbName.DataContext.ToString();
            String address = txbAddress.DataContext.ToString();
            String game = txbGame.DataContext.ToString();
            DateTime datetime = new DateTime();
            LinkedList<Player> listPlayers = new LinkedList<Player>();
            bool teams = false;

            Tournament tournament = new Tournament(name, address, game, datetime, teams, listPlayers);

            bool finalphase = false;

            tournament.generateTurn(finalphase);

            tournament.currentTurn.svizzera();
            
        }
    }
}

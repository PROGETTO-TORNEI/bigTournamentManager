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
    /// Logica di interazione per pointsWindow.xaml
    /// </summary>
    public partial class PointsWindow : Window
    {
        public Tournament tournament;
        public Player player;

        public PointsWindow(Tournament t, Player p)
        {
            InitializeComponent();

            this.tournament = t;
            this.player = p;

            lbl1.Content = this.player;
            txb1.Text = this.player.Name + " " + this.player.LastName;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.player.Points = Int32.Parse(txb1.Text);//funziona?
            this.Close();
        }
    }
}

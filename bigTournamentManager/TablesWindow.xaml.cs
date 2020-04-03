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
    /// Logica di interazione per TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
        public Tournament tournament;
        public Turn turn;

        public TablesWindow(Tournament t, Turn si)
        {
            InitializeComponent();

            this.tournament = t;
            this.turn = si;
            lbx1.ItemsSource = this.turn.getListTables();

            cbx1.ItemsSource = this.turn.getListTables();

            //fai vedere solo turno selez
        }

        private void cbx1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //fai vedere solo turno selez
        }
    }
}

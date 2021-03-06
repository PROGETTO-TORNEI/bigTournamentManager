﻿using System;
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
        public TurnsWindow turnsWindow;

        public TablesWindow(Tournament t, Turn si, TurnsWindow turnsWindow)
        {
            InitializeComponent();

            this.tournament = t;
            this.turn = si;
            this.turnsWindow = turnsWindow;

            cbx1.ItemsSource = this.turn.getListTables();
        }

        private void cbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table selectedTable = (Table)cbx1.SelectedItem;
            lbx1.ItemsSource = selectedTable.getPlayers();
        }

        private void lbx1_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Player player = (Player)lbx1.SelectedItem;
            //player.setPoints(Int32.Parse(txb1.Text));
            //txb1.Clear();
            PointsWindow win = new PointsWindow(this.tournament, player, lbx1);
            win.Show();
        }

        private void btnSavePoints_Click(object sender, RoutedEventArgs e)
        {
            SingletonDBMS.GetInstance().InsertScores(this.tournament);
            MessageBox.Show("Punteggi salvati");
            this.turn.IsSaved = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.turnsWindow.Show();
            this.Close();
        }
    }
}

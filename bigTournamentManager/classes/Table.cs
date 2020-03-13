using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    class Table
    {
        private LinkedList<Player> players;
        private int tableNumber;

        public Table(LinkedList<Player> players, int tableNumber)
        {
            this.players = players;
            this.tableNumber = tableNumber;
        }
    }
}

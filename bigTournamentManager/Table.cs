using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public class Table
    {
        private LinkedList<Player> players;
        private int tableNumber;

        public Table(int tableNumber)
        {
            this.players = new LinkedList<Player>();
            this.tableNumber = tableNumber;
        }

        public void addPlayer(Player player)
        {
            this.players.AddLast(player);
        }

        public LinkedList<Player> getPlayers()
        {
            return this.players;
        }

        public override string ToString()
        {
            return "Tavolo " + this.tableNumber;
        }
    }
}

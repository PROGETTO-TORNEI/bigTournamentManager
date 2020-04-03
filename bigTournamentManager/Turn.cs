using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public class Turn
    {             
        private Boolean italianRound;
        private int tablePlayersNumber;
        private LinkedList<Player> listPlayers;
        private LinkedList<Table> listTables;
        private int roundNumber;
        private int qualifiedPlayersNumber;

        public Turn(bool italianRound, int tablePlayersNumber, LinkedList<Player> listPlayers, int roundNumber)
        {
            this.italianRound = italianRound;
            this.tablePlayersNumber = tablePlayersNumber;
            this.listPlayers = listPlayers;
            this.roundNumber = roundNumber;
            this.listPlayers = null;
        }

        public LinkedList<Table> getListTables()
        {
            return this.listTables;
        }

        public bool svizzera() {

            if (this.roundNumber == 1) {
                this.shuffleList();
            }

            IEnumerator<Player> en = this.listPlayers.GetEnumerator();

            bool exit = false;
            int c = 1;

            while (!exit)
            {
                Table table = new Table(c);
                c++;
                for (int i = 0; i < this.tablePlayersNumber; i++)
                {
                    if (en.MoveNext())
                    {
                        Player p = en.Current;
                        table.addPlayer(p);
                    }
                    else
                    {
                        Player ghost = new Player();
                        table.addPlayer(ghost);
                        exit = true;
                    }
                }
                this.listTables.AddLast(table);
            }
            
            return true;
        }

        protected LinkedList<Player> shuffleList() {
            Random Rand = new Random();
            LinkedList<Player> nl = new LinkedList<Player>(this.listPlayers.OrderBy((o) =>
            {
                return (Rand.Next() % this.listPlayers.Count);
            }));
            this.listPlayers = nl;
            return this.listPlayers;
        }

        public override string ToString()
        {
            return "Turno " + this.roundNumber + ": " +  this.tablePlayersNumber + " giocatori";
        }
    }
}

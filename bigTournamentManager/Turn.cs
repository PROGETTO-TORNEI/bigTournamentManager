using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    abstract class Turn
    {             
        protected Boolean italianRound;
        protected int tablePlayersNumber;
        protected LinkedList<Player> listPlayers;
        protected LinkedList<Table> listTable;
        protected int roundNumber;

        public Turn(bool italianRound, int tablePlayersNumber, LinkedList<Player> listPlayers, int roundNumber)
        {
            this.italianRound = italianRound;
            this.tablePlayersNumber = tablePlayersNumber;
            this.listPlayers = listPlayers;
            this.roundNumber = roundNumber;
        }

        public abstract bool generateTables();

        protected bool svizzera() {

            if (this.roundNumber == 0) {
                this.shuffleList();
            }

            IEnumerator<Player> en = this.listPlayers.GetEnumerator();

            bool exit = false;

            while (!exit)
            {
                Table table = new Table();
                for (int i = 0; i < this.tablePlayersNumber; i++)
                {
                    if (en.MoveNext())
                        table.addPlayer(en.Current);
                    else
                    {
                        table.addPlayer(new Player());
                        exit = true;
                    }
                }
                this.listTable.AddLast(table);
            }
            
            return true;
        }

        protected LinkedList<Player> shuffleList() {
            Random Rand = new Random();
            this.listPlayers = new LinkedList<Player>(this.listPlayers.OrderBy((o) =>
            {
                return (Rand.Next() % this.listPlayers.Count);
            }));
            return this.listPlayers;
        }

    }
}

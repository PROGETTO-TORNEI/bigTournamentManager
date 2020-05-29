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
        private List<Player> listPlayers;
        private LinkedList<Table> listTables;
        private int roundNumber;
        private int qualifiedPlayersNumber;
        private bool isSaved;

        public int TablePlayersNumber { get => tablePlayersNumber; set => tablePlayersNumber = value; }
        public List<Player> ListPlayers { get => listPlayers; set => listPlayers = value; }
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }
        public bool IsSaved { get => isSaved; set => isSaved = value; }

        public Turn(bool italianRound, int tablePlayersNumber, List<Player> listPlayers, int roundNumber)
        {
            this.italianRound = italianRound;
            this.tablePlayersNumber = tablePlayersNumber;
            this.listPlayers = listPlayers;
            this.roundNumber = roundNumber;
            this.listTables = new LinkedList<Table>();
            this.isSaved = false;
        }

        public LinkedList<Table> getListTables()
        {
            return this.listTables;
        }

        public bool svizzera() {

            if (this.roundNumber == 1) {
                this.shuffleList();
            }
            else
            {
                this.listPlayers.Sort();
            }

            IEnumerator<Player> en = this.listPlayers.GetEnumerator();
        
            int nTables = 0;
            int c = 0;

            do {
                nTables++;
                c += this.tablePlayersNumber;
            } while (c < this.listPlayers.Count());      

            for(int l = 0; l < nTables; l++)
            {
                Table table = new Table(l);
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
                    }
                }
                this.listTables.AddLast(table);
            }
            
            return true;
        }

        protected bool shuffleList() {
            Random Rand = new Random();
            List<Player> nl = new List<Player>(this.listPlayers.OrderBy((o) =>
            {
                return (Rand.Next() % this.listPlayers.Count);
            }));
            this.listPlayers = nl;
            return true;
        }

        public override string ToString()
        {
            return "Turno " + this.roundNumber + (this.isSaved ? "  Salvato":"");
        }
    }
}

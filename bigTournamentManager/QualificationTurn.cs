using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    class QualificationTurn : Turn
    {
        private int qualifiedPlayersNumber;

        public QualificationTurn(bool italianRound, int tablePlayersNumber, int qualifiedPlayersNumber, LinkedList<Player> listPlayers, int roundNumber) : base(italianRound, tablePlayersNumber, listPlayers, roundNumber)
        {
            this.qualifiedPlayersNumber = qualifiedPlayersNumber;
        }

        public override bool generateTables()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    class FinalPhaseTurn : Turn
    {

        public FinalPhaseTurn(bool italianRound, int tablePlayersNumber, LinkedList<Player> listPlayers, int roundNumber) : base(italianRound, tablePlayersNumber, listPlayers, roundNumber)
        {

        }

        public override bool generateTables()
        {
            throw new NotImplementedException();
        }
    }
}

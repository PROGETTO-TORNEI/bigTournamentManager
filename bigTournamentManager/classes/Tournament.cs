using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    class Tornament
    {
        private String name;
        private String game;
        private String address;
        private DateTime date;
        private Boolean teams;
        private LinkedList<Player> players;
        private Turn currentTurn;
        private int roundNumber;
    }

}

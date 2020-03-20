using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public class Tournament
    {
        private String name;
        private String address;
        private String game;
        private DateTime date;
        private Boolean teams;
        private LinkedList<Player> listPlayers;
        private Turn currentTurn;
        private int roundNumber;

        public Tournament(string name, string game, string address, DateTime date, bool teams, LinkedList<Player> listPlayers)
        {
            this.name = name;
            this.game = game;
            this.address = address;
            this.date = date;
            this.teams = teams;
            this.listPlayers = listPlayers;
            this.currentTurn = null;
            this.roundNumber = 0;
        }
        
        /*public bool generateTurn(bool finalphase) {
            if (finalphase)
            {
                this.currentTurn = new FinalPhaseTurn(this.listPlayers,this.roundNumber);
            }
            else
            {
                this.currentTurn = new QualificationTurn(this.listPlayers,this.roundNumber);
            }
            return true;
        }*/

        
    }

}

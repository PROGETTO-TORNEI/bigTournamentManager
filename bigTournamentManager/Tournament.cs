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
        private List<Player> listPlayers;
        private Turn turn;
        private int roundNumber;

        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Game { get => game; set => game = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Teams { get => teams; set => teams = value; }
        public List<Player> ListPlayers { get => listPlayers; set => listPlayers = value; }
        public Turn CurrentTurn { get => turn; set => turn = value; }
        public int RoundNumber { get => roundNumber; set => roundNumber = value; }

        public Tournament(string name, string game, string address, DateTime date, bool teams, List<Player> listPlayers)
        {
            this.name = name;
            this.game = game;
            this.address = address;
            this.date = date;
            this.teams = teams;
            this.listPlayers = listPlayers;
            this.turn = null;
            this.roundNumber = 0;
        }        
        
        public void setTurn(Turn t)
        {
            this.turn = t;
        }

        public List<Player> getListPlayers()
        {
            return this.listPlayers;
        }

        public int nextRoundNumber()
        {
            this.roundNumber++;
            return this.roundNumber;
        }

    }

}

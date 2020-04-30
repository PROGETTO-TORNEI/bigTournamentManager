using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public class Player
    {
        private String name;
        private String lastName;
        private String nickname;
        private String mail;
        private int points;

        public Player(){}

        public Player(string nickname)
        {
            this.nickname = nickname;
        }

        public int getPoints()
        {
            return this.points;
        }

        public void setPoints(int p)
        {
            this.points = p;
        }

        public override string ToString()
        {
            return this.nickname + ": " + this.points + " points";
        }
    }


}

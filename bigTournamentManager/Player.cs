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

        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Mail { get => mail; set => mail = value; }
        public int Points { get => points; set => points = value; }

        public override string ToString()
        {
            return this.name + " " + this.lastName + " - " + this.points + " pts";
        }
    }


}

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

        public Player(){}

        public Player(string nickname)
        {
            this.nickname = nickname;
        }
    }


}

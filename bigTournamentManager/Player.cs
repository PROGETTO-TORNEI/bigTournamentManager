using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public class Player : IEquatable<Player>, IComparable<Player>
    {
        private String name;
        private String lastName;
        private String nickname;
        private String mail;
        private int points;
        private bool stillInPlay;

        public Player(){}

        public Player(string nickname)
        {
            this.nickname = nickname;
            this.stillInPlay = true;
            this.points = 0;
        }

        public string FirstName { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Mail { get => mail; set => mail = value; }
        public int Points { get => points; set => points = value; }

        public override string ToString()
        {
            return this.nickname + " - " + this.points + " pts";
        }

        int IComparable<Player>.CompareTo(Player other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return other.Points.CompareTo(this.Points);
        }

        bool IEquatable<Player>.Equals(Player other)
        {
            if (other == null) return false;
            return (this.Points.Equals(other.Points));
        }
    }


}

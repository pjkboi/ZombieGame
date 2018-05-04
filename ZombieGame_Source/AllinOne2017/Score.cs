using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinOne2017
{
    class Score :IComparable<Score>
    {
        private int points;
        public int Points
        {
            get { return points; }
        }

        private string initials;
        public string Initials
        {
            get { return initials; }
        }
        public Score(int points, string initials)
        {
            this.points = points;
            this.initials = initials;
        }

        public int CompareTo(Score other)
        {
            return (other.points - this.points);
        }
    }
}

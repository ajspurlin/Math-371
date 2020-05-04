using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapefileParse
{

    class Point
    {
        public int id = 0;
        public double lat = 0;
        public double lon = 0;

        public List<Path> paths = new List<Path>();

        public Point(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point other = (Point)obj;
                return Math.Abs(other.lat - lat) < 0.0000001 && Math.Abs(other.lon - lon) < 0.0000001;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return "(" + lat + ", " + lon + ")";
        }
    }
}

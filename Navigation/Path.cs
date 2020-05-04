using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation
{
    class Path
    {
        public int id;

        public Point pointA;
        public Point pointB;

        public String name;
        public List<String> names = new List<String>();
        public String roadType;

        public double length;

        public Boolean circled = false;

        public Path(Point pointA, Point pointB, String name, String roadType)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.name = name;
            foreach (String n in name.Split('/'))
                names.Add(n.Trim());
            this.roadType = roadType;
        }

        public Point GetOtherPoint(Point point)
        {
            if (point.Equals(pointA))
                return pointB;
            else if (point.Equals(pointB))
                return pointA;
            else
                Console.WriteLine("Neither point matched!!");
            return pointA;
        }

        public Point Midpoint()
        {
            return new Point((pointA.lat + pointB.lat)/2, (pointA.lon + pointB.lon) / 2);
        }

        //Use the equation for distance from a point to a line.
        public double DistanceToPath(double lat, double lon)
        {
            double x0 = lon;
            double y0 = lat;
            double x1 = pointA.lon;
            double y1 = pointA.lat;
            double x2 = pointB.lon;
            double y2 = pointB.lat;

            double lengthSquared = Math.Pow(x2-x1, 2) + Math.Pow(y2-y1, 2);
            
            //The case where the two endpoints are equal
            if (lengthSquared == 0)
                return Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));

            double t = ((x0 - x1) * (x2 - x1) + (y0 - y1) * (y2 - y1)) / lengthSquared;
            t = Math.Max(0, Math.Min(1, t));

            return Math.Sqrt(Math.Pow(x0 - (x1 + t * (x2 - x1)), 2) + Math.Pow(y0 - (y1 + t * (y2 - y1)), 2));
        }

        public override bool Equals(object obj)
        {
            if (obj is Path)
            {
                Path other = (Path)obj;
                return other.pointA.Equals(pointA) && other.pointB.Equals(pointB) && other.name == name && Math.Abs(other.length - length) < 0.0000001;
            }
            else
                return false;
        }
    }
}

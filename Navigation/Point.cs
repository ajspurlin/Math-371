using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation
{

    class Point
    {
        public int id = 0;
        public double lat = 0;
        public double lon = 0;

        public List<Path> paths = new List<Path>();

        public Boolean starred = false;

        //Which node did the shortest path come from?
        public Point nodeFrom;
        //What is the length of the path up to this node?
        public double lengthFrom = 0;

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

        public List<Path> remainingPaths()
        {
            List<Path> remaining = new List<Path>();
            foreach (Path path in paths)
                if (!path.circled)
                    remaining.Add(path);
            return remaining;
        }

        public Path PathLeadingTo(Point point)
        {
            foreach (Path path in paths)
            {
                if (path.pointB.Equals(point))
                {
                    return path;
                }
            }
            return null;
        }

        public double Distance(Point point)
        {
            double angle = (lon - point.lon) * Math.PI / 180;

            //Convert the latitude and longitude to radians.
            double latA = lat * Math.PI / 180;
            double lonA = lon * Math.PI / 180;
            double latB = point.lat * Math.PI / 180;
            double lonB = point.lon * Math.PI / 180;

            double distance = Math.Acos(Math.Sin(latA) * Math.Sin(latB) + Math.Cos(latA) * Math.Cos(latB) * Math.Cos(angle)) * 180 / Math.PI;

            return distance * 60 * 1.1515;
        }

        public double GridDistance(Point point)
        {
            return Math.Sqrt(Math.Pow(lat-point.lat, 2) + Math.Pow(lon - point.lon, 2));
        }

        public String IntersectionName()
        {
            List<String> names = new List<String>();

            foreach(Path path in paths)
                if (!names.Contains(path.name))
                    names.Add(path.name);

            String name = "";

            foreach (String n in names)
                name += n + (names.IndexOf(n) < names.Count-1 ? "\n" : "");

            return name;
        }

        public override string ToString()
        {
            return "(" + lat + ", " + lon + ")";
        }
    }
}

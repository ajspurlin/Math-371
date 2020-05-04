using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapefileParse
{
    class Path
    {
        public Point pointA;
        public Point pointB;

        public String name;
        public String roadType;

        public double length;

        public Path(Point pointA, Point pointB, String name, String roadType)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.name = name;
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
    }
}

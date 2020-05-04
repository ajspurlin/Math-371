using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catfood.Shapefile;
using Newtonsoft.Json;

namespace ShapefileParse
{
    class Program
    {

        static List<Point> points = new List<Point>();
        static List<Path> paths = new List<Path>();
        static System.IO.StreamWriter file;


        static void Main(string[] args)
        {
            Shapefile shapefile = new Shapefile("C:/Users/Austin Spurlin/Downloads/tl_2019_29097_roads.shp");
            file = new System.IO.StreamWriter("C:/Users/Austin Spurlin/Downloads/output.txt");

            Console.WriteLine();

            // a shapefile contains one type of shape (and possibly null shapes)
            Console.WriteLine("Type: {0}, Shapes: {1:n0}", shapefile.Type, shapefile.Count);

            // a shapefile also defines a bounding box for all shapes in the file
            Console.WriteLine("Bounds: {0},{1} -> {2},{3}",
                shapefile.BoundingBox.Left,
                shapefile.BoundingBox.Top,
                shapefile.BoundingBox.Right,
                shapefile.BoundingBox.Bottom);
            Console.WriteLine();

            foreach (Shape shape in shapefile)
            {
                //Console.WriteLine("Shape {0:n0}, Type {1}", shape.RecordNumber, shape.Type);

                String roadType = "";
                String name = "";

                // each shape may have associated metadata
                string[] metadataNames = shape.GetMetadataNames();
                if (metadataNames != null)
                {
                    foreach (string metadataName in metadataNames)
                    {
                        if (metadataName.Equals("rttyp"))
                            roadType = shape.GetMetadata(metadataName);
                        if (metadataName.Equals("fullname"))
                            name = shape.GetMetadata(metadataName);
                        //Console.WriteLine("{0}={1} ({2})", metadataName, shape.GetMetadata(metadataName), shape.DataRecord.GetDataTypeName(shape.DataRecord.GetOrdinal(metadataName)));
                    }
                }

                if (name == "")
                    name = "Unnamed";
                
                //If the road name ends with TRL, it is a walking trail and should not be included.
                if (!name.ToUpper().EndsWith("TRL"))
                    switch (shape.Type)
                    {
                        case ShapeType.PolyLine:
                            ShapePolyLine line = shape as ShapePolyLine;
                            if (line.Parts.Count > 0)
                            {
                                Point lastPoint = null;
                                foreach (PointD coordinate in line.Parts[0])
                                {
                                    Point point = AddPoint(coordinate.Y, coordinate.X);
                                    if (lastPoint != null)
                                        JoinPoints(lastPoint, point, name, roadType);
                                    lastPoint = point;
                                }
                            }
                            break;

                        default:
                            Console.WriteLine(name + " is a " + shape.Type);
                            break;
                    }
            }

            List<Point> pointsToRemove = new List<Point>();
            foreach (Point point in points)
            {
                if (point.paths.Count == 0)
                    pointsToRemove.Add(point);

                file.WriteLine(point.lat + ", " + point.lon + ":");
                foreach (Path path in point.paths)
                    if (path.pointA.Equals(point))
                        file.WriteLine(path.name + " (" + path.roadType + ")  " + path.pointB);
                    else
                        file.WriteLine(path.name + " (" + path.roadType + ")  " + path.pointA);
            }

            while (pointsToRemove.Count > 0)
            {
                points.Remove(pointsToRemove[0]);
                pointsToRemove.RemoveAt(0);
            }

            Console.WriteLine("Checking for intersections...");

            CheckForIntersections();

            Console.WriteLine();
            Console.WriteLine("Cleaing up...");

            CleanUp();

            file.Close();

            Console.WriteLine();
            Console.WriteLine("Exporting");

            Export();

            Console.WriteLine();
            Console.WriteLine("Creating Diagram");

            GenerateDiagram();

            Console.WriteLine();
            Console.WriteLine("Done");
        }

        //Many intersections don't occur at a defined point. We need to find any points that come very close to another road, we will assume they meet.
        static void CheckForIntersections()
        {
            List<Path> pathsToRemove = new List<Path>();
            //Loop through each path
            for (int index = 0; index < paths.Count;)
            {
                if (index % 50 == 0)
                {
                    Console.WriteLine(index + " / " + paths.Count);
                }

                Path path = paths[index];

                //Get the distance (in miles) between the points.
                double distance = Distance(path.pointA, path.pointB);

                //Get a list of points that are within a circle around the midpoint so we don't have to check all points for each iteration.
                List<Point> region = new List<Point>();
                foreach (Point point in points)
                {
                    //Use the length of the path as a radius for our check region.
                    if (Distance(path.Midpoint(), point) < distance)
                    {
                        region.Add(point);
                    }
                }

                //Determine how many iterations are necessary to check the length of the line.
                //We want to check for nearby points about every 0.005 miles (26.4 feet).
                int iterations = (int) (distance / 0.005);

                double dLat = (path.pointB.lat - path.pointA.lat) / iterations;
                double dLon = (path.pointB.lon - path.pointA.lon) / iterations;

                Point pathPoint;
                for (int i = 0; i <= iterations; i ++)
                {
                    pathPoint = new Point(path.pointA.lat + dLat * i, path.pointA.lon + dLon * i);
                    List<Point> pointsToRemove = new List<Point>();
                    foreach (Point other in region)
                    {
                        //If the other point in the region is within 0.01 miles (52.8 ft) of the current point, it needs added to the list.
                        if (Distance(pathPoint, other) < 0.01 && !Attached(other, path.pointA, 5, null))
                        {
                            Console.WriteLine(other);
                            Console.WriteLine(other.paths.Count);
                            file.WriteLine("-----");
                            file.WriteLine("Path Point: " + pathPoint);
                            file.WriteLine("Other point " + other + " next connection " + other.paths[0].GetOtherPoint(other) + "");
                            file.WriteLine("  # Of paths from other point: " + other.paths.Count);
                            file.WriteLine("  # Of paths from pointA: " + path.pointA.paths.Count);
                            file.WriteLine("  # Of paths from pointB: " + path.pointB.paths.Count);
                            JoinPoints(path.pointA, other, path.name, path.roadType);
                            JoinPoints(path.pointB, other, path.name, path.roadType);
                            path.pointA.paths.Remove(path);
                            path.pointB.paths.Remove(path);
                            pathsToRemove.Add(path);
                            pointsToRemove.Add(other);
                            file.WriteLine("Adding (" + other.lat + ", " + other.lon + ") to " + path.name);
                        }
                    }
                    while (pointsToRemove.Count > 0)
                    {
                        region.Remove(pointsToRemove[0]);
                        pointsToRemove.RemoveAt(0);
                    }
                }
                index += 1;
            }

            while (pathsToRemove.Count > 0)
            {
                paths.Remove(pathsToRemove[0]);
                pathsToRemove.RemoveAt(0);
            }
        }

        //Determine if two points are connected within a certain number of steps.
        static Boolean Attached(Point pointA, Point pointB, int stepsRemaining, Point cameFrom)
        {
            if (pointA.Equals(pointB))
                return true;
            if (stepsRemaining <= 0)
                return false;

            foreach (Path path in pointA.paths)
            {
                Point other = path.GetOtherPoint(pointA);
                if (!other.Equals(cameFrom))
                    if (Attached(other, pointB, stepsRemaining - 1, pointA))
                        return true;
            }

            return false;
        }

        static Point AddPoint(double lat, double lon)
        {
            Point point = new Point(lat, lon);

            foreach (Point p in points)
            {
                if (point.Equals(p))
                {
                    file.WriteLine("Loaded point " + p);
                    return p;
                }
            }
            
            points.Add(point);

            return point;
        }

        static Path JoinPoints(Point pointA, Point pointB, String name, String roadType)
        {
            foreach (Path other in paths)
            {
                if ((other.pointA.Equals(pointA) && other.pointB.Equals(pointB)) || (other.pointB.Equals(pointA) && other.pointA.Equals(pointB)))
                {
                    other.name = other.name + " / " + name;
                    other.roadType = other.roadType + " / " + roadType;
                    return other;
                }
            }

            Path path = new Path(pointA, pointB, name, roadType);
            pointA.paths.Add(path);
            pointB.paths.Add(path);
            paths.Add(path);
            file.WriteLine("Joining (" + pointA.lat + ", " + pointA.lon + ") and (" + pointB.lat + ", " + pointB.lon + ") distance: " + Distance(pointA, pointB));
            return path;
        }
        
        public static double Distance(Point pointA, Point pointB)
        {
            double angle = (pointA.lon - pointB.lon) * Math.PI / 180;

            //Convert the latitude and longitude to radians.
            double latA = pointA.lat * Math.PI / 180;
            double lonA = pointA.lon * Math.PI / 180;
            double latB = pointB.lat * Math.PI / 180;
            double lonB = pointB.lon * Math.PI / 180;
            
            double distance = Math.Acos(Math.Sin(latA) * Math.Sin(latB) + Math.Cos(latA) * Math.Cos(latB) * Math.Cos(angle)) * 180 / Math.PI;
            
            return distance * 60 * 1.1515;
        }

        static void CleanUp()
        {
            foreach (Path path in paths)
            {
                path.length = Distance(path.pointA, path.pointB);
            }

            List<Point> pointsToRemove = new List<Point>();

            foreach (Point point in points)
            {
                if (point.paths.Count == 2)
                {
                    Path pathA = point.paths[0];
                    Path pathB = point.paths[1];

                    if (pathA.name == pathB.name)
                    {
                        Point pointA = pathA.GetOtherPoint(point);
                        Point pointB = pathB.GetOtherPoint(point);

                        paths.Remove(pathA);
                        paths.Remove(pathB);

                        pointA.paths.Remove(pathA);
                        pointB.paths.Remove(pathB);

                        file.WriteLine("Removing " + point);

                        Path path = JoinPoints(pointA, pointB, pathA.name, pathA.roadType);
                        path.length = pathA.length + pathB.length;

                        pointsToRemove.Add(point);
                    }
                }
            }

            Console.WriteLine("Cleaned Up " + pointsToRemove.Count + " Extra Nodes.");

            while (pointsToRemove.Count > 0) {
                points.Remove(pointsToRemove[0]);
                pointsToRemove.RemoveAt(0);
            }
        }

        static void Export()
        {
            StreamWriter json = new StreamWriter("C:/Users/Austin Spurlin/Downloads/jsonOutput.json");
            
            JsonWriter writer = new JsonTextWriter(json);

            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("Nodes");
            writer.WriteStartArray();

            int id = 0;
            foreach (Point point in points)
            {
                point.id = id++;

                writer.WriteStartObject();
                writer.WritePropertyName("ID");
                writer.WriteValue(point.id);
                writer.WritePropertyName("Lat");
                writer.WriteValue(point.lat);
                writer.WritePropertyName("Lon");
                writer.WriteValue(point.lon);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            id = 0;

            writer.WritePropertyName("Edges");
            writer.WriteStartArray();
            foreach (Path path in paths)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("ID");
                writer.WriteValue(id ++);
                writer.WritePropertyName("PointA");
                writer.WriteValue(path.pointA.id);
                writer.WritePropertyName("PointB");
                writer.WriteValue(path.pointB.id);
                writer.WritePropertyName("Name");
                writer.WriteValue(path.name);
                writer.WritePropertyName("RoadType");
                writer.WriteValue(path.roadType);
                writer.WritePropertyName("Length");
                writer.WriteValue(path.length);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();

            writer.Close();
            json.Close();
        }

        static void GenerateDiagram()
        {
            //Based on the dimensions of the county, the height should be 68% of the width.
            int width = 5000;
            int height = (int) (width * 0.68);

            Image image = new Bitmap(width, height);

            Graphics drawing = Graphics.FromImage(image);
            
            drawing.Clear(Color.White);

            Pen pathPen = new Pen(Color.Black);
            pathPen.Width = 3;
            Brush nodeBrush = new SolidBrush(Color.Red);

            //Determine what the bounding box of the nodes are.
            double minLat = double.MaxValue;
            double maxLat = double.MinValue;
            double minLon = double.MaxValue;
            double maxLon = double.MinValue;
            foreach (Point point in points)
            {
                if (point.lat < minLat)
                    minLat = point.lat;
                else if (point.lat > maxLat)
                    maxLat = point.lat;
                else if (point.lon < minLon)
                    minLon = point.lon;
                else if (point.lon > maxLon)
                    maxLon = point.lon;
            }

            //Determine a scale factor for the longitude and latitude.
            double scaleLon = width / (maxLon - minLon);
            double scaleLat = height / (maxLat - minLat);

            foreach (Path path in paths)
            {
                PointF pointA = new PointF((float) ((path.pointA.lon - minLon) * scaleLon), (float) (height - (path.pointA.lat - minLat) * scaleLat));
                PointF pointB = new PointF((float) ((path.pointB.lon - minLon) * scaleLon), (float) (height - (path.pointB.lat - minLat) * scaleLat));
                drawing.DrawLine(pathPen, pointA, pointB);
            }

            float nodeRadius = 3;

            foreach (Point point in points)
            {
                RectangleF rect = new RectangleF((float) ((point.lon - minLon) * scaleLon) - nodeRadius, (float) (height - (point.lat - minLat) * scaleLat) - nodeRadius, nodeRadius*2, nodeRadius*2);
                drawing.FillEllipse(nodeBrush, rect);
            }
            
            drawing.Save();

            pathPen.Dispose();
            nodeBrush.Dispose();
            drawing.Dispose();

            image.Save("C:/Users/Austin Spurlin/Downloads/diagram.png");
        }
    }
}

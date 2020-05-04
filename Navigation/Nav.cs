using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Navigation
{
    class Nav
    {

        //A list of all points
        public List<Point> points = new List<Point>();
        //A list of all points (nodes)
        public List<Path> allPaths = new List<Path>();
        //A list of all paths, but paths will be removed as necessary per the algorithm.
        public List<Path> paths = new List<Path>();

        public String instructions = "";
        public List<Path> route = new List<Path>();

        public Nav()
        {
            Reload();
        }

        public void Reload()
        {
            points = new List<Point>();
            paths = new List<Path>();

            Load();

            SortPaths();
        }

        public bool Calculate(Point source, Point sink)
        {
            Routing routingForm = new Routing();

            routingForm.updateTotalNodes(points.Count());
            routingForm.updateTotalPaths(paths.Count());

            Task task = Task.Run(() =>
            {
                Application.Run(routingForm);
            });
            
            int nodesVisited = 0;
            int pathsTraveled = 0;
            int pathsRemoved = 0;

            double totalDistance = source.Distance(sink);
            double furthestDistance = 0;
            double distanceRemaining = totalDistance;
            routingForm.updateRemainingDistance(distanceRemaining);

            source.starred = true;
            nodesVisited += 1;
            routingForm.updateNodesVisited(nodesVisited);

            //Remove any paths that end at the source
            List<Path> pathsToRemove = new List<Path>();
            foreach (Path path in paths)
            {
                if (path.pointB.Equals(source))
                {
                    pathsToRemove.Add(path);
                }
            }
            while (pathsToRemove.Count > 0)
            {
                pathsRemoved += 1;
                routingForm.updatePathsRemoved(pathsRemoved);
                Path path = pathsToRemove[0];
                path.pointA.paths.Remove(path);
                paths.Remove(path);
                pathsToRemove.RemoveAt(0);
            }

            while (true)
            {
                //Look for the shortest total length after traveling a path from all starred nodes.
                //Note: The shortest path from a node will always be first in the list of remaining nodes since the lists are sorted and we are removing them as we go.
                Path shortestPath = null;
                double shortestLength = -1;
                foreach (Point node in points)
                {
                    List<Path> remainingPaths = node.remainingPaths();
                    //Check if the node is starred and has at least one path remaining.
                    if (node.starred && remainingPaths.Count > 0)
                    {
                        //Check if the total length so far plus the length of the next path is less than our current minimum.
                        //If the shorestLength is -1 we havn't found any node yet, so it will become our current min.
                        if (node.lengthFrom + remainingPaths[0].length < shortestLength || shortestLength == -1)
                        {
                            //Update the shorest path and shorest length.
                            shortestPath = remainingPaths[0];
                            shortestLength = node.lengthFrom + remainingPaths[0].length;
                        }
                    }
                }

                if (shortestPath == null)
                {
                    return false;
                }

                //Determine the new node based on where the shorest path ends.
                Point newNode = shortestPath.pointB;
                nodesVisited += 1;
                routingForm.updateNodesVisited(nodesVisited);
                pathsTraveled += 1;
                routingForm.updatePathsTraveled(pathsTraveled);

                double distance = newNode.Distance(source);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    routingForm.updateFurthestDistance(furthestDistance);
                }

                distance = newNode.Distance(sink);

                if (distance < distanceRemaining)
                {
                    distanceRemaining = distance;
                    routingForm.updateRemainingDistance(distanceRemaining);
                    routingForm.UpdateProgress(1 - distanceRemaining / totalDistance);
                }

                //Circle the new path
                shortestPath.circled = true;
                //Star the new node, update where it came from, and what the total length to the node is.
                newNode.starred = true;
                newNode.nodeFrom = shortestPath.pointA;
                newNode.lengthFrom = shortestLength;


                //Remove all paths ending at the new node, except for the circled path.
                pathsToRemove = new List<Path>();
                foreach (Path path in paths)
                {
                    if (path.pointB.Equals(newNode))
                    {
                        if (!path.Equals(shortestPath))
                            pathsToRemove.Add(path);
                    }
                }

                while (pathsToRemove.Count > 0)
                {
                    pathsRemoved += 1;
                    routingForm.updatePathsRemoved(pathsRemoved);
                    Path path = pathsToRemove[0];
                    path.pointA.paths.Remove(path);
                    paths.Remove(path);
                    pathsToRemove.RemoveAt(0);
                }

                //If the sink node has been starred, break from the loop.
                if (sink.starred)
                    break;
            }


            //Start at the sink and work backward to find the best path.
            String outPath = sink.ToString();
            Point curNode = sink;
            while (true)
            {
                Path path = curNode.nodeFrom.PathLeadingTo(curNode);
                route.Add(path);
                curNode = curNode.nodeFrom;
                if (curNode.Equals(source))
                    break;
            }

            //Reverse the path since we built the route backward.
            route.Reverse();

            String instructions = "";
            String prevInstruction = "";
            double prevLength = 0;
            foreach (Path path in route)
            {
                String instruction = "Go " + GetDirection(path) + " on " + path.name;
                if (prevInstruction.Equals(""))
                {
                    prevInstruction = instruction;
                    prevLength = 0;
                }

                if (prevInstruction.Equals(instruction))
                {
                    prevLength += path.length;
                } else
                {
                    instructions = instructions + prevInstruction + " for " + Math.Round(prevLength*100)/100 + " miles.\n";
                    prevInstruction = instruction;
                    prevLength = path.length;
                }
            }

            if (prevLength > 0)
                instructions = instructions + prevInstruction + " for " + Math.Round(prevLength * 100) / 100 + " miles.";
            
            routingForm.Close();

            Results resultsForm = new Results();
            resultsForm.updateTotalNodes(points.Count);
            resultsForm.updateTotalPaths(allPaths.Count);
            resultsForm.updateNodesVisited(nodesVisited);
            resultsForm.updatePathsTraveled(pathsTraveled);
            resultsForm.updatePathsRemoved(pathsRemoved);
            resultsForm.updateDistance(sink.lengthFrom);
            resultsForm.updateDirections(instructions);

            task = Task.Run(() =>
            {
                Application.Run(resultsForm);
            });

            return true;
        }

        public String GetDirection(Path path)
        {
            //Get the angle from the start point to the end point on the path.
            double angle = Math.Atan((path.pointA.lat - path.pointB.lat) / (path.pointA.lon - path.pointB.lon));

            //Fix the angle if it is in quadrants 2 or 3.
            if (path.pointA.lon > path.pointB.lon)
            {
                angle = angle + Math.PI;
            }

            //Make sure the angle is betwee 0 and 2pi.
            while (angle < 0)
                angle = angle + 2 * Math.PI;
            while (angle >= 2 * Math.PI)
                angle = angle - 2 * Math.PI;
            
            String dir = "";

            if (angle > (15.0 / 8.0) * Math.PI || angle < (1.0 / 8.0) * Math.PI)
                dir = "East";
            else if (angle > (13.0 / 8.0) * Math.PI)
                dir = "Southeast";
            else if (angle > (11.0 / 8.0) * Math.PI)
                dir = "South";
            else if (angle > (9.0 / 8.0) * Math.PI)
                dir = "Southwest";
            else if (angle > (7.0 / 8.0) * Math.PI)
                dir = "West";
            else if (angle > (5.0 / 8.0) * Math.PI)
                dir = "Northwest";
            else if (angle > (3.0 / 8.0) * Math.PI)
                dir = "North";
            else
                dir = "Northeast";

            return dir;
        }

        public void SortPaths()
        {
            foreach (Point point in points)
            {
                point.paths.Sort((a, b) => a.length.CompareTo(b.length));
            }
        }

        public Point GetIntersection(String street1, String street2)
        {
            foreach (Path path in paths)
                if (path.names.Contains(street1))
                {
                    foreach (Path other in path.pointA.paths)
                        foreach (String name in other.names)
                            if (name.Equals(street2))
                                return path.pointA;
                    foreach (Path other in path.pointB.paths)
                        foreach (String name in other.names)
                            if (name.Equals(street2))
                                return path.pointB;
                }
            return null;
        }

        //Return a path that crosses a point.
        public Path getPathNear(double lat, double lon)
        {
            double closestDistance = double.MaxValue;
            Path closestPath = null;
            double distance = 0;

            foreach (Path path in paths)
            {
                distance = path.DistanceToPath(lat, lon);
                if (distance < 0.0005 && distance < closestDistance)
                {
                    closestPath = path;
                    closestDistance = distance;
                }
            }

            return closestPath;
        }

        public Point getPointNear(double lat, double lon)
        {
            double closestDistance = double.MaxValue;
            Point closestPoint = null;
            double distance = 0;

            Point other = new Point(lat, lon);

            foreach (Point point in points)
            {
                distance = point.Distance(other);
                if (distance < 0.025 && distance < closestDistance)
                {
                    closestPoint = point;
                    closestDistance = distance;
                }
            }

            return closestPoint;
        }

        public List<String> GetStreetNames()
        {
            List<String> streets = new List<String>();

            foreach (Path path in paths)
                foreach (String name in path.name.Split('/'))
                    if (!streets.Contains(name.Trim()))
                        streets.Add(name.Trim());

            return streets;
        }

        public List<String> GetStreetNames(String street)
        {
            List<String> streets = new List<String>();

            foreach (Path path in paths)
            {
                bool isStreet = false;

                foreach (String name in path.names)
                    if (name.ToUpper().Equals(street.ToUpper()))
                        isStreet = true;

                if (isStreet)
                {
                    foreach (Path other in path.pointA.paths)
                        foreach (String name in other.names)
                            if (!path.names.Contains(name))
                                if (!streets.Contains(name))
                                    streets.Add(name);
                    foreach (Path other in path.pointB.paths)
                        foreach (String name in other.names)
                            if (!path.names.Contains(name))
                                if (!streets.Contains(name.Trim()))
                                    streets.Add(name);
                }
            }

            return streets;
        }

        public void Load()
        {
            Loading loadingForm = new Loading();

            Task task = Task.Run(() =>
            {
                Application.Run(loadingForm);
            });

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            Console.WriteLine(Root);
            StreamReader json = new StreamReader(Root + "/Data.json");

            JObject o = JObject.Parse(json.ReadToEnd());
            

            foreach (JProperty c in o.Children())
            {
                if (c.Name.Equals("Nodes"))
                {
                    JArray array = (JArray) c.Value;
                    double count = array.Children().Count();
                    double index = 0;
                    foreach (JObject n in array.Children())
                    {
                        loadingForm.UpdateProgress(index / count * 0.5);
                        int id = Int32.Parse(n.GetValue("ID").ToString());
                        double lat = Double.Parse(n.GetValue("Lat").ToString());
                        double lon = Double.Parse(n.GetValue("Lon").ToString());
                        Point point = new Point(lat, lon);
                        point.id = id;
                        points.Add(point);
                        index += 1;
                    }
                }
                else if (c.Name.Equals("Edges"))
                {
                    JArray array = (JArray)c.Value;
                    double count = array.Children().Count();
                    double index = 0;
                    foreach (JObject n in array.Children())
                    {
                        loadingForm.UpdateProgress(0.5 + index / count * 0.5);
                        int id = Int32.Parse(n.GetValue("ID").ToString());
                        int pointA = Int32.Parse(n.GetValue("PointA").ToString());
                        int pointB = Int32.Parse(n.GetValue("PointB").ToString());
                        String name = n.GetValue("Name").ToString();
                        String roadType = n.GetValue("RoadType").ToString();
                        double length = Double.Parse(n.GetValue("Length").ToString());
                        Path path = new Path(GetPoint(pointA), GetPoint(pointB), name, roadType);
                        path.length = length;
                        path.id = id;
                        paths.Add(path);
                        path.pointA.paths.Add(path);

                        path = new Path(GetPoint(pointB), GetPoint(pointA), name, roadType);
                        path.length = length;
                        path.id = -id;
                        paths.Add(path);
                        path.pointA.paths.Add(path);
                        index += 1;
                    }
                }
            }
            
            allPaths.AddRange(paths);

            loadingForm.Close();
        }

        public Point GetPoint(int id)
        {
            foreach (Point point in points)
            {
                if (point.id == id)
                    return point;
            }

            return null;
        }
    }
}

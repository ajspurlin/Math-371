using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navigation
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        public void updateTotalNodes(int nodeCount)
        {
            totalNodes.Text = "Total Nodes: " + nodeCount;
        }

        public void updateTotalPaths(int pathCount)
        {
            totalPaths.Text = "Total Paths: " + pathCount;
        }

        public void updateNodesVisited(int nodeCount)
        {
            nodes.Text = "Nodes Visited: " + nodeCount;
        }

        public void updatePathsTraveled(int pathCount)
        {
            paths.Text = "Paths Traveled: " + pathCount;
        }

        public void updatePathsRemoved(int pathCount)
        {
            pathsRemoved.Text = "Paths Removed: " + pathCount;
        }

        public void updateDistance(double distance)
        {
            this.distance.Text = "Total Distance (mi): " + Math.Round(distance * 100) / 100;
        }

        public void updateDirections(String directions)
        {
            this.directions.Text = directions;
        }
    }
}

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
    public partial class Routing : Form
    {
        bool closed = false;

        public Routing()
        {
            InitializeComponent();
        }

        public void UpdateProgress(double percent)
        {
            progress.Value = Math.Max(Math.Min((int)(percent * 100),100),0);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            closed = true;
        }

        public void updateTotalNodes(int nodeCount)
        {
            if (closed)
                return;
            totalNodes.Text = "Total Nodes: " + nodeCount;
        }

        public void updateTotalPaths(int pathCount)
        {
            if (closed)
                return;
            totalPaths.Text = "Total Paths: " + pathCount;
        }

        public void updateNodesVisited(int nodeCount)
        {
            if (closed)
                return;
            nodes.Text = "Nodes Visited: " + nodeCount;
        }

        public void updatePathsTraveled(int pathCount)
        {
            if (closed)
                return;
            paths.Text = "Paths Traveled: " + pathCount;
        }

        public void updatePathsRemoved(int pathCount)
        {
            if (closed)
                return;
            pathsRemoved.Text = "Paths Removed: " + pathCount;
        }

        public void updateFurthestDistance(double distance)
        {
            if (closed)
                return;
            furthest.Text = "Furthest Distance (mi): " + Math.Round(distance*100)/100;
        }

        public void updateRemainingDistance(double distance)
        {
            if (closed)
                return;
            remaining.Text = "Remaining Distance (mi): " + Math.Round(distance * 100) / 100;
        }
    }
}

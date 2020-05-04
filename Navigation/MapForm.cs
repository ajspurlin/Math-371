using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navigation
{
    public partial class MapForm : Form
    {
        //Navigation object.
        Nav nav;

        //Size of the node diagram.
        int imageWidth = 4000;
        float imageRatio = 0.68f;
        Color backgroundColor = Color.White;
        Color pathColor = Color.Gray;
        Color pathHighlightColor = Color.Blue;
        Color pathTraveledColor = Color.Gray;
        Color nodeColor = Color.Red;
        Color nodeHighlightColor = Color.FromArgb(255, 255, 64, 87);
        Color nodeSelectColor = Color.Blue;

        //Variables to keep track of the current map location and zoom.
        bool mousePressed = false;
        PointF panMouseStart = new PointF();
        PointF panImageStart = new PointF();
        float zoom = 1;
        PointF mapPos = new PointF();

        //Variables to keep track of the hover point.
        bool mouseOnMap = false;
        Path hoverPath = null;
        Point hoverNode = null;

        //Keep track of the source and sink as well as the route.
        Point source;
        Point sink;
        List<Path> route = new List<Path>();

        //Image for the map.
        Image image;

        public MapForm()
        {
            //AllocConsole();

            InitializeComponent();

            mapBox.BackColor = backgroundColor;
            instructions.Text = "Select two red nodes in the network to find the shortest path.";

            nav = new Nav();

            UpdateImage(true);

            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();



        private void mapBox_MouseEnter(object sender, EventArgs e)
        {
            mouseOnMap = true;
        }

        private void mapBox_MouseLeave(object sender, EventArgs e)
        {
            mouseOnMap = false;
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            float prevZoom = zoom;

            zoom += e.Delta * (1f / 1000f) * zoom;

            if (zoom < 0.9)
                zoom = 0.9f;
            else if (zoom > 20)
                zoom = 20;

            MouseEventArgs mouse = e as MouseEventArgs;
            PointF mousePos = mouse.Location;


            float x = mousePos.X - mapBox.Location.X;
            float y = mousePos.Y - mapBox.Location.Y;

            float xPrevZoom = prevZoom * mapBox.Size.Width / imageWidth;
            float yPrevZoom = prevZoom * mapBox.Size.Height / (imageWidth * imageRatio);

            float prevX = x / xPrevZoom;
            float prevY = y / yPrevZoom;

            float xZoom = zoom * mapBox.Size.Width / imageWidth;
            float yZoom = zoom * mapBox.Size.Height / (imageWidth * imageRatio);

            int newX = (int)(x / xZoom);
            int newY = (int)(y / yZoom);

            mapPos.X = newX - prevX + mapPos.X;
            mapPos.Y = newY - prevY + mapPos.Y;

            mapBox.Refresh();


        }

        private void mapBox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mousePressed)
                if (mouse.Button == MouseButtons.Left)
                {
                    PointF mousePos = mouse.Location;

                    float deltaX = mousePos.X - panMouseStart.X;
                    float deltaY = mousePos.Y - panMouseStart.Y;

                    float xZoom = zoom * mapBox.Size.Width / imageWidth;
                    float yZoom = zoom * mapBox.Size.Height / (imageWidth * imageRatio);

                    mapPos.X = panImageStart.X + (deltaX / xZoom);
                    mapPos.Y = panImageStart.Y + (deltaY / yZoom);

                    mapBox.Refresh();
                }

            if (mouseOnMap)
            {
                RectangleF bounds = GetCoordinateRect();

                float xPos = e.X;
                float yPos = mapBox.Height - e.Y;

                float lat = bounds.Y + (yPos / mapBox.Height) * bounds.Height;
                float lon = bounds.X + (xPos / mapBox.Width) * bounds.Width;

                latitude.Text = "Latitude: " + lat;
                longitude.Text = "Longitude: " + lon;

                bool update = false;

                Path newPathHover = nav.getPathNear(lat, lon);
                if ((newPathHover != null && !newPathHover.Equals(hoverPath)) || (hoverPath != null && newPathHover == null))
                {
                    hoverPath = newPathHover;
                    update = true;
                }

                if (newPathHover != null)
                {
                    roadHighlight.Text = "Road Name: " + newPathHover.name;
                    length.Text = "Length (mi): " + Math.Round(newPathHover.length * 100) / 100;
                    roadHighlight.Visible = true;
                    length.Visible = true;
                }
                else
                {
                    roadHighlight.Visible = false;
                    length.Visible = false;
                }

                Point newNodeHover = nav.getPointNear(lat, lon);
                if ((newNodeHover != null && !newNodeHover.Equals(hoverNode)) || (hoverNode != null && newNodeHover == null))
                {
                    hoverNode = newNodeHover;
                    update = true;
                }

                if (newNodeHover != null)
                {
                    intersectionHighlight.Text = "Intersection: \n" + newNodeHover.IntersectionName();
                    intersectionHighlight.Visible = true;
                }
                else
                {
                    intersectionHighlight.Visible = false;
                }

                if (update)
                    mapBox.Refresh();
            }
        }

        private void mapBox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left)
            {
                if (hoverNode != null)
                {

                }
                else if (!mousePressed)
                {
                    mousePressed = true;
                    panMouseStart = mouse.Location;
                    panImageStart = mapPos;
                }
            }
        }

        private void mapBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mousePressed && hoverNode != null)
            {
                if (source == null)
                {
                    source = hoverNode;
                }
                else if (sink == null)
                {
                    sink = hoverNode;

                    bool success = nav.Calculate(source, sink);
                    if (!success)
                    {
                        MessageBox.Show("Unable to reach the destination. Please try a different pair of nodes.");
                        return;
                    }

                    route = nav.route;

                    UpdateImage(false);

                    instructions.Text = "Click \"Reset Route\" to try a different pair of nodes.";

                    mapBox.Refresh();
                }
            }
            mousePressed = false;
        }

        private void mapBox_Paint(object sender, PaintEventArgs e)
        {
            float xZoom = zoom * mapBox.Size.Width / imageWidth;
            float yZoom = zoom * mapBox.Size.Height / (imageWidth * imageRatio);

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.ScaleTransform(xZoom, yZoom);
            e.Graphics.DrawImage(image, mapPos.X, mapPos.Y);

            Pen hoverPen = new Pen(pathHighlightColor);
            hoverPen.Width = 3;
            Brush hoverBrush = new SolidBrush(nodeHighlightColor);
            Brush selectionBrush = new SolidBrush(nodeSelectColor);

            RectangleF bounds = GetCoordinateRect();
            float nodeRadius = 3f;

            //If the user hasn't picked a sink yet, we want to show them the highlighted road and points.
            //Otherwise we want to show the calculated route.
            if (sink == null)
            {
                if (hoverPath != null)
                {
                    PointF pointA = new PointF(((float)hoverPath.pointA.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom, (mapBox.Size.Height - ((float)hoverPath.pointA.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom);
                    PointF pointB = new PointF(((float)hoverPath.pointB.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom, (mapBox.Size.Height - ((float)hoverPath.pointB.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom);
                    e.Graphics.DrawLine(hoverPen, pointA, pointB);
                }

                if (hoverNode != null)
                {
                    RectangleF rect = new RectangleF(((float)hoverNode.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom - nodeRadius, (mapBox.Size.Height - ((float)hoverNode.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                    e.Graphics.FillEllipse(hoverBrush, rect);
                }

                if (source != null)
                {
                    RectangleF rect = new RectangleF(((float)source.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom - nodeRadius, (mapBox.Size.Height - ((float)source.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                    e.Graphics.FillEllipse(selectionBrush, rect);
                }
            }
            else
            {
                foreach (Path path in route)
                {
                    PointF pointA = new PointF(((float)path.pointA.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom, (mapBox.Size.Height - ((float)path.pointA.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom);
                    PointF pointB = new PointF(((float)path.pointB.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom, (mapBox.Size.Height - ((float)path.pointB.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom);
                    e.Graphics.DrawLine(hoverPen, pointA, pointB);
                }

                RectangleF rect = new RectangleF(((float)source.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom - nodeRadius, (mapBox.Size.Height - ((float)source.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                e.Graphics.FillEllipse(selectionBrush, rect);

                rect = new RectangleF(((float)sink.lon - bounds.X) * (mapBox.Size.Width / bounds.Width) / xZoom - nodeRadius, (mapBox.Size.Height - ((float)sink.lat - bounds.Y) * (mapBox.Size.Height / bounds.Height)) / yZoom - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                e.Graphics.FillEllipse(selectionBrush, rect);
            }

            hoverPen.Dispose();
            hoverBrush.Dispose();
            selectionBrush.Dispose();
        }

        private RectangleF GetFullCoordinateRect()
        {
            //Determine what the bounding box of the nodes are.
            float minLat = float.MaxValue;
            float maxLat = float.MinValue;
            float minLon = float.MaxValue;
            float maxLon = float.MinValue;
            foreach (Point point in nav.points)
            {
                if (point.lat < minLat)
                    minLat = (float)point.lat;
                else if (point.lat > maxLat)
                    maxLat = (float)point.lat;
                else if (point.lon < minLon)
                    minLon = (float)point.lon;
                else if (point.lon > maxLon)
                    maxLon = (float)point.lon;
            }

            return new RectangleF(minLon, minLat, maxLon - minLon, maxLat - minLat);
        }

        //Return a rectangle whose bounds are the longitude and latitude currently visible on screen.
        private RectangleF GetCoordinateRect()
        {
            int width = imageWidth;
            int height = (int)(width * imageRatio);

            RectangleF bounds = GetFullCoordinateRect();

            float xZoom = zoom * mapBox.Size.Width / imageWidth;
            float yZoom = zoom * mapBox.Size.Height / (imageWidth * imageRatio);

            float latRange = bounds.Height;
            float lonRange = bounds.Width;

            bounds.X = bounds.X - mapPos.X * (lonRange / imageWidth);
            bounds.Width = lonRange / zoom;
            bounds.Y = bounds.Y + latRange + mapPos.Y * (latRange / (imageWidth * imageRatio)) - latRange / zoom;
            bounds.Height = latRange / zoom;

            return bounds;
        }

        public void UpdateImage(bool showNodes)
        {
            int width = imageWidth;
            int height = (int)(width * imageRatio);

            Image image = new Bitmap(width, height);

            Graphics drawing = Graphics.FromImage(image);

            drawing.Clear(backgroundColor);

            Pen pathPen = new Pen(pathColor);
            Pen pathTraveledPen = new Pen(pathTraveledColor);
            pathPen.Width = 2;
            pathTraveledPen.Width = 2;
            Brush nodeBrush = new SolidBrush(nodeColor);

            //Determine what the bounding box of the nodes are.
            double minLat = double.MaxValue;
            double maxLat = double.MinValue;
            double minLon = double.MaxValue;
            double maxLon = double.MinValue;
            foreach (Point point in nav.points)
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

            foreach (Path path in nav.allPaths)
            {
                PointF pointA = new PointF((float)((path.pointA.lon - minLon) * scaleLon), (float)(height - (path.pointA.lat - minLat) * scaleLat));
                PointF pointB = new PointF((float)((path.pointB.lon - minLon) * scaleLon), (float)(height - (path.pointB.lat - minLat) * scaleLat));
                if (path.pointA.starred && path.pointB.starred)
                    drawing.DrawLine(pathTraveledPen, pointA, pointB);
                else
                    drawing.DrawLine(pathPen, pointA, pointB);
            }

            float nodeRadius = 2f;

            if (showNodes)
                foreach (Point point in nav.points)
                {
                    RectangleF rect = new RectangleF((float)((point.lon - minLon) * scaleLon) - nodeRadius, (float)(height - (point.lat - minLat) * scaleLat) - nodeRadius, nodeRadius * 2, nodeRadius * 2);
                    drawing.FillEllipse(nodeBrush, rect);
                }

            drawing.Save();

            pathPen.Dispose();
            pathTraveledPen.Dispose();
            nodeBrush.Dispose();
            drawing.Dispose();

            this.image = image;

            mapBox.Refresh();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            nav = new Nav();

            source = null;
            sink = null;
            route = new List<Path>();

            UpdateImage(true);

            instructions.Text = "Select two red nodes in the network to find the shortest path.";

            mapBox.Refresh();
        }

        private void resetView_Click(object sender, EventArgs e)
        {
            mousePressed = false;
            panMouseStart = new PointF();
            panImageStart = new PointF();
            zoom = 1;
            mapPos = new PointF();

            mapBox.Refresh();
        }
    }
}

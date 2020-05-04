namespace Navigation
{
    partial class Results
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.totalNodes = new System.Windows.Forms.Label();
            this.totalPaths = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Label();
            this.pathsRemoved = new System.Windows.Forms.Label();
            this.paths = new System.Windows.Forms.Label();
            this.nodes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.directions = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(488, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Austin Spurlin";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(246, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 33);
            this.label1.TabIndex = 9;
            this.label1.Text = "Math 371 - Final Project";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // totalNodes
            // 
            this.totalNodes.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalNodes.Location = new System.Drawing.Point(91, 187);
            this.totalNodes.Name = "totalNodes";
            this.totalNodes.Size = new System.Drawing.Size(272, 25);
            this.totalNodes.TabIndex = 24;
            this.totalNodes.Text = "Total Nodes: 0";
            // 
            // totalPaths
            // 
            this.totalPaths.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPaths.Location = new System.Drawing.Point(91, 212);
            this.totalPaths.Name = "totalPaths";
            this.totalPaths.Size = new System.Drawing.Size(272, 25);
            this.totalPaths.TabIndex = 23;
            this.totalPaths.Text = "Total Paths: 0";
            // 
            // distance
            // 
            this.distance.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distance.Location = new System.Drawing.Point(91, 312);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(272, 25);
            this.distance.TabIndex = 21;
            this.distance.Text = "Total Distance (mi): 0";
            // 
            // pathsRemoved
            // 
            this.pathsRemoved.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathsRemoved.Location = new System.Drawing.Point(91, 287);
            this.pathsRemoved.Name = "pathsRemoved";
            this.pathsRemoved.Size = new System.Drawing.Size(272, 25);
            this.pathsRemoved.TabIndex = 20;
            this.pathsRemoved.Text = "Paths Removed: 0";
            // 
            // paths
            // 
            this.paths.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paths.Location = new System.Drawing.Point(91, 262);
            this.paths.Name = "paths";
            this.paths.Size = new System.Drawing.Size(272, 25);
            this.paths.TabIndex = 19;
            this.paths.Text = "Paths Traveled: 0";
            // 
            // nodes
            // 
            this.nodes.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodes.Location = new System.Drawing.Point(91, 237);
            this.nodes.Name = "nodes";
            this.nodes.Size = new System.Drawing.Size(272, 25);
            this.nodes.TabIndex = 18;
            this.nodes.Text = "Nodes Visited: 0";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(85, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(272, 30);
            this.label4.TabIndex = 17;
            this.label4.Text = "Statistics:";
            // 
            // directions
            // 
            this.directions.BackColor = System.Drawing.SystemColors.Control;
            this.directions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.directions.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.directions.Location = new System.Drawing.Point(381, 187);
            this.directions.Name = "directions";
            this.directions.ReadOnly = true;
            this.directions.Size = new System.Drawing.Size(531, 159);
            this.directions.TabIndex = 25;
            this.directions.Text = "";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(370, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(322, 30);
            this.label3.TabIndex = 26;
            this.label3.Text = "Directions:";
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 401);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.directions);
            this.Controls.Add(this.totalNodes);
            this.Controls.Add(this.totalPaths);
            this.Controls.Add(this.distance);
            this.Controls.Add(this.pathsRemoved);
            this.Controls.Add(this.paths);
            this.Controls.Add(this.nodes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Results";
            this.Text = "Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalNodes;
        private System.Windows.Forms.Label totalPaths;
        private System.Windows.Forms.Label distance;
        private System.Windows.Forms.Label pathsRemoved;
        private System.Windows.Forms.Label paths;
        private System.Windows.Forms.Label nodes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox directions;
        private System.Windows.Forms.Label label3;
    }
}
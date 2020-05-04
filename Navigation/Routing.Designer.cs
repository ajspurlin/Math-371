namespace Navigation
{
    partial class Routing
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
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nodes = new System.Windows.Forms.Label();
            this.paths = new System.Windows.Forms.Label();
            this.pathsRemoved = new System.Windows.Forms.Label();
            this.furthest = new System.Windows.Forms.Label();
            this.remaining = new System.Windows.Forms.Label();
            this.totalPaths = new System.Windows.Forms.Label();
            this.totalNodes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(100, 403);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(378, 23);
            this.progress.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(488, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Searching for Shortest Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "Math 371 - Final Project";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(488, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Austin Spurlin";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(168, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(322, 30);
            this.label4.TabIndex = 9;
            this.label4.Text = "Statistics:";
            // 
            // nodes
            // 
            this.nodes.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodes.Location = new System.Drawing.Point(174, 198);
            this.nodes.Name = "nodes";
            this.nodes.Size = new System.Drawing.Size(322, 25);
            this.nodes.TabIndex = 10;
            this.nodes.Text = "Nodes Visited: 0";
            // 
            // paths
            // 
            this.paths.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paths.Location = new System.Drawing.Point(174, 223);
            this.paths.Name = "paths";
            this.paths.Size = new System.Drawing.Size(322, 25);
            this.paths.TabIndex = 11;
            this.paths.Text = "Paths Traveled: 0";
            // 
            // pathsRemoved
            // 
            this.pathsRemoved.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathsRemoved.Location = new System.Drawing.Point(174, 248);
            this.pathsRemoved.Name = "pathsRemoved";
            this.pathsRemoved.Size = new System.Drawing.Size(322, 25);
            this.pathsRemoved.TabIndex = 12;
            this.pathsRemoved.Text = "Paths Removed: 0";
            // 
            // furthest
            // 
            this.furthest.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.furthest.Location = new System.Drawing.Point(174, 273);
            this.furthest.Name = "furthest";
            this.furthest.Size = new System.Drawing.Size(322, 25);
            this.furthest.TabIndex = 13;
            this.furthest.Text = "Furthest Distance (mi): 0";
            // 
            // remaining
            // 
            this.remaining.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remaining.Location = new System.Drawing.Point(174, 298);
            this.remaining.Name = "remaining";
            this.remaining.Size = new System.Drawing.Size(322, 25);
            this.remaining.TabIndex = 14;
            this.remaining.Text = "Remaining Distance (mi): 0";
            // 
            // totalPaths
            // 
            this.totalPaths.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPaths.Location = new System.Drawing.Point(174, 173);
            this.totalPaths.Name = "totalPaths";
            this.totalPaths.Size = new System.Drawing.Size(322, 25);
            this.totalPaths.TabIndex = 15;
            this.totalPaths.Text = "Total Paths: 0";
            // 
            // totalNodes
            // 
            this.totalNodes.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalNodes.Location = new System.Drawing.Point(174, 148);
            this.totalNodes.Name = "totalNodes";
            this.totalNodes.Size = new System.Drawing.Size(322, 25);
            this.totalNodes.TabIndex = 16;
            this.totalNodes.Text = "Total Nodes: 0";
            // 
            // Routing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 473);
            this.Controls.Add(this.totalNodes);
            this.Controls.Add(this.totalPaths);
            this.Controls.Add(this.remaining);
            this.Controls.Add(this.furthest);
            this.Controls.Add(this.pathsRemoved);
            this.Controls.Add(this.paths);
            this.Controls.Add(this.nodes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Routing";
            this.Text = "Calculating";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label nodes;
        private System.Windows.Forms.Label paths;
        private System.Windows.Forms.Label pathsRemoved;
        private System.Windows.Forms.Label furthest;
        private System.Windows.Forms.Label remaining;
        private System.Windows.Forms.Label totalPaths;
        private System.Windows.Forms.Label totalNodes;
    }
}
namespace Navigation
{
    partial class MapForm
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
            this.mapBox = new System.Windows.Forms.PictureBox();
            this.latitude = new System.Windows.Forms.Label();
            this.longitude = new System.Windows.Forms.Label();
            this.roadHighlight = new System.Windows.Forms.Label();
            this.intersectionHighlight = new System.Windows.Forms.Label();
            this.reset = new System.Windows.Forms.Button();
            this.resetView = new System.Windows.Forms.Button();
            this.length = new System.Windows.Forms.Label();
            this.instructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapBox
            // 
            this.mapBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapBox.BackColor = System.Drawing.Color.White;
            this.mapBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapBox.Location = new System.Drawing.Point(12, 143);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(917, 494);
            this.mapBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapBox.TabIndex = 0;
            this.mapBox.TabStop = false;
            this.mapBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mapBox_Paint);
            this.mapBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseDown);
            this.mapBox.MouseEnter += new System.EventHandler(this.mapBox_MouseEnter);
            this.mapBox.MouseLeave += new System.EventHandler(this.mapBox_MouseLeave);
            this.mapBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseMove);
            this.mapBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapBox_MouseUp);
            // 
            // latitude
            // 
            this.latitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.latitude.BackColor = System.Drawing.Color.Transparent;
            this.latitude.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latitude.Location = new System.Drawing.Point(12, 12);
            this.latitude.Name = "latitude";
            this.latitude.Size = new System.Drawing.Size(181, 32);
            this.latitude.TabIndex = 1;
            this.latitude.Text = "Latitude:";
            // 
            // longitude
            // 
            this.longitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.longitude.BackColor = System.Drawing.Color.Transparent;
            this.longitude.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longitude.Location = new System.Drawing.Point(12, 38);
            this.longitude.Name = "longitude";
            this.longitude.Size = new System.Drawing.Size(181, 32);
            this.longitude.TabIndex = 2;
            this.longitude.Text = "Longitude:";
            // 
            // roadHighlight
            // 
            this.roadHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.roadHighlight.BackColor = System.Drawing.Color.Transparent;
            this.roadHighlight.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roadHighlight.Location = new System.Drawing.Point(199, 12);
            this.roadHighlight.Name = "roadHighlight";
            this.roadHighlight.Size = new System.Drawing.Size(317, 32);
            this.roadHighlight.TabIndex = 3;
            this.roadHighlight.Text = "Road Name:";
            this.roadHighlight.Visible = false;
            // 
            // intersectionHighlight
            // 
            this.intersectionHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.intersectionHighlight.BackColor = System.Drawing.Color.Transparent;
            this.intersectionHighlight.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intersectionHighlight.Location = new System.Drawing.Point(538, 12);
            this.intersectionHighlight.Name = "intersectionHighlight";
            this.intersectionHighlight.Size = new System.Drawing.Size(391, 90);
            this.intersectionHighlight.TabIndex = 4;
            this.intersectionHighlight.Text = "Intersection: ";
            this.intersectionHighlight.Visible = false;
            // 
            // reset
            // 
            this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reset.Location = new System.Drawing.Point(854, 105);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 5;
            this.reset.Text = "Reset Route";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // resetView
            // 
            this.resetView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetView.Location = new System.Drawing.Point(854, 79);
            this.resetView.Name = "resetView";
            this.resetView.Size = new System.Drawing.Size(75, 23);
            this.resetView.TabIndex = 6;
            this.resetView.Text = "Reset View";
            this.resetView.UseVisualStyleBackColor = true;
            this.resetView.Click += new System.EventHandler(this.resetView_Click);
            // 
            // length
            // 
            this.length.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.length.BackColor = System.Drawing.Color.Transparent;
            this.length.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.length.Location = new System.Drawing.Point(199, 38);
            this.length.Name = "length";
            this.length.Size = new System.Drawing.Size(317, 32);
            this.length.TabIndex = 7;
            this.length.Text = "Length (mi):";
            this.length.Visible = false;
            // 
            // instructions
            // 
            this.instructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.instructions.BackColor = System.Drawing.Color.Transparent;
            this.instructions.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructions.Location = new System.Drawing.Point(12, 117);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(593, 20);
            this.instructions.TabIndex = 8;
            this.instructions.Text = "Instructions";
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 649);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.length);
            this.Controls.Add(this.resetView);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.intersectionHighlight);
            this.Controls.Add(this.roadHighlight);
            this.Controls.Add(this.longitude);
            this.Controls.Add(this.latitude);
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapForm";
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mapBox;
        private System.Windows.Forms.Label latitude;
        private System.Windows.Forms.Label longitude;
        private System.Windows.Forms.Label roadHighlight;
        private System.Windows.Forms.Label intersectionHighlight;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button resetView;
        private System.Windows.Forms.Label length;
        private System.Windows.Forms.Label instructions;
    }
}
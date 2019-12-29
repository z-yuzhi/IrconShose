namespace IronShoes
{
    partial class frmDrawTools
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
            this.components = new System.ComponentModel.Container();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.tipToolName = new System.Windows.Forms.ToolTip(this.components);
            this.pnlFork = new System.Windows.Forms.Panel();
            this.pnlINode = new System.Windows.Forms.Panel();
            this.pnlONode = new System.Windows.Forms.Panel();
            this.pnlDHSignal = new System.Windows.Forms.Panel();
            this.pnlNone = new System.Windows.Forms.Panel();
            this.pnlSHSignal = new System.Windows.Forms.Panel();
            this.pnlSSignal = new System.Windows.Forms.Panel();
            this.pnlDSignal = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlPath
            // 
            this.pnlPath.BackColor = System.Drawing.Color.White;
            this.pnlPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPath.Location = new System.Drawing.Point(12, 152);
            this.pnlPath.Name = "pnlPath";
            this.pnlPath.Size = new System.Drawing.Size(40, 40);
            this.pnlPath.TabIndex = 0;
            this.pnlPath.Tag = "路段";
            this.pnlPath.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlPath.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlPath.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlPath.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // tipToolName
            // 
            this.tipToolName.AutoPopDelay = 5000;
            this.tipToolName.InitialDelay = 0;
            this.tipToolName.ReshowDelay = 100;
            // 
            // pnlFork
            // 
            this.pnlFork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFork.BackColor = System.Drawing.Color.White;
            this.pnlFork.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFork.Location = new System.Drawing.Point(58, 152);
            this.pnlFork.Name = "pnlFork";
            this.pnlFork.Size = new System.Drawing.Size(40, 40);
            this.pnlFork.TabIndex = 0;
            this.pnlFork.Tag = "道岔";
            this.pnlFork.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlFork.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlFork.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlFork.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlINode
            // 
            this.pnlINode.BackColor = System.Drawing.Color.White;
            this.pnlINode.BackgroundImage = global::IronShoes.Properties.Resources.INode;
            this.pnlINode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlINode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlINode.Location = new System.Drawing.Point(12, 198);
            this.pnlINode.Name = "pnlINode";
            this.pnlINode.Size = new System.Drawing.Size(40, 40);
            this.pnlINode.TabIndex = 0;
            this.pnlINode.Tag = "绝缘节";
            this.pnlINode.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlINode.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlINode.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlINode.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlONode
            // 
            this.pnlONode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlONode.BackColor = System.Drawing.Color.White;
            this.pnlONode.BackgroundImage = global::IronShoes.Properties.Resources.ONode;
            this.pnlONode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlONode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlONode.Location = new System.Drawing.Point(58, 198);
            this.pnlONode.Name = "pnlONode";
            this.pnlONode.Size = new System.Drawing.Size(40, 40);
            this.pnlONode.TabIndex = 0;
            this.pnlONode.Tag = "绝缘节";
            this.pnlONode.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlONode.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlONode.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlONode.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlDHSignal
            // 
            this.pnlDHSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDHSignal.BackColor = System.Drawing.Color.White;
            this.pnlDHSignal.BackgroundImage = global::IronShoes.Properties.Resources.DHSignal;
            this.pnlDHSignal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlDHSignal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDHSignal.Location = new System.Drawing.Point(58, 106);
            this.pnlDHSignal.Name = "pnlDHSignal";
            this.pnlDHSignal.Size = new System.Drawing.Size(40, 40);
            this.pnlDHSignal.TabIndex = 0;
            this.pnlDHSignal.Tag = "信息机";
            this.pnlDHSignal.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlDHSignal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlDHSignal.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlDHSignal.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlNone
            // 
            this.pnlNone.BackColor = System.Drawing.SystemColors.Highlight;
            this.pnlNone.BackgroundImage = global::IronShoes.Properties.Resources.Pointer;
            this.pnlNone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlNone.Location = new System.Drawing.Point(12, 12);
            this.pnlNone.Name = "pnlNone";
            this.pnlNone.Size = new System.Drawing.Size(40, 40);
            this.pnlNone.TabIndex = 1;
            this.pnlNone.Tag = "选取";
            this.pnlNone.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlNone.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlNone.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlNone.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlSHSignal
            // 
            this.pnlSHSignal.BackColor = System.Drawing.Color.White;
            this.pnlSHSignal.BackgroundImage = global::IronShoes.Properties.Resources.SHSignal;
            this.pnlSHSignal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlSHSignal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSHSignal.Location = new System.Drawing.Point(12, 106);
            this.pnlSHSignal.Name = "pnlSHSignal";
            this.pnlSHSignal.Size = new System.Drawing.Size(40, 40);
            this.pnlSHSignal.TabIndex = 0;
            this.pnlSHSignal.Tag = "信息机";
            this.pnlSHSignal.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlSHSignal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlSHSignal.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlSHSignal.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlSSignal
            // 
            this.pnlSSignal.BackColor = System.Drawing.Color.White;
            this.pnlSSignal.BackgroundImage = global::IronShoes.Properties.Resources.SSignal;
            this.pnlSSignal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlSSignal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSSignal.Location = new System.Drawing.Point(12, 60);
            this.pnlSSignal.Name = "pnlSSignal";
            this.pnlSSignal.Size = new System.Drawing.Size(40, 40);
            this.pnlSSignal.TabIndex = 0;
            this.pnlSSignal.Tag = "信息机";
            this.pnlSSignal.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlSSignal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlSSignal.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlSSignal.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // pnlDSignal
            // 
            this.pnlDSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDSignal.BackColor = System.Drawing.Color.White;
            this.pnlDSignal.BackgroundImage = global::IronShoes.Properties.Resources.DSignal;
            this.pnlDSignal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlDSignal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDSignal.Location = new System.Drawing.Point(58, 60);
            this.pnlDSignal.Name = "pnlDSignal";
            this.pnlDSignal.Size = new System.Drawing.Size(40, 40);
            this.pnlDSignal.TabIndex = 0;
            this.pnlDSignal.Tag = "信息机";
            this.pnlDSignal.Click += new System.EventHandler(this.pnlElement_Click);
            this.pnlDSignal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlElement_Paint);
            this.pnlDSignal.MouseEnter += new System.EventHandler(this.pnlElement_MouseEnter);
            this.pnlDSignal.MouseLeave += new System.EventHandler(this.pnlElement_MouseLeave);
            // 
            // frmDrawTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(110, 464);
            this.Controls.Add(this.pnlNone);
            this.Controls.Add(this.pnlINode);
            this.Controls.Add(this.pnlONode);
            this.Controls.Add(this.pnlSHSignal);
            this.Controls.Add(this.pnlDHSignal);
            this.Controls.Add(this.pnlSSignal);
            this.Controls.Add(this.pnlDSignal);
            this.Controls.Add(this.pnlFork);
            this.Controls.Add(this.pnlPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrawTools";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "绘制工具";
            this.Load += new System.EventHandler(this.frmDrawTools_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmDrawTools_MouseClick);
            this.MouseEnter += new System.EventHandler(this.frmDrawTools_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.Panel pnlNone;
        private System.Windows.Forms.ToolTip tipToolName;
        private System.Windows.Forms.Panel pnlFork;
        private System.Windows.Forms.Panel pnlDSignal;
        private System.Windows.Forms.Panel pnlSSignal;
        private System.Windows.Forms.Panel pnlDHSignal;
        private System.Windows.Forms.Panel pnlSHSignal;
        private System.Windows.Forms.Panel pnlONode;
        private System.Windows.Forms.Panel pnlINode;



    }
}
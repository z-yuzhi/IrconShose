namespace IronShoes
{
    partial class frmPicture
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlWorkArea = new System.Windows.Forms.Panel();
            this.vscWScroll = new System.Windows.Forms.VScrollBar();
            this.hscWScroll = new System.Windows.Forms.HScrollBar();
            this.picWorkArea = new System.Windows.Forms.PictureBox();
            this.tmrAutoSave = new System.Windows.Forms.Timer(this.components);
            this.sfDialog = new System.Windows.Forms.SaveFileDialog();
            this.ofDialog = new System.Windows.Forms.OpenFileDialog();
            this.tsslCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbSaveProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.ssrStatus = new System.Windows.Forms.StatusStrip();
            this.pnlWorkArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWorkArea)).BeginInit();
            this.ssrStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWorkArea
            // 
            this.pnlWorkArea.AutoScroll = true;
            this.pnlWorkArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlWorkArea.Controls.Add(this.vscWScroll);
            this.pnlWorkArea.Controls.Add(this.hscWScroll);
            this.pnlWorkArea.Controls.Add(this.picWorkArea);
            this.pnlWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkArea.Location = new System.Drawing.Point(0, 0);
            this.pnlWorkArea.Name = "pnlWorkArea";
            this.pnlWorkArea.Size = new System.Drawing.Size(1800, 678);
            this.pnlWorkArea.TabIndex = 3;
            // 
            // vscWScroll
            // 
            this.vscWScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vscWScroll.Location = new System.Drawing.Point(1779, 0);
            this.vscWScroll.Name = "vscWScroll";
            this.vscWScroll.Size = new System.Drawing.Size(17, 657);
            this.vscWScroll.TabIndex = 2;
            this.vscWScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // hscWScroll
            // 
            this.hscWScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hscWScroll.Location = new System.Drawing.Point(0, 657);
            this.hscWScroll.Name = "hscWScroll";
            this.hscWScroll.Size = new System.Drawing.Size(1779, 17);
            this.hscWScroll.TabIndex = 1;
            this.hscWScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // picWorkArea
            // 
            this.picWorkArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picWorkArea.Location = new System.Drawing.Point(0, 0);
            this.picWorkArea.Name = "picWorkArea";
            this.picWorkArea.Size = new System.Drawing.Size(1779, 657);
            this.picWorkArea.TabIndex = 0;
            this.picWorkArea.TabStop = false;
            this.picWorkArea.Paint += new System.Windows.Forms.PaintEventHandler(this.picWorkArea_Paint);
            this.picWorkArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picWorkArea_MouseClick);
            this.picWorkArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWorkArea_MouseDown);
            this.picWorkArea.MouseEnter += new System.EventHandler(this.picWorkArea_MouseEnter);
            this.picWorkArea.MouseLeave += new System.EventHandler(this.picWorkArea_MouseLeave);
            this.picWorkArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picWorkArea_MouseMove);
            this.picWorkArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picWorkArea_MouseUp);
            // 
            // tmrAutoSave
            // 
            this.tmrAutoSave.Enabled = true;
            this.tmrAutoSave.Tick += new System.EventHandler(this.tmrAutoSave_Tick);
            // 
            // sfDialog
            // 
            this.sfDialog.DefaultExt = "cfg";
            this.sfDialog.Filter = "*.cfg|*.cfg";
            // 
            // ofDialog
            // 
            this.ofDialog.DefaultExt = "cfg";
            this.ofDialog.Filter = "*.cfg|*.cfg";
            // 
            // tsslCoordinate
            // 
            this.tsslCoordinate.Name = "tsslCoordinate";
            this.tsslCoordinate.Size = new System.Drawing.Size(95, 17);
            this.tsslCoordinate.Text = "X:=     Y:=       ";
            // 
            // tspbSaveProgress
            // 
            this.tspbSaveProgress.Name = "tspbSaveProgress";
            this.tspbSaveProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // ssrStatus
            // 
            this.ssrStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCoordinate,
            this.tspbSaveProgress});
            this.ssrStatus.Location = new System.Drawing.Point(0, 678);
            this.ssrStatus.Name = "ssrStatus";
            this.ssrStatus.Size = new System.Drawing.Size(1800, 22);
            this.ssrStatus.TabIndex = 0;
            this.ssrStatus.Text = "statusStrip1";
            // 
            // frmPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 700);
            this.Controls.Add(this.pnlWorkArea);
            this.Controls.Add(this.ssrStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPicture";
            this.Text = "图形编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmPicture_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.pnlWorkArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWorkArea)).EndInit();
            this.ssrStatus.ResumeLayout(false);
            this.ssrStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlWorkArea;
        private System.Windows.Forms.PictureBox picWorkArea;
        private System.Windows.Forms.VScrollBar vscWScroll;
        private System.Windows.Forms.HScrollBar hscWScroll;
        private System.Windows.Forms.Timer tmrAutoSave;
        private System.Windows.Forms.SaveFileDialog sfDialog;
        private System.Windows.Forms.OpenFileDialog ofDialog;
        private System.Windows.Forms.ToolStripStatusLabel tsslCoordinate;
        private System.Windows.Forms.ToolStripProgressBar tspbSaveProgress;
        private System.Windows.Forms.StatusStrip ssrStatus;
    }
}


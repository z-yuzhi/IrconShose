namespace IronShoes
{
    partial class frmSystemConfig
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
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tpBasic = new System.Windows.Forms.TabPage();
            this.ckbShowGrid = new System.Windows.Forms.CheckBox();
            this.lblGLC = new System.Windows.Forms.Label();
            this.lblGLColor = new System.Windows.Forms.Label();
            this.lblWBC = new System.Windows.Forms.Label();
            this.lblWBColor = new System.Windows.Forms.Label();
            this.grpWorkSize = new System.Windows.Forms.GroupBox();
            this.txtWHeight = new System.Windows.Forms.TextBox();
            this.txtWWith = new System.Windows.Forms.TextBox();
            this.lblWHeight = new System.Windows.Forms.Label();
            this.lblWWith = new System.Windows.Forms.Label();
            this.grpGridSize = new System.Windows.Forms.GroupBox();
            this.txtGHeight = new System.Windows.Forms.TextBox();
            this.txtGWith = new System.Windows.Forms.TextBox();
            this.lblGHeight = new System.Windows.Forms.Label();
            this.lblGWith = new System.Windows.Forms.Label();
            this.tpElement = new System.Windows.Forms.TabPage();
            this.txtLgDiameter = new System.Windows.Forms.TextBox();
            this.lblLgDiameter = new System.Windows.Forms.Label();
            this.txtLnWidth = new System.Windows.Forms.TextBox();
            this.lblLnWidth = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabConfig.SuspendLayout();
            this.tpBasic.SuspendLayout();
            this.grpWorkSize.SuspendLayout();
            this.grpGridSize.SuspendLayout();
            this.tpElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.tpBasic);
            this.tabConfig.Controls.Add(this.tpElement);
            this.tabConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabConfig.Location = new System.Drawing.Point(0, 0);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(394, 240);
            this.tabConfig.TabIndex = 0;
            // 
            // tpBasic
            // 
            this.tpBasic.Controls.Add(this.ckbShowGrid);
            this.tpBasic.Controls.Add(this.lblGLC);
            this.tpBasic.Controls.Add(this.lblGLColor);
            this.tpBasic.Controls.Add(this.lblWBC);
            this.tpBasic.Controls.Add(this.lblWBColor);
            this.tpBasic.Controls.Add(this.grpWorkSize);
            this.tpBasic.Controls.Add(this.grpGridSize);
            this.tpBasic.Location = new System.Drawing.Point(4, 22);
            this.tpBasic.Name = "tpBasic";
            this.tpBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpBasic.Size = new System.Drawing.Size(386, 214);
            this.tpBasic.TabIndex = 0;
            this.tpBasic.Text = "基本设置";
            this.tpBasic.UseVisualStyleBackColor = true;
            // 
            // ckbShowGrid
            // 
            this.ckbShowGrid.AutoSize = true;
            this.ckbShowGrid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbShowGrid.Location = new System.Drawing.Point(235, 100);
            this.ckbShowGrid.Name = "ckbShowGrid";
            this.ckbShowGrid.Size = new System.Drawing.Size(96, 16);
            this.ckbShowGrid.TabIndex = 6;
            this.ckbShowGrid.Text = "是否显示网格";
            this.ckbShowGrid.UseVisualStyleBackColor = true;
            // 
            // lblGLC
            // 
            this.lblGLC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblGLC.Location = new System.Drawing.Point(317, 68);
            this.lblGLC.Name = "lblGLC";
            this.lblGLC.Size = new System.Drawing.Size(40, 20);
            this.lblGLC.TabIndex = 5;
            this.lblGLC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGLC.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // lblGLColor
            // 
            this.lblGLColor.AutoSize = true;
            this.lblGLColor.Location = new System.Drawing.Point(259, 72);
            this.lblGLColor.Name = "lblGLColor";
            this.lblGLColor.Size = new System.Drawing.Size(53, 12);
            this.lblGLColor.TabIndex = 4;
            this.lblGLColor.Text = "网格颜色";
            // 
            // lblWBC
            // 
            this.lblWBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWBC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWBC.Location = new System.Drawing.Point(317, 40);
            this.lblWBC.Name = "lblWBC";
            this.lblWBC.Size = new System.Drawing.Size(40, 20);
            this.lblWBC.TabIndex = 3;
            this.lblWBC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWBC.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // lblWBColor
            // 
            this.lblWBColor.AutoSize = true;
            this.lblWBColor.Location = new System.Drawing.Point(223, 44);
            this.lblWBColor.Name = "lblWBColor";
            this.lblWBColor.Size = new System.Drawing.Size(89, 12);
            this.lblWBColor.TabIndex = 2;
            this.lblWBColor.Text = "工作区背景颜色";
            // 
            // grpWorkSize
            // 
            this.grpWorkSize.Controls.Add(this.txtWHeight);
            this.grpWorkSize.Controls.Add(this.txtWWith);
            this.grpWorkSize.Controls.Add(this.lblWHeight);
            this.grpWorkSize.Controls.Add(this.lblWWith);
            this.grpWorkSize.Location = new System.Drawing.Point(28, 20);
            this.grpWorkSize.Name = "grpWorkSize";
            this.grpWorkSize.Size = new System.Drawing.Size(189, 84);
            this.grpWorkSize.TabIndex = 0;
            this.grpWorkSize.TabStop = false;
            this.grpWorkSize.Text = "工作区域";
            // 
            // txtWHeight
            // 
            this.txtWHeight.Location = new System.Drawing.Point(56, 48);
            this.txtWHeight.MaxLength = 5;
            this.txtWHeight.Name = "txtWHeight";
            this.txtWHeight.Size = new System.Drawing.Size(114, 21);
            this.txtWHeight.TabIndex = 1;
            this.txtWHeight.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtWHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // txtWWith
            // 
            this.txtWWith.Location = new System.Drawing.Point(56, 20);
            this.txtWWith.MaxLength = 5;
            this.txtWWith.Name = "txtWWith";
            this.txtWWith.Size = new System.Drawing.Size(114, 21);
            this.txtWWith.TabIndex = 0;
            this.txtWWith.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtWWith.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // lblWHeight
            // 
            this.lblWHeight.AutoSize = true;
            this.lblWHeight.Location = new System.Drawing.Point(19, 52);
            this.lblWHeight.Name = "lblWHeight";
            this.lblWHeight.Size = new System.Drawing.Size(29, 12);
            this.lblWHeight.TabIndex = 4;
            this.lblWHeight.Text = "高度";
            // 
            // lblWWith
            // 
            this.lblWWith.AutoSize = true;
            this.lblWWith.Location = new System.Drawing.Point(19, 24);
            this.lblWWith.Name = "lblWWith";
            this.lblWWith.Size = new System.Drawing.Size(29, 12);
            this.lblWWith.TabIndex = 5;
            this.lblWWith.Text = "宽度";
            // 
            // grpGridSize
            // 
            this.grpGridSize.Controls.Add(this.txtGHeight);
            this.grpGridSize.Controls.Add(this.txtGWith);
            this.grpGridSize.Controls.Add(this.lblGHeight);
            this.grpGridSize.Controls.Add(this.lblGWith);
            this.grpGridSize.Location = new System.Drawing.Point(28, 110);
            this.grpGridSize.Name = "grpGridSize";
            this.grpGridSize.Size = new System.Drawing.Size(189, 84);
            this.grpGridSize.TabIndex = 1;
            this.grpGridSize.TabStop = false;
            this.grpGridSize.Text = "网格大小";
            // 
            // txtGHeight
            // 
            this.txtGHeight.Location = new System.Drawing.Point(56, 48);
            this.txtGHeight.MaxLength = 2;
            this.txtGHeight.Name = "txtGHeight";
            this.txtGHeight.Size = new System.Drawing.Size(114, 21);
            this.txtGHeight.TabIndex = 1;
            this.txtGHeight.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtGHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // txtGWith
            // 
            this.txtGWith.Location = new System.Drawing.Point(56, 20);
            this.txtGWith.MaxLength = 2;
            this.txtGWith.Name = "txtGWith";
            this.txtGWith.Size = new System.Drawing.Size(114, 21);
            this.txtGWith.TabIndex = 0;
            this.txtGWith.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtGWith.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // lblGHeight
            // 
            this.lblGHeight.AutoSize = true;
            this.lblGHeight.Location = new System.Drawing.Point(19, 52);
            this.lblGHeight.Name = "lblGHeight";
            this.lblGHeight.Size = new System.Drawing.Size(29, 12);
            this.lblGHeight.TabIndex = 4;
            this.lblGHeight.Text = "高度";
            // 
            // lblGWith
            // 
            this.lblGWith.AutoSize = true;
            this.lblGWith.Location = new System.Drawing.Point(19, 24);
            this.lblGWith.Name = "lblGWith";
            this.lblGWith.Size = new System.Drawing.Size(29, 12);
            this.lblGWith.TabIndex = 5;
            this.lblGWith.Text = "宽度";
            // 
            // tpElement
            // 
            this.tpElement.Controls.Add(this.txtLgDiameter);
            this.tpElement.Controls.Add(this.lblLgDiameter);
            this.tpElement.Controls.Add(this.txtLnWidth);
            this.tpElement.Controls.Add(this.lblLnWidth);
            this.tpElement.Location = new System.Drawing.Point(4, 22);
            this.tpElement.Name = "tpElement";
            this.tpElement.Padding = new System.Windows.Forms.Padding(3);
            this.tpElement.Size = new System.Drawing.Size(386, 214);
            this.tpElement.TabIndex = 1;
            this.tpElement.Text = "元素设置";
            this.tpElement.UseVisualStyleBackColor = true;
            // 
            // txtLgDiameter
            // 
            this.txtLgDiameter.Location = new System.Drawing.Point(87, 57);
            this.txtLgDiameter.MaxLength = 2;
            this.txtLgDiameter.Name = "txtLgDiameter";
            this.txtLgDiameter.Size = new System.Drawing.Size(100, 21);
            this.txtLgDiameter.TabIndex = 1;
            this.txtLgDiameter.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtLgDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // lblLgDiameter
            // 
            this.lblLgDiameter.AutoSize = true;
            this.lblLgDiameter.Location = new System.Drawing.Point(17, 61);
            this.lblLgDiameter.Name = "lblLgDiameter";
            this.lblLgDiameter.Size = new System.Drawing.Size(65, 12);
            this.lblLgDiameter.TabIndex = 0;
            this.lblLgDiameter.Text = "信号灯直径";
            // 
            // txtLnWidth
            // 
            this.txtLnWidth.Location = new System.Drawing.Point(87, 30);
            this.txtLnWidth.MaxLength = 2;
            this.txtLnWidth.Name = "txtLnWidth";
            this.txtLnWidth.Size = new System.Drawing.Size(100, 21);
            this.txtLnWidth.TabIndex = 1;
            this.txtLnWidth.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtLnWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TextKeyPress);
            // 
            // lblLnWidth
            // 
            this.lblLnWidth.AutoSize = true;
            this.lblLnWidth.Location = new System.Drawing.Point(29, 34);
            this.lblLnWidth.Name = "lblLnWidth";
            this.lblLnWidth.Size = new System.Drawing.Size(53, 12);
            this.lblLnWidth.TabIndex = 0;
            this.lblLnWidth.Text = "线路宽度";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(234, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(315, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 268);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSystemConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绘制及显示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSystemConfig_FormClosed);
            this.tabConfig.ResumeLayout(false);
            this.tpBasic.ResumeLayout(false);
            this.tpBasic.PerformLayout();
            this.grpWorkSize.ResumeLayout(false);
            this.grpWorkSize.PerformLayout();
            this.grpGridSize.ResumeLayout(false);
            this.grpGridSize.PerformLayout();
            this.tpElement.ResumeLayout(false);
            this.tpElement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage tpBasic;
        private System.Windows.Forms.TabPage tpElement;
        private System.Windows.Forms.GroupBox grpGridSize;
        private System.Windows.Forms.TextBox txtGHeight;
        private System.Windows.Forms.TextBox txtGWith;
        private System.Windows.Forms.Label lblGHeight;
        private System.Windows.Forms.Label lblGWith;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpWorkSize;
        private System.Windows.Forms.TextBox txtWHeight;
        private System.Windows.Forms.TextBox txtWWith;
        private System.Windows.Forms.Label lblWHeight;
        private System.Windows.Forms.Label lblWWith;
        private System.Windows.Forms.Label lblWBColor;
        private System.Windows.Forms.Label lblWBC;
        private System.Windows.Forms.Label lblGLC;
        private System.Windows.Forms.Label lblGLColor;
        private System.Windows.Forms.CheckBox ckbShowGrid;
        private System.Windows.Forms.TextBox txtLnWidth;
        private System.Windows.Forms.Label lblLnWidth;
        private System.Windows.Forms.TextBox txtLgDiameter;
        private System.Windows.Forms.Label lblLgDiameter;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using IronShoes.Properties;
using IronShoes.UserControls;

namespace IronShoes
{
    public partial class frmDrawTools : Form
    {
        /// <summary> 选择的绘制工具
        /// </summary>
        Panel _pnlSelected = null;

        /// <summary> 图片frmPicture实例
        /// </summary>
        frmPicture _fmtemp = null;

        #region
        /// <summary> 设备UserControlRailwayMon实例
        /// </summary>
        //UserControlRailwayMon userControlRailwayMon = null;
        #endregion

        #region 窗体事件
        /// <summary> 窗体构造函数
        /// </summary>
        public frmDrawTools()
        {
            InitializeComponent();
        }

        /// <summary> 窗体加载事件
        /// </summary>
        private void frmDrawTools_Load(object sender, EventArgs e)
        {
            _pnlSelected = pnlNone;
            _fmtemp = this.Owner as frmPicture;
        }

        /// <summary> 窗体鼠标进入事件
        /// </summary>
        private void frmDrawTools_MouseEnter(object sender, EventArgs e)
        {
            this.Activate();
        }

        /// <summary> 窗体鼠标点击事件
        /// </summary>
        private void frmDrawTools_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Right.Equals(e.Button))
            {
                SelectDefaultTool();
            }
        }
        #endregion

        #region 绘制工具
        /// <summary> 绘制工具内容事件
        /// </summary>
        private void pnlElement_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Panel pnltemp = sender as Panel;
            #region
            switch (pnltemp.Name)
            {
                default: { break; }

                case "pnlPath":
                    {
                        using (Pen linepen = new Pen(Color.Blue, 4))
                        {
                            e.Graphics.DrawLine(linepen, 5, pnltemp.Height / 2 - 2, pnltemp.Width - 10, pnltemp.Height / 2 - 2);
                        }
                        break;
                    }
                case "pnlFork":
                    {
                        using (Pen linepen = new Pen(Color.FromArgb(0, 255, 0), 4))
                        {
                            e.Graphics.DrawLine(linepen, 5, pnltemp.Height / 4 * 3 - 2, pnltemp.Width - 10, pnltemp.Height / 4 * 3 - 2);
                            linepen.Color = Color.FromArgb(255, 255, 0);
                            e.Graphics.DrawLine(linepen, 5, pnltemp.Height / 4 * 3 - 2, pnltemp.Width - 10, pnltemp.Height / 4 - 2);
                        }
                        break;
                    }
            }
            #endregion
        }

        /// <summary> 绘制工具点击事件
        /// </summary>
        private void pnlElement_Click(object sender, EventArgs e)
        {
            Bitmap bitcursor = new Bitmap(64, 64);
            Pen cursorpen = new Pen(Color.Black, 3);
            Panel pnltemp = sender as Panel;
            _fmtemp.InitDrawParams();
            try
            {
                using (Graphics gp = Graphics.FromImage(bitcursor))
                {
                    if (!"pnlNone".Equals(pnltemp.Name))
                    {
                        #region 绘制十字鼠标
                        gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        gp.DrawImage(Resources.Crosswise, 11.5f, 11.5f);
                        #endregion
                        switch (pnltemp.Name)
                        {
                            default: { break; }
                            case "pnlPath":
                                {
                                    using (Pen elepen = new Pen(Color.Blue, 4))
                                    {
                                        gp.DrawLine(elepen, 40, 40, 60, 40);
                                    }
                                    _fmtemp._eletype = ElementType.Path;
                                    break;
                                }
                            case "pnlFork":
                                {
                                    using (Pen linepen = new Pen(Color.FromArgb(0, 255, 0), 4))
                                    {
                                        gp.DrawLine(linepen, 36, 64 / 4 * 3 - 2, 56, 64 / 4 * 3 - 2);
                                        linepen.Color = Color.FromArgb(255, 255, 0);
                                        gp.DrawLine(linepen, 36, 64 / 4 * 3 - 2, 56, 64 / 4 * 2 - 2);
                                    }
                                     _fmtemp._eletype = ElementType.Fork;
                                    break;
                                }
                            case "pnlDSignal":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                    _fmtemp._eletype = ElementType.DSignal;
                                    break;
                                }
                            case "pnlDHSignal":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                    _fmtemp._eletype = ElementType.DHSignal;
                                    break;
                                }
                            case "pnlSSignal":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                     _fmtemp._eletype = ElementType.SSignal;
                                    break;
                                }
                            case "pnlSHSignal":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                    _fmtemp._eletype = ElementType.SHSignal;
                                    break;
                                }
                            case "pnlINode":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                    _fmtemp._eletype = ElementType.INode;
                                    break;
                                }

                            case "pnlONode":
                                {
                                    using (Bitmap temp = new Bitmap(pnltemp.BackgroundImage,
                                        pnltemp.BackgroundImage.Width * 3 / 4, pnltemp.BackgroundImage.Height * 3 / 4))
                                    {
                                        gp.DrawImage(temp, 36, 38);
                                    }
                                    _fmtemp._eletype = ElementType.ONode;
                                    break;
                                }
                        }
                        this.Cursor = new Cursor(bitcursor.GetHicon());
                        _fmtemp._wcsr = this.Cursor;
                        ((IDisposable)bitcursor).Dispose();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        _fmtemp._wcsr = this.Cursor;
                        _fmtemp._eletype = ElementType.None;
                    }
                    _pnlSelected.BackColor = Color.White;
                    _pnlSelected = pnltemp;
                    _pnlSelected.BackColor = SystemColors.Highlight;
                }
            }
            finally
            {
                ((IDisposable)bitcursor).Dispose();
                ((IDisposable)cursorpen).Dispose();
            }
        }

        /// <summary> 设置选中默认工具
        /// </summary>
        public void SelectDefaultTool()
        {
            pnlElement_Click(pnlNone, null);
        }

        /// <summary> 绘制工作Panel鼠标进入事件
        /// </summary>
        private void pnlElement_MouseEnter(object sender, EventArgs e)
        {
            Panel tmp = sender as Panel;
            if (null != tmp)
            {
                tipToolName.Show(tmp.Tag.ToString(), tmp, new Point(0, -1 * this.FontHeight - tmp.Margin.Bottom));
            }
        }

        /// <summary> 绘制工作Panel鼠标离开事件
        /// </summary>
        private void pnlElement_MouseLeave(object sender, EventArgs e)
        {
            tipToolName.Hide(sender as Panel);
        }
        #endregion
    }
}

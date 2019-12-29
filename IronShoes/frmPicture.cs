using IronShoes.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace IronShoes
{
    public partial class frmPicture : Form
    {
        #region 窗口
        /// <summary> 绘图工具窗体
        /// </summary>
        frmDrawTools _fdt = null;

        /// <summary> 绘制及显示窗体
        /// </summary>
        frmSystemConfig _fsc = null;
        #endregion

        #region 变量
        /// <summary> 工作区域
        /// </summary>
        WorkArea _wa = null;

        /// <summary> 绘图元素各类
        /// </summary>
        public ElementType _eletype = ElementType.None;

        /// <summary> 鼠标左键按下标志
        /// </summary>
        Boolean _isleftdown = false;

        /// <summary> 鼠标右键按下标志
        /// </summary>
        Boolean _isrightdown = false;

        /// <summary> 框选鼠标左键按下位置
        /// </summary>
        Point _mldpoint = Point.Empty;

        /// <summary> 鼠标在工作区域移动位置
        /// </summary>
        Point _mlmpoint = Point.Empty;

        /// <summary> 鼠标左键点击次数(用于连续绘制路段)
        /// </summary>
        Int32 _mlclick = 0;

        /// <summary> 框选区域大小
        /// </summary>
        Rectangle _selectrect = Rectangle.Empty;

        /// <summary> 打开文件
        /// </summary>
        private String _filepath = String.Empty;

        /// <summary> 文件自动保存路径
        /// </summary>
        private String _autosavepath = String.Empty;

        /// <summary> 打开文件是否改动
        /// </summary>
        private Boolean _ischange = false;

        /// <summary> 鼠标样式
        /// </summary>
        public Cursor _wcsr = null;

        /// <summary> 程序标题名称
        /// </summary>
        private String _frmtitle = "图形编辑—{0}";

        /// <summary> 元素是否移动(用于鼠标左键抬起)
        /// </summary>
        private Boolean _ismove = false;

        /// <summary> ctrl键是否按下
        /// 1、元素多选
        /// 2、信号机微调
        /// </summary>
        private Boolean _isctrl = false;

        /// <summary> shift键是否按下
        /// 1、信号机旋转
        /// </summary>
        private Boolean _isshift = false;


       //static private string filename = System.Text.RegularExpressions.Regex.Replace(Application.ExecutablePath, "\\.exe$", ".cfg", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        #endregion

        #region 窗体事件
        /// <summary> 窗体构造函数
        /// </summary>
        public frmPicture()
        {
            _wa = new WorkArea();
            InitializeComponent();
        }

        /// <summary> 窗体加载事件
        /// </summary>
        private void frmPicture_Load(object sender, EventArgs e)
        { 
            //tsmiFileNew_Click(sender, e);

            //固定绘图窗口位置
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(60,230);

            #region 清除临时文件及加载打开的文件
            _filepath = @"C:\Users\Administrator\Desktop\绘图3.cfg";

            String openfile = @"C:\Users\Administrator\Desktop\绘图3.cfg";
            _autosavepath = @"C:\Users\Administrator\Desktop\绘图3.cfg";
            if (!_filepath.Equals(openfile))
            {
                if (File.Exists(_autosavepath))
                {
                    if (DialogResult.Yes ==
                        MessageBox.Show("工作区域从错误中恢复，是否打开恢复文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        _filepath = openfile;
                        openfile = _autosavepath;
                    }
                }
                else
                {
                    _filepath = openfile;
                }
            }
            using (FileStream fs = new FileStream(openfile, FileMode.Open))
            {
                _wa.OpenWork(fs);
                fs.Close();
            }
            #endregion
            String filename = Path.GetFileName(openfile);
            //this.Text = String.Format(_frmtitle, filename.Substring(0, filename.IndexOf(".")));
            this.Text = String.Format(filename.Substring(0, filename.IndexOf(".")));
            CreateWorkArea();
            hscWScroll.Value = 0;
            vscWScroll.Value = 0;
            ScrollBar_Scroll(null, null);
            tmrAutoSave_Tick(sender, e);
            _ischange = false;
            _wa.RecordHistory(false);


            PictureBox pictureBox01;
            pictureBox01 = new System.Windows.Forms.PictureBox();
            pictureBox01.BackColor = System.Drawing.SystemColors.Control;
            pictureBox01.BackgroundImage = global::IronShoes.Properties.Resources.drawtool;
            pictureBox01.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox01.Location = new System.Drawing.Point(658, 452);
            pictureBox01.Name = "pictureBox01";
            pictureBox01.Size = new System.Drawing.Size(50, 50);
            this.Controls.Add(pictureBox01);
            pictureBox01.BringToFront();



        }

        /// <summary> 窗关闭之前事件
        /// </summary>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region
            ///* 判断如果Saved()返回DialogResult.Cancel，程序窗体停止关闭
            // */
            //if (DialogResult.Cancel == Saved())
            //{
            //    e.Cancel = true;
            //    return;
            //}
            //ClearTempFile();
            //if (null != _fdt && !_fdt.IsDisposed)
            //{
            //    ((IDisposable)_fdt).Dispose();
            //}
            //if (null != _fsc && !_fsc.IsDisposed)
            //{
            //    ((IDisposable)_fsc).Dispose();
            //}
            #endregion
        }

        /// <summary> 窗体键盘按下事件<para />
        /// 1、移动选中元素
        /// </summary>
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            _isctrl = e.Control;
            _isshift = e.Shift;
            //键盘移动绘制元素位移量
            Size offset = Size.Empty;
            /* 判断按键为前头和Delete
             */
            if ((37 <= e.KeyValue && 40 >= e.KeyValue) ||
                46 == e.KeyValue)
            {
                if (!_ischange)
                {
                    _ischange = true;
                }
            }
            else
            {
                e.Handled = true;
                return;
            }

            if (_wa.IsSelectElement)
            {
                switch (e.KeyValue)
                {
                    default: { break; }
                    case 37://←
                        {
                            offset = new Size(-_wa.GridWidth, 0);
                            _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
                            break;
                        }
                    case 38://↑
                        {
                            offset = new Size(0, -_wa.GridHeight);
                            _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
                            break;
                        }
                    case 39://→
                        {
                            offset = new Size(_wa.GridWidth, 0);
                            _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
                            break;
                        }
                    case 40://↓
                        {
                            offset = new Size(0, _wa.GridHeight);
                            _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
                            break;
                        }
                    case 46://Delete
                        {
                            _wa.DeleteElement();
                            break;
                        }
                }
                _wa.RecordHistory();
                picWorkArea.Invalidate();
            }
        }

        /// <summary> 窗体键盘抬起事件
        /// </summary>
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            _isctrl = e.Control;
            _isshift = e.Shift;
        }

        /// <summary> 窗口大小改变事件
        /// </summary>
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            /* 如果窗口最小化，退出事件
             */
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }
            CreateWorkArea();
            ScrollBar_Scroll(null, null);
        }

        /// <summary> 工作区域滚动条事件<para />
        /// 1、生成绘图区域背景和绘图图片<para />
        /// 2、绘制显示区域内的元素
        /// </summary>
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (null != picWorkArea.BackgroundImage)
            {
                ((IDisposable)picWorkArea.BackgroundImage).Dispose();
            }
            if (null != sender)
            {
                switch ((sender as Control).Name)
                {
                    default: { break; }
                    case "hscWScroll":
                        {
                            _wa.ViewSize = new Rectangle(e.NewValue, _wa.ViewSize.Location.Y, picWorkArea.Width, picWorkArea.Height);
                            break;
                        }
                    case "vscWScroll":
                        {
                            _wa.ViewSize = new Rectangle(_wa.ViewSize.Location.X, e.NewValue, picWorkArea.Width, picWorkArea.Height);
                            break;
                        }
                }
            }
            else
            {
                _wa.ViewSize = new Rectangle(hscWScroll.Value, vscWScroll.Value, picWorkArea.Width, picWorkArea.Height);
            }
            picWorkArea.BackgroundImage = _wa.CreateBackground();
            CreateDrawArea();
            using (Graphics gp = Graphics.FromImage(picWorkArea.Image))
            {
                _wa.GetDrawElement();
                _wa.DrawElement(gp, Point.Empty, ElementType.None);
            }
            if (!Point.Empty.Equals(_mldpoint) && null != sender)
            {
                switch ((sender as Control).Name)
                {
                    default: { break; }
                    case "hscWScroll":
                        {
                            _mldpoint.X += e.OldValue - e.NewValue;
                            break;
                        }
                    case "vscWScroll":
                        {
                            _mldpoint.Y += e.OldValue - e.NewValue;
                            break;
                        }
                }
            }
        }

        /// <summary> 自动保存计时器事件
        /// </summary>
        private void tmrAutoSave_Tick(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(_autosavepath))
            //{
            //    if (String.IsNullOrEmpty(_filepath))
            //    {
            //        _autosavepath = Path.GetTempPath() + @"\~temp.cfgt";
            //    }
            //}
            //if (!String.IsNullOrEmpty(_filepath))
            //{
            //    if (File.Exists(_autosavepath))
            //    {
            //        File.SetAttributes(_autosavepath, FileAttributes.Normal);
            //    }
            //}
            //Save(_autosavepath);
            //if (!String.IsNullOrEmpty(_filepath))
            //{
            //    File.SetAttributes(_autosavepath, FileAttributes.Hidden);
            //}
        }
        #endregion

        #region 工作区域事件
        /// <summary> 工作区域鼠标单击事件
        /// </summary>
        private void picWorkArea_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                //判断图形种类，绘制元素
                if (!ElementType.None.Equals(_eletype))
                {
                    if (!_ischange)
                    {
                        _ischange = true;
                    }
                    _wa.RecordHistory(true);
                    using (Graphics gp = Graphics.FromImage(picWorkArea.Image))
                    {
                        switch (_eletype)
                        {
                            default:
                                {
                                    _wa.DrawElement(gp, new Point(e.Location.X + _wa.ViewSize.X, e.Location.Y + _wa.ViewSize.Y), _eletype);
                                    _wa.RecordHistory();
                                    break;
                                }
                            case ElementType.Path:
                                {
                                    if (0 < _mlclick)
                                    {
                                        _wa.DrawElement(gp, new Point(_mldpoint.X + _wa.ViewSize.X, _mldpoint.Y + _wa.ViewSize.Y),
                                                  _eletype, new Point(_mlmpoint.X + _wa.ViewSize.X, _mlmpoint.Y + _wa.ViewSize.Y));
                                        _mldpoint = e.Location;
                                        _wa.RecordHistory();
                                    }
                                    else
                                    {
                                        _mldpoint = e.Location;
                                        _mlclick += 1;
                                    }
                                    break;
                                }
                            case ElementType.Fork:
                                {
                                    if (0 < _mlclick)
                                    {
                                        _wa.DrawElement(gp, new Point(_mldpoint.X + _wa.ViewSize.X, _mldpoint.Y + _wa.ViewSize.Y),
                                                  _eletype, new Point(_mlmpoint.X + _wa.ViewSize.X, _mlmpoint.Y + _wa.ViewSize.Y));
                                        InitDrawParams();
                                        _wa.RecordHistory();
                                    }
                                    else
                                    {
                                        _mldpoint = e.Location;
                                        _mlclick += 1;
                                    }
                                        break;
                                    }
                        }
                    }
                    picWorkArea.Invalidate();
                }
            }
            else
            {
                if (!ElementType.None.Equals(_eletype))
                {
                    _fdt.SelectDefaultTool();
                    InitDrawParams();
                    picWorkArea.Invalidate();
                }
            }
        }

        /// <summary> 工作区域鼠标移动事件
        /// </summary>
        private void picWorkArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X > picWorkArea.Width || e.Y > picWorkArea.Height)
            {
                return;
            }

            //坐标位置
            //tsslCoordinate.Text = ("X:=" + (hscWScroll.Value + e.X)).PadRight(10, ' ') +
            //    (" Y:=" + (vscWScroll.Value + e.Y)).PadRight(11, ' ');

            //MessageBox.Show(tsslCoordinate.Text);

            Point location = new Point(e.Location.X + _wa.ViewSize.X, e.Location.Y + _wa.ViewSize.Y);
            //判断图形种类为NONE
            if (ElementType.None.Equals(_eletype))
            {
                //判断鼠标左键是否按下
                if (!_isleftdown)
                {
                    if (_wa.OverElement(location))
                    {
                        MouseState();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    //判断是否有元素选中,并且鼠标样式不为箭头形或手形
                    if (_wa.IsSelectElement &&
                        (!Cursors.Default.Equals(this.Cursor) && !Cursors.Hand.Equals(this.Cursor)))
                    {
                        if (!_ischange)
                        {
                            _ischange = true;
                        }
                        _wa.MoveElement(location);
                        _ismove = true;
                        
                    }
                    //判断框选鼠标左键按下位置不为空，并且鼠标样式为箭头形
                    if (!Point.Empty.Equals(_mldpoint) && Cursors.Default.Equals(this.Cursor))
                    {
                        //判断框选鼠标左键按下位置与鼠标移动向，计算框选区域
                        if (_mldpoint.X > e.Location.X && _mldpoint.Y > e.Location.Y)
                        {
                            _selectrect.Location = e.Location;
                        }
                        else if (_mldpoint.X > e.Location.X && _mldpoint.Y < e.Location.Y)
                        {
                            _selectrect.Location = new Point(e.Location.X, _mldpoint.Y);
                        }
                        else if (_mldpoint.X < e.Location.X && _mldpoint.Y > e.Location.Y)
                        {
                            _selectrect.Location = new Point(_mldpoint.X, e.Location.Y);
                        }
                        else
                        {
                            _selectrect.Location = _mldpoint;
                        }
                        _selectrect.Width = Math.Abs(e.Location.X - _mldpoint.X);
                        _selectrect.Height = Math.Abs(e.Location.Y - _mldpoint.Y);
                    }
                    picWorkArea.Invalidate();

                    
                }
            }
            else
            {
                /* 如果绘制元素为路段或道岔
                 */
                if (ElementType.Path.Equals(_eletype) || ElementType.Fork.Equals(_eletype))
                {
                    _mlmpoint = e.Location; 
                    picWorkArea.Invalidate();
                }
            }
        }

        /// <summary> 工作区域鼠标按下事件
        /// </summary>
        private void picWorkArea_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        _isleftdown = true;
                        //判断框选鼠标左键按下位置为空，并且鼠标样式为箭头形
                        if (Point.Empty.Equals(_mldpoint) && ElementType.None.Equals(_eletype))
                        {
                            _mldpoint = e.Location;
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        _isrightdown = true;

                        //坐标位置
                        tsslCoordinate.Text = ("X:=" + (hscWScroll.Value + e.X)).PadRight(10, ' ') +
                                        (" Y:=" + (vscWScroll.Value + e.Y)).PadRight(11, ' ');

                        MessageBox.Show(tsslCoordinate.Text);

                        break;
                    }
            }
        }

        /// <summary> 工作区域鼠标抬起事件
        /// </summary>
        private void picWorkArea_MouseUp(object sender, MouseEventArgs e)
        {
            Point location = new Point(e.Location.X + _wa.ViewSize.X, e.Location.Y + _wa.ViewSize.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        #region 鼠标选中元素
                        //判断图形种类为NONE
                        if (ElementType.None.Equals(_eletype))
                        {
                            //判断鼠标样式为箭头形或手形
                            if (Cursors.Default.Equals(this.Cursor) || 
                                Cursors.Hand.Equals(this.Cursor) ||
                                _isctrl)
                            {
                                //判断框选区域大小为空
                                if (Size.Empty.Equals(_selectrect.Size))
                                {
                                    _selectrect.Location = location;
                                }
                                else
                                {
                                    _selectrect.X += hscWScroll.Value;
                                    _selectrect.Y += vscWScroll.Value;
                                }
                                CreateDrawArea();
                                using (Graphics gp = Graphics.FromImage(picWorkArea.Image))
                                {
                                    if (_wa.SelectElement(_selectrect, _isctrl))
                                    {
                                        _wa.RecordHistory();
                                    }
                                    _wa.DrawElement(gp, Point.Empty, ElementType.None);
                                }
                                picWorkArea_MouseMove(sender, e);
                            }
                            picWorkArea.Invalidate();
                        }
                        #endregion
                        #region 元素移动完成
                        if (_ismove)
                        {
                            _wa.RecordHistory();
                            _ismove = false;
                        }
                        #endregion
                        #region 清除选择框、绘制路段或道岔临时变量
                        _selectrect = Rectangle.Empty;
                        if (!ElementType.Path.Equals(_eletype) && !ElementType.Fork.Equals(_eletype))
                        {
                            _isleftdown = false;
                            _mldpoint = Point.Empty;
                        }
                        #endregion
                        break;
                    }
                case MouseButtons.Right:
                    {
                        _isrightdown = false;
                        break;
                    }
            }
        }

        /// <summary> 工作区域进入事件<para />
        /// 1、设置鼠标进入样式
        /// </summary>
        private void picWorkArea_MouseEnter(object sender, EventArgs e)
        {
            if (null != _wcsr)
            {
                this.Cursor = _wcsr;
            }
        }

        /// <summary> 工作区域离开事件<para />
        /// 1、设置鼠标离开样式
        /// </summary>
        private void picWorkArea_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /// <summary> 工作区域重绘事件
        /// </summary>
        private void picWorkArea_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //判断如果有元素选中
            if (_wa.IsSelectElement)
            {
                _wa.DrawElement(e.Graphics);
            }
            //判断框选鼠标左键按下位置不为空，绘制选择框
            if (!Point.Empty.Equals(_mldpoint))
            {
                Pen dotpen = new Pen(Color.Red, 1.5f);
                /* 如果绘制路段或道岔，绘制临时线
                 * 否则绘制框选框
                 */
                if (ElementType.Path.Equals(_eletype) || ElementType.Fork.Equals(_eletype))
                {
                    using (dotpen)
                    {
                        dotpen.DashStyle = DashStyle.Dash;
                        _mldpoint.X += hscWScroll.Value;
                        _mldpoint.Y += vscWScroll.Value;
                        _mlmpoint.X += hscWScroll.Value;
                        _mlmpoint.Y += vscWScroll.Value;
                        _mldpoint = _wa.CorrectPoint(_mldpoint);
                        _mlmpoint = _wa.CorrectPoint(_mlmpoint);
                        _mldpoint.X -= hscWScroll.Value;
                        _mldpoint.Y -= vscWScroll.Value;
                        _mlmpoint.X -= hscWScroll.Value;
                        _mlmpoint.Y -= vscWScroll.Value;
                        e.Graphics.DrawLine(dotpen, _mldpoint, _mlmpoint);
                    }
                }
                else
                {
                    using (dotpen)
                    {
                        dotpen.DashStyle = DashStyle.Dash;
                        e.Graphics.DrawRectangle(dotpen, _selectrect);
                    }
                }
            }
        }
        #endregion

        #region 工具条事件
        /// <summary> 显示绘制工具窗口
        /// </summary>
       // private void tsbtnDrawTools_Click(object sender, EventArgs e)
       // {
            //判断绘图工具窗体为NULL或已经释放
            //if (null == _fdt || _fdt.IsDisposed)
            //{
            //    _fdt = new frmDrawTools();
            //    _fdt.Location = new Point(this.Location.X + (this.Width - this.ClientSize.Width) / 2 + pnlWorkArea.Margin.Left,
            //        SystemInformation.CaptionHeight + pnlWorkArea.Location.Y + this.Location.Y +
            //        (this.Width - this.ClientSize.Width) / 2 + pnlWorkArea.Margin.Top);
            //    _fdt.Show(this);
            //}
            //else
            //{
            //    _fdt.Activate();
            //}
       // }
        #endregion

        #region 菜单事件
        #region 文件
        /// <summary> 文件新建事件
        /// </summary>
       // private void tsmiFileNew_Click(object sender, EventArgs e)
       // {
           // picWorkArea.BackgroundImage = Image.FromFile(@"C:\Users\Administrator\Desktop\绘图.png");
            /* 判断如果Saved()返回DialogResult.Cancel，文件停止新建
             */
            //if (DialogResult.Cancel == Saved())
            //{
            //    return;
            //}
            //ClearTempFile();
            ////this.Text = String.Format(_frmtitle, "新建工作区");
            //_wa.NewWork();
            //_filepath = String.Empty;
            //_autosavepath = String.Empty;
            //hscWScroll.Value = 0;
            //vscWScroll.Value = 0;
            //CreateWorkArea();
            //ScrollBar_Scroll(null, null);
            //SetAutSaveTime();
            //tmrAutoSave_Tick(sender, e);
            //_ischange = false;
            //_wa.RecordHistory(false);
     //   }

        /// <summary> 文件打开事件
        /// </summary>
       // private void tsmiFileOpen_Click(object sender, EventArgs e)
       // {
            /* 判断如果Saved()返回DialogResult.Cancel，文件停止打开
             */
            //if (DialogResult.Cancel == Saved())
            //{
            //    return;
            //}
            //if (DialogResult.OK == ofDialog.ShowDialog())
            //{
            //    #region 清除临时文件及加载打开的文件
            //    ClearTempFile();
            //    String openfile = ofDialog.FileName;
            //    _autosavepath = Path.GetDirectoryName(openfile) + @"\~" + Path.GetFileName(openfile) + "t";
            //    if (!_filepath.Equals(openfile))
            //    {
            //        if (File.Exists(_autosavepath))
            //        {
            //            if (DialogResult.Yes ==
            //                MessageBox.Show("工作区域从错误中恢复，是否打开恢复文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //            {
            //                _filepath = openfile;
            //                openfile = _autosavepath;
            //            }
            //        }
            //        else
            //        {
            //            _filepath = openfile;
            //        }
            //    }
            //    using (FileStream fs = new FileStream(openfile, FileMode.Open))
            //    {
            //        _wa.OpenWork(fs);
            //        fs.Close();
            //    }
            //    #endregion
            //    String filename = Path.GetFileName(openfile);
            //    //this.Text = String.Format(_frmtitle, filename.Substring(0, filename.IndexOf(".")));
            //    this.Text = String.Format(filename.Substring(0, filename.IndexOf(".")));
            //    CreateWorkArea();
            //    hscWScroll.Value = 0;
            //    vscWScroll.Value = 0;
            //    ScrollBar_Scroll(null, null);
            //    SetAutSaveTime();
            //    tmrAutoSave_Tick(sender, e);
            //    _ischange = false;
            //    _wa.RecordHistory(false);
            //}
      //  }

        /// <summary> 文件保存事件
        /// </summary>
        private void tsmiFileSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_filepath))
            {
                tsmiSaveAs_Click(sender, e);
            }
            else
            {
                if (Save(_filepath))
                {
                    _ischange = false;
                    MessageBox.Show("保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary> 文件另存为事件
        /// </summary>
        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == sfDialog.ShowDialog())
            {
                ClearTempFile();
                _filepath = sfDialog.FileName;
                _autosavepath = Path.GetDirectoryName(_filepath) + @"\~" + Path.GetFileName(_filepath) + "t";
                if (Save(_filepath))
                {
                    _ischange = false;
                    MessageBox.Show("保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                String filename = Path.GetFileName(_filepath);
                //this.Text = String.Format(_frmtitle, filename.Substring(0, filename.IndexOf(".")));
                this.Text = String.Format(filename.Substring(0, filename.IndexOf(".")));
                tmrAutoSave_Tick(sender, e);
                _ischange = false;
            }
        }
        #endregion

        #region 系统设置
        /// <summary> 绘制及显示事件
        /// </summary>
        private void tsmiShowSet_Click(object sender, EventArgs e)
        {
            //判断绘制及显示窗体为NULL或已经释放
            if (null == _fsc || _fsc.IsDisposed)
            {
                _fsc = new frmSystemConfig(_wa);
                _fsc.Owner = this;
                if (DialogResult.OK == _fsc.ShowDialog())
                {
                    _ischange = true;
                    _wa.RecordHistory();
                    CreateWorkArea();
                    _wa.ResetElementAll();
                    ScrollBar_Scroll(null, null);
                }
            }
        }

        #region
        /// <summary> 自动保存时间事件
        /// </summary>
        // private void tsmiAST_Click(object sender, EventArgs e)
        //{
        //ToolStripMenuItem ctltemp = sender as ToolStripMenuItem;
        //switch (_wa.AutoSaveTime)
        //{
        //    default:
        //        {
        //            tsmiAST30.Text = tsmiAST30.Text.Replace("√", "  ");
        //            break;
        //        }
        //    case 10:
        //        {
        //            tsmiAST10.Text = tsmiAST10.Text.Replace("√", "  ");
        //            break;
        //        }
        //    case 20:
        //        {
        //            tsmiAST20.Text = tsmiAST20.Text.Replace("√", "  ");
        //            break;
        //        }
        //}
        //_wa.AutoSaveTime = Convert.ToInt32(ctltemp.Tag);
        //ctltemp.Text = ctltemp.Text.Replace("  ", "√");
        //tmrAutoSave.Interval = 1000 * 60 * _wa.AutoSaveTime;
        //tmrAutoSave_Tick(sender, e);
        //if (!String.IsNullOrEmpty(_filepath))
        //{
        //    Save(_filepath);
        //}
        //}
        #endregion


        #endregion

        #region 编辑
        /// <summary> 删除事件
        /// </summary>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            frmMain_KeyDown(this, new KeyEventArgs(Keys.Delete));
        }

        /// <summary> 撤销事件
        /// </summary>
        private void tsmiBack_Click(object sender, EventArgs e)
        {
           // picWorkArea.BackgroundImage = Image.FromFile(@"C:\Users\Administrator\Desktop\绘图.png");
            Point location = Point.Empty;
            if (_wa.ToBack(out location))
            {
                HistoryWorkArea(location);
            }
        }

        /// <summary> 恢复事件
        /// </summary>
        private void tsmiRestore_Click(object sender, EventArgs e)
        {
            //picWorkArea.BackgroundImage = Image.FromFile(@"C:\Users\Administrator\Desktop\绘图.png");
            Point location = Point.Empty;
            if (_wa.Restore(out location))
            {
                HistoryWorkArea(location);
            }
        }
        #endregion
        #endregion

        #region 自定义函数
        /// <summary> 设置鼠标选中元素上的样式
        /// </summary>
        private void MouseState()
        {
            if (_wa.IsSelectElement &&
                !HoverMode.None.Equals(_wa.SelectElementHoverArea) &&
                !_isctrl)
            {
                switch (_wa.SelectElementHoverArea)
                {
                    default:
                        {
                            this.Cursor = Cursors.Default;
                            break;
                        }
                    case HoverMode.Point:
                        {
                            this.Cursor = Cursors.SizeNS;
                            break;
                        }
                    case HoverMode.Face:
                    case HoverMode.Line:
                        {
                            this.Cursor = Cursors.SizeAll;
                            break;
                        }
                    case HoverMode.Rotate:
                        {

                            //绘制旋转鼠标样式
                            using (Bitmap bmpcursor = new Bitmap(Resources.Rotate, 32, 32))
                            {
                                this.Cursor = new Cursor(bmpcursor.GetHicon());
                            }

                            break;
                        }
                }
            }
            else
            {
                this.Cursor = Cursors.Hand;
            }
        }

        #region 工作区域是否保存
        /// <summary> 工作区域是否保存
        /// </summary>
        /// <returns>提示框点击按钮值</returns>
        // private DialogResult Saved()
        //{
        //DialogResult save = DialogResult.None;
        //if (_ischange)
        //{
        //    save = MessageBox.Show("工作区域文件有更改，是否保存更改？",
        //        "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //    if (DialogResult.Yes == save)
        //    {
        //        _ischange = false;
        //        tsmiFileSave_Click(tsmiFileSave, null);
        //    }
        //}
        //return save;
        // }
        #endregion

        /// <summary> 保存工作区域
        /// </summary>
        private Boolean Save(String filepath)
        {
            Int32 rsize = 0;
            Int32 wsize = 0;
            Byte[] arrbyte = new Byte[1024];
            MemoryStream ms = _wa.SaveWork();
            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    do
                    {
                        rsize = ms.Read(arrbyte, 0, 1024);
                        if (0 != rsize)
                        {
                            fs.Write(arrbyte, 0, rsize);
                            wsize += rsize;
                            tspbSaveProgress.Value = Convert.ToInt32(wsize * 100 / ms.Length);
                            Application.DoEvents();
                        }
                    } while (0 != rsize);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                tspbSaveProgress.Value = 0;
                ms.Close();
                if (null != ms)
                {
                    ((IDisposable)ms).Dispose();
                }
            }
        }

        /// <summary> 创建工作区域<para />
        /// 设置滚动条及绘图区域大小
        /// </summary>
        private void CreateWorkArea()
        {
            Int32 largechange = 0;
            hscWScroll.SmallChange = _wa.GridWidth;
            vscWScroll.SmallChange = _wa.GridHeight;
            if (_wa.Width > pnlWorkArea.Width)
            {
                hscWScroll.Visible = true;
            }
            else
            {
                picWorkArea.Width = _wa.Width + 1;
                hscWScroll.Visible = false;
            }
            if (_wa.Height > pnlWorkArea.Height)
            {
                vscWScroll.Visible = true;
            }
            else
            {
                picWorkArea.Height = _wa.Height + 1;
                vscWScroll.Visible = false;
            }
            if (hscWScroll.Visible)
            {
                if (vscWScroll.Visible)
                {
                    picWorkArea.Width = pnlWorkArea.Width - 22;
                }
                else
                {
                    picWorkArea.Width = pnlWorkArea.Width - pnlWorkArea.Margin.All - 1;
                }
                hscWScroll.Width = picWorkArea.Width;
                largechange = Convert.ToInt32(_wa.Width * (1 - 0.618));
                hscWScroll.Maximum = _wa.Width - picWorkArea.Width + largechange;
                hscWScroll.LargeChange = largechange;
            }
            if (vscWScroll.Visible)
            {
                if (hscWScroll.Visible)
                {
                    picWorkArea.Height = pnlWorkArea.Height - 22;
                }
                else
                {
                    picWorkArea.Height = pnlWorkArea.Height - pnlWorkArea.Margin.All - 1;
                }
                vscWScroll.Height = picWorkArea.Height;
                largechange = Convert.ToInt32(_wa.Height * (1 - 0.618));
                vscWScroll.Maximum = _wa.Height - picWorkArea.Height + largechange;
                vscWScroll.LargeChange = largechange;
            }
        }

        /// <summary> 删除临时文件
        /// </summary>
        private void ClearTempFile()
        {
            if (File.Exists(_autosavepath))
            {
                if (!String.IsNullOrEmpty(_filepath))
                {
                    File.SetAttributes(_autosavepath, FileAttributes.Normal);
                }
                File.Delete(_autosavepath);
            }
        }

        #region 设置自动保存时间
        /// <summary> 设置自动保存时间
        /// </summary>
        //private void SetAutSaveTime()
        //{
        //    tsmiAST10.Text = tsmiAST10.Text.Replace("√", "  ");
        //    tsmiAST20.Text = tsmiAST20.Text.Replace("√", "  ");
        //    tsmiAST30.Text = tsmiAST30.Text.Replace("√", "  ");
        //    tmrAutoSave.Interval = 1000 * 60 * _wa.AutoSaveTime;
        //    switch (_wa.AutoSaveTime)
        //    {
        //        default:
        //            {
        //                tsmiAST30.Text = tsmiAST30.Text.Replace("  ", "√");
        //                break;
        //            }
        //        case 10:
        //            {
        //                tsmiAST10.Text = tsmiAST10.Text.Replace("  ", "√");
        //                break;
        //            }
        //        case 20:
        //            {
        //                tsmiAST20.Text = tsmiAST20.Text.Replace("  ", "√");
        //                break;
        //            }
        //    }
        //}
        #endregion

        /// <summary> 清除绘制路段和道岔使用变量.
        /// </summary>
        public void InitDrawParams()
        {
            _isleftdown = false;
            _mldpoint = Point.Empty;
            _mlmpoint = Point.Empty;
            _mlclick = 0;
        }

        /// <summary> 创建绘制图片区域
        /// </summary>
        private void CreateDrawArea()
        {
            if (null != picWorkArea.Image)
            {
                ((IDisposable)picWorkArea.Image).Dispose();
            }
            picWorkArea.Image = new Bitmap(picWorkArea.Width, picWorkArea.Height);

        }

        /// <summary> 显示历史工作空间
        /// </summary>
        /// <param name="location">滚动条位置</param>
        private void HistoryWorkArea(Point location)
        {
            CreateWorkArea();
            hscWScroll.Value = location.X;
            vscWScroll.Value = location.Y;
            ScrollBar_Scroll(null, null);


         
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronShoes.data;
using MySql.Data.MySqlClient;


namespace IronShoes.UserControls
{
    public partial class UserControlRailwayMon : UserControl
    {
        public UserControlRailwayMon()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 窗口
        /// </summary>
        //设备上道
         UserControlTernOnTheWay userControlTernOnTheWay = null;

        //图片窗口
        frmPicture picture = null;

        #region
        ///// <summary> 工作区域
        ///// </summary>
        //WorkArea _wa = null;

        ///// <summary> 绘图元素各类
        ///// </summary>
        //public ElementType _eletype = ElementType.None;

        ///// <summary> 鼠标样式
        ///// </summary>
        //public Cursor _wcsr = null;

        ///// <summary> 鼠标左键按下标志
        ///// </summary>
        //Boolean _isleftdown = false;

        ///// <summary> 鼠标右键按下标志
        ///// </summary>
        //Boolean _isrightdown = false;

        ///// <summary> 框选鼠标左键按下位置
        ///// </summary>
        //Point _mldpoint = Point.Empty;

        ///// <summary> 鼠标在工作区域移动位置
        ///// </summary>
        //Point _mlmpoint = Point.Empty;

        ///// <summary> 鼠标左键点击次数(用于连续绘制路段)
        ///// </summary>
        //Int32 _mlclick = 0;

        ///// <summary> 打开文件是否改动
        ///// </summary>
        //private Boolean _ischange = false;

        ///// <summary> ctrl键是否按下
        ///// 1、元素多选
        ///// 2、信号机微调
        ///// </summary>
        //private Boolean _isctrl = false;

        ///// <summary> shift键是否按下
        ///// 1、信号机旋转
        ///// </summary>
        //private Boolean _isshift = false;
        #endregion

        private void UserControlRailwayMon_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;

            //获取当前日期
            string week = "";
            switch(today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break; 
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
             this.labelToday.Text = week + "   " + today.ToString("yyyy年MM月dd日");


            //自动增加时间
            Timer timer = new Timer();
            timer.Tick += new EventHandler(labelTime_Click);
            timer.Enabled = true;
            timer.Interval = 1000;
            
            //上道铁鞋
            string inRegionCount = GlobalDefine.s_dbGateway.GetInRegionCount().ToString();
            //
            //MessageBox.Show(inRegionCount);
            labelWayIronShoesCount.Text = inRegionCount;

            //下道铁鞋
            string nextIronShoesCount = GlobalDefine.s_dbGateway.GetNextIronShoesCount().ToString();
            //
            //MessageBox.Show(NextIronShoesCount);
            labelNextIronShoesCount.Text = nextIronShoesCount;

            //入柜铁鞋
            string intoIronShoesCount = GlobalDefine.s_dbGateway.GetIntoIronShoesCount().ToString();
            //MessageBox.Show(intoIronShoesCount);
            labelIntoIronShoesCount.Text = intoIronShoesCount;

            //故障铁鞋
            string faultIronShoesCount = GlobalDefine.s_dbGateway.GetFaultIronShoesCount().ToString();
            labelFaultIronShoesCount.Text = faultIronShoesCount;

            //显示图片窗口
            #region
            //try
            //{
            //    picture = new frmPicture();
            //    picture.Show(this);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    //MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    // MessageBox.Show("显示成功！");
            //    Console.WriteLine();
            //}
            #endregion
            #region 
            picture = new frmPicture();
            picture.Dock = DockStyle.Fill;
            picture.TopLevel = false;
            picture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            picWorkArea.Controls.Add(picture);
            picture.Show();
            #endregion

            // picWorkArea.BackgroundImage = Image.FromFile(@"C:\Users\Administrator\Desktop\绘图.png");

            //创建绘图区域
            // _wa = new WorkArea();

        }

        //设备上道
        private void btn_ternOnTheWay_Click(object sender, EventArgs e)
        {
            //弹出弹出框
            userControlTernOnTheWay = new UserControlTernOnTheWay();
            userControlTernOnTheWay.Show();
        }

        
        private void labelTime_Click(object sender, EventArgs e)
        {
            //获取当前时间
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");

        }
        
        //弹出绘制窗口
        private void btnpicture_Click_1(object sender, EventArgs e)
        {
            //frmDrawTools frmDrawTools = new frmDrawTools();
            //frmDrawTools.Show();
        }

        #region
        /// <summary> 窗体键盘按下事件<para />
        /// 1、移动选中元素
        /// </summary>
        private void UserControlRailwayMon_KeyDown(object sender, KeyEventArgs e)
        {
            //_isctrl = e.Control;
            //_isshift = e.Shift;
            ////键盘移动绘制元素位移量
            //Size offset = Size.Empty;
            ///* 判断按键为前头和Delete
            // */
            //if ((37 <= e.KeyValue && 40 >= e.KeyValue) ||
            //    46 == e.KeyValue)
            //{
            //    if (!_ischange)
            //    {
            //        _ischange = true;
            //    }
            //}
            //else
            //{
            //    e.Handled = true;
            //    return;
            //}

            //if (_wa.IsSelectElement)
            //{
            //    switch (e.KeyValue)
            //    {
            //        default: { break; }
            //        case 37://←
            //            {
            //                offset = new Size(-_wa.GridWidth, 0);
            //                _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
            //                break;
            //            }
            //        case 38://↑
            //            {
            //                offset = new Size(0, -_wa.GridHeight);
            //                _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
            //                break;
            //            }
            //        case 39://→
            //            {
            //                offset = new Size(_wa.GridWidth, 0);
            //                _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
            //                break;
            //            }
            //        case 40://↓
            //            {
            //                offset = new Size(0, _wa.GridHeight);
            //                _wa.MoveElementTo(offset, new Object[] { _isctrl, _isshift });
            //                break;
            //            }
            //        case 46://Delete
            //            {
            //                _wa.DeleteElement();
            //                break;
            //            }
            //    }
            //    _wa.RecordHistory();
            //    picWorkArea.Invalidate();
            //}
        }

        /// <summary> 窗体键盘抬起事件
        /// </summary>
        private void UserControlRailwayMon_KeyUp(object sender, KeyEventArgs e)
        {
            //_isctrl = e.Control;
            //_isshift = e.Shift;
        }

        /// <summary> 窗口大小改变事件
        /// </summary>
        private void UserControlRailwayMon_SizeChanged(object sender, EventArgs e)
        {
            /* 如果窗口最小化，退出事件
            */
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    return;
            //}
            //CreateWorkArea();
            //ScrollBar_Scroll(null, null);
        }

        #endregion

    }
}

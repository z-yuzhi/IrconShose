using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 信号机
    /// </summary>
    [Serializable]
    class DSignal : SignalElements
    {
        /// <summary> 白色信号灯
        /// </summary>
        private Light _whilelight = null;
        /// <summary> 白色信号灯
        /// </summary>
        private Light WhileLight
        {
            get { return _whilelight; }
            set { _whilelight = value; }
        }

        /// <summary> 红色信号灯
        /// </summary>
        private Light _redlight = null;
        /// <summary> 红色信号灯
        /// </summary>
        private Light RedLight
        {
            get { return _redlight; }
            set { _redlight = value; }
        }

        /// <summary> 构造函数
        /// </summary>
        public DSignal()
            : base()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数
        /// </summary>
        public DSignal(Point drawpoint)
        {
            InitializeComponent();
            DrawPoint = drawpoint;
        }

        /// <summary> 初始化类变量
        /// </summary>
        private void InitializeComponent()
        {
            _iscorrect = false;
            Style.BackColor = Color.FromArgb(0, 128, 255);
            Style.BorderColor = Color.FromArgb(177, 136, 223); Style.Color = Color.Black;
            Style.Color = Color.Black;
            _whilelight = new Light();
            _whilelight.Style.BackColor = Color.White;
            _redlight = new Light();
            _redlight.Style.BackColor = Color.Red;
        }

        /// <summary> 获取原始图片
        /// </summary>
        protected override Bitmap SourceImage
        {
            get
            {
                if (null != _sourceimage)
                {
                    ((IDisposable)_sourceimage).Dispose();
                }
                Bitmap temp = new Bitmap(LightDiameter * 2 + 1, LightDiameter + 1);
                _whilelight.Diameter = LightDiameter;
                _redlight.Diameter = LightDiameter;
                using (Graphics gp = Graphics.FromImage(temp))
                {
                    _whilelight.Draw(gp, new Point(0, 0));
                    _redlight.Draw(gp, new Point(LightDiameter, 0));
                }
                Bitmap source = new Bitmap(temp.Width + 8, temp.Height + 8);
                using (Graphics gp = Graphics.FromImage(source))
                {
                    gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Style.Color, 2);
                    gp.DrawImage(temp, 3, 4);
                    gp.DrawLine(pen, 3 + temp.Width, 3, 3 + temp.Width, source.Height - 3);
                    ((IDisposable)pen).Dispose();
                }
                ((IDisposable)temp).Dispose();
                return source;
            }
        }
    }
}

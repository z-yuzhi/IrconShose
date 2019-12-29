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
    class SSignal : SignalElements
    {

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
        public SSignal()
            : base()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数
        /// </summary>
        public SSignal(Point drawpoint)
        {
            InitializeComponent();
            DrawPoint = drawpoint;
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
                Bitmap temp = new Bitmap(LightDiameter + 1, LightDiameter + 1);
                _redlight.Diameter = LightDiameter;
                using (Graphics gp = Graphics.FromImage(temp))
                {
                    _redlight.Draw(gp, new Point(0, 0));
                }
                Bitmap source = new Bitmap(temp.Width + 8, temp.Height + 8);
                using (Graphics gp = Graphics.FromImage(source))
                {
                    gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Color.Black, 2);
                    gp.DrawImage(temp, 3, 4);
                    gp.DrawLine(pen, 3 + temp.Width, 3, 3 + temp.Width, source.Height - 3);
                    ((IDisposable)pen).Dispose();
                }
                ((IDisposable)temp).Dispose();
                return source;
            }
        }

        /// <summary> 初始化类变量
        /// </summary>
        private void InitializeComponent()
        {
            _iscorrect = false;
            Style.BackColor = Color.FromArgb(0, 128, 255);
            Style.BorderColor = Color.FromArgb(177, 136, 223);
            Style.Color = Color.Black;
            _redlight = new Light();
            _redlight.Style.BackColor = Color.Red;
        }
    }
}

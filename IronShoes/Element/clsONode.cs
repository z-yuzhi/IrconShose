using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 绝缘节
    /// </summary>
    [Serializable]
    class ONode : ImageElements
    {
        /// <summary> 构造函数
        /// </summary>
        public ONode()
            : base()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数
        /// </summary>
        public ONode(Point drawpoint)
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
                Bitmap source = new Bitmap(16, 16);
                Bitmap temp = new Bitmap(source.Width + 4, source.Height + 4);
                using (Graphics gp = Graphics.FromImage(temp))
                {
                    Pen pen = new Pen(Color.FromArgb(0, 255, 255), 2);
                    gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gp.DrawLine(pen, temp.Width / 2, 1, temp.Width / 2, temp.Height - 1);
                    gp.DrawLine(pen, 1, temp.Height / 2, temp.Width - 1, temp.Height / 2);
                    gp.DrawEllipse(pen, 2, 2, source.Width, source.Height);
                    ((IDisposable)pen).Dispose();
                }
                using (Graphics gp = Graphics.FromImage(source))
                {
                    temp = new Bitmap(temp, source.Width - 2, source.Height - 2);
                    gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gp.DrawImage(temp, 1, 1);
                }
                ((IDisposable)temp).Dispose();
                return source;
            }
        }

        /// <summary> 初始化类变量
        /// </summary>
        private void InitializeComponent()
        {
            _iscorrect = true;
            _canrotate = false;
        }
    }
}

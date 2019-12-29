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
    class INode : ImageElements
    {
        /// <summary> 构造函数
        /// </summary>
        public INode()
            : base()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数
        /// </summary>
        public INode(Point drawpoint)
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
                using (Graphics gp = Graphics.FromImage(source))
                {
                    gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Color.FromArgb(0, 255, 255), 2f);
                    gp.DrawLine(pen, source.Width / 2, 2, source.Width / 2, source.Height - 2);
                    ((IDisposable)pen).Dispose();
                }
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

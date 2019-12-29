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
    abstract class SignalElements : ImageElements
    {
        public SignalElements()
            : base()
        {

        }

        private Int32 _lightdiameter = 12;
        /// <summary> 信息灯直径
        /// </summary>
        public Int32 LightDiameter
        {
            get { return _lightdiameter; }
            set
            {
                _lightdiameter = value;
                _isdraw = true;
                if (null != _sourceimage)
                {
                    ((IDisposable)_sourceimage).Dispose();
                    _sourceimage = null;
                }
            }
        }

        /// <summary> 信号灯
        /// </summary>
        [Serializable]
        protected class Light
        {
            public Light() { }

            /// <summary> 灯直径
            /// </summary>
            private Int32 _diameter = 0;
            /// <summary> 灯直径
            /// </summary>
            public Int32 Diameter
            {
                get { return _diameter; }
                set { _diameter = value; }
            }

            /// <summary> 绘制样式
            /// </summary>
            private Style _style = null;
            /// <summary> 绘制样式
            /// </summary>
            public Style Style
            {
                get
                {
                    if (null == _style)
                    {
                        _style = new Style();
                        _style.BorderColor = Color.Black;
                        _style.LineHeight = 1;
                    }
                    return _style;
                }
                set { _style = value; }
            }

            /// <summary> 信号灯绘制
            /// </summary>
            /// <param name="gp">绘图图面实例</param>
            /// <param name="location">绘制位置</param>
            public void Draw(Graphics gp, Point location)
            {
                gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                SolidBrush brush = new SolidBrush(_style.BackColor);
                Pen pen = new Pen(_style.BorderColor);
                gp.FillEllipse(brush, location.X, location.Y, _diameter, _diameter);
                gp.DrawEllipse(pen, location.X, location.Y, _diameter, _diameter);
                ((IDisposable)brush).Dispose();
                ((IDisposable)pen).Dispose();
            }
        }
    }
}

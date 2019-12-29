using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 样式类
    /// </summary>
    [Serializable]
    public class Style
    {
        /// <summary> 边框颜色
        /// </summary>
        private Color _bordercolor = Color.Empty;
        /// <summary> 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; }
        }

        /// <summary> 背景颜色
        /// </summary>
        private Color _backcolor = Color.Empty;
        /// <summary> 背景颜色
        /// </summary>
        public Color BackColor
        {
            get { return _backcolor; }
            set { _backcolor = value; }
        }

        /// <summary> 前景颜色
        /// </summary>
        private Color _color = Color.Empty;
        /// <summary> 前景颜色
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary> 画线高度
        /// </summary>
        private Single _lineheight = 0;
        /// <summary> 画线高度
        /// </summary>
        public Single LineHeight
        {
            get { return _lineheight; }
            set { _lineheight = value; }
        }
    }
}

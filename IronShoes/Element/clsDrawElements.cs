using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 绘画元素
    /// </summary>
    [Serializable]
    abstract class DrawElements : Elements
    {
        #region 属性
        private Int32 _index;
        /// <summary> 序号
        /// </summary>
        public Int32 Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private String _stationname;
        /// <summary> 站场名称
        /// </summary>
        public String StationName
        {
            get { return _stationname; }
            set { _stationname = value; }
        }

        private String _stationindex;
        /// <summary> 站场序号
        /// </summary>
        public String StationIndex
        {
            get { return _stationindex; }
            set { _stationindex = value; }
        }
        #endregion

        /// <summary> 绘制线段端点
        /// </summary>
        /// <param name="gp">绘图图面实例</param>
        /// <param name="cappoint">端点坐标</param>
        /// <param name="width">线段宽度</param>
        public void DrawCap(Graphics gp, Point cappoint, Single width)
        {
            //端点圆形直径
            Single d = 4;
            if (1 != width)
            {
                d = width + 2;
            }
            //圆形绘制偏移量
            Single offset = d / 2.0f;
            gp.FillEllipse(Brushes.White, cappoint.X - offset, cappoint.Y - offset, d, d);
            gp.DrawEllipse(Pens.Black, cappoint.X - offset, cappoint.Y - offset, d, d);
        }
    }
}

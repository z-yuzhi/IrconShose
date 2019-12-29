using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 元素
    /// </summary>
    [Serializable]
    public abstract class Elements : ICloneable
    {
        #region 属性  
        protected HoverMode _hoverarea = HoverMode.None;
        /// <summary> 选中方式
        /// </summary>
        public HoverMode HoverArea
        {
            get { return _hoverarea; }
            //set { _selected = value; }
        }

        private Boolean _selected = false;
        /// <summary> 是否选中
        /// </summary>
        public virtual Boolean Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        /// <summary> 元素移动历史坐标点
        /// </summary>
        protected Point? _old_muslocation = null;

        /// <summary> 元素是否吸附网格
        /// </summary>
        protected Boolean _iscorrect = true;
        /// <summary> 元素是否吸附网格
        /// </summary>
        public Boolean IsCorrect
        {
            get { return _iscorrect; }
        }
        #endregion

        #region 抽象函数
        /// <summary> 绘制元素
        /// </summary>
        /// <param name="gp">绘图图面实例</param>
        /// <param name="offsetlocation">绘制偏移坐标</param>
        public abstract void Draw(Graphics gp, Point offsetlocation);

        /// <summary> 鼠标是否覆盖
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        /// <returns>覆盖返回true,否则返回false</returns>
        public abstract Boolean MouseOver(Point muslocation);

        /// <summary> 鼠标移动元素
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        public abstract Boolean MouseMove(Point muslocation, Boolean multiple);

        /// <summary> 鼠标是否选中
        /// </summary>
        /// <param name="rect">鼠标选择区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public abstract Boolean MouseSelected(Rectangle rect);

        /// <summary> 是否可以移动
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public abstract Boolean IsMove(Rectangle rect);

        /// <summary> 是否在工作区域绘制
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public abstract Boolean IsShow(Rectangle rect);
        #endregion

        /// <summary> 校验鼠标是否进入线点
        /// </summary>
        /// <param name="checkpoint">校验点坐标</param>
        /// <param name="muslocation">鼠标位置坐</param>
        /// <returns>进入返回true，否则返回false</returns>
        protected Boolean PointOver(Point checkpoint, Point muslocation)
        {
            if ((muslocation.X > checkpoint.X - 2 && muslocation.X < checkpoint.X + 2 &&
                 muslocation.Y > checkpoint.Y - 2 && muslocation.Y < checkpoint.Y + 2))
            {
                return true;
            }
            return false;
        }

        /// <summary> 校验鼠标是否进入线段
        /// </summary>
        /// <param name="startpoint">起始点</param>
        /// <param name="endpoint">结束点</param>
        /// <param name="muslocation">鼠标位置坐</param>
        /// <returns>进入返回true，否则返回false</returns>
        protected Boolean LineOver(Point startpoint, Point endpoint, Point muslocation)
        {
            Int32 minpoint = startpoint.X;
            Int32 maxpoint = endpoint.X;
            Boolean flagX = false;
            Boolean flagY = false;
            if (minpoint > maxpoint)
            {
                minpoint = endpoint.X;
                maxpoint = startpoint.X;
            }

            if (minpoint <= muslocation.X && muslocation.X <= maxpoint)
            {
                flagX = true;
            }

            if (startpoint.Y == endpoint.Y && flagX && 2 >= Math.Abs(muslocation.Y - startpoint.Y))
            {
                _hoverarea = HoverMode.Line;
                return true;
            }

            minpoint = startpoint.Y;
            maxpoint = endpoint.Y;
            if (minpoint > maxpoint)
            {
                minpoint = endpoint.Y;
                maxpoint = startpoint.Y;
            }
            if (minpoint <= muslocation.Y && muslocation.Y <= maxpoint)
            {
                flagY = true;
            }

            if (startpoint.X == endpoint.X && flagY && 2 >= Math.Abs(muslocation.X - startpoint.X))
            {
                _hoverarea = HoverMode.Line;
                return true;
            }

            if (flagX && flagY)
            {
                Double dblele = Math.Sqrt(Math.Pow(startpoint.X - endpoint.X, 2) + Math.Pow(startpoint.Y - endpoint.Y, 2));
                Double dblmsu = Math.Sqrt(Math.Pow(startpoint.X - muslocation.X, 2) + Math.Pow(startpoint.Y - muslocation.Y, 2));
                Double dblangle = Math.Abs(Math.Asin(Math.Abs(startpoint.X - endpoint.X) / dblele) - Math.Asin(Math.Abs(startpoint.X - muslocation.X) / dblmsu));
                Double dbllength = dblmsu * Math.Sin(dblangle);
                if (2 >= dbllength)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> 校验鼠标是否进入线段
        /// </summary>
        /// <param name="startpoint">起始点</param>
        /// <param name="endpoint">结束点</param>
        /// <param name="muslocation">鼠标位置坐</param>
        /// <returns>进入返回true，否则返回false</returns>
        protected Boolean LineOver(Point startpoint, Point endpoint, Rectangle rect)
        {
            Point point1 = rect.Location;
            Point point2 = new Point(rect.X + rect.Width, rect.Y);
            Point point3 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            Point point4 = new Point(rect.X, rect.Y + rect.Height);
            #region Top
            if (LineIntersect(startpoint, endpoint, point1, point2))
            {
                return true;
            }
            #endregion

            #region right
            if (LineIntersect(startpoint, endpoint, point2, point3))
            {
                return true;
            }
            #endregion

            #region Bottom
            if (LineIntersect(startpoint, endpoint, point3, point4))
            {
                return true;
            }
            #endregion

            #region left
            if (LineIntersect(startpoint, endpoint, point1, point4))
            {
                return true;
            }
            #endregion

            if (PointInRectangle(startpoint, rect))
            {
                return true;
            }

            if (PointInRectangle(endpoint, rect))
            {
                return true;
            }
            return false;
        }

        /// <summary> 判断两条线段相交
        /// </summary>
        /// <param name="lx1">The LX1.</param>
        /// <param name="ly1">The ly1.</param>
        /// <param name="lx2">The LX2.</param>
        /// <param name="ly2">The ly2.</param>
        /// <returns>Boolean.</returns>
        Boolean LineIntersect(Point lx1, Point ly1, Point lx2, Point ly2)
        {
            if (Math.Max(lx1.X, ly1.X) < Math.Min(lx2.X, ly2.X))
            {
                return false;
            }
            if (Math.Max(lx1.Y, ly1.Y) < Math.Min(lx2.Y, ly2.Y))
            {
                return false;
            }
            if (Math.Max(lx2.X, ly2.X) < Math.Min(lx1.X, ly1.X))
            {
                return false;
            }
            if (Math.Max(lx2.Y, ly2.Y) < Math.Min(lx1.Y, ly1.Y))
            {
                return false;
            }
            if (Multiply(lx2, ly1, lx1) * Multiply(ly1, ly2, lx1) < 0)
            {
                return false;
            }
            if (Multiply(lx1, ly2, lx2) * Multiply(ly2, ly1, lx2) < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary> 判断点在矩形区域呢
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="rect">The rect.</param>
        /// <returns>Boolean.</returns>
        protected Boolean PointInRectangle(Point location, Rectangle rect)
        {
            Point point1 = rect.Location;
            Point point2 = new Point(rect.X + rect.Width, rect.Y);
            Point point3 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            Point point4 = new Point(rect.X, rect.Y + rect.Height);
            if (Multiply(location, point1, point2) * Multiply(location, point4, point3) <= 0 &&
                Multiply(location, point4, point1) * Multiply(location, point3, point2) <= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary> 移动元素
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns>选中返回true，否则返回false</returns>
        public void KeyMove(Size offset)
        {
            _old_muslocation = Point.Empty;
            MouseMove(new Point(offset.Width, offset.Height), true);
        }

        /// <summary> 鼠标位置与图形顶点向量叉乘
        /// </summary>
        /// <param name="p1">线段顶点</param>
        /// <param name="p2">线段顶点</param>
        /// <param name="p0">线外一点</param>
        /// <returns>Double.</returns>
        protected Double Multiply(Point p1, Point p2, Point p0)
        {
            return ((p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y));
        }

        #region ICloneable 成员
        public Object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}

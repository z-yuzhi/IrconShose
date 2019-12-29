using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    [Serializable]
    class ElePath : DrawElements
    {
        /// <summary> 选中部位枚举
        /// </summary>
        enum SelectedMode
        {
            None,
            StartPoint,
            EndPoint,
            Line
        }

        /// <summary> 构造函数
        /// </summary>
        public ElePath()
            : base()
        { }

        /// <summary> 构造函数
        /// </summary>
        public ElePath(Point startpoint, Point endpoint)
        {
            _startpoint = startpoint;
            _endpoint = endpoint;
        }


        /// <summary> 起始点
        /// </summary>
        private Point _startpoint;
        /// <summary> 起始点
        /// </summary>
        public Point StartPoint
        {
            get { return _startpoint; }
            set { _startpoint = value; }
        }

        /// <summary> 结束点
        /// </summary>
        private Point _endpoint;
        /// <summary> 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return _endpoint; }
            set { _endpoint = value; }
        }

        /// <summary> 选中部位
        /// </summary>
        private SelectedMode _selectedmodel = SelectedMode.None;

        /// <summary> 绘制元素
        /// </summary>
        /// <param name="gp">绘图图面实例</param>
        /// <param name="offsetlocation">绘制偏移坐标</param>
        public override void Draw(Graphics gp, Point offsetlocation)
        {
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (Color.Empty.Equals(Style.Color))
            {
                Style.Color = Color.Blue;
            }
            Pen pathpen = new Pen(Style.Color);
            pathpen.Width = Style.LineHeight;
            using (pathpen)
            {
                if (Selected)
                {
                    pathpen.Color = Color.Red;
                }
                Point start = new Point(StartPoint.X - offsetlocation.X, StartPoint.Y - offsetlocation.Y);
                Point end = new Point(EndPoint.X - offsetlocation.X, EndPoint.Y - offsetlocation.Y);
                gp.DrawLine(pathpen, start, end);
                #region 线段端点
                DrawCap(gp, start, pathpen.Width);
                DrawCap(gp, end, pathpen.Width);
                #endregion
            }
        }

        /// <summary> 绘制样式
        /// </summary>
        private Style _style = new Style();
        /// <summary> 绘制样式
        /// </summary>
        public virtual Style Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary> 鼠标是否覆盖
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        /// <returns>覆盖返回true,否则返回false</returns>
        public override Boolean MouseOver(Point muslocation)
        {
            _old_muslocation = null;
            if (PointOver(StartPoint, muslocation))
            {
                _hoverarea = HoverMode.Point;
                _selectedmodel = SelectedMode.StartPoint;
                return true;
            }

            if (PointOver(EndPoint, muslocation))
            {
                _hoverarea = HoverMode.Point;
                _selectedmodel = SelectedMode.EndPoint;
                return true;
            }

            if (LineOver(StartPoint, EndPoint, muslocation))
            {
                _hoverarea = HoverMode.Line;
                _selectedmodel = SelectedMode.Line;
                return true;
            }
            _hoverarea = HoverMode.None;
            _selectedmodel = SelectedMode.None;
            return false;
        }

        /// <summary> 鼠标移动元素
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        public override Boolean MouseMove(Point muslocation, Boolean multiple)
        {
            if (multiple)
            {
                _selectedmodel = SelectedMode.Line;
            }
            switch (_selectedmodel)
            {
                default: { break; }
                case SelectedMode.StartPoint:
                    {
                        StartPoint = muslocation;
                        break;
                    }
                case SelectedMode.EndPoint:
                    {
                        EndPoint = muslocation;
                        break;
                    }
                case SelectedMode.Line:
                    {
                        if (null == _old_muslocation)
                        {
                            _old_muslocation = muslocation;
                        }
                        else
                        {
                            Int32 move_x = muslocation.X - _old_muslocation.Value.X;
                            Int32 move_y = muslocation.Y - _old_muslocation.Value.Y;
                            StartPoint = new Point(StartPoint.X + move_x, StartPoint.Y + move_y);
                            EndPoint = new Point(EndPoint.X + move_x, EndPoint.Y + move_y);
                            _old_muslocation = muslocation;
                        }
                        break;
                    }
            }
            return true;
        }

        /// <summary> 鼠标是否选中
        /// </summary>
        /// <param name="rect">鼠标选择区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public override Boolean MouseSelected(Rectangle rect)
        {
            if (Size.Empty.Equals(rect.Size))
            {
                if (MouseOver(rect.Location))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (IsShow(rect))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary> 是否可以移动
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public override bool IsMove(Rectangle rect)
        {
            Boolean flag = PointInRectangle(StartPoint, rect);
            flag &= PointInRectangle(EndPoint, rect);
            return flag;
        }

        /// <summary> 是否在工作区域绘制
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public override bool IsShow(Rectangle rect)
        {
            if (LineOver(StartPoint, EndPoint, rect))
            {
                return true;
            }
            return false;
        }
    }
}

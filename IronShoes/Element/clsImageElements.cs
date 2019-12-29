using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IronShoes
{
    /// <summary> 图片元素
    /// </summary>
    [Serializable]
    abstract class ImageElements : Elements
    {
        /// <summary> 选中部位枚举
        /// </summary>
        enum SelectedMode
        {
            None,
            Face,
            Point
        }

        #region 属性
        /// <summary> 原始图片
        /// </summary>
        protected Bitmap _sourceimage = null;
        /// <summary> 原始图片
        /// </summary>
        protected virtual Bitmap SourceImage
        {
            get { return _sourceimage; }
        }

        /// <summary> 显示图片
        /// </summary>
        private Bitmap _showimage = null;
        /// <summary> 显示图片
        /// </summary>
        public Bitmap ShowImage
        {
            get
            {
                if (null == _showimage ||
                    _isdraw)
                {
                    Rotate();
                }
                return _showimage;
            }
            //set { _image = value; }
        }

        /// <summary> 是否旋转
        /// </summary>
        protected Boolean _isdraw = false;

        /// <summary> 旋转角度
        /// </summary>
        private Int32 _rotateangle = 0;
        /// <summary> 旋转角度
        /// </summary>
        public Int32 RotateAngle
        {
            get { return _rotateangle; }
            set
            {
                _isdraw = true;
                _rotateangle = value;
            }
        }

        /// <summary> 原图宽度
        /// </summary>
        public Int32 SourceWidth
        {
            get
            {
                if (null != _sourceimage)
                {
                    return _sourceimage.Width;
                }
                return 0;
            }
        }

        /// <summary> 原图高度
        /// </summary>
        public Int32 SourceHeight
        {
            get
            {
                if (null != _sourceimage)
                {
                    return _sourceimage.Height;
                }
                return 0;
            }
        }

        /// <summary> 旋转直径
        /// </summary>
        private Int32 _diameter = 0;
        /// <summary> 旋转直径
        /// </summary>
        public Int32 Diameter
        {
            get
            {
                if (0 != _diameter)
                {
                    return _diameter;
                }
                _diameter = Convert.ToInt32(Math.Sqrt(Math.Pow(SourceWidth, 2) + Math.Pow(SourceHeight, 2)));
                return _diameter;
            }
        }

        /// <summary> 旋转半径
        /// </summary>
        private Int32 _radius = 0;
        /// <summary> 旋转半径
        /// </summary>
        public Int32 Radius
        {
            get
            {
                if (0 != _radius)
                {
                    return _radius;
                }
                _radius = Diameter / 2;
                return _radius;
            }
        }

        /// <summary> 左上顶点旋转角度
        /// </summary>
        private Int32 _ltangle = 0;
        /// <summary> 左上顶点旋转角度
        /// </summary>
        public Int32 ltAngle
        {
            get
            {
                if (null != _sourceimage)
                {
                    if (0 != _ltangle)
                    {
                        return _ltangle;
                    }
                    else
                    {
                        //左上角顶点弧度
                        Double radian = Math.Asin((SourceHeight / 2.0) / Radius);
                        //X轴夹角
                        return Convert.ToInt32(radian * 180 / Math.PI);
                    }
                }
                return 0;
            }
        }

        /// <summary> 右上顶点旋转角度
        /// </summary>
        public Int32 rtAngle
        {
            get { return 180 - ltAngle; }
        }

        /// <summary> 右下顶点旋转角度
        /// </summary>
        public Int32 rbAngle
        {
            get { return 180 + ltAngle; }
        }

        /// <summary> 左下顶点旋转角度
        /// </summary>
        public Int32 lbAngle
        {
            get { return 360 - ltAngle; }
        }

        /// <summary> 左上顶点坐标偏移量
        /// </summary>
        public Point ltOffsetPoint
        {
            get
            {
                Double radian = (RotateAngle + ltAngle) * Math.PI / 180.0;
                return new Point(Convert.ToInt32(Radius * Math.Cos(radian)), Convert.ToInt32(Radius * Math.Sin(radian)));
            }
        }

        /// <summary> 右上顶点坐标偏移量
        /// </summary>
        public Point rtOffsetPoint
        {
            get
            {
                Double radian = (RotateAngle + rtAngle) * Math.PI / 180.0;
                return new Point(Convert.ToInt32(Radius * Math.Cos(radian)), Convert.ToInt32(Radius * Math.Sin(radian)));
            }
        }

        /// <summary> 右下顶点坐标偏移量
        /// </summary>
        public Point rbOffsetPoint
        {
            get
            {
                Double radian = (RotateAngle + rbAngle) * Math.PI / 180.0;
                return new Point(Convert.ToInt32(Radius * Math.Cos(radian)), Convert.ToInt32(Radius * Math.Sin(radian)));
            }
        }

        /// <summary> 左下顶点坐标偏移量
        /// </summary>
        public Point lbOffsetPoint
        {
            get
            {
                Double radian = (RotateAngle + lbAngle) * Math.PI / 180.0;
                return new Point(Convert.ToInt32(Radius * Math.Cos(radian)), Convert.ToInt32(Radius * Math.Sin(radian)));
            }
        }

        /// <summary> 中心点坐标偏移量
        /// </summary>
        private Point _centeroffsetpoint = Point.Empty;
        /// <summary> 中心点坐标偏移量
        /// </summary>
        public Point CenterOffsetPoint
        {
            get
            {
                if (!Point.Empty.Equals(_centeroffsetpoint))
                {
                    return _centeroffsetpoint;
                }
                _centeroffsetpoint = new Point(SourceWidth / 2, SourceHeight / 2);
                return _centeroffsetpoint;
            }
            set { _centeroffsetpoint = value; }
        }

        /// <summary> 绘图点
        /// </summary>
        private Point _drawpoint;
        /// <summary> 绘图点
        /// </summary>
        public Point DrawPoint
        {
            get { return _drawpoint; }
            set { _drawpoint = value; }
        }

        /// <summary> 是否显示边框
        /// </summary>
        protected Boolean _isshowborder = false;

        /// <summary> 边框颜色
        /// </summary>
        protected Color _bordercolor = Color.FromArgb(177, 136, 223);

        /// <summary> 背景颜色
        /// </summary>
        protected Color _backcolor = Color.Transparent;

        /// <summary> 选中部位
        /// </summary>
        private SelectedMode _selectedmodel = SelectedMode.None;

        /// <summary> 是否可以旋转
        /// </summary>
        protected Boolean _canrotate = true;

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
        #endregion

        #region 抽象函数
        /// <summary> 绘制元素
        /// </summary>
        /// <param name="gp">绘图图面实例</param>
        /// <param name="offsetlocation">绘制偏移坐标</param>
        public override void Draw(Graphics gp, Point offsetlocation)
        {
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gp.DrawImage(ShowImage, DrawPoint.X - offsetlocation.X - CenterOffsetPoint.X, DrawPoint.Y - offsetlocation.Y - CenterOffsetPoint.Y);
        }

        /// <summary> 鼠标是否覆盖
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        /// <returns>覆盖返回true,否则返回false</returns>
        public override bool MouseOver(Point muslocation)
        {
            _old_muslocation = null;
            Point point1 = new Point(DrawPoint.X - ltOffsetPoint.X, DrawPoint.Y - ltOffsetPoint.Y);
            Point point2 = new Point(DrawPoint.X - rtOffsetPoint.X, DrawPoint.Y - rtOffsetPoint.Y);
            Point point3 = new Point(DrawPoint.X - rbOffsetPoint.X, DrawPoint.Y - rbOffsetPoint.Y);
            Point point4 = new Point(DrawPoint.X - lbOffsetPoint.X, DrawPoint.Y - lbOffsetPoint.Y);
            if (Selected && _canrotate)
            {
                if ((point3.X - 2 <= muslocation.X && muslocation.X <= point3.X + 2) &&
                    (point3.Y - 2 <= muslocation.Y && muslocation.Y <= point3.Y + 2))
                {
                    _hoverarea = HoverMode.Rotate;
                    _selectedmodel = SelectedMode.Point;
                    return true;
                }
            }
            if (Multiply(muslocation, point1, point2) * Multiply(muslocation, point4, point3) <= 0 &&
                Multiply(muslocation, point4, point1) * Multiply(muslocation, point3, point2) <= 0)
            {
                _hoverarea = HoverMode.Face;
                _selectedmodel = SelectedMode.Face;
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
                _selectedmodel = SelectedMode.Face;
            }
            switch (_selectedmodel)
            {
                default: { break; }
                case SelectedMode.Point:
                    {
                        Point point3 = new Point(DrawPoint.X - rbOffsetPoint.X, DrawPoint.Y - rbOffsetPoint.Y);
                        Double a_pow = Math.Pow(muslocation.X - point3.X, 2) + Math.Pow(muslocation.Y - point3.Y, 2);
                        Double b_pow = Math.Pow(muslocation.X - DrawPoint.X, 2) + Math.Pow(muslocation.Y - DrawPoint.Y, 2);
                        Double c_pow = Math.Pow(Radius, 2);
                        Double cos_a = (c_pow + b_pow - a_pow) / (2 * Math.Sqrt(b_pow) * Math.Sqrt(c_pow));
                        if (1.0000000000000000 < cos_a)
                        {
                            cos_a = 1;
                        }
                        if (0 < (point3.X - DrawPoint.X) * (muslocation.Y - DrawPoint.Y) - (point3.Y - DrawPoint.Y) * (muslocation.X - DrawPoint.X))
                        {
                            RotateAngle += Convert.ToInt32(Math.Acos(cos_a) * 180 / Math.PI);
                        }
                        else
                        {
                            RotateAngle -= Convert.ToInt32(Math.Acos(cos_a) * 180 / Math.PI);
                        }
                        break;
                    }
                case SelectedMode.Face:
                    {
                        if (null == _old_muslocation)
                        {
                            _old_muslocation = muslocation;
                        }
                        else
                        {
                            if (multiple)
                            {
                                Int32 move_x = muslocation.X - _old_muslocation.Value.X;
                                Int32 move_y = muslocation.Y - _old_muslocation.Value.Y;
                                DrawPoint = new Point(DrawPoint.X + move_x, DrawPoint.Y + move_y);
                            }
                            else
                            {
                                DrawPoint = muslocation;
                            }
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
            _old_muslocation = null;
            _isdraw = true;
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
            if (null == _sourceimage)
            {
                _sourceimage = SourceImage;
            }
            Point point1 = new Point(DrawPoint.X - ltOffsetPoint.X, DrawPoint.Y - ltOffsetPoint.Y);
            Point point2 = new Point(DrawPoint.X - rtOffsetPoint.X, DrawPoint.Y - rtOffsetPoint.Y);
            Point point3 = new Point(DrawPoint.X - rbOffsetPoint.X, DrawPoint.Y - rbOffsetPoint.Y);
            Point point4 = new Point(DrawPoint.X - lbOffsetPoint.X, DrawPoint.Y - lbOffsetPoint.Y);
            Boolean flag = PointInRectangle(point1, rect);
            flag &= PointInRectangle(point2, rect);
            flag &= PointInRectangle(point3, rect);
            flag &= PointInRectangle(point4, rect);
            return flag;
        }

        /// <summary> 是否在工作区域绘制
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public override bool IsShow(Rectangle rect)
        {
            Point point1 = new Point(DrawPoint.X - ltOffsetPoint.X, DrawPoint.Y - ltOffsetPoint.Y);
            Point point2 = new Point(DrawPoint.X - rtOffsetPoint.X, DrawPoint.Y - rtOffsetPoint.Y);
            Point point3 = new Point(DrawPoint.X - rbOffsetPoint.X, DrawPoint.Y - rbOffsetPoint.Y);
            Point point4 = new Point(DrawPoint.X - lbOffsetPoint.X, DrawPoint.Y - lbOffsetPoint.Y);
            if (LineOver(point1, point2, rect) ||
                LineOver(point2, point3, rect) ||
                LineOver(point3, point4, rect) ||
                LineOver(point1, point4, rect))
            {
                return true;
            }
            return false;
        }
        #endregion

        /// <summary> 图片旋转
        /// </summary>
        private void Rotate()
        {
            if (null == _sourceimage)
            {
                _sourceimage = SourceImage;
            }
            _diameter = 0;
            _radius = 0;
            //生成图片的宽和高
            Int32 trewidth = Diameter + 4;
            Int32 treheight = trewidth;

            Bitmap bmptre = new Bitmap(trewidth, treheight);
            Bitmap bmptemp = new Bitmap(_sourceimage.Width, _sourceimage.Height);
            using (Graphics gp = Graphics.FromImage(bmptemp))
            {
                gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                if (!Color.Empty.Equals(Style.BackColor))
                {
                    gp.Clear(Style.BackColor);
                }
                gp.DrawImage(_sourceimage, 0, 0);
            }
            using (Graphics gp = Graphics.FromImage(bmptre))
            {
                gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //计算偏移量
                Point offset = new Point((trewidth - SourceWidth) / 2, (treheight - SourceHeight) / 2);
                //构造图像显示区域：让图像的中心与窗口的中心点一致
                Rectangle rect = new Rectangle(offset.X, offset.Y, SourceWidth, SourceHeight);
                CenterOffsetPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                gp.TranslateTransform(CenterOffsetPoint.X, CenterOffsetPoint.Y);
                gp.RotateTransform(RotateAngle);
                //恢复图像在水平和垂直方向的平移
                gp.TranslateTransform(-CenterOffsetPoint.X, -CenterOffsetPoint.Y);
                gp.DrawImage(bmptemp, rect);
                if (Selected)
                {
                    gp.DrawRectangle(Pens.Red, rect);
                    if (_canrotate)
                    {
                        gp.FillEllipse(Brushes.Red, rect.Right - 2, rect.Bottom - 2, 4, 4);
                    }
                }
                else
                {
                    if (!Color.Empty.Equals(Style.BorderColor))
                    {
                        using (Pen bpen = new Pen(Style.BorderColor, 2))
                        {
                            gp.DrawRectangle(bpen, rect);
                        }
                    }
                }
                //重至绘图的所有变换
                gp.ResetTransform();
            }
            if (null != bmptemp)
            {
                ((IDisposable)bmptemp).Dispose();
            }
            if (null != _showimage)
            {
                ((IDisposable)_showimage).Dispose();
            }
            if (_isdraw)
            {
                _isdraw = false;
            }
            _showimage = bmptre;
        }
    }
}

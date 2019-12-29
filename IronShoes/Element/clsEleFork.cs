using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 道岔
    /// </summary>
    [Serializable]
    class EleFork : DrawElements
    {
        /// <summary> 选中部位枚举
        /// </summary>
        enum SelectedMode
        {
            None,
            Root,
            Fork1,
            Fork2,
            Line
        }

        /// <summary> 正位道岔
        /// </summary>
        private ElePath _pro = null;

        /// <summary> 反位道岔
        /// </summary>
        private ElePath _con = null;

        /// <summary> 构造函数
        /// </summary>
        public EleFork()
            : base()
        {
            InitializeComponent();
        }

        /// <summary> 构造函数
        /// </summary>
        public EleFork(Point root, Point fork1, Point fork2)
        {
            InitializeComponent();
            _pro.StartPoint = root;
            _pro.EndPoint = fork1;
            _con.StartPoint = root;
            _con.EndPoint = fork2;
        }

        /// <summary> 是否选中
        /// </summary>
        public override bool Selected
        {
            get
            {
                return _pro.Selected;
            }
            set
            {
                _pro.Selected = value;
                _con.Selected = value;
            }
        }

        /// <summary> 道岔交汇点
        /// </summary>
        public Point Root
        {
            get { return _pro.StartPoint; }

            set
            {
                _pro.StartPoint = value;
                _con.StartPoint = value;
            }
        }

        /// <summary> 岔1点
        /// </summary>
        public Point Fork1
        {
            get { return _pro.EndPoint; }
            set { _pro.EndPoint = value; }
        }

        /// <summary> 岔2点
        /// </summary>
        public Point Fork2
        {
            get { return _con.EndPoint; }
            set { _con.EndPoint = value; }
        }

        /// <summary> 正位道岔样式
        /// </summary>
        public Style ProStyle
        {
            get { return _pro.Style; }
            set { _pro.Style = value; }
        }

        /// <summary> 反位道岔样式
        /// </summary>
        public Style ConStyle
        {
            get { return _con.Style; }
            set { _con.Style = value; }
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
            if (Selected)
            {
                _pro.Selected = true;
                _con.Selected = true;
            }
            _pro.Draw(gp, offsetlocation);
            _con.Draw(gp, offsetlocation);
        }

        /// <summary> 鼠标是否覆盖
        /// </summary>
        /// <param name="muslocation">鼠标点坐标</param>
        /// <returns>覆盖返回true,否则返回false</returns>
        public override bool MouseOver(Point muslocation)
        {
            _pro.MouseOver(muslocation);
            _con.MouseOver(muslocation);
            if (PointOver(Root, muslocation))
            {
                _hoverarea = HoverMode.Point;
                _selectedmodel = SelectedMode.Root;
                return true;
            }

            if (PointOver(Fork1, muslocation))
            {
                _hoverarea = HoverMode.Point;
                _selectedmodel = SelectedMode.Fork1;
                return true;
            }

            if (PointOver(Fork2, muslocation))
            {
                _hoverarea = HoverMode.Point;
                _selectedmodel = SelectedMode.Fork2;
                return true;
            }

            if (LineOver(Root, Fork1, muslocation))
            {
                _hoverarea = HoverMode.Line;
                _selectedmodel = SelectedMode.Line;
                return true;
            }

            if (LineOver(Root, Fork2, muslocation))
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
                case SelectedMode.Root:
                    {
                        Root = muslocation;
                        break;
                    }
                case SelectedMode.Fork1:
                    {
                        Fork1 = muslocation;
                        break;
                    }
                case SelectedMode.Fork2:
                    {
                        Fork2 = muslocation;
                        break;
                    }
                case SelectedMode.Line:
                    {
                        _pro.MouseMove(muslocation, true);
                        _con.MouseMove(muslocation, true);
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
            Boolean flag = PointInRectangle(Root, rect);
            flag &= PointInRectangle(Fork1, rect);
            flag &= PointInRectangle(Fork2, rect);
            return flag;
        }

        /// <summary> 是否在工作区域绘制
        /// </summary>
        /// <param name="rect">工作区域</param>
        /// <returns>选中返回true,否则返回false</returns>
        public override bool IsShow(Rectangle rect)
        {
            if (LineOver(Root, Fork1, rect) || LineOver(Root, Fork2, rect))
            {
                return true;
            }
            return false;
        }

        /// <summary> 初始化类变量
        /// </summary>
        private void InitializeComponent()
        {
            _pro = new ElePath();
            _pro.Style.Color = Color.FromArgb(0, 255, 0);
            _pro.Style.LineHeight = 2;

            _con = new ElePath();
            _con.Style.Color = Color.FromArgb(255, 255, 0);
            _con.Style.LineHeight = 2;
        }
    }
}

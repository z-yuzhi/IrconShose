
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace IronShoes
{
    public class WorkArea
    {
        #region 属性
        /// <summary> 工作区域宽度
        /// </summary>
        public Int32 Width
        {
            get { return Config.Width; }
            set { Config.Width = value; }
        }

        /// <summary> 工作区域高度
        /// </summary>
        public Int32 Height
        {
            get { return Config.Height; }
            set { Config.Height = value; }
        }

        /// <summary> 网格宽度
        /// </summary>
        public Int32 GridWidth
        {
            get { return Config.GridWidth; }
            set { Config.GridWidth = value; }
        }

        /// <summary> 网格高度
        /// </summary>
        public Int32 GridHeight
        {
            get { return Config.GridHeight; }
            set { Config.GridHeight = value; }
        }

        /// <summary> 背景颜色
        /// </summary>
        public Color BackColor
        {
            get { return Config.BackColor; }
            set { Config.BackColor = value; }
        }

        /// <summary> 网格颜色
        /// </summary>
        public Color GridColor
        {
            get { return Config.GridColor; }
            set { Config.GridColor = value; }
        }

        /// <summary> 是否显示网格
        /// </summary>
        public Boolean ShowGrid
        {
            get { return Config.ShowGrid; }
            set { Config.ShowGrid = value; }
        }

        /// <summary> 自动保存时间
        /// </summary>
        public Int32 AutoSaveTime
        {
            get { return Config.AutoSaveTime; }
            set { Config.AutoSaveTime = value; }
        }

        /// <summary> 绘制铁路线宽度
        /// </summary>
        public Single LineWidth
        {
            get { return Config.LineWidth; }
            set { Config.LineWidth = value; }
        }

        /// <summary> 信息灯直径
        /// </summary>
        public Int32 LightDiameter
        {
            get { return Config.LightDiameter; }
            set { Config.LightDiameter = value; }
        }

        private AreaConfig _config = null;
        /// <summary> 工作空间属性配置
        /// </summary>
        private AreaConfig Config
        {
            get
            {
                if (null == _config)
                {
                    _config = new AreaConfig();
                }
                return _config;
            }
            set { _config = value; }
        }

        private Boolean _isselectelement = false;
        /// <summary> 是否选中元素
        /// </summary>
        public Boolean IsSelectElement
        {
            get { return _isselectelement; }
        }

        /// <summary> 选中元素
        /// </summary>
        private Elements _selectedelement;

        /// <summary> 选中元素鼠标覆盖区域
        /// </summary>
        public HoverMode SelectElementHoverArea
        {
            get
            {
                if (_isselectelement)
                {
                    if (1 < Config.SelectedElements.Count
                        && !HoverMode.None.Equals(_selectedelement.HoverArea))
                    {
                        return HoverMode.Face;
                    }
                    return _selectedelement.HoverArea;
                }
                return HoverMode.None;
            }
        }

        /// <summary> 是否重绘工作区域
        /// </summary>
        private Boolean _isredraw = false;

        /// <summary> 工作空间历史记录
        /// </summary>
        private List<History> _oldconfig = new List<History>();

        /// <summary> 工作空间历史记录序号
        /// </summary>
        private Int32 _oldconfigindex = 0;

        private Rectangle _viewsize = Rectangle.Empty;
        public Rectangle ViewSize
        {
            get { return _viewsize; }
            set { _viewsize = value; }
        }

        private List<Elements> _showelements = new List<Elements>();
        /// <summary> 工作区域绘制的所有元素
        /// </summary>
       
        #endregion

        #region 函数
        /// <summary> 生成工作区域背景
        /// </summary>
        /// <returns>Bitmap实例的背景图片</returns>
        public Bitmap CreateBackground()
        {
            if (Rectangle.Empty.Equals(_viewsize))
            {
                return null;
            }
            Bitmap bgImage = new Bitmap(_viewsize.Width, _viewsize.Height);
            using (Graphics gp = Graphics.FromImage(bgImage))
            {
                gp.Clear(Config.BackColor);
                if (Config.ShowGrid)
                {
                    Pen lingpen = new Pen(Config.GridColor);
                    Int32 offset = Config.GridWidth - _viewsize.X % Config.GridWidth;
                    for (Int32 i = offset; i <= bgImage.Width; i += Config.GridWidth)
                    {
                        gp.DrawLine(lingpen, i, 0, i, _viewsize.Height);
                    }
                    offset = Config.GridHeight - _viewsize.Y % Config.GridHeight;
                    for (Int32 i = offset; i <= bgImage.Height; i += Config.GridHeight)
                    {
                        gp.DrawLine(lingpen, 0, i, _viewsize.Width, i);
                    }
                    ((IDisposable)lingpen).Dispose();
                }
            }
            return bgImage;
        }

        /// <summary> 校对鼠标位置坐标与网格对齐
        /// </summary>
        /// <param name="muslocation">鼠标位置坐标</param>
        /// <returns>校对鼠标位置坐标</returns>
        public Point CorrectPoint(Point muslocation)
        {
            Int32 X = (muslocation.X / Config.GridWidth) * Config.GridWidth;
            Int32 Y = (muslocation.Y / Config.GridHeight) * Config.GridHeight;
            if (muslocation.X % Config.GridWidth > Config.GridWidth / 2)
            {
                X += Config.GridWidth;
            }
            if (muslocation.Y % Config.GridHeight > Config.GridHeight / 2)
            {
                Y += Config.GridHeight;
            }
            return new Point(X, Y);
        }

        /// <summary> 向工作区域绘制元素
        /// </summary>
        /// <param name="gp">绘图对象实例</param>
        /// <param name="muslocation">鼠标位置坐标</param>
        /// <param name="eletype">图形种类</param>
        /// <param name="argv">参数数组<para />
        /// argv[0]:绘制类型为路段，muslocation表示StartPoint坐标，argv[0]表示EndPoint坐标；<para />
        /// 　　　　绘制类型为道岔，muslocation表示Root坐标，argv[0]表示Fork2坐标；
        /// </param>
        /// <returns>绘图成功返true，否则返回false</returns>
        public Boolean DrawElement(Graphics gp, Point muslocation, ElementType eletype,params Object[] argv)
        {
            try
            {
                switch (eletype)
                {
                    default:
                        {
                            //判断是否需要重新绘制
                            if (_isredraw)
                            {
                                Bitmap ways = new Bitmap(_viewsize.Width, _viewsize.Height);
                                Bitmap signals = new Bitmap(_viewsize.Width, _viewsize.Height);
                                Bitmap nodes = new Bitmap(_viewsize.Width, _viewsize.Height);
                                Graphics ways_gp = Graphics.FromImage(ways);
                                Graphics signals_gp = Graphics.FromImage(signals);
                                Graphics nodes_gp = Graphics.FromImage(nodes);
                                gp.Clear(Color.Transparent);
                                foreach (Elements show in _showelements)
                                {
                                    if (!show.Selected)
                                    {
                                        switch (show.GetType().Name)
                                        {
                                            default: { break; }
                                            case "ElePath":
                                            case "EleFork":
                                                {
                                                    show.Draw(ways_gp, _viewsize.Location);
                                                    break;
                                                }
                                            case "INode":
                                            case "ONode":
                                                {
                                                    show.Draw(nodes_gp, _viewsize.Location);
                                                    break;
                                                }
                                            case "DSignal":
                                            case "DHSignal":
                                            case "SSignal":
                                            case "SHSignal":
                                                {
                                                    show.Draw(signals_gp, _viewsize.Location);
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        show.Selected = true;
                                    }
                                }
                                gp.DrawImage(ways, 0, 0);
                                gp.DrawImage(nodes, 0, 0);
                                gp.DrawImage(signals, 0, 0);
                                ((IDisposable)ways).Dispose();
                                ((IDisposable)signals).Dispose();
                                ((IDisposable)nodes).Dispose();
                                ((IDisposable)ways_gp).Dispose();
                                ((IDisposable)signals_gp).Dispose();
                                ((IDisposable)nodes_gp).Dispose();
                            }
                            _isredraw = false;
                            break;
                        }
                    case ElementType.Path:
                        {
                            ElePath ele = new ElePath();
                            ele.StartPoint = CorrectPoint(muslocation);
                            ele.EndPoint = CorrectPoint((Point)argv[0]);
                            ele.Style.LineHeight = Config.LineWidth;
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.Fork:
                        {
                            EleFork ele = new EleFork();
                            ele.Root = CorrectPoint(muslocation);
                            ele.Fork2 = CorrectPoint((Point)argv[0]);
                            ele.Fork1 = new Point(ele.Fork2.X, ele.Root.Y);
                            ele.ProStyle.LineHeight = Config.LineWidth;
                            ele.ConStyle.LineHeight = Config.LineWidth;
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.DSignal:
                        {
                            DSignal ele = new DSignal();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            ele.LightDiameter = Config.LightDiameter;
                            CheckImageLocation(ele,muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.DHSignal:
                        {
                            DHSignal ele = new DHSignal();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            ele.LightDiameter = Config.LightDiameter;
                            CheckImageLocation(ele, muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.SSignal:
                        {
                            SSignal ele = new SSignal();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            ele.LightDiameter = Config.LightDiameter;
                            CheckImageLocation(ele, muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.SHSignal:
                        {
                            SHSignal ele = new SHSignal();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            ele.LightDiameter = Config.LightDiameter;
                            CheckImageLocation(ele, muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.INode:
                        {
                            INode ele = new INode();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            CheckImageLocation(ele, muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                    case ElementType.ONode:
                        {
                            ONode ele = new ONode();
                            ele.DrawPoint = CorrectPoint(muslocation);
                            CheckImageLocation(ele, muslocation);
                            ele.Draw(gp, _viewsize.Location);
                            Config.AllElements.Add(ele);
                            _showelements.Add(ele);
                            break;
                        }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 向工作区域绘制选中元素
        /// </summary>
        /// <param name="gp">绘图对象实例</param>
        /// <returns>绘图成功返true，否则返回false</returns>
        public Boolean DrawElement(Graphics gp)
        {
            try
            {
                foreach (Elements item in Config.SelectedElements)
                {
                    item.Selected = true;
                    item.Draw(gp, _viewsize.Location);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 是否选中元素
        /// </summary>
        /// <param name="rect">鼠标选择区域</param>
        /// <returns>选中返回true，否则返回false</returns>
        public Boolean SelectElement(Rectangle rect, Boolean ctrl)
        {
            _isredraw = true;
            Int32 oldselectedcount = Config.SelectedElements.Count;
            Int32 selected = 0;
            for (Int32 i = _showelements.Count - 1; 0 <= i; i--)
            {
                Boolean oldselected = _showelements[i].Selected;
                Boolean nowselected = _showelements[i].MouseSelected(rect);
                if (!oldselected && nowselected)
                {
                    if (!ctrl && Size.Empty.Equals(rect.Size))
                    {
                        if (0 == selected)
                        {
                            selected += 1;
                            _showelements[i].Selected = true;
                            Config.SelectedElements.Add(_showelements[i]);
                            _selectedelement = _showelements[i];
                        }
                    }
                    else
                    {
                        _showelements[i].Selected = true;
                        Config.SelectedElements.Add(_showelements[i]);
                        _selectedelement = _showelements[i];
                    }
                }
                else if ((!ctrl && oldselected && !nowselected) ||
                         ( ctrl && oldselected &&  nowselected))
                {
                    Config.SelectedElements.Remove(_showelements[i]);
                    _showelements[i].Selected = false;
                }
            }
            if (0 != Config.SelectedElements.Count)
            {
                _isselectelement = true;
            }
            else
            {
                _isselectelement = false;
            }
            return _isredraw;
        }

        /// <summary> 是否移动到元素上
        /// </summary>
        /// <param name="muslocation">鼠标位置坐标</param>
        /// <returns>选中返回true，否则返回false</returns>
        public Boolean OverElement(Point muslocation)
        {
            Int32 i = 0;
            for (i = Config.SelectedElements.Count - 1; 0 <= i; i--)
            {
                if (Config.SelectedElements[i].MouseOver(muslocation))
                {
                    _selectedelement = Config.SelectedElements[i];
                    return true;
                }
            }
            if (0 == i)
            {
                _selectedelement = null;
            }
            for (i = _showelements.Count - 1; 0 <= i; i--)
            {
                if (_showelements[i].Selected)
                {
                    continue;
                }
                if (_showelements[i].MouseOver(muslocation))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> 移动元素
        /// </summary>
        /// <param name="muslocation">鼠标位置坐标</param>
        /// <returns>选中返回true，否则返回false</returns>
        public Boolean MoveElement(Point muslocation)
        {
            try
            {
                if (1 == Config.SelectedElements.Count)
                {
                    if (Config.SelectedElements[0].IsCorrect)
                    {
                        muslocation = CorrectPoint(muslocation);
                    }
                    Elements tmp = Config.SelectedElements[0].Clone() as Elements;
                    tmp.MouseMove(muslocation, false);
                    if (tmp.IsMove(new Rectangle(0, 0, Width, Height)))
                    {
                        Config.SelectedElements[0].MouseMove(muslocation, false);
                    }
                    tmp = null;
                }
                else
                {
                    Boolean flag = true;
                    foreach (Elements item in Config.SelectedElements)
                    {
                        Elements tmp = Config.SelectedElements[0].Clone() as Elements;
                        tmp.MouseMove(muslocation, true);
                        flag &= tmp.IsMove(new Rectangle(0, 0, Width, Height));
                        tmp = null;
                    }
                    if (flag)
                    {
                        foreach (Elements item in Config.SelectedElements)
                        {
                            item.MouseMove(CorrectPoint(muslocation), true);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 移动元素
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="argv">参数数组<para />
        /// argv[0]：CTRL按键状态<para />
        /// argv[1]：SHIFT按键状态<para />
        /// </param>
        /// <returns>选中返回true，否则返回false</returns>
        public Boolean MoveElementTo(Size offset, params Object[] argv)
        {
            try
            {
                if (1 == Config.SelectedElements.Count && 0 != argv.Length)
                {
                    if (typeof(SignalElements).Equals(Config.SelectedElements[0].GetType().BaseType))
                    {
                        Boolean isctrl = Convert.ToBoolean(argv[0]);
                        Boolean isshift = Convert.ToBoolean(argv[1]);
                        if (isctrl && isshift)
                        {
                            (Config.SelectedElements[0] as SignalElements).RotateAngle += offset.Height / GridHeight * 45;
                            return true;
                        }
                        if (isctrl)
                        {
                            Config.SelectedElements[0].KeyMove(new Size(offset.Width / GridWidth, offset.Height / GridHeight));
                            return true;
                        }
                        if (isshift)
                        {
                            (Config.SelectedElements[0] as SignalElements).RotateAngle += offset.Height / GridHeight;
                            return true;
                        }
                    }
                }
                foreach (Elements item in Config.SelectedElements)
                {
                    item.KeyMove(offset);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 删除选中文件
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean DeleteElement()
        {
            try
            {
                foreach (Elements item in Config.SelectedElements)
                {
                    Config.AllElements.Remove(item);
                    _showelements.Remove(item);
                }
                Config.SelectedElements.Clear();
                _isselectelement = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 打开指定文件的工作区域
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>提示信息</returns>
        public String OpenWork(FileStream fs)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                Config = bf.Deserialize(fs) as AreaConfig;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return String.Empty;
        }

        /// <summary> 保存工作区域指定文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>成功返回true，否则返回false</returns>
        public MemoryStream SaveWork()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            try
            {
                if (0 != Config.SelectedElements.Count)
                {
                    foreach (Elements item in Config.SelectedElements)
                    {
                        item.Selected = false;
                    }
                    Config.SelectedElements.Clear();
                }
                bf.Serialize(ms, Config);
                ms.Position = 0;
            }
            catch (Exception)
            {
                return null;
            }
            return ms;
        }

        /// <summary> 新建工作区域配置
        /// </summary>
        /// <returns>成功返回true，否则返回false</returns>
        public Boolean NewWork()
        {
            Config = new AreaConfig();
            return true;
        }

        /// <summary> 记录工作空间历史操作
        /// </summary>
        public void RecordHistory(params Object[] argv)
        {
            if (0 != argv.Length)
            {
                if (Convert.ToBoolean(argv[0]))
                {
                    if (0 != _oldconfig.Count)
                    {
                        return;
                    }
                }
                else
                {
                    _oldconfig.Clear();
                    _oldconfigindex = 0;
                    return;
                }
            }
            if (0 != _oldconfigindex)
            {
                for (Int32 i = _oldconfigindex - 1; 0 <= i; i--)
                {
                    _oldconfig.RemoveAt(i);
                }
                _oldconfigindex = 0;
            }
            if (51 == _oldconfig.Count)
            {
                _oldconfig.RemoveAt(_oldconfig.Count - 1);
            }
            _oldconfig.Insert(0, new History(_viewsize.Location, Config.Clone() as AreaConfig));
        }

        /// <summary> 撤销操作
        /// </summary>
        public Boolean ToBack(out Point location)
        {
            if (_oldconfig.Count - 1 == _oldconfigindex)
            {
                location = Point.Empty;
                return false;
            }
            _oldconfigindex += 1;
            History histemp = _oldconfig[_oldconfigindex];
            Config = histemp.Config.Clone() as AreaConfig;
            if (_oldconfig.Count - 1 == _oldconfigindex
                && (0 == histemp.ScrollLocation.X && 0 == histemp.ScrollLocation.Y))
            {
                location = _oldconfig[_oldconfigindex - 1].ScrollLocation;
            }
            else
            {
                location = histemp.ScrollLocation;
            }
            _isselectelement = Config.SelectedElements.Count > 0;
            _isredraw = true;
            return true;
        }

        /// <summary> 恢复操作
        /// </summary>
        public Boolean Restore(out Point location)
        {
            if (0 == _oldconfigindex)
            {
                location = Point.Empty;
                return false;
            }
            _oldconfigindex -= 1;
            History histemp = _oldconfig[_oldconfigindex];
            Config = histemp.Config.Clone() as AreaConfig;
            location = histemp.ScrollLocation;
            _isselectelement = Config.SelectedElements.Count > 0;
            _isredraw = true;
            return true;
        }

        /// <summary> 重新设置元素绘制属性
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean ResetElementAll()
        {
            _isredraw = true;
            List<Elements> removeelement = new List<Elements>();
            foreach (Elements item in Config.AllElements)
            {
                if (item.IsMove(new Rectangle(0, 0, Width, Height)))
                {
                    switch (item.GetType().BaseType.Name)
                    {
                        default: { break; }
                        case "DrawElements":
                            {
                                switch (item.GetType().Name)
                                {
                                    default: { break; }
                                    case "ElePath":
                                        {
                                            ElePath tmp = item as ElePath;
                                            tmp.Style.LineHeight = Config.LineWidth;
                                            tmp.StartPoint = CorrectPoint(tmp.StartPoint);
                                            tmp.EndPoint = CorrectPoint(tmp.EndPoint);
                                            break;
                                        }
                                    case "EleFork":
                                        {
                                            EleFork tmp = item as EleFork;
                                            tmp.ProStyle.LineHeight = Config.LineWidth;
                                            tmp.ConStyle.LineHeight = Config.LineWidth;
                                            tmp.Root = CorrectPoint(tmp.Root);
                                            tmp.Fork1 = CorrectPoint(tmp.Fork1);
                                            tmp.Fork2 = CorrectPoint(tmp.Fork2);
                                            break;
                                        }
                                }
                                break;
                            }
                        case "SignalElements":
                            {
                                SignalElements tmp = item as SignalElements;
                                tmp.LightDiameter = Config.LightDiameter;
                                tmp.DrawPoint = CorrectPoint(tmp.DrawPoint);
                                break;
                            }
                    }
                }
                else
                {
                    removeelement.Add(item);
                }
            }
            foreach (Elements item in removeelement)
            {
                Config.AllElements.Remove(item);
            }
            return true;
        }

        /// <summary> 获取绘制元素
        /// </summary>
        public void GetDrawElement()
        {
            _isredraw = true;
            _showelements.Clear();
            for (Int32 i = 0; i < Config.AllElements.Count; i++)
            {
                if (Config.AllElements[i].IsShow(_viewsize))
                {
                    _showelements.Add(Config.AllElements[i]);
                }
            }
        }

        /// <summary> 校验绘制图片元素位置<para />
        /// 1、防止图片绘制到工作区域外
        /// </summary>
        /// <param name="ele">图片元素实例</param>
        /// <param name="muslocation">鼠标位置</param>
        private void CheckImageLocation(ImageElements ele, Point muslocation)
        {
            Int32 ih = ele.ShowImage.Height / 2;
            Int32 iw = ele.ShowImage.Width / 2;
            if (!ele.IsMove(new Rectangle(0, 0, Width, Height)))
            {
                if (muslocation.X - iw < 0)
                {
                    muslocation.X = iw;
                }
                if (muslocation.X + iw > Width)
                {
                    muslocation.X = Width - iw;
                }
                if (muslocation.Y - ih < 0)
                {
                    muslocation.Y = ih;
                }
                if (muslocation.Y + ih > Height)
                {
                    muslocation.Y = Height - ih;
                }
                ele.DrawPoint = CorrectPoint(muslocation);
            }
        }
        #endregion

        /// <summary> 操作历史记录信息
        /// </summary>
        class History
        {
            /// <summary> 构造函数
            /// </summary>
            public History(Point location, AreaConfig config)
            {
                _scrolllocation = location;
                _config = config;
            }

            /// <summary> 滚动条位置
            /// </summary>
            private Point _scrolllocation = Point.Empty;
            /// <summary> 滚动条位置
            /// </summary>
            public Point ScrollLocation
            {
                get { return _scrolllocation; }
                set { _scrolllocation = value; }
            }

            /// <summary> 工作空间
            /// </summary>
            private AreaConfig _config = null;
            /// <summary> 工作空间
            /// </summary>
            public AreaConfig Config
            {
                get { return _config; }
                set { _config = value; }
            }
        }
    }
}

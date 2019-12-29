using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace IronShoes
{
    [Serializable]
    public class AreaConfig : ICloneable
    {
        #region 工作区域属性
        private Int32 _width = 3072;
        /// <summary> 工作区域宽度
        /// </summary>
        public Int32 Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private Int32 _height = 1536;
        /// <summary> 工作区域高度
        /// </summary>
        public Int32 Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private Int32 _gridwidth = 8;
        /// <summary> 网格宽度
        /// </summary>
        public Int32 GridWidth
        {
            get { return _gridwidth; }
            set { _gridwidth = value; }
        }

        private Int32 _gridheight = 8;
        /// <summary> 网格高度
        /// </summary>
        public Int32 GridHeight
        {
            get { return _gridheight; }
            set { _gridheight = value; }
        }

        private Color _backcolor = Color.Black;
        /// <summary> 工作区背景颜色
        /// </summary>
        public Color BackColor
        {
            get { return _backcolor; }
            set { _backcolor = value; }
        }

        private Color _gridcolor = Color.Gray;
        /// <summary> 网格线颜色
        /// </summary>
        public Color GridColor
        {
            get { return _gridcolor; }
            set { _gridcolor = value; }
        }

        private Boolean _showgrid = true;
        /// <summary> 是否显示网格线
        /// </summary>
        public Boolean ShowGrid
        {
            get { return _showgrid; }
            set { _showgrid = value; }
        }

        private Int32 _autosavtime = 10;
        public Int32 AutoSaveTime
        {
            get { return _autosavtime; }
            set { _autosavtime = value; }
        }
        #endregion

        #region 元素属性
        private List<Elements> _allelements;
        /// <summary> 工作区域绘制的所有元素
        /// </summary>
        public List<Elements> AllElements
        {
            get
            {
                if (null == _allelements)
                {
                    _allelements = new List<Elements>();
                }
                return _allelements;
            }
        }

        private List<Elements> _selectedelements;
        /// <summary> 选中元素
        /// </summary>
        public List<Elements> SelectedElements
        {
            get
            {
                if (null == _selectedelements)
                {
                    _selectedelements = new List<Elements>();
                }
                return _selectedelements;
            }
        }

        private Single _linewidth = 2;
        /// <summary> 绘制铁路线宽度
        /// </summary>
        public Single LineWidth
        {
            get { return _linewidth; }
            set { _linewidth = value; }
        }

        private Int32 _lightdiameter = 12;
        /// <summary> 信息灯直径
        /// </summary>
        public Int32 LightDiameter
        {
            get { return _lightdiameter; }
            set { _lightdiameter = value; }
        }

        #endregion

        #region ICloneable 成员

        public object Clone()
        {
            AreaConfig copy = null;
            //MemoryStream stream = new MemoryStream();
            //BinaryFormatter formatter = new BinaryFormatter();
            //using (stream)
            //{
            //    formatter.Serialize(stream, this);
            //    stream.Position = 0;
            //    copy = formatter.Deserialize(stream) as AreaConfig;
            //    stream.Close();
            //}
            copy = this.MemberwiseClone() as AreaConfig;
            copy._allelements = new List<Elements>();
            copy._selectedelements = new List<Elements>();
            copy._allelements.AddRange(this.AllElements.ToArray());
            copy._selectedelements.AddRange(this.SelectedElements.ToArray());
            return copy;
        }

        #endregion
    }
}

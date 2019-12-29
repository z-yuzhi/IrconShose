using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IronShoes
{
    /// <summary> 图形种类
    /// </summary>
    public enum ElementType
    {
        None,
        /// <summary> 路段
        /// </summary>
        Path,
        /// <summary> 道岔
        /// </summary>
        Fork,
        /// <summary> 信号机
        /// </summary>
        DSignal,
        /// <summary> 信号机
        /// </summary>
        DHSignal,
        /// <summary> 信号机
        /// </summary>
        SSignal,
        /// <summary> 信号机
        /// </summary>
        SHSignal,
        /// <summary> 绝缘节
        /// </summary>
        INode,
        /// <summary> 绝缘节
        /// </summary>
        ONode
    }

    /// <summary> 元素选中方式
    /// </summary>
    public enum HoverMode
    {
        None,
        /// <summary> 点
        /// </summary>
        Point,
        /// <summary> 线
        /// </summary>
        Line,
        /// <summary> 面
        /// </summary>
        Face,
        /// <summary> 旋转
        /// </summary>
        Rotate
    }
}

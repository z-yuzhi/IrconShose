using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronShoes.data
{
    /// <summary>
    /// 数据库连接状态类型
    /// </summary>
    public enum ConnStateType
    {
        Closed = 0,
        Connecting = 1,
        Executing = 2,
        Fetching = 3,
        Open = 4,
        Unknow = 5
    }

}

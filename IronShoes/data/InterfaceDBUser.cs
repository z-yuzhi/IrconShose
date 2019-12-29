using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronShoes.data
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface InterfaceDBUser
    {
        string UserName
        {
            get;
            set;
        }
        int UserID
        {
            get;
            set;
        }
        int UserType
        {
            get;
            set;
        }
    }
}

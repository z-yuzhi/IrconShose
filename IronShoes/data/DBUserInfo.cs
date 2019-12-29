using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronShoes.data
{
    /// <summary>
    /// 数据库用户信息
    /// </summary>
    public class DBUserInfo : InterfaceDBUser
    {
        private string _strUserName;
        private int _iUserID;
        private int _iUserType;

        public string UserName
        {
            get
            {
                return _strUserName;
            }
            set
            {
                _strUserName = value;
            }
        }
        public int UserID
        {
            get
            {
                return _iUserID;
            }
            set
            {
                _iUserID = value;
            }
        }
        public int UserType
        {
            get
            {
                return _iUserType;
            }
            set
            {
                _iUserType = value;
            }
        }
    }
}

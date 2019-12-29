using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IronShoes.UserControls;

namespace IronShoes
{

    public class frmFactory
    {
        private static FormMain _frmMain = null;
        private static UserControlRailwayMon _userControlRailwayMon;

        /// <summary>
        /// 获取主程序窗口
        /// </summary>
        /// <returns></returns>
        public static FormMain GetFrmMainInstance()
        {
            if (null == _frmMain)
            {
                _frmMain = new FormMain();
            }
            return _frmMain;
        }

        /// <summary>
        /// 获取铁轨监控界面
        /// </summary>
        /// <returns></returns>
        public static UserControlRailwayMon GetUCRailwayMon()
        {
            if(null == _userControlRailwayMon)
            {
                _userControlRailwayMon = new UserControlRailwayMon();
            }
            return _userControlRailwayMon;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using IronShoes.entity;

namespace IronShoes.data
{
    public interface InterfaceDB
    {
        string Server
        {
            get;
            set;
        }

        string DatabaseName
        {
            get;
            set;
        }

        string User
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        ConnStateType ConnState
        {
            get;
        }

        bool ConnectDB(string sDBServer, string sDBName, string sUser, string sPwd, string port);

        /// <summary>
        /// 获取指定终端编号的终端ID
        /// </summary>
        /// <param name="termCode">终端编号</param>
        /// <returns></returns>
        int GetIDByTermCode(string termCode);

        /// <summary>
        /// 获取铁鞋区域数据
        /// </summary>
        /// <returns></returns>
        DataTable GetTXRegionData();

        #region  主页面
        /// <summary>
        /// 获取设备上道条数
        /// </summary>
        /// <returns></returns>
        object GetInRegionCount();

        /// <summary>
        /// 下道铁鞋
        /// </summary>
        /// <returns></returns>
        object GetNextIronShoesCount();

        /// <summary>
        /// 入柜铁鞋
        /// </summary>
        /// <returns></returns>
        object GetIntoIronShoesCount();

        /// <summary>
        /// 故障铁鞋
        /// </summary>
        /// <returns></returns>
        object GetFaultIronShoesCount();
        #endregion

        #region 设备上道
        /// <summary>
        /// 获取需要上岛的设备id
        /// </summary>
        /// <returns></returns>
        int GetIsRegionID(string termCode);

        /// <summary>
        /// 获取上道区域
        /// </summary>
        /// <returns></returns>
        List<string> GetRoad();

        /// <summary>
        /// 获取操作人员名单
        /// </summary>
        /// <returns></returns>
        List<string> GetPersonName();
        #endregion

        void Close();
    }
}

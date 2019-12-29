using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;
using IronShoes.entity;

namespace IronShoes.data
    
{

    /// <summary>
    /// 数据库增删改查操作
    /// </summary>

    public class MySQLDBImplSQL : InterfaceDB
    {
        private string m_sConnectionString;
        private string m_strServer;
        private string m_strDataBase;
        private string m_strUser;
        private string m_strPwd;
        private string m_strErrMsg;

        private MySqlConnection m_Connection;
        private MySqlDataAdapter m_sqlAdapter;


        public string Server
        {
            get
            {
                return m_strServer;
            }
            set
            {
                m_strServer = value;
            }
        }

        public string DatabaseName
        {
            get
            {
                return m_strDataBase;
            }
            set
            {
                m_strDataBase = value;
            }
        }

        public string User
        {
            get
            {
                return m_strUser;
            }
            set
            {
                m_strUser = value;
            }
        }

        public string Password
        {
            get
            {
                return m_strPwd;
            }
            set
            {
                m_strPwd = value;
            }
        }

        public string Errmsg
        {
            get
            {
                return m_strErrMsg;
            }
            set
            {
                m_strErrMsg = value;
            }
        }

        public ConnStateType ConnState
        {
            get
            {
                switch (m_Connection.State)
                {
                    case ConnectionState.Closed:
                        return ConnStateType.Closed;
                    case ConnectionState.Connecting:
                        return ConnStateType.Connecting;
                    case ConnectionState.Executing:
                        return ConnStateType.Executing;
                    case ConnectionState.Fetching:
                        return ConnStateType.Fetching;
                    case ConnectionState.Open:
                        return ConnStateType.Open;
                    default:
                        return ConnStateType.Unknow;
                }
            }
        }

        public MySQLDBImplSQL()
        {
            //
            // TODO: Add constructor logic here
            m_Connection = new MySqlConnection();
            //
            m_sqlAdapter = new MySqlDataAdapter();
        }

        private bool ReconnectDB()
        {
            if (m_Connection.State == ConnectionState.Closed)
            {
                try
                {
                    m_Connection.Open();
                }
                catch (MySqlException)
                {
                    return false;
                }
                return true;
            }
            else if (m_Connection.State == ConnectionState.Open)
                return true;

            return true;
        }

        public bool ConnectDB(string sDBServer, string sDBName, string sUser, string sPwd, string port)
        {
            if (null == port || "".Equals(port))
            {
                port = "3306";
            }
            m_strServer = sDBServer;
            m_strDataBase = sDBName;
            m_strUser = sUser;
            m_strPwd = sPwd;
            m_sConnectionString = "Server=" + m_strServer + ";Port=" + port + "; Database=" + m_strDataBase +
                ";Uid=" + m_strUser + ";Pwd=" + sPwd + ";";
            m_Connection.ConnectionString = m_sConnectionString;
            try
            {
                m_Connection.Open();
            }
            catch (MySqlException)
            {
                return false;
            }
            return true;
        }

        public void Close()
        {
            if (m_Connection.State == ConnectionState.Open)
                m_Connection.Close();
        }

        /// <summary>
        /// 获取指定终端编号的终端ID
        /// </summary>
        /// <param name="termCode">终端编号</param>
        /// <returns></returns>
        public int GetIDByTermCode(string termCode)
        {
            int termID = -1;
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT term_id FROM jc_terminal WHERE isdelete = 1 AND term_code LIKE '" + termCode + "'";
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    termID = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                }
            }
            catch (Exception)
            {
            }
            return termID;
        }

        /// <summary>
        /// 获取铁鞋区域数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTXRegionData()
        {
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT R.region_id, R.road_id, R.points, R.region_type, T.road_code, T.road_name FROM tx_region R" +
                " INNER JOIN tx_railroad T ON R.road_id = T.road_id";
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                sqlAdapter.Fill(dt);
            }
            catch (Exception)
            {
            }
            return dt;
        }

        #region 主页面
        /// <summary>
        /// 获取设备上道数量
        /// </summary>
        /// <returns></returns>
        public object GetInRegionCount()
        {
            Object inRegionCount = "0";
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from jc_terminal where isdelete = 1 AND in_region = 1";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                inRegionCount = sqlCmd.ExecuteScalar();
                //sqlAdapter.Fill(dt);
                //if (dt.Rows.Count < 0)
                //{
                //    inRegionCount = sqlCmd.ExecuteScalar();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("查询到的总条数" + inRegionCount);

            #region
            //String connetStr = "server=119.96.189.95;port=3306;user=haiwei;password=haiwei123; database=smart_iron_shoes;";
            //string sql = "select count(*) from jc_terminal where isdelete = 1 AND in_region = 1";
            //Object inRegionCount = "0";
            //MySqlConnection conn = new MySqlConnection(connetStr);
            //MySqlCommand comm = new MySqlCommand(sql, conn);
            //try
            //{
            //    conn.Open();
            //    inRegionCount = comm.ExecuteScalar();
            //}
            //catch (MySqlException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //};
            //Console.WriteLine("查询到的总条数" + inRegionCount);
            #endregion

            return inRegionCount;
        }


        /// <summary>
        /// 获取下道铁鞋数量
        /// </summary>
        /// <returns></returns>
        public object GetNextIronShoesCount()
        {
            object nextIronShoesCount = "0";
            //string sql = "select count(*) from jc_terminal where isdelete = 1 AND in_region = 0";
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from jc_terminal where isdelete = 1 AND in_region = 0";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                nextIronShoesCount = sqlCmd.ExecuteScalar();
                //sqlAdapter.Fill(dt);
                //if (dt.Rows.Count < 0)
                //{
                //    nextIronShoesCount = sqlCmd.ExecuteScalar();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("查询到的总条数" + nextIronShoesCount);
            return nextIronShoesCount;
        }


        /// <summary>
        /// 获取入柜铁鞋数量
        /// </summary>
        /// <returns></returns>
        public object GetIntoIronShoesCount()
        {
            object intoIronShoesCount = "0";
            //string sql = "select count(*) from jc_terminal where isdelete = 1 AND is_in = 1";
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from jc_terminal where isdelete = 1 AND is_in = 1";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                intoIronShoesCount = sqlCmd.ExecuteScalar();
                //sqlAdapter.Fill(dt);
                //if (dt.Rows.Count < 0)
                //{
                //    intoIronShoesCount = sqlCmd.ExecuteScalar();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("查询到的总条数" + intoIronShoesCount);
            return intoIronShoesCount;
        }


        /// <summary>
        /// 获取故障铁鞋数量
        /// </summary>
        /// <returns></returns>
        public object GetFaultIronShoesCount()
        {
            object faultIronShoesCount = "0";
            //string sql = "select count(*) from jc_terminal where isdelete = 1 AND is_fault = 1";
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from jc_terminal where isdelete = 1 AND is_fault = 1";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            DataTable dt = new DataTable();
            try
            {
                faultIronShoesCount = sqlCmd.ExecuteScalar();
                //sqlAdapter.Fill(dt);
                //if (dt.Rows.Count < 0)
                //{
                //    faultIronShoesCount = sqlCmd.ExecuteScalar();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("查询到的总条数" + faultIronShoesCount);
            return faultIronShoesCount;
        }
        #endregion


        #region 设备上道

        /// <summary>
        /// 获取需要上道的设备id
        /// </summary>
        /// <returns></returns>
        public int GetIsRegionID(string termCode)
        {
            //List<jcTerminal> jcTerminalList = new List<jcTerminal>();
            int count = 0;
            JcTerminal jcTerminal = new JcTerminal();
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from jc_terminal where  in_region = 0 and is_fault = 0 and term_code = '" + termCode + "' ";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            #region
            //DataTable dt = new DataTable();
            //try
            //{
            //    sqlAdapter.Fill(dt);
            //}
            //catch (Exception)
            //{
            //}
            //MySqlDataReader reader = sqlCmd.ExecuteReader();

            //try
            //{
            //    if (reader != null)
            //    {
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    Console.WriteLine(reader[i]);
            //                }
            //                jcTerminal jcTerminal = new jcTerminal();
            //                jcTerminal.TermCode = reader["term_code"].ToString();
            //                jcTerminalList.Add(jcTerminal);
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("没有查到数据！");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "数据库操作异常！！");
            //}
            //Console.WriteLine("==============================查到的数据" + jcTerminalList);
            #endregion
            try
            {
                count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("查询到的总条数" + count);
            return count;
        }

        MySqlDataReader reader = null;

        /// <summary>
        /// 获取上道区域
        /// </summary>
        /// <returns></returns>
        public List<string> GetRoad()
        {
            List<string> txRailroadsList = new List<string>();
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select road_id,direction,road_name from tx_railroad";
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            try
            {
                reader = sqlCmd.ExecuteReader();
                #region
                //try
                //{
                //    if (reader != null)
                //    {
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                for (int i = 0; i < reader.FieldCount; i++)
                //                {
                //                    TxRailroad txRailroad = new TxRailroad()
                //                    {
                //                        RoadId = reader.GetInt32(0),
                //                        RoadName = reader.GetString(1)
                //                    };

                //                    txRailroadsList.Add(txRailroad);
                //                    Console.WriteLine(reader[i]);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            Console.WriteLine("没有查到数据！");
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message + "数据库操作异常！！");
                //}
                //Console.WriteLine("==============================查到的数据" + txRailroadsList.ToString());
                #endregion
                while (reader.Read())
                {
                    txRailroadsList.Add(reader["road_name"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //关闭MySqlDataAdapter
                reader.Close();
            }

            return txRailroadsList;
            
        }

        /// <summary>
        /// 获取操作人员名单
        /// </summary>
        /// <returns></returns>
        public List<string> GetPersonName()
        {
            List<string> personNameList = new List<string>();
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = m_Connection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select person_id,person_name from tx_person";
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            try
            {
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    personNameList.Add(reader["person_name"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //关闭MySqlDataAdapter
                reader.Close();
            }
            return personNameList;
        }

        /// <summary>
        /// 铁鞋上道
        /// </summary>
        /// <returns></returns>
        //public List<string> InRegion()
        //{
           
        //    int count = 0;
        //    JcTerminal jcTerminal = new JcTerminal();
        //    List<string> personNameList = new List<string>();
        //    MySqlCommand sqlCmd = new MySqlCommand();
        //    sqlCmd.Connection = m_Connection;
        //    sqlCmd.CommandType = CommandType.Text;
        //    sqlCmd.CommandText = "update jc_terminal set in_region = 1,region_id = {0} , operator_id={1} where isdelete = 1"+ jcTerminal.RegionId + jcTerminal.OperatorId;
        //    MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
        //    sqlAdapter.SelectCommand = sqlCmd;
            



        //    return null;
        //}

        #endregion
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace IronShoes
{
    public class SysConfig
    {
        private static RegistryKey rk;

        public static void init()
        {
            rk = Registry.CurrentUser.OpenSubKey("Software\\HW\\TieXie", true);
            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey("Software\\HW\\TieXie");
            }
        }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public static string UserName
        {
            get
            {
                return (string)rk.GetValue("UserName", "");
            }
            set
            {
                rk.SetValue("UserName", value);
            }
        }

        /// <summary>
        /// 中心IP地址
        /// </summary>
        public static string ServerIP
        {
            get
            {
                return (string)rk.GetValue("ServerIP", "127.0.0.1");
            }
            set
            {
                rk.SetValue("ServerIP", value);
            }
        }

        /// <summary>
        /// 中心端口
        /// </summary>
        public static int ServerPort
        {
            get
            {
                int port = 10000;
                int.TryParse((string)rk.GetValue("ServerPort", "10000"), out port);
                return port;
            }
            set
            {
                rk.SetValue("ServerPort", value.ToString());
            }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        public static string DBServer
        {
            get
            {
                return (string)rk.GetValue("DBServer", "119.96.189.95");
            }
            set
            {
                rk.SetValue("DBServer", value);
            }
        }

        /// <summary>
        /// 数据库端口
        /// </summary>
        public static int DBPort
        {
            get
            {
                int port = 10000;
                int.TryParse((string)rk.GetValue("DBPort", "3306"), out port);
                return port;
            }
            set
            {
                rk.SetValue("DBPort", value.ToString());
            }
        }

        public static string DBName
        {
            //smart_iron_shoes
            get
            {
                return (string)rk.GetValue("DBName", "smart_iron_shoes");
            }
            set
            {
                rk.SetValue("DBName", value);
            }
        }

        public static string DBUser
        {
            //haiwei
            get
            {
                return (string)rk.GetValue("DBUser", "haiwei");
            }
            set
            {
                rk.SetValue("DBUser", value);
            }
        }

        public static string DBPassword
        {
            //haiwei123
            get
            {
                return (string)rk.GetValue("DBPassword", "haiwei123");
            }
            set
            {
                rk.SetValue("DBPassword", value);
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public static string Password = "";
        
        /// <summary>
        /// 线颜色
        /// </summary>
        public static long LineColor
        {
            get
            {
                return System.Drawing.Color.Blue.ToArgb();
            }
        }
        /// <summary>
        /// 点颜色
        /// </summary>
        public static long PointColor
        {
            get
            {
                return System.Drawing.Color.Blue.ToArgb();
            }
        }
        /// <summary>
        /// 点报警颜色
        /// </summary>
        public static long AlarmColor
        {
            get
            {
                return System.Drawing.Color.Red.ToArgb();
            }
        }
        /// <summary>
        /// 点大小
        /// </summary>
        public static int PointSize
        {
            get
            {
                return 24;
            }
        }
        /// <summary>
        /// 线粗度
        /// </summary>
        public static int LineSize
        {
            get
            {
                return 1;
            }
        }

    }

}

using IronShoes.data;
using IronShoes.entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IronShoes
{
    public partial class UserControlTernOnTheWay : Form
    {
        public UserControlTernOnTheWay()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlTernOnTheWay_Load(object sender, EventArgs e)
        {
            //区域下拉框填充数据
            cboPosition1();

            //操作人员下拉框填充数据
            cboPerson();
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        private void btn_registration_Click(object sender, EventArgs e)
        {
            //验证铁鞋编号是否可用
            isTermCode();

            //设备上道
            InRegion();

            //非空验证
            isEmpty();

        }

        /// <summary>
        /// 铁鞋编号
        /// </summary>
        private void tbox_terminalIDs_TextChanged(object sender, EventArgs e)
        {
            //单击清空文本框
            //tbox_terminalIDs.Text = "";
        }


        /// <summary>
        /// 验证铁鞋编号是否正确
        /// </summary>
        public void isTermCode()
        {
            string termCode = tbox_terminalIDs.Text;
            int count = GlobalDefine.s_dbGateway.GetIsRegionID(termCode);
            if (count == 1)
            {
                MessageBox.Show("铁鞋编号输入正确！");
            }
            else
            {
                MessageBox.Show("铁鞋编号输入错误，请重新输入！");
                tbox_terminalIDs.Text = "";
            }
        }

        /// <summary>
        /// 填充数据到区域下拉框
        /// </summary>
        public void cboPosition1()
        {
            //cbo_position1.DisplayMember = "road_name";
            //cbo_position1.ValueMember = "road_id";
            cbo_position1.DataSource = GlobalDefine.s_dbGateway.GetRoad();
        }


        /// <summary>
        /// 操作人员下拉框填充数据
        /// </summary>
        public void cboPerson()
        {
            //cbo_position1.DisplayMember = "person_name";
            //cbo_position1.ValueMember = "person_id";
            cbo_person.DataSource = GlobalDefine.s_dbGateway.GetPersonName();
        }

        /// <summary>
        /// 设备上道
        /// </summary>
        public void InRegion()
        {
            //获取相关的字段信息
            //铁鞋编号
            string termCode = tbox_terminalIDs.Text;
            //位置
            int regionId = cbo_position1.SelectedIndex + 1;
            //操作人
            int operatorId = cbo_person.SelectedIndex + 1;

            string connetStr = "server=119.96.189.95;port=3306;user=haiwei;password=haiwei123; database=smart_iron_shoes;";
            string sql = "update jc_terminal set in_region = 1,region_id = "+ regionId + " , operator_id= "+ operatorId + " where term_code = '" + termCode + "' ";
            MySqlConnection conn = new MySqlConnection(connetStr);
            MySqlCommand comm = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                int result = comm.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("设备上道成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("设备上道失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            };
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            string terminalID = tbox_terminalIDs.Text.Trim();
            string position = cbo_position1.Text;
            string person = cbo_person.Text;
            if (terminalID.Equals(string.Empty) || position.Equals(string.Empty) || person.Equals(string.Empty))
            {
                MessageBox.Show("信息不能为空！");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

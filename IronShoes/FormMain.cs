using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronShoes.UserControls;

namespace IronShoes
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 设备管理界面
        /// </summary>
        private UserControlEquipmentManage _userControlEquipManage = null;

        /// <summary>
        /// 人员管理界面
        /// </summary>
        private UserControlPersonManage _userControlUserManage = null;
        
        /// <summary> 文件自动保存路径
        /// </summary>
        private String _autosavepath = String.Empty;

        /// <summary> 打开文件
        /// </summary>
        private String _filepath = String.Empty;

        private ComponentResourceManager _resources = new ComponentResourceManager(typeof(FormMain));

        public static string AppNameAndVol = "铁路智能铁鞋防溜管理系统 v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            //数据库连接
            try
            {
                GlobalDefine.s_dbGateway = new data.MySQLDBImplSQL();
                if (!GlobalDefine.s_dbGateway.ConnectDB(SysConfig.DBServer, SysConfig.DBName,
                                SysConfig.DBUser, SysConfig.DBPassword, SysConfig.DBPort.ToString()))
                    MessageBox.Show("数据库连接失败!");
                else
                    MessageBox.Show("数据库连接成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接异常：" + ex.Message);
            }

            frmFactory.GetUCRailwayMon().Dock = DockStyle.Fill;
            frmFactory.GetUCRailwayMon().Parent = this.panelMain;
            frmFactory.GetUCRailwayMon().Visible = true;

            _userControlEquipManage = new UserControlEquipmentManage();
            _userControlEquipManage.Dock = DockStyle.Fill;
            _userControlEquipManage.Parent = this.panelMain;
            _userControlEquipManage.Visible = false;

            _userControlUserManage = new UserControlPersonManage();
            _userControlUserManage.Dock = DockStyle.Fill;
            _userControlUserManage.Parent = this.panelMain;
            _userControlUserManage.Visible = false;

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            labelServer.Text = "中心服务器：" + SysConfig.ServerIP + ":" + SysConfig.ServerPort.ToString();
            labelLogTime.Text = "登录时间：" + DateTime.Now.ToString();
            labelUserInfo.Text = "用户：" + SysConfig.UserName;
            this.Text = AppNameAndVol + " - 铁鞋管理";

           
            
        }

        //private void buttonShoesApply_Click(object sender, EventArgs e)
        //{
        //    frmShoesApply frm = new frmShoesApply();
        //    frm.ShowDialog();
        //}

        //private void buttonSystemSet_Click(object sender, EventArgs e)
        //{
        //    frmSystemSet frm = new frmSystemSet();
        //    frm.ShowDialog();
        //}

        //private void buttonAbout_Click(object sender, EventArgs e)
        //{
        //    frmAbout frm = new frmAbout();
        //    frm.ShowDialog();
        //}

        //private void buttonEquipmentManage_Click(object sender, EventArgs e)
        //{
        //    frmFactory.GetUCRailwayMon().Visible = false;
        //    _userControlEquipManage.Visible = true;
        //    _userControlUserManage.Visible = false;

        //    changeButton(sender);
        //}

        //private void buttonPersonManage_Click(object sender, EventArgs e)
        //{
        //    frmFactory.GetUCRailwayMon().Visible = false;
        //    _userControlEquipManage.Visible = false;
        //    _userControlUserManage.Visible = true;

        //    changeButton(sender);
        //}

        //private void buttonRailwayMon_Click(object sender, EventArgs e)
        //{
        //    frmFactory.GetUCRailwayMon().Visible = true;
        //    _userControlEquipManage.Visible = false;
        //    _userControlUserManage.Visible = false;

        //    changeButton(sender);
        //}

        private void changeButton(object btn)
        {
            if (btn.GetType() == typeof(DevComponents.DotNetBar.ButtonX))
            {
                //buttonRailwayMon.Image = (Image)_resources.GetObject("buttonRailwayMon.Image");
                //buttonEquipmentManage.Image = (Image)_resources.GetObject("buttonEquipmentManage.Image");
                //buttonPersonManage.Image = (Image)_resources.GetObject("buttonPersonManage.Image");
                //buttonRailwayMon.HoverImage = (Image)_resources.GetObject("buttonRailwayMon.HoverImage");
                //buttonEquipmentManage.HoverImage = (Image)_resources.GetObject("buttonEquipmentManage.HoverImage");
                //buttonPersonManage.HoverImage = (Image)_resources.GetObject("buttonPersonManage.HoverImage");

                string btnName = ((DevComponents.DotNetBar.ButtonX)btn).Name;
                switch (btnName)
                {
                    case "buttonRailwayMon":
                        //buttonRailwayMon.Image = global::IronShoes.Properties.Resources.btn_monitor_hover;
                        //buttonRailwayMon.HoverImage = global::IronShoes.Properties.Resources.btn_monitor_hover;
                        this.Text = AppNameAndVol + " - 铁鞋管理";
                        break;
                    case "buttonEquipmentManage":
                        //buttonEquipmentManage.Image = global::IronShoes.Properties.Resources.btn_remoteReplay_hover;
                        //buttonEquipmentManage.HoverImage = global::IronShoes.Properties.Resources.btn_remoteReplay_hover;
                        this.Text = AppNameAndVol + " - 设备管理";
                        break;
                    case "buttonPersonManage":
                        //buttonPersonManage.Image = global::IronShoes.Properties.Resources.btn_localReplay_hover;
                        //buttonPersonManage.HoverImage = global::IronShoes.Properties.Resources.btn_localReplay_hover;
                        this.Text = AppNameAndVol + " - 人员管理";
                        break;
                }
            }
        }

        /// <summary>
        /// 强制下道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItemShoesPlace_Click(object sender, EventArgs e)
        {
            frmShoesApply frm = new frmShoesApply();
            frm.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // GlobalDefine.s_dbGateway.Close();
        }

        /// <summary>
        /// 更换铁道图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItemFigure_Click(object sender, EventArgs e)
        {
           
        //    if (DialogResult.OK == ofDialog.ShowDialog())
        //    {
        //        #region 清除临时文件及加载打开的文件
        //        ClearTempFile();
        //        String openfile = ofDialog.FileName;
        //        _autosavepath = Path.GetDirectoryName(openfile) + @"\~" + Path.GetFileName(openfile) + "t";
        //        if (!_filepath.Equals(openfile))
        //        {
        //            if (File.Exists(_autosavepath))
        //            {
        //                if (DialogResult.Yes ==
        //                    MessageBox.Show("工作区域从错误中恢复，是否打开恢复文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        //                {
        //                    _filepath = openfile;
        //                    openfile = _autosavepath;
        //                }
        //            }
        //            else
        //            {
        //                _filepath = openfile;
        //            }
        //        }
        //        using (FileStream fs = new FileStream(openfile, FileMode.Open))
        //        {
        //           // _wa.OpenWork(fs);
        //            fs.Close();
        //        }
        //        #endregion
        //        String filename = Path.GetFileName(openfile);
        //        //this.Text = String.Format(_frmtitle, filename.Substring(0, filename.IndexOf(".")));
        //        this.Text = String.Format(filename.Substring(0, filename.IndexOf(".")));
        //        //CreateWorkArea();
        //        //hscWScroll.Value = 0;
        //        //vscWScroll.Value = 0;
        //        //ScrollBar_Scroll(null, null);
        //        //SetAutSaveTime();
        //        //tmrAutoSave_Tick(sender, e);
        //        //_ischange = false;
        //        //_wa.RecordHistory(false);
        //    }

        }

        /// <summary> 删除临时文件
        /// </summary>
        private void ClearTempFile()
        {
            if (File.Exists(_autosavepath))
            {
                if (!String.IsNullOrEmpty(_filepath))
                {
                    File.SetAttributes(_autosavepath, FileAttributes.Normal);
                }
                File.Delete(_autosavepath);
            }
        }
    }
}

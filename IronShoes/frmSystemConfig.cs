using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IronShoes
{
    public partial class frmSystemConfig : Form
    {
        /// <summary> 工作区域对象实例
        /// </summary>
        private WorkArea _wa = null;

        /// <summary> 内容是否改变标志
        /// </summary>
        private Boolean _ischange = false;        
        #region 窗体事件
        /// <summary> 窗体构造函数
        /// </summary>
        /// <param name="wa">工作区域对象实例</param>
        public frmSystemConfig(WorkArea wa)
        {
            InitializeComponent();
            _wa = wa;
            #region 基本设置
            txtWWith.Text = _wa.Width.ToString();
            txtWHeight.Text = _wa.Height.ToString();
            txtGWith.Text = _wa.GridWidth.ToString();
            txtGHeight.Text = _wa.GridHeight.ToString();
            lblWBC.BackColor = _wa.BackColor;
            lblGLC.BackColor = _wa.GridColor;
            ckbShowGrid.Checked = _wa.ShowGrid;
            #endregion
            #region 元素设置
            txtLnWidth.Text = _wa.LineWidth.ToString();
            txtLgDiameter.Text = _wa.LightDiameter.ToString();
            #endregion
        }

        /// <summary> 窗关闭之后事件
        /// </summary>
        private void frmSystemConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((IDisposable)this).Dispose();
        }
        #endregion

        /// <summary> 输入框内容改变事件
        ///     1、判断工作区域参数是否有改变
        /// </summary>
        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (!_ischange)
            {
                _ischange = true;
            }
        }

        /// <summary> 确定按键点击事件
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_ischange)
            {
                #region 基本设置
                _wa.Width = Convert.ToInt32(txtWWith.Text);
                _wa.Height = Convert.ToInt32(txtWHeight.Text);
                _wa.GridWidth = Convert.ToInt32(txtGWith.Text);
                _wa.GridHeight = Convert.ToInt32(txtGHeight.Text);
                _wa.BackColor = lblWBC.BackColor;
                _wa.GridColor = lblGLC.BackColor;
                _wa.ShowGrid = ckbShowGrid.Checked;
                #endregion
                #region 元素设置
                _wa.LineWidth = Convert.ToSingle(txtLnWidth.Text);
                _wa.LightDiameter =  Convert.ToInt32(txtLgDiameter.Text);
                #endregion
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary> 颜色选择点击事件
        /// </summary>
        private void SelectColor_Click(object sender, EventArgs e)
        {
            Label lbltemp = sender as Label;
            using (ColorDialog clDialog = new ColorDialog())
            {
                clDialog.Color = lbltemp.BackColor;
                if (!clDialog.Color.IsKnownColor &&
                    !clDialog.Color.IsNamedColor &&
                    !clDialog.Color.IsSystemColor)
                {
                    clDialog.FullOpen = true;
                    clDialog.CustomColors = new Int32[] { ColorTranslator.ToWin32(clDialog.Color) };
                }
                if (DialogResult.OK == clDialog.ShowDialog(this))
                {
                    lbltemp.BackColor = clDialog.Color;
                    txt_TextChanged(sender, e);
                }
            }
        }

        /// <summary> 输入框键盘输入事件
        ///     1、只允许输入正整数
        /// </summary>
        private void txt_TextKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}

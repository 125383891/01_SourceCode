using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerService
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 微信企业号  用户数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Wchat_User_Click(object sender, EventArgs e)
        {
            MsgShow();
        }

        private void MsgShow() {
            lbx_msg.Items.Add("111");
        }
    }
}

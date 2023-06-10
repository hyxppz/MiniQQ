using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniQQLib;

namespace MiniQQClient
{
    public partial class ChangeNickName : Form
    {
        public ChangeNickName()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (new_name.Text != "")
            {
                ModNameReq change_name = new ModNameReq();
                change_name.FriendNickName = new_name.Text.Trim();
                change_name.FriendName = old_name.Text.Trim();
                change_name.Username = MyTools.getUserinfo().Username;
                TcpClientManager.Instance.SendMesg(change_name, MsgType.MSG_TYPE_MOD_NAME_REQ);

            }
            else
            {
                MessageBox.Show("请填写新昵称！", "提示");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using MiniQQLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniQQClient
{
    public partial class FriendForm : Form
    {
        public void RecAddFriendRspAction(AddFriendRsp rsp)
        {
            if (rsp.Result)
            {

                MyTools.setUserinfo(rsp.userinfo);
                MessageBox.Show(rsp.ErrorMsg);
            }
            else
            {
                MessageBox.Show(rsp.ErrorMsg);
            }
        }
        public FriendForm()
        {
            InitializeComponent();
            TcpClientManager.Instance.RecAddFriendRspAction = RecAddFriendRspAction;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Username.Text.Trim() == "")
            {
                MessageBox.Show("请输入DD号/用户名");
                return;
            }
            AddFriendReq req = new AddFriendReq();
            req.Username = MyTools.getUserinfo().Username;
            req.FriendName = Username.Text.Trim();
            TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_ADD_FRIEND_REQ);
        }
    }
}

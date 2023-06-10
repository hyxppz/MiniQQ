using MiniQQLib;

namespace MiniQQClient
{
    public partial class UserRegister : Form
    {
        public void RecRegisterRspAct(RegisterRsp rsp)
        {
            if (rsp.Result)
            {
                MessageBox.Show(rsp.ErrorMsg);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(rsp.ErrorMsg);
            }
        }
        public UserRegister()
        {
            InitializeComponent();
            TcpClientManager.Instance.RecRegisterRspAction = RecRegisterRspAct;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (Username.Text.Trim() == "")
            {
                MessageBox.Show("请输入DD号/用户名","提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            if (Pwd.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            if (PwdConfirm.Text.Trim() != Pwd.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            RegisterReq req = new RegisterReq();
            req.Username = Username.Text.Trim();
            req.Password = Pwd.Text.Trim();
            TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_REGISTER_REQ);

        }

    }
}

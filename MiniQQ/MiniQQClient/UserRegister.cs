using MiniQQLib;

namespace MiniQQClient
{
    public partial class UserRegister : Form
    {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterReq req = new RegisterReq();
            req.Username = "LSZ";
            req.Password = "12345";
            TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_REGISTER_REQ);
        }
    }
}

using MiniQQLib;
using MiniQQServer;

namespace MiniQQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TCPServerManager.Instance.OpenServer(19521);
            button1.Enabled = false;
            TCPServerManager.Instance.RecRegisterReqAction = UserRegister;


        }

        public void UserRegister(RegisterReq registerReq)
        {

        }
    }
}
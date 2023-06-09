using MiniQQLib;

namespace MiniQQClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {

            InitializeComponent();
            TcpClientManager.Instance.RecLoginRspAction = HandleLoginResponse;

        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            TcpClientManager.Instance.Init("127.0.0.1");
            TcpClientManager.Instance.StartConnect();

            //zxy
            string username = account.Text;
            string Password = password.Text;

            // 创建一个新的 LoginReq 对象，这是你的登录请求
            LoginReq loginRequest = new LoginReq();
            loginRequest.Username = username;
            loginRequest.Password = Password;

            // 使用 TcpClientManager 发送登录请求
            TcpClientManager.Instance.SendMesg(loginRequest, MsgType.MSG_TYPE_LOGIN_REQ);


            // todo登录成功
            /*if (true)
            {
                Random rnd = new Random();
               
                this.DialogResult = DialogResult.OK;
                Userinfo userinfo = new Userinfo();
                int a = rnd.Next(3);
                if (a>1) { userinfo.Username = "2"; } else {  userinfo.Username = "1"; }
               
                userinfo.Password = "1";
                List<FriendInfo> friendInfos = new List<FriendInfo>();
                userinfo.FriendInfos = friendInfos;
               /* FriendInfo f1 = new FriendInfo();
                f1.FriendName = "榜一大哥";
                FriendInfo f2 = new FriendInfo();
                f2.FriendName = "小天才";
                f2.FriendNickName = "大笨蛋";
                friendInfos.Add(f1);
                friendInfos.Add(f2);
                MyTools.setUserinfo(userinfo);
                LoginReq lo=new LoginReq();
                lo.Username= userinfo.Username;
                TcpClientManager.Instance.SendMesg(lo, MsgType.MSG_TYPE_LOGIN_REQ);
            }*/
        }

        private void Registerbutton_Click(object sender, EventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.ShowDialog();
        }

        //zxy
        public void HandleLoginResponse(LoginRsp loginResponse)
        {
            if (loginResponse.Result)
            {
                // 登录成功，加载用户信息并转到聊天界面
                
                MyTools.setUserinfo(loginResponse.User);
                this.DialogResult = DialogResult.OK;

                // 隐藏登录窗口，打开聊天窗口
                
            }
            else
            {
                // 登录失败，显示错误消息
                MessageBox.Show(loginResponse.ErrorMsg) ;

            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Click(object sender, EventArgs e)
        {
            label1.Focus();
        }
    }
}

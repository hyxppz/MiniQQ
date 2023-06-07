using MiniQQLib;

namespace MiniQQClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {

            InitializeComponent();
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            // todo登录成功
            if (true)
            {
               this.DialogResult = DialogResult.OK;
                Userinfo userinfo = new Userinfo();
                userinfo.Username = "1";
                userinfo.Password = "1";
                List<FriendInfo> friendInfos = new List<FriendInfo>();
                userinfo.FriendInfos = friendInfos;
                FriendInfo f1=new FriendInfo();
                f1.FriendName= "榜一大哥";
                FriendInfo f2 = new FriendInfo();
                f2.FriendName = "小天才";
                f2.FriendNickName = "大笨蛋";
                friendInfos.Add(f1);
                friendInfos.Add(f2);
                MyTools.setUserinfo(userinfo);
            }
        }

        private void Registerbutton_Click(object sender, EventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.ShowDialog();
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

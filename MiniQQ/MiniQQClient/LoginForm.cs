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
            // todu登录成功
            if (true)
            {
                this.DialogResult = DialogResult.OK;

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

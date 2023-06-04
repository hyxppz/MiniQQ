namespace MiniQQClient
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Loginbutton = new Button();
            Registerbutton = new Button();
            titleIcon = new PictureBox();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            title = new Label();
            account = new TextBox();
            password = new TextBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)titleIcon).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // Loginbutton
            // 
            Loginbutton.BackColor = Color.DeepSkyBlue;
            Loginbutton.FlatStyle = FlatStyle.Flat;
            Loginbutton.ForeColor = Color.White;
            Loginbutton.Location = new Point(102, 274);
            Loginbutton.Name = "Loginbutton";
            Loginbutton.Size = new Size(222, 35);
            Loginbutton.TabIndex = 0;
            Loginbutton.Text = "登录";
            Loginbutton.UseVisualStyleBackColor = false;
            Loginbutton.Click += Loginbutton_Click;
            // 
            // Registerbutton
            // 
            Registerbutton.BackColor = Color.Transparent;
            Registerbutton.BackgroundImageLayout = ImageLayout.None;
            Registerbutton.FlatAppearance.BorderSize = 0;
            Registerbutton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Registerbutton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Registerbutton.FlatStyle = FlatStyle.Flat;
            Registerbutton.ForeColor = Color.Silver;
            Registerbutton.Location = new Point(0, 297);
            Registerbutton.Name = "Registerbutton";
            Registerbutton.Size = new Size(70, 32);
            Registerbutton.TabIndex = 1;
            Registerbutton.Text = "用户注册";
            Registerbutton.UseVisualStyleBackColor = false;
            Registerbutton.Click += Registerbutton_Click;
            // 
            // titleIcon
            // 
            titleIcon.BackColor = Color.Transparent;
            titleIcon.Image = Properties.Resources.dog_opacity;
            titleIcon.Location = new Point(16, 3);
            titleIcon.Name = "titleIcon";
            titleIcon.Size = new Size(31, 30);
            titleIcon.SizeMode = PictureBoxSizeMode.Zoom;
            titleIcon.TabIndex = 3;
            titleIcon.TabStop = false;
            titleIcon.Click += pictureBox2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = Properties.Resources.login_bg;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel2);
            panel1.ImeMode = ImeMode.NoControl;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(429, 174);
            panel1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Cursor = Cursors.Hand;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(396, 9);
            label1.Name = "label1";
            label1.Size = new Size(21, 22);
            label1.TabIndex = 6;
            label1.Text = "X";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.dog;
            pictureBox1.Location = new Point(155, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 95);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(titleIcon);
            panel2.Controls.Add(title);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(117, 36);
            panel2.TabIndex = 5;
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            title.ForeColor = Color.White;
            title.Location = new Point(43, 3);
            title.Name = "title";
            title.Size = new Size(47, 30);
            title.TabIndex = 5;
            title.Text = "DD";
            // 
            // account
            // 
            account.BorderStyle = BorderStyle.None;
            account.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            account.Location = new Point(141, 196);
            account.Name = "account";
            account.PlaceholderText = "DD账号";
            account.Size = new Size(183, 28);
            account.TabIndex = 5;
            // 
            // password
            // 
            password.BorderStyle = BorderStyle.None;
            password.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            password.Location = new Point(141, 230);
            password.Name = "password";
            password.PasswordChar = '*';
            password.PlaceholderText = "密码";
            password.Size = new Size(183, 28);
            password.TabIndex = 6;
            password.TextChanged += password_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.account;
            pictureBox2.Location = new Point(102, 198);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(33, 26);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.password;
            pictureBox3.Location = new Point(102, 232);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(33, 26);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(429, 327);
            ControlBox = false;
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(password);
            Controls.Add(account);
            Controls.Add(panel1);
            Controls.Add(Registerbutton);
            Controls.Add(Loginbutton);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(429, 327);
            MinimumSize = new Size(429, 327);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MiniQQ登录";
            Load += LoginForm_Load;
            Click += LoginForm_Click;
            ((System.ComponentModel.ISupportInitialize)titleIcon).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Loginbutton;
        private Button Registerbutton;
        private PictureBox titleIcon;
        private Panel panel1;
        private Label title;
        private Panel panel2;
        private PictureBox pictureBox1;
        private TextBox account;
        private TextBox password;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label1;
    }
}
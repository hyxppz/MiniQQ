﻿namespace MiniQQClient
{
    partial class UserRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserRegister));
            name = new Label();
            password = new Label();
            Username = new TextBox();
            Pwd = new TextBox();
            PwdConfirm = new TextBox();
            label1 = new Label();
            reg_button = new Button();
            SuspendLayout();
            // 
            // name
            // 
            name.AutoSize = true;
            name.BackColor = Color.Transparent;
            name.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            name.ForeColor = SystemColors.HighlightText;
            name.Location = new Point(44, 50);
            name.Name = "name";
            name.Size = new Size(82, 17);
            name.TabIndex = 0;
            name.Text = "DD号/用户名";
            // 
            // password
            // 
            password.AutoSize = true;
            password.BackColor = Color.Transparent;
            password.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            password.ForeColor = SystemColors.HighlightText;
            password.Location = new Point(44, 89);
            password.Name = "password";
            password.Size = new Size(32, 17);
            password.TabIndex = 1;
            password.Text = "密码";
            // 
            // Username
            // 
            Username.BackColor = Color.White;
            Username.Location = new Point(132, 47);
            Username.Name = "Username";
            Username.Size = new Size(209, 23);
            Username.TabIndex = 2;
            // 
            // Pwd
            // 
            Pwd.Location = new Point(132, 86);
            Pwd.Name = "Pwd";
            Pwd.PasswordChar = '*';
            Pwd.Size = new Size(208, 23);
            Pwd.TabIndex = 3;
            // 
            // PwdConfirm
            // 
            PwdConfirm.Location = new Point(132, 124);
            PwdConfirm.Name = "PwdConfirm";
            PwdConfirm.PasswordChar = '*';
            PwdConfirm.Size = new Size(208, 23);
            PwdConfirm.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.HighlightText;
            label1.Location = new Point(44, 127);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 4;
            label1.Text = "确认密码";
            // 
            // reg_button
            // 
            reg_button.BackColor = SystemColors.Info;
            reg_button.Location = new Point(78, 166);
            reg_button.Name = "reg_button";
            reg_button.Size = new Size(239, 35);
            reg_button.TabIndex = 6;
            reg_button.Text = "注册";
            reg_button.UseVisualStyleBackColor = false;
            reg_button.Click += button1_Click;
            // 
            // UserRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(406, 254);
            Controls.Add(reg_button);
            Controls.Add(PwdConfirm);
            Controls.Add(label1);
            Controls.Add(Pwd);
            Controls.Add(Username);
            Controls.Add(password);
            Controls.Add(name);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(422, 293);
            MinimizeBox = false;
            MinimumSize = new Size(422, 293);
            Name = "UserRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "用户注册";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label name;
        private Label password;
        private TextBox Username;
        private TextBox Pwd;
        private TextBox PwdConfirm;
        private Label label1;
        private Button reg_button;
    }
}
﻿namespace MiniQQClient
{
    partial class FriendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendForm));
            Username = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // Username
            // 
            Username.Location = new Point(12, 105);
            Username.Name = "Username";
            Username.PlaceholderText = "请输入好友DD号/用户名";
            Username.Size = new Size(157, 23);
            Username.TabIndex = 0;
            Username.TextAlign = HorizontalAlignment.Center;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Info;
            button1.Location = new Point(172, 105);
            button1.Name = "button1";
            button1.Size = new Size(61, 23);
            button1.TabIndex = 1;
            button1.Text = "添加";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FriendForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(254, 234);
            Controls.Add(button1);
            Controls.Add(Username);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(270, 273);
            MinimizeBox = false;
            MinimumSize = new Size(270, 273);
            Name = "FriendForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "添加好友";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Username;
        private Button button1;
    }
}
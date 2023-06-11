﻿namespace MiniQQClient
{
    partial class ChangeNickName
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
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            old_name = new TextBox();
            textBox1 = new TextBox();
            new_name = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.HighlightText;
            label1.Location = new Point(98, 62);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(158, 31);
            label1.TabIndex = 0;
            label1.Text = "好友用户名：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.HighlightText;
            label3.Location = new Point(98, 137);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(182, 31);
            label3.TabIndex = 1;
            label3.Text = "好友当前备注：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.HighlightText;
            label2.Location = new Point(98, 206);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(158, 31);
            label2.TabIndex = 2;
            label2.Text = "修改备注为：";
            // 
            // old_name
            // 
            old_name.Enabled = false;
            old_name.Location = new Point(320, 57);
            old_name.Margin = new Padding(6, 5, 6, 5);
            old_name.Name = "old_name";
            old_name.Size = new Size(360, 38);
            old_name.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(320, 131);
            textBox1.Margin = new Padding(6, 5, 6, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(360, 38);
            textBox1.TabIndex = 4;
            // 
            // new_name
            // 
            new_name.Location = new Point(320, 206);
            new_name.Margin = new Padding(6, 5, 6, 5);
            new_name.Name = "new_name";
            new_name.Size = new Size(360, 38);
            new_name.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(484, 292);
            button1.Margin = new Padding(6, 5, 6, 5);
            button1.Name = "button1";
            button1.Size = new Size(150, 42);
            button1.TabIndex = 6;
            button1.Text = "确定";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(234, 292);
            button2.Margin = new Padding(6, 5, 6, 5);
            button2.Name = "button2";
            button2.Size = new Size(150, 42);
            button2.TabIndex = 7;
            button2.Text = "修改";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // ChangeNickName
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_bg;
            ClientSize = new Size(868, 419);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(new_name);
            Controls.Add(textBox1);
            Controls.Add(old_name);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Margin = new Padding(6, 5, 6, 5);
            Name = "ChangeNickName";
            Text = "修改好友昵称";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label2;
        public TextBox old_name;
        public TextBox textBox1;
        private TextBox new_name;
        private Button button1;
        private Button button2;
    }
}
namespace MiniQQClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            friendList = new Panel();
            nofriend = new Label();
            friendExample = new Panel();
            friendExample_name = new Label();
            friendExample_online = new PictureBox();
            label1 = new Label();
            addFriendIcon = new Label();
            placeholder_panel = new Panel();
            chat_panel = new Panel();
            button3 = new Button();
            textBox3 = new TextBox();
            button2 = new Button();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            friendList.SuspendLayout();
            friendExample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)friendExample_online).BeginInit();
            placeholder_panel.SuspendLayout();
            chat_panel.SuspendLayout();
            SuspendLayout();
            // 
            // friendList
            // 
            friendList.AutoScroll = true;
            friendList.BackColor = Color.Transparent;
            friendList.BorderStyle = BorderStyle.Fixed3D;
            friendList.Controls.Add(nofriend);
            friendList.Location = new Point(24, 22);
            friendList.Margin = new Padding(111);
            friendList.Name = "friendList";
            friendList.Size = new Size(189, 592);
            friendList.TabIndex = 0;
            // 
            // nofriend
            // 
            nofriend.AutoSize = true;
            nofriend.Cursor = Cursors.Hand;
            nofriend.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
            nofriend.ForeColor = Color.Yellow;
            nofriend.Location = new Point(8, 286);
            nofriend.Name = "nofriend";
            nofriend.Size = new Size(162, 24);
            nofriend.TabIndex = 0;
            nofriend.Text = "还没好友？去添加";
            nofriend.Visible = false;
            nofriend.Click += label2_Click;
            // 
            // friendExample
            // 
            friendExample.BackColor = Color.Transparent;
            friendExample.Controls.Add(friendExample_name);
            friendExample.Controls.Add(friendExample_online);
            friendExample.Location = new Point(34, 622);
            friendExample.Name = "friendExample";
            friendExample.Size = new Size(179, 22);
            friendExample.TabIndex = 3;
            friendExample.Visible = false;
            // 
            // friendExample_name
            // 
            friendExample_name.AutoSize = true;
            friendExample_name.BackColor = Color.Transparent;
            friendExample_name.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            friendExample_name.ForeColor = Color.White;
            friendExample_name.Location = new Point(29, 0);
            friendExample_name.Name = "friendExample_name";
            friendExample_name.Size = new Size(36, 18);
            friendExample_name.TabIndex = 1;
            friendExample_name.Text = "张三";
            // 
            // friendExample_online
            // 
            friendExample_online.BackColor = Color.Transparent;
            friendExample_online.Image = Properties.Resources.dog;
            friendExample_online.Location = new Point(0, 0);
            friendExample_online.Name = "friendExample_online";
            friendExample_online.Size = new Size(23, 21);
            friendExample_online.SizeMode = PictureBoxSizeMode.Zoom;
            friendExample_online.TabIndex = 2;
            friendExample_online.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(39, 11);
            label1.Name = "label1";
            label1.Size = new Size(36, 18);
            label1.TabIndex = 0;
            label1.Text = "好友";
            // 
            // addFriendIcon
            // 
            addFriendIcon.Anchor = AnchorStyles.Bottom;
            addFriendIcon.AutoSize = true;
            addFriendIcon.BackColor = Color.Transparent;
            addFriendIcon.Cursor = Cursors.Hand;
            addFriendIcon.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            addFriendIcon.ForeColor = Color.White;
            addFriendIcon.Location = new Point(164, 7);
            addFriendIcon.Name = "addFriendIcon";
            addFriendIcon.Size = new Size(27, 25);
            addFriendIcon.TabIndex = 4;
            addFriendIcon.Text = "+";
            addFriendIcon.Click += addFriendIcon_Click;
            // 
            // placeholder_panel
            // 
            placeholder_panel.BackColor = Color.Transparent;
            placeholder_panel.BackgroundImage = Properties.Resources.dog;
            placeholder_panel.BackgroundImageLayout = ImageLayout.Zoom;
            placeholder_panel.Controls.Add(chat_panel);
            placeholder_panel.Location = new Point(269, 22);
            placeholder_panel.Name = "placeholder_panel";
            placeholder_panel.Size = new Size(774, 592);
            placeholder_panel.TabIndex = 5;
            // 
            // chat_panel
            // 
            chat_panel.BackColor = Color.Transparent;
            chat_panel.BackgroundImage = Properties.Resources.dog;
            chat_panel.BackgroundImageLayout = ImageLayout.Zoom;
            chat_panel.Controls.Add(button3);
            chat_panel.Controls.Add(textBox3);
            chat_panel.Controls.Add(button2);
            chat_panel.Controls.Add(textBox2);
            chat_panel.Controls.Add(button1);
            chat_panel.Controls.Add(textBox1);
            chat_panel.Controls.Add(richTextBox1);
            chat_panel.Location = new Point(0, 0);
            chat_panel.Name = "chat_panel";
            chat_panel.Size = new Size(774, 592);
            chat_panel.TabIndex = 6;
            // 
            // button3
            // 
            button3.Location = new Point(606, 532);
            button3.Name = "button3";
            button3.Size = new Size(154, 23);
            button3.TabIndex = 7;
            button3.Text = "发送";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox3
            // 
            textBox3.ForeColor = SystemColors.WindowText;
            textBox3.Location = new Point(3, 358);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(768, 168);
            textBox3.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(606, 329);
            button2.Name = "button2";
            button2.Size = new Size(154, 23);
            button2.TabIndex = 5;
            button2.Text = "清空";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(3, 566);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(190, 23);
            textBox2.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(269, 18);
            button1.Name = "button1";
            button1.Size = new Size(183, 23);
            button1.TabIndex = 3;
            button1.Text = "连接服务";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(190, 23);
            textBox1.TabIndex = 2;
            textBox1.Text = "127.0.0.1";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(3, 58);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(768, 263);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1094, 633);
            Controls.Add(placeholder_panel);
            Controls.Add(addFriendIcon);
            Controls.Add(friendExample);
            Controls.Add(label1);
            Controls.Add(friendList);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1110, 671);
            MinimizeBox = false;
            MinimumSize = new Size(1110, 671);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            friendList.ResumeLayout(false);
            friendList.PerformLayout();
            friendExample.ResumeLayout(false);
            friendExample.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)friendExample_online).EndInit();
            placeholder_panel.ResumeLayout(false);
            chat_panel.ResumeLayout(false);
            chat_panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel friendList;
        private Label label1;
        private Panel friendExample;
        private Label friendExample_name;
        private PictureBox friendExample_online;
        private Label addFriendIcon;
        private Label nofriend;
        private Panel placeholder_panel;
        private Panel chat_panel;
        private RichTextBox richTextBox1;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private TextBox textBox3;
    }
}
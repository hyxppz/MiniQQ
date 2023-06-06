using System.Collections.Generic;
using MiniQQLib;
namespace MiniQQClient
{
    public partial class Form1 : Form
    {

        public void ExceptionAction(string str)
        {

        }
        public Form1()
        {
            InitializeComponent();

            TcpClientManager.Instance.ExceptionMsgAction = ExceptionAction;
            //createFriend("榜一大哥", true);
            //createFriend("榜二大哥", false);
            //createFriend("张三", true);
            //createFriend("李四", false);
            //createFriend("李五", false);
            //createFriend("李六", false);
            //createFriend("刘思佐", false);
            //createFriend("郝宇星", true);

            createFriend("榜一大哥");
            createFriend("榜二大哥", FriendStatus.OFFLINE);
            createFriend("张三");
            createFriend("李四", FriendStatus.OFFLINE);
            createFriend("李五", FriendStatus.OFFLINE);
            createFriend("李六", FriendStatus.OFFLINE);
            createFriend("刘思佐", FriendStatus.OFFLINE);
            createFriend("郝宇星");
            createFriend("张学渊",FriendStatus.WAIT);

        }




        // sizuo start
        public enum FriendStatus
        {
            ONLINE = 0,//在线
            OFFLINE,//离线
            WAIT,//请求添加好友
            
        }
        List<Panel> friends = new List<Panel>();

        void createFriend(string name, FriendStatus status=FriendStatus.ONLINE)
        {
            int length = friends.Count;
            Panel panel = new Panel();
            Label label = new Label();
            PictureBox pictureBox = new PictureBox();
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(label);
            panel.Cursor = Cursors.Hand;
            
            // 
            // friendExample
            // 
            panel.BackColor = Color.Transparent;
            panel.Location = new Point(3, 17 + 25 * (length));
            panel.Name = name + "_friend";
            panel.Size = new Size(149, 22);
            panel.TabIndex = 3;
            // 
            // friendExample_name
            // 
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label.ForeColor = Color.White;
            label.Location = new Point(29, 0);
            label.Name = name + "_name";
            label.Size = new Size(37, 19);
            label.TabIndex = 1;
            label.Text = name;
           
            // 
            // friendExample_online
            // 
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = name + "_status";
            pictureBox.Size = new Size(23, 21);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;
           
            if (status==FriendStatus.ONLINE)
            {
                pictureBox.Image = Properties.Resources.dog;
                panel.Click += openChat;
                pictureBox.Click += openChat;
                label.Click += openChat;
            }
            else if(status == FriendStatus.OFFLINE)
            {
                pictureBox.Image = Properties.Resources.dog_opacity;
                panel.Click += openChat;
                pictureBox.Click += openChat;
                label.Click += openChat;
            }
            else if(status==FriendStatus.WAIT)
            {
                EventHandler waitClick = (object? sender, EventArgs e) => {
                    var confirmResult = MessageBox.Show("是否通过"+name+"的好友请求？",
                                   name+"请求添加您为好友",
                                   MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        // If 'Yes', do something here.
                    }
                    else
                    {
                        // If 'No', do something here.
                    }
                };
                label.ForeColor = Color.LightGreen;
                pictureBox.Image = Properties.Resources.dog_wait;
                panel.Click += waitClick;
                pictureBox.Click += waitClick;
                label.Click += waitClick;
            }
            friendList.Controls.Add(panel);
            friends.Add(panel);
        }

      

        private void openChat(object? sender, EventArgs e)
        {
           
        }

        private void addFriendIcon_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
          
        }



        // sizuo end
    }
}
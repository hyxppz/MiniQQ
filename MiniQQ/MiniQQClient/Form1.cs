using System.Collections.Generic;

namespace MiniQQClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createFriend("榜一大哥", true);
            createFriend("榜二大哥", false);
            createFriend("张三", true);
            createFriend("李四", false);
            createFriend("李五", false);
            createFriend("李六", false);
            createFriend("刘思佐", false);
            createFriend("郝宇星", true);
        }




        // sizuo start
        List<Panel> friends = new List<Panel>();

        void createFriend(string name, bool online)
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
            pictureBox.Name = name + "_online";
            pictureBox.Size = new Size(23, 21);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;
            if (online)
            {
                pictureBox.Image = Properties.Resources.dog;
            }
            else
            {
                pictureBox.Image = Properties.Resources.dog_opacity;
            }
            friendList.Controls.Add(panel);
            friends.Add(panel);
        }

        private void addFriendIcon_Click(object sender, EventArgs e)
        {
            createFriend("大哥", true);
        }
        // sizuo end
    }
}
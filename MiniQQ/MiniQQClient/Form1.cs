using MiniQQLib;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

            this.Text = "DD - " + MyTools.getUserinfo().Username;
            TcpClientManager.Instance.ExceptionMsgAction = ExceptionAction;
            TcpClientManager.Instance.RecRefreshfriendListRspAction = RefreshfriendList;
            TcpClientManager.Instance.RecAddFriendRspAction = RecAddFriendRspAction;
            resetFriendsPanel();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//设置该属性 为false

        }


        public void RecAddFriendRspAction(AddFriendRsp rsp)
        {
            Action delega1 = () =>
            {
                if (rsp.Result)
                {

                    MyTools.setUserinfo(rsp.userinfo);
                    resetFriendsPanel();
                    MessageBox.Show(rsp.ErrorMsg);
                }
                else
                {
                    MessageBox.Show(rsp.ErrorMsg);
                }
            };
            //使用异步多线程更新
            if (this.InvokeRequired)
            {
                //新建一个线程，线程里面调用拉姆达表达式，拉姆达表达式里面使用异步的形式调用委托，委托里面再修改控件的父级
                new Thread(() => this.Invoke(delega1)).Start();
            }
            else
            {
                delega1();
            }
        }

        // sizuo start
        void RefreshfriendList(RefreshFriendListRsp rsp)
        {
            MyTools.setUserinfo(rsp.userinfo);

            resetFriendsPanel();
        }
        void resetFriendsPanel()
        {
            friends.ForEach(e => friendList.Controls.Remove(e));
            friends = new List<Panel>();
            Userinfo u = MyTools.getUserinfo();
            if (u.FriendInfos.FindAll(e => e.Status != FriendStatus.NOREPLY).Count == 0)
            {
                nofriend.Visible = true;
                return;
            }
            else
            {
                nofriend.Visible = false;
            }

            u.FriendInfos.ForEach(f =>
            {
                if (f.Status != FriendStatus.NOREPLY)
                {
                    createFriend(f, f.Status);

                }

            });
        }
        List<Panel> friends = new List<Panel>();

        void createFriend(FriendInfo friendInfo, FriendStatus status = FriendStatus.ONLINE)
        {
            string name = friendInfo.FriendName;
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
            if (friendInfo.FriendNickName != null)
            {
                label.Text = friendInfo.FriendNickName + "(" + name + ")";
            }
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

            if (status == FriendStatus.ONLINE)
            {
                pictureBox.Image = Properties.Resources.dog;
                panel.Click += openChat;
                pictureBox.Click += openChat;
                label.Click += openChat;
            }
            else if (status == FriendStatus.OFFLINE)
            {
                pictureBox.Image = Properties.Resources.dog_opacity;
                panel.Click += openChat;
                pictureBox.Click += openChat;
                label.Click += openChat;
            }
            else if (status == FriendStatus.WAIT)
            {
                EventHandler waitClick = (object? sender, EventArgs e) =>
                {
                    var confirmResult = MessageBox.Show("是否通过" + name + "的好友请求？",
                                   name + "请求添加您为好友",
                                   MessageBoxButtons.YesNoCancel);
                    if (confirmResult == DialogResult.Yes)
                    {
                        AddFriendReq req = new AddFriendReq();
                        req.Username = MyTools.getUserinfo().Username;
                        req.FriendName = name;
                        TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_ADD_FRIEND_REQ);
                    }
                    else if(confirmResult==DialogResult.No)
                    {
                        RefuseReq req=new RefuseReq(); 
                        req.FriendName = name;
                        req.Username= MyTools.getUserinfo().Username;
                        TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_REFUSE_REQ);
                    }
                };
                label.ForeColor = Color.LightGreen;
                pictureBox.Image = Properties.Resources.dog_wait;
                panel.Click += waitClick;
                pictureBox.Click += waitClick;
                label.Click += waitClick;
            }
            Action delega1 = () =>
            {
                friendList.Controls.Add(panel);
                friends.Add(panel);
            };

            //使用异步多线程更新
            if (this.InvokeRequired)
            {
                //新建一个线程，线程里面调用拉姆达表达式，拉姆达表达式里面使用异步的形式调用委托，委托里面再修改控件的父级
                new Thread(() => this.Invoke(delega1)).Start();
            }
            else
            {
                delega1();
            }

        }



        private void openChat(object? sender, EventArgs e)
        {

        }

        private void addFriend()
        {
            FriendForm form = new FriendForm();
            form.ShowDialog();
           /* if (form.DialogResult == DialogResult.Cancel || form.DialogResult == DialogResult.OK)
            {
                resetFriendsPanel();
            }*/

        }

        private void addFriendIcon_Click(object sender, EventArgs e)
        {
            addFriend();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            addFriend();

        }



        // sizuo end
    }
}
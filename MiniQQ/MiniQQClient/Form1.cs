using Microsoft.VisualBasic.Logging;
using MiniQQLib;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MiniQQClient
{
    public partial class Form1 : Form
    {
        public string CurrentFriendUser;
        public void ExceptionAction(string str)
        {

        }

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();

            this.Text = "DD - " + MyTools.getUserinfo().Username;
            TcpClientManager.Instance.ExceptionMsgAction = ExceptionAction;
            TcpClientManager.Instance.RecRefreshfriendListRspAction = RefreshfriendList;
            TcpClientManager.Instance.RecAddFriendRspAction = RecAddFriendRspAction;
            TcpClientManager.Instance.RecModNameRspAction = RecModNameRspAction;
            TcpClientManager.Instance.RecMSGMSGAction = UpdateUI;
            myDelegateUI = new MyDelegateUI(RecMsg);//��ί��
          
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//���ø����� Ϊfalse
        
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1EventProcessor);//����¼�
        }

        public void timer1EventProcessor(object source, EventArgs e)
        {
            resetFriendsPanel();
            timer1.Stop();
        }

        public delegate void MyDelegateUI(MSGMSG msg); //����ί������
        MyDelegateUI myDelegateUI; //����ί�ж���

        public void UpdateUI(MSGMSG msg)
        {
            RichTextBox rtx = GetTextBoxByName(msg.SrcUsername);
            rtx.AppendText("�û� " + msg.SrcUsername + " ����˵��" + msg.Msg + "\r\n");
        }

        public void RecMsg(MSGMSG msg)
        {
            try
            {
                this.Invoke(myDelegateUI,msg); //richTextBox1.AppendText("TEST line \r");
                Application.DoEvents();

               
            
            }
            catch (Exception)
            {

                throw;
            }
        
           
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
            //ʹ���첽���̸߳���
            if (this.InvokeRequired)
            {
                //�½�һ���̣߳��߳����������ķ����ʽ����ķ����ʽ����ʹ���첽����ʽ����ί�У�ί���������޸Ŀؼ��ĸ���
                new Thread(() => this.Invoke(delega1)).Start();
            }
            else
            {
                delega1();
            }
        }

        void RecModNameRspAction(ModNameRsp modnamersp)
        {


            Action delega1 = () =>
            {
                if (modnamersp.Result)
                {

                    resetFriendsPanel();
                    MessageBox.Show(modnamersp.ErrorMsg);
                }
                else
                {
                    MessageBox.Show(modnamersp.ErrorMsg);
                }
            };

            //ʹ���첽���̸߳���
            if (this.InvokeRequired)
            {
                //�½�һ���̣߳��߳����������ķ����ʽ����ķ����ʽ����ʹ���첽����ʽ����ί�У�ί���������޸Ŀؼ��ĸ���
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
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { resetFriendsPanel(); }));
                return;
            }
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
            bool isFirst = true;

            u.FriendInfos.ForEach(f =>
            {
                if (f.Status != FriendStatus.NOREPLY)
                {
                    createFriend(f, isFirst, f.Status);
                    isFirst = false;
                }

            });
        }
        List<Panel> friends = new List<Panel>();

        void createFriend(FriendInfo friendInfo, bool isFirst, FriendStatus status = FriendStatus.ONLINE)
        {
            string name = friendInfo.FriendName;

            RichTextBox rtx = GetTextBoxByName(name);//�����ؼ�

            string nickname = friendInfo.FriendNickName;

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
                panel.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
                pictureBox.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
                label.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
            }
            else if (status == FriendStatus.OFFLINE)
            {
                pictureBox.Image = Properties.Resources.dog_opacity;
                panel.Click += openChat;
                pictureBox.Click += openChat;
                label.Click += openChat;
                panel.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
                pictureBox.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
                label.DoubleClick += (sender, e) => changeName(sender, e, name, nickname);
            }
            else if (status == FriendStatus.WAIT)
            {
                EventHandler waitClick = (object? sender, EventArgs e) =>
                {
                    var confirmResult = MessageBox.Show("�Ƿ�ͨ��" + name + "�ĺ�������",
                                   name + "���������Ϊ����",
                                   MessageBoxButtons.YesNoCancel);
                    if (confirmResult == DialogResult.Yes)
                    {
                        AddFriendReq req = new AddFriendReq();
                        req.Username = MyTools.getUserinfo().Username;
                        req.FriendName = name;
                        TcpClientManager.Instance.SendMesg(req, MsgType.MSG_TYPE_ADD_FRIEND_REQ);
                    }
                    else if (confirmResult == DialogResult.No)
                    {
                        RefuseReq req = new RefuseReq();
                        req.FriendName = name;
                        req.Username = MyTools.getUserinfo().Username;
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

            //ʹ���첽���̸߳���
            if (this.InvokeRequired)
            {
                //�½�һ���̣߳��߳����������ķ����ʽ����ķ����ʽ����ʹ���첽����ʽ����ί�У�ί���������޸Ŀؼ��ĸ���
                new Thread(() => this.Invoke(delega1)).Start();
            }
            else
            {
                delega1();
            }

            if (true)
            {
                openChat(panel, null);
            }
        }



        private void openChat(object? sender, EventArgs e)
        {
            var i1 = sender is Label;
            var i2 = sender is Panel;
            var i3 = sender is PictureBox;
            string controlName = "";
            if (i1)
            {
                controlName = ((Label)sender).Name;

            }
            if (i2)
            {
                controlName = ((Panel)sender).Name;
            }
            if (i3)
            {
                controlName = ((PictureBox)sender).Name;
            }
            if (controlName.Contains("_"))
            {

                string[] strArray = controlName.Split('_');
                string userName = strArray[0];
                CurrentFriendUser = userName;
                label2.Text = "���û� " + userName + " ������";
                RichTextBox r = GetTextBoxByName(userName);

                //panel1.Invoke(new EventHandler(delegate
                //{
                //    if (panel1.Controls.Count > 0)
                //    {
                //        panel1.Controls.RemoveAt(0);
                //    }
                //    panel1.Controls.Add(r);
                //}));

                if (panel1.Controls.Count > 0)
                {
                    panel1.Controls.RemoveAt(0);
                }
                panel1.Controls.Add(r);
            }

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

        private void changeName(object sender, EventArgs e, string name, string nickname)
        {
            ChangeNickName form = new ChangeNickName();
            form.old_name.Text = name.Trim();
            if (nickname == "" || nickname == null)
            {
                form.textBox1.Text = "��δ���ù���ע�������ã�";
            }
            else
            {
                form.textBox1.Text = nickname.Trim();
            }

            form.ShowDialog();
        }

        private void addFriendIcon_Click(object sender, EventArgs e)
        {
            addFriend();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            addFriend();

        }




        public void ShowRecvMsg(string str)
        {
            //richTextBox1.Invoke(new EventHandler(delegate
            //{
            //    richTextBox1.AppendText(str + "\r\n");
            //}));

        }



        private void button2_Click(object sender, EventArgs e)
        {
            //richTextBox1.Clear();
        }

        public Dictionary<string, RichTextBox> DicTextBox = new Dictionary<string, RichTextBox>();
        private RichTextBox GetTextBoxByName(string userName)
        {
            if (DicTextBox.Keys.Contains(userName))
            {
                return DicTextBox[userName];
            }
            else
            {
                RichTextBox rtx = new RichTextBox();
                rtx.Location = new Point(0, 0);
                rtx.Size = new Size(panel1.Width-1, 390);
                rtx.TabIndex = 0;
                rtx.Text = "";
                rtx.ReadOnly = true;
                DicTextBox.Add(userName, rtx);
                return rtx;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string replacedValue = textBox1.Text.Replace("\n\r", "").Replace("\n\t", "").Replace("\r\n", "");
                if (panel1.Controls.Count > 0)
                {
                    RichTextBox r = panel1.Controls[0] as RichTextBox;
                    if (replacedValue != string.Empty)
                    {
                        r.AppendText(replacedValue + "\r\n");
                        int start = r.Text.LastIndexOf(replacedValue);
                        r.Select(start, replacedValue.Length);
                        r.SelectionColor = Color.YellowGreen;
                        r.SelectionAlignment = HorizontalAlignment.Right;
                        r.Select(r.Text.Length, 0);
                        r.ScrollToCaret();
                        MSGMSG msg = new MSGMSG();
                        msg.Msg = replacedValue;
                        msg.SrcUsername = MyTools.getUserinfo().Username;
                        msg.DesUsername = CurrentFriendUser;

                        TcpClientManager.Instance.SendMesg(msg, MsgType.MSG_TYPE_MSG);
                        textBox1.Clear();
                    }
                }


            }
            catch (Exception)
            {
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//�жϻس���
            {
                button1_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//�жϻس���
            {
                button1_Click(sender, e);
            }
        }


















        // sizuo end
    }
}
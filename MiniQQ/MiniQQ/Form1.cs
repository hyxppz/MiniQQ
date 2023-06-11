using MiniQQLib;
using MiniQQServer;

namespace MiniQQ
{
    public partial class Form1 : Form
    {
        public void ShowLog(string log)
        {
            textBox1.Invoke(new EventHandler(delegate
            {
                textBox1.Text += log + "\r\n";
            }));
        }
        public Form1()
        {
            InitializeComponent();
            //test();
            try
            {

            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLog("��������...");
                TCPServerManager.Instance.OpenServer(19521);
                button1.Enabled = false;
                TCPServerManager.Instance.RecRegisterReqAction = UserRegister;
                TCPServerManager.Instance.RecAddFriendReqAction = AddFriend;
                TCPServerManager.Instance.RecLoginReqAction = Login;
                TCPServerManager.Instance.RecRefuseReqAction = Refuse;
                TCPServerManager.Instance.ExceptionMsgAction = ShowLog;
                TCPServerManager.Instance.RecMSGMSGAction = RecvMsg;
                TCPServerManager.Instance.RecModNameReqAction = ChangeName;

                ShowLog("���������ɹ�...");
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }

        }

        public void Refuse(RefuseReq req)
        {
            UserInfomations info = getAllUsersInfo();
            Userinfo? friend = info.MyUserInfos.Find((u) => u.Username == req.FriendName);
            Userinfo? user = info.MyUserInfos.Find((u) => u.Username == req.Username);
            if (user != null)
            {
                user.FriendInfos.Remove(user.FriendInfos.Find(f => f.FriendName == req.FriendName));
                RefreshFriendListRsp refreshFriendListRsp1 = new RefreshFriendListRsp();
                refreshFriendListRsp1.userinfo = user;
                TCPServerManager.Instance.SendObjectByUserName(user.Username, refreshFriendListRsp1, MsgType.MSG_TYPE_REFRESH_FRIEND);
            }
            if (friend != null)
            {
                friend.FriendInfos.Remove(friend.FriendInfos.Find(f => f.FriendName == req.Username));
                RefreshFriendListRsp refreshFriendListRsp1 = new RefreshFriendListRsp();
                refreshFriendListRsp1.userinfo = friend;
                TCPServerManager.Instance.SendObjectByUserName(friend.Username, refreshFriendListRsp1, MsgType.MSG_TYPE_REFRESH_FRIEND);
            }
            saveUsers(info);

        }

        //zxy
        public void Login(LoginReq loginReq, string ip)
        {
            string username = loginReq.Username;
            string password = loginReq.Password;
            Userinfo? userinfo = null;
            if (getAllUsers() != null)
            {
                userinfo = getAllUsers().Find((u) => u.Username == loginReq.Username && u.Password == loginReq.Password);
            }



            if (userinfo != null)
            {
                // ��ȡ������Ϣ����������һ����ȡ������Ϣ�ķ��� GetFriends
                //List<FriendInfo> friendInfos = GetFriends(username);

                // ���ͳɹ��ͺ�����Ϣ
                LoginRsp loginResponse = new LoginRsp();
                loginResponse.Result = true;
                loginResponse.User = userinfo;
                //loginResponse.FriendInfos = friendInfos;
                TCPServerManager.Instance.SendObjectByIP(ip, loginResponse, MsgType.MSG_TYPE_LOGIN_RSP);
                ShowLog($"�û� {username} ��¼�ɹ�...");
            }
            else
            {
                // ����ʧ��
                LoginRsp loginResponse = new LoginRsp();
                loginResponse.Result = false;
                loginResponse.ErrorMsg = "�û����벻��ȷ���û�������";
                TCPServerManager.Instance.SendObjectByIP(ip, loginResponse, MsgType.MSG_TYPE_LOGIN_RSP);
                ShowLog($"�û� {username} ��¼ʧ��...");
            }
        }

        public void AddFriend(AddFriendReq addFriendReq, string ip)
        {
            AddFriendRsp addFriendRsp = new AddFriendRsp();
            UserInfomations info = getAllUsersInfo();
            Userinfo? friend = info.MyUserInfos.Find((u) => u.Username == addFriendReq.FriendName);
            if (friend == null)
            {
                addFriendRsp.ErrorMsg = "���˺Ų�����";
                addFriendRsp.Result = false;
                TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                return;
            }

            Userinfo? user = info.MyUserInfos.Find((u) => u.Username == addFriendReq.Username);
            if (user == null)
            {
                return;

            }
            else
            {
                if (user.Username == addFriendReq.FriendName)
                {
                    addFriendRsp.ErrorMsg = "��������Լ�Ϊ����";
                    addFriendRsp.Result = false;
                    TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                    return;
                }
                FriendInfo? f0 = user.FriendInfos.Find(f => f.FriendName == addFriendReq.FriendName);
                if (f0 != null && f0.Status == FriendStatus.WAIT)
                {
                    // ��������б����޸�״̬
                    f0.Status = friend.Status;
                    FriendInfo? f1 = friend.FriendInfos.Find(f => f.FriendName == user.Username);
                    if (f1 != null)
                    {
                        f1.Status = user.Status;
                    }
                    saveUsers(info);
                    addFriendRsp.ErrorMsg = "��ӳɹ������Կ�ʼ�Ի�������";
                    addFriendRsp.Result = true;
                    addFriendRsp.userinfo = user;
                    // �Ƹ��Է�ˢ�º����б�
                    RefreshFriendListRsp refreshFriendListRsp1 = new RefreshFriendListRsp();
                    refreshFriendListRsp1.userinfo = friend;
                    TCPServerManager.Instance.SendObjectByUserName(friend.Username, refreshFriendListRsp1, MsgType.MSG_TYPE_REFRESH_FRIEND);
                    TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);

                    return;
                }

            }
            FriendInfo? m = user.FriendInfos.Find(f => f.FriendName == addFriendReq.FriendName);
            if (m != null)
            {
                if (m.Status == FriendStatus.NOREPLY)
                {
                    addFriendRsp.ErrorMsg = "���������ѷ��ͣ������ĵȴ�";
                    addFriendRsp.Result = false;

                }
                else
                {
                    addFriendRsp.ErrorMsg = "�Է��Ѿ������ĺ����ˣ���ȥ�����";
                    addFriendRsp.Result = false;
                }
                TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                return;
            }
            FriendInfo friendInfo = new FriendInfo();
            friendInfo.FriendName = friend.Username;
            friendInfo.Status = FriendStatus.NOREPLY;
            user.FriendInfos.Add(friendInfo);
            // ���Ѷ���ʾ�Ƿ�ͬ��ȴ�״̬
            FriendInfo friendInfo1 = new FriendInfo();
            friendInfo1.FriendName = user.Username;
            friendInfo1.Status = FriendStatus.WAIT;
            friend.FriendInfos.Add(friendInfo1);
            saveUsers(info);
            addFriendRsp.ErrorMsg = "��ӳɹ�,�ȴ��Է�Ӧ��";
            addFriendRsp.Result = true;
            addFriendRsp.userinfo = user;

            RefreshFriendListRsp refreshFriendListRsp = new RefreshFriendListRsp();
            refreshFriendListRsp.userinfo = friend;
            TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
            //�Ƹ��Է�ˢ�º����б�
            TCPServerManager.Instance.SendObjectByUserName(friend.Username, refreshFriendListRsp, MsgType.MSG_TYPE_REFRESH_FRIEND);
        }

        public void UserRegister(RegisterReq registerReq, string ip)
        {
            RegisterRsp registerRsp = new RegisterRsp();
            if (
                getUserByName(registerReq.Username) != null
                )
            {
                registerRsp.Result = false;
                registerRsp.ErrorMsg = "�û��Ѵ���";

            }
            else
            {
                saveUser(registerReq.Username, registerReq.Password);
                registerRsp.Username = registerReq.Username;
                registerRsp.Result = true;
                registerRsp.ErrorMsg = "ע��ɹ���";
            }



            TCPServerManager.Instance.SendObjectByIP(ip, registerRsp, MsgType.MSG_TYPE_REGISTER_RSP);
        }

        public Userinfo? getUserByName(string Username)
        {
            try
            {
                List<Userinfo> allUsers = getAllUsers();
                if (allUsers != null)
                {
                    return allUsers.Find((u) => u.Username == Username);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex) { }
            return null;

        }

        public UserInfomations saveUsers(UserInfomations u)
        {

            MyTools.Serialize2Fill("1.data", u);
            return u;
        }


        public Userinfo saveUser(string Username, string Password)
        {
            Userinfo userInfo = new Userinfo();
            List<Userinfo> allUsers = getAllUsers();
            if (allUsers == null)
            {
                allUsers = new List<Userinfo>();
            }
            userInfo.Username = Username;
            userInfo.Password = Password;
            userInfo.FriendInfos = new List<FriendInfo>();
            allUsers.Add(userInfo);
            UserInfomations u = new UserInfomations();
            u.MyUserInfos.AddRange(allUsers);
            MyTools.Serialize2Fill("1.data", u);
            return userInfo;
        }

        public void ChangeName(ModNameReq modnamereq, string ip)
        {
            ModNameRsp modNameRsp = new ModNameRsp();
            UserInfomations info = getAllUsersInfo();
            Userinfo user = info.MyUserInfos.Find((u) => u.Username == modnamereq.Username);
            if (user != null)
            {
                FriendInfo change_friend = user.FriendInfos.Find((u) => u.FriendName == modnamereq.FriendName);
                if (change_friend != null)
                {
                    if (change_friend.Status == FriendStatus.WAIT || change_friend.Status == FriendStatus.NOREPLY)
                    {
                        modNameRsp.Result = false;
                        modNameRsp.ErrorMsg = "�޸�ʧ�ܣ�����δ��ӣ�";
                        saveUsers(info);
                        TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);
                    }
                    else
                    {
                        change_friend.FriendNickName = modnamereq.FriendNickName;
                        saveUsers(info);

                        modNameRsp.Result = true;
                        modNameRsp.ErrorMsg = "�޸��ǳƳɹ�";
                        modNameRsp.FriendName = modnamereq.FriendName;
                        modNameRsp.Username = modnamereq.Username;
                        modNameRsp.FriendNickName = modnamereq.FriendNickName;

                        RefreshFriendListRsp refreshFriendListRsp1 = new RefreshFriendListRsp();
                        refreshFriendListRsp1.userinfo = user;
                        TCPServerManager.Instance.SendObjectByUserName(user.Username, refreshFriendListRsp1, MsgType.MSG_TYPE_REFRESH_FRIEND);
                        TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);

                    }

                }
                else
                {
                    modNameRsp.Result = false;
                    modNameRsp.ErrorMsg = "�޸�ʧ�ܣ�";
                    saveUsers(info);
                    TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);
                }

            }
            else
            {
                modNameRsp.Result = false;
                modNameRsp.ErrorMsg = "�޸�ʧ�ܣ�";
                saveUsers(info);
                TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);

            }
        }

        public UserInfomations getAllUsersInfo()
        {
            try
            {
                UserInfomations info = (UserInfomations)MyTools.DeserializeFromFile("1.data");
                return info;
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        public List<Userinfo> getAllUsers()
        {
            UserInfomations info = getAllUsersInfo();
            if (info == null)
            {
                return null;

            }
            else
            {
                return info.MyUserInfos;
            }

        }

        public void RecvMsg(MSGMSG msg)
        {
            ShowLog("�û� " + msg.SrcUsername + "���û� " + msg.DesUsername + "���� " + msg.Msg);
            TCPServerManager.Instance.SendObjectByUserName(msg.DesUsername, msg, MsgType.MSG_TYPE_MSG);
        }


        public void test()
        {
            UserInfomations u = new UserInfomations();
            Userinfo u1 = new Userinfo();
            u1.Username = "username";
            u1.Password = "password";

            FriendInfo f1 = new FriendInfo();
            f1.FriendName = "1";
            f1.FriendNickName = "2";

            FriendInfo f2 = new FriendInfo();
            f2.FriendName = "3";
            f2.FriendNickName = "4";

            u1.FriendInfos.Add(f1);
            u1.FriendInfos.Add(f2);

            u.MyUserInfos.Add(u1);



            Userinfo u2 = new Userinfo();
            u2.Username = "username";
            u2.Password = "password";

            FriendInfo f3 = new FriendInfo();
            f3.FriendName = "11";
            f3.FriendNickName = "21";

            FriendInfo f4 = new FriendInfo();
            f4.FriendName = "31";
            f4.FriendNickName = "41";

            u2.FriendInfos.Add(f3);
            u2.FriendInfos.Add(f4);

            u.MyUserInfos.Add(u2);


            MyTools.Serialize2Fill("1.data", u);


            UserInfomations newU = (UserInfomations)MyTools.DeserializeFromFile("1.data");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)           //��ֹ�ֶ����ʱ��Ϊ0������textBox1.Text.Length - 1ʱ����
            {
                textBox1.SelectionStart = textBox1.Text.Length - 1;
                textBox1.ScrollToCaret();//���ؼ����ݹ�������ǰ�������λ��
            }
        }
    }
}
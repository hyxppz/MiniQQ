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
                ShowLog("启动服务...");
                TCPServerManager.Instance.OpenServer(19521);
                button1.Enabled = false;
                TCPServerManager.Instance.RecRegisterReqAction = UserRegister;
                TCPServerManager.Instance.RecAddFriendReqAction = AddFriend;
                TCPServerManager.Instance.RecLoginReqAction = Login;
                TCPServerManager.Instance.RecRefuseReqAction = Refuse;
                TCPServerManager.Instance.ExceptionMsgAction = ShowLog;
                TCPServerManager.Instance.RecMSGMSGAction = RecvMsg;
                TCPServerManager.Instance.RecModNameReqAction = ChangeName;

                ShowLog("服务启动成功...");
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



            Userinfo? userinfo = getAllUsers().Find((u) => u.Username == loginReq.Username && u.Password == loginReq.Password);


            if (userinfo != null)
            {
                // 获取好友信息。假设你有一个获取好友信息的方法 GetFriends
                //List<FriendInfo> friendInfos = GetFriends(username);

                // 发送成功和好友信息
                LoginRsp loginResponse = new LoginRsp();
                loginResponse.Result = true;
                loginResponse.User = userinfo;
                //loginResponse.FriendInfos = friendInfos;
                TCPServerManager.Instance.SendObjectByIP(ip, loginResponse, MsgType.MSG_TYPE_LOGIN_RSP);
            }
            else
            {
                // 发送失败
                LoginRsp loginResponse = new LoginRsp();
                loginResponse.Result = false;
                loginResponse.ErrorMsg = "用户密码不正确";
                TCPServerManager.Instance.SendObjectByIP(ip, loginResponse, MsgType.MSG_TYPE_LOGIN_RSP);
            }
        }

        public void AddFriend(AddFriendReq addFriendReq, string ip)
        {
            AddFriendRsp addFriendRsp = new AddFriendRsp();
            UserInfomations info = getAllUsersInfo();
            Userinfo? friend = info.MyUserInfos.Find((u) => u.Username == addFriendReq.FriendName);
            if (friend == null)
            {
                addFriendRsp.ErrorMsg = "该账号不存在";
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
                    addFriendRsp.ErrorMsg = "不能添加自己为好友";
                    addFriendRsp.Result = false;
                    TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                    return;
                }
                FriendInfo? f0 = user.FriendInfos.Find(f => f.FriendName == addFriendReq.FriendName);
                if (f0 != null && f0.Status == FriendStatus.WAIT)
                {
                    // 互相好友列表中修改状态
                    f0.Status = friend.Status;
                    FriendInfo? f1 = friend.FriendInfos.Find(f => f.FriendName == user.Username);
                    if (f1 != null)
                    {
                        f1.Status = user.Status;
                    }
                    saveUsers(info);
                    addFriendRsp.ErrorMsg = "添加成功，可以开始对话聊天了";
                    addFriendRsp.Result = true;
                    addFriendRsp.userinfo = user;
                    // 推给对方刷新好友列表
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
                    addFriendRsp.ErrorMsg = "好友请求已发送，请耐心等待";
                    addFriendRsp.Result = false;

                }
                else
                {
                    addFriendRsp.ErrorMsg = "对方已经是您的好友了，快去聊天吧";
                    addFriendRsp.Result = false;
                }
                TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                return;
            }
            FriendInfo friendInfo = new FriendInfo();
            friendInfo.FriendName = friend.Username;
            friendInfo.Status = FriendStatus.NOREPLY;
            user.FriendInfos.Add(friendInfo);
            // 朋友端显示是否同意等待状态
            FriendInfo friendInfo1 = new FriendInfo();
            friendInfo1.FriendName = user.Username;
            friendInfo1.Status = FriendStatus.WAIT;
            friend.FriendInfos.Add(friendInfo1);
            saveUsers(info);
            addFriendRsp.ErrorMsg = "添加成功,等待对方应答";
            addFriendRsp.Result = true;
            addFriendRsp.userinfo = user;

            RefreshFriendListRsp refreshFriendListRsp = new RefreshFriendListRsp();
            refreshFriendListRsp.userinfo = friend;
            TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
            //推给对方刷新好友列表
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
                registerRsp.ErrorMsg = "用户已存在";

            }
            else
            {
                saveUser(registerReq.Username, registerReq.Password);
                registerRsp.Username = registerReq.Username;
                registerRsp.Result = true;
                registerRsp.ErrorMsg = "注册成功！";
            }



            TCPServerManager.Instance.SendObjectByIP(ip, registerRsp, MsgType.MSG_TYPE_REGISTER_RSP);
        }

        public Userinfo? getUserByName(string Username)
        {
            List<Userinfo> allUsers = getAllUsers();
            return allUsers.Find((u) => u.Username == Username);
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
                        modNameRsp.ErrorMsg = "修改失败，好友未添加！";
                        saveUsers(info);
                        TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);
                    }
                    else
                    {
                        change_friend.FriendNickName = modnamereq.FriendNickName;
                        saveUsers(info);

                        modNameRsp.Result = true;
                        modNameRsp.ErrorMsg = "修改昵称成功";
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
                    modNameRsp.ErrorMsg = "修改失败！";
                    saveUsers(info);
                    TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);
                }

            }
            else
            {
                modNameRsp.Result = false;
                modNameRsp.ErrorMsg = "修改失败！";
                saveUsers(info);
                TCPServerManager.Instance.SendObjectByIP(ip, modNameRsp, MsgType.MSG_TYPE_MOD_NAME_RSP);

            }
        }

        public UserInfomations getAllUsersInfo()
        {
            UserInfomations info = (UserInfomations)MyTools.DeserializeFromFile("1.data");
            return info;
        }

        public List<Userinfo> getAllUsers()
        {
            UserInfomations info = getAllUsersInfo();
            return info.MyUserInfos;
        }

        public void  RecvMsg(MSGMSG msg)
        {
            ShowLog("用户 "+msg.SrcUsername +"向用户 "+msg.DesUsername + "发送 " + msg.Msg);
            TCPServerManager.Instance.SendObjectByUserName(msg.DesUsername,msg,MsgType.MSG_TYPE_MSG);
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


    }
}
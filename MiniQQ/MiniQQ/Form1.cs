using MiniQQLib;
using MiniQQServer;

namespace MiniQQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TCPServerManager.Instance.OpenServer(19521);
            button1.Enabled = false;
            TCPServerManager.Instance.RecRegisterReqAction = UserRegister;

            TCPServerManager.Instance.RecAddFriendReqAction = AddFriend;
        }


        public void AddFriend(AddFriendReq addFriendReq, string ip)
        {
            AddFriendRsp addFriendRsp = new AddFriendRsp();
            Userinfo? friend = getUserByName(addFriendReq.FriendName);
            if (friend==null)
            {
                addFriendRsp.ErrorMsg = "该账号不存在";
                addFriendRsp.Result = false;
                TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                return;
            }
            UserInfomations info = getAllUsersInfo();
            Userinfo? user = info.MyUserInfos.Find((u) => u.Username == addFriendReq.Username);
            if (user==null)
            {
                return;

            }
            FriendInfo? m = user.FriendInfos.Find(f => f.FriendName == addFriendReq.FriendName);
            if (m!=null)
            {
                if (m.Status==FriendStatus.NOREPLY)
                {
                    addFriendRsp.ErrorMsg = "好友请求已发送，请耐心等待";
                    addFriendRsp.Result = false;
                    
                }
                else
                {
                    addFriendRsp.ErrorMsg = "好友已存在";
                    addFriendRsp.Result = false;
                }
                TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
                return;
            }
            FriendInfo friendInfo = new FriendInfo();
            friendInfo.FriendName = friend.Username;
            friendInfo.Status = FriendStatus.NOREPLY;
            user.FriendInfos.Add(friendInfo);
            saveUsers(info);
            addFriendRsp.ErrorMsg = "添加成功,等待对方应答";
            addFriendRsp.Result = true;
            addFriendRsp.userinfo= user;
            TCPServerManager.Instance.SendObjectByIP(ip, addFriendRsp, MsgType.MSG_TYPE_ADD_FRIEND_RSP);
        }

        public void UserRegister(RegisterReq registerReq,string ip)
        {
            RegisterRsp registerRsp = new RegisterRsp();
            if (
                getUserByName(registerReq.Username)!=null
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
            return allUsers.Find((u)=>u.Username==Username);
        }

        public UserInfomations saveUsers(UserInfomations u)
        {
         
            MyTools.Serialize2Fill("1.data", u);
            return u;
        }


        public Userinfo saveUser(string Username,string Password)
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

        public void test()
        {
            UserInfomations u = new UserInfomations();
            Userinfo u1 =new Userinfo();
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
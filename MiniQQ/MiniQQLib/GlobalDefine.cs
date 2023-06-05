using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQQLib
{

    public class GlobalDefine
    {
        const int MSG_TYPE_REGISTER_REQ = 0;//注册请求
        const int MSG_TYPE_REGISTER_RSP = 1;//注册应答
        const int MSG_TYPE_LOGIN_REQ = 2;//登录请求
        const int MSG_TYPE_LOGIN_RSP = 3;//登录应答
        const int MSG_TYPE_ADD_FRIEND_REQ = 4;//添加好友请求
        const int MSG_TYPE_ADD_FRIEND_RSP = 5;//添加好友应答
        const int MSG_TYPE_MOD_NAME_REQ = 6;//修改昵称请求
        const int MSG_TYPE_MOD_NAME_RSP = 7;//修改昵称应答
        const int MSG_TYPE_MSG= 8;//聊天消息
    }

    public class LoginReq
    {
        public string Username;
        public string Password;
    }

    public class LoginRsq
    {
        public bool Result { get; set; }
    }

    public class MSGMSG
    {
    }

}

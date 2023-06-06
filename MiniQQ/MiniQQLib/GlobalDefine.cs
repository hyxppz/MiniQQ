using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQQLib
{
    public enum MsgType
    {
        MSG_TYPE_REGISTER_REQ =0,//注册请求
        MSG_TYPE_REGISTER_RSP,//注册应答
        MSG_TYPE_LOGIN_REQ,//登录请求
        MSG_TYPE_LOGIN_RSP,//登录应答
        MSG_TYPE_ADD_FRIEND_REQ,//添加好友请求
        MSG_TYPE_ADD_FRIEND_RSP,//添加好友应答
        MSG_TYPE_MOD_NAME_REQ,//修改昵称请求
        MSG_TYPE_MOD_NAME_RSP,//修改昵称应答
        MSG_TYPE_MSG,//聊天消息
        MSG_TYPE_QUERY_REQ,//信息查询请求
        MSG_TYPE_QUERY_RSP,//信息查询返回
    }


    [Serializable]
    public class RegisterReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    [Serializable]
    public class RegisterRsp
    {
        public string Username { get; set; }
        public bool Result { get; set; }

    }
    [Serializable]
    public class LoginReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    [Serializable]
    public class LoginRsp
    {
        public string Username { get; set; }
        public bool Result { get; set; }
        
    }
    [Serializable]
    public class AddFriendReq
    {
        public string Username { get; set; }
        public string FriendName { get; set; }
    }
    [Serializable]
    public class AddFriendRsp
    {
        public string Username { get; set; }
        public string FriendName { get; set; }
        public bool Result { get; set; }

    }
    [Serializable]
    public class ModNameReq
    {
        public string Username { get; set; }
        public string FriendName { get; set; }
        public string FriendNickName { get; set; }
    }
    [Serializable]
    public class ModNameRsp
    {
        public string Username { get; set; }
        public string FriendName { get; set; }
        public string FriendNickName { get; set; }
        public bool Result { get; set; }
    }
    [Serializable]
    public class MSGMSG
    {
        public string SrcUsername { get; set; }
        public string DesUsername { get; set; }
        public string Msg { get; set; }
    }
    [Serializable]
    public class QueryReq
    {
        public string Username { get; set; }
    }
    [Serializable]
    public class FriendInfo
    {
        public string FriendName { get; set; }
        public string FriendNickName { get; set; }
    }
    [Serializable]
    public class QueryRsp
    {
        public QueryRsp() 
        {
            FriendInfos = new List<FriendInfo>();
        }
        public string Username { get; set; }
        public bool Result { get; set; }

        public List<FriendInfo> FriendInfos { get; set; }
    }



    [Serializable]
    public class Userinfo
    {
        public Userinfo()
        {
            FriendInfos = new List<FriendInfo>();
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<FriendInfo> FriendInfos { get; set; }
    }

    [Serializable]
    public class UserInfomations
    {
        public UserInfomations()
        {
            MyUserInfos = new List<Userinfo>();
        }
        public List<Userinfo> MyUserInfos { get; set; }
    }



}

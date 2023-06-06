using System.Net;
using System.Net.Sockets;
using System.Text;

using System.Threading.Tasks;
using MiniQQLib;
using System.Collections;

namespace MiniQQClient
{
    internal class TcpClientManager
    {
        private static TcpClientManager instance;

        private TcpClientManager() { }

        public static TcpClientManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TcpClientManager();
                }
                return instance;
            }
        }


        private IPAddress ipAddress = null;
        private TcpClient _client = null;
        private NetworkStream _stream = null;
        byte[] sendBuf = new byte[1024 * 1024 * 2];

        enum ConnectionStatus
        {
            init,
            connected,
            disconnect
        }
        private ConnectionStatus _connectionState = ConnectionStatus.init;


        public Action<string> ExceptionMsgAction { get; set; }

        public Action<MiniQQLib.RegisterRsp> RecRegisterRspAction { get; set; }
        public Action<MiniQQLib.LoginRsp> RecLoginRspAction { get; set; }
        public Action<MiniQQLib.AddFriendRsp> RecAddFriendRspAction { get; set; }
        public Action<MiniQQLib.ModNameRsp> RecModNameRspAction { get; set; }
        public Action<MiniQQLib.MSGMSG> RecMSGMSGAction { get; set; }
        public Action<MiniQQLib.QueryRsp> RecQueryRspAction { get; set; }


        public void  Init(string ip)
        {
            ipAddress = IPAddress.Parse(ip);
            _connectionState = ConnectionStatus.init;

        }

        public bool StartConnect()
        {
            Task.Run(ConnectThread);
            Thread th_ReceiveData = new Thread(ReceiveMsg);
            th_ReceiveData.IsBackground = true;
            th_ReceiveData.Start();
            return true;
        }

        /// <summary>
        /// 发送
        /// </summary>
        public void SendMsg(string msg)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                _stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                _connectionState = ConnectionStatus.disconnect;
                if (ExceptionMsgAction!=null)
                {
                    ExceptionMsgAction.Invoke(ex.ToString());
                }
                
            }

        }

        public bool SendMesg(object o, MsgType msgType)
        {
            string msgContent = MyTools.Serialize<object>(o);
            byte[] b1 = MyTools.intToBytes(msgContent.Length); 
            byte[] b2 = MyTools.intToBytes((int)msgType);
            byte[] b3 = Encoding.UTF8.GetBytes(msgContent);
            Buffer.BlockCopy(b1, 0, sendBuf, 0, 4);
            Buffer.BlockCopy(b2, 0, sendBuf, 4, 4);
            Buffer.BlockCopy(b3, 0, sendBuf, 8, msgContent.Length);
            _stream.Write(sendBuf, 0, 8+ msgContent.Length);
            return true;
        }

        public void ConnectThread()
        {
            while (true)
            {
                if (_connectionState == ConnectionStatus.init || _connectionState == ConnectionStatus.disconnect)
                {
                    try
                    {
                        if (ExceptionMsgAction != null)
                        {
                            ExceptionMsgAction.Invoke("重连中");
                        }
                      
                        _client = new TcpClient();
                        _client.Connect(ipAddress, 19521);
                    }
                    catch (Exception ex)
                    {
                        _connectionState = ConnectionStatus.disconnect;
                        if (ExceptionMsgAction != null)
                        {
                            ExceptionMsgAction.Invoke("发生错误，断开连接");
                        }
                       
                        Thread.Sleep(3000);
                        continue;
                    }
                    _connectionState = ConnectionStatus.connected;
                    _stream = _client.GetStream();


                    if (ExceptionMsgAction != null)
                    {
                        ExceptionMsgAction.Invoke("服务器已连接");
                    }
                    
                }

                Thread.Sleep(1000);
            }

        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveMsg()
        {
            while (true)
            {
                try
                {

                    if (_connectionState == ConnectionStatus.connected)
                    {
                        byte[] data = new byte[1024*1024*2];
                        int r1 = _stream.Read(data, 0, 4);
                        int msgTotalLength = MyTools.bytesToInt(data);
                        int r2 = _stream.Read(data, 4, 4);
                        int msgType = MyTools.bytesToInt(data,4);
                        MsgType t = (MsgType)msgType;
                        int r3 = _stream.Read(data, 8, msgTotalLength);
                        //if(bytesRead == 0)
                        //{
                        //    _connectionState = ConnectionStatus.disconnect;
                        //    ExceptionMsgAction.Invoke("发生错误，断开连接");
                        //}
                        string message = Encoding.UTF8.GetString(data, 8, msgTotalLength);
                        if (message != string.Empty)
                        {
                            switch (t)
                            {
                                case MsgType.MSG_TYPE_REGISTER_RSP:
                                    RegisterRsp o = new RegisterRsp();
                                    o = MyTools.Desrialize<RegisterRsp>(o, message);
                                    RecRegisterRspAction.Invoke(o);
                                    break;
                                case MsgType.MSG_TYPE_LOGIN_RSP:
                                    LoginRsp o1 = new LoginRsp();
                                    o1 = MyTools.Desrialize<LoginRsp>(o1, message);
                                    RecLoginRspAction.Invoke(o1);
                                    break;
                                case MsgType.MSG_TYPE_ADD_FRIEND_RSP:
                                    AddFriendRsp o2 = new AddFriendRsp();
                                    o2 = MyTools.Desrialize<AddFriendRsp>(o2, message);
                                    RecAddFriendRspAction.Invoke(o2);
                                    break;
                                case MsgType.MSG_TYPE_MOD_NAME_RSP:
                                    ModNameRsp o3 = new ModNameRsp();
                                    o3 = MyTools.Desrialize<ModNameRsp>(o3, message);
                                    RecModNameRspAction.Invoke(o3);
                                    break;
                                case MsgType.MSG_TYPE_MSG:
                                    MSGMSG o4 = new MSGMSG();
                                    o4 = MyTools.Desrialize<MSGMSG>(o4, message);
                                    RecMSGMSGAction.Invoke(o4);
                                    break;
                                case MsgType.MSG_TYPE_QUERY_RSP:
                                    QueryRsp o5 = new QueryRsp();
                                    o5 = MyTools.Desrialize<QueryRsp>(o5, message);
                                    RecQueryRspAction.Invoke(o5);
                                    break;
                            }

                          
                        }
                    }

                    Thread.Sleep(100);

                }
                catch (Exception ex)
                {
                    _connectionState = ConnectionStatus.disconnect;
                    if (ExceptionMsgAction != null)
                    {
                        ExceptionMsgAction.Invoke("发生错误，断开连接");
                    }
                  
                    Thread.Sleep(3000);
                }
            }

        }
    }
}

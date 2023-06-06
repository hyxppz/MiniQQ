using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MiniQQLib;


namespace MiniQQServer
{
    internal class TCPServerManager
    {

        private static TCPServerManager instance;

        private TCPServerManager() { }

        public static TCPServerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TCPServerManager();
                }
                return instance;
            }
        }

        private Socket ServerSocket = null;//服务端  
        public Dictionary<string, MySession> dic_ClientSocket = new Dictionary<string, MySession>();//tcp客户端字典
        private Dictionary<string, Thread> dic_ClientThread = new Dictionary<string, Thread>();//线程字典,每新增一个连接就添加一条线程
        private Dictionary<string, string> dic_UserIP = new Dictionary<string, string>();//username-IP 对应字典,每新增一个连接就添加一条线程
        private bool Flag_Listen = true;//监听客户端连接的标志
        byte[] sendBuf = new byte[1024 * 1024 * 2];
        public Action<string> ExceptionMsgAction { get; set; }

        public Action<int, string> UpdateClient { get; set; }

        public Action<MiniQQLib.RegisterReq> RecRegisterReqAction { get; set; }
        public Action<MiniQQLib.LoginReq> RecLoginReqAction { get; set; }
        public Action<MiniQQLib.AddFriendReq> RecAddFriendReqAction { get; set; }
        public Action<MiniQQLib.ModNameReq> RecModNameReqAction { get; set; }
        public Action<MiniQQLib.MSGMSG> RecMSGMSGAction { get; set; }
        public Action<MiniQQLib.QueryReq> RecQueryReqAction { get; set; }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="port">端口号</param>
        public bool OpenServer(int port)
        {
            try
            {
                Flag_Listen = true;
                // 创建负责监听的套接字，注意其中的参数；
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 创建包含ip和端口号的网络节点对象；
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                try
                {
                    // 将负责监听的套接字绑定到唯一的ip和端口上；
                    ServerSocket.Bind(endPoint);
                }
                catch
                {
                    return false;
                }
                // 设置监听队列的长度；
                ServerSocket.Listen(100);
                // 创建负责监听的线程；
                Thread Thread_ServerListen = new Thread(ListenConnecting);
                Thread_ServerListen.IsBackground = true;
                Thread_ServerListen.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseServer()
        {
            lock (dic_ClientSocket)
            {
                foreach (var item in dic_ClientSocket)
                {
                    item.Value.Close();//关闭每一个连接
                }
                dic_ClientSocket.Clear();//清除字典
            }
            lock (dic_ClientThread)
            {
                foreach (var item in dic_ClientThread)
                {
                    item.Value.Abort();//停止线程
                }
                dic_ClientThread.Clear();
            }
            Flag_Listen = false;
            //ServerSocket.Shutdown(SocketShutdown.Both);//服务端不能主动关闭连接,需要把监听到的连接逐个关闭
            if (ServerSocket != null)
                ServerSocket.Close();

        }
        /// <summary>
        /// 监听客户端请求的方法；
        /// </summary>
        private void ListenConnecting()
        {
            while (Flag_Listen)  // 持续不断的监听客户端的连接请求；
            {
                try
                {
                    Socket sokConnection = ServerSocket.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                    // 将与客户端连接的 套接字 对象添加到集合中；
                    string str_EndPoint = sokConnection.RemoteEndPoint.ToString();
                    MySession myTcpClient = new MySession() { TcpSocket = sokConnection };
                    if(ExceptionMsgAction !=null)
                    {
                        ExceptionMsgAction.Invoke(str_EndPoint + " 已连接");
                    }
                  
                    //创建线程接收数据
                    Thread th_ReceiveData = new Thread(ReceiveData);
                    th_ReceiveData.IsBackground = true;
                    th_ReceiveData.Start(myTcpClient);
                    //把线程及客户连接加入字典
                    dic_ClientThread.Add(str_EndPoint, th_ReceiveData);
                    dic_ClientSocket.Add(str_EndPoint, myTcpClient);
                    if (UpdateClient!=null)
                    {
                        UpdateClient.Invoke(0, str_EndPoint);
                    }
                    
                }
                catch
                {

                }
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sokConnectionparn"></param>
        private void ReceiveData(object sokConnectionparn)
        {
            MySession tcpClient = sokConnectionparn as MySession;
            Socket socketClient = tcpClient.TcpSocket;
            bool Flag_Receive = true;
            string ipAddr = socketClient.RemoteEndPoint.ToString();
            byte[] sendBuf = new byte[10240];
            byte[] b1 = new byte[4];
            byte[] b2 = new byte[4];

            while (Flag_Receive)
            {
                try
                {
                    // 定义一个2M的缓存区；
                    byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                    // 将接受到的数据存入到输入  arrMsgRec中；
                    int length = -1;
                    try
                    {
                        length = socketClient.Receive(b1, 4,SocketFlags.None); // 接收数据，并返回数据的长度；
                        int msgTotalLength = MyTools.bytesToInt(b1);
                        length = socketClient.Receive(b2, 4, SocketFlags.None); // 接收数据，并返回数据的长度；
                        int msgType = MyTools.bytesToInt(b2);
                        MsgType t = (MsgType)msgType;
                        length = socketClient.Receive(arrMsgRec, msgTotalLength, SocketFlags.None); // 接收数据，并返回数据的长度；
                        //判断是否为空
                        string rectstr = System.Text.Encoding.UTF8.GetString(arrMsgRec,0,length);
                        if (rectstr != string.Empty)
                        {
                            switch (t) 
                            {
                                case MsgType.MSG_TYPE_REGISTER_REQ:
                                    RegisterReq o = new RegisterReq();
                                    o = MyTools.Desrialize<RegisterReq>(o, rectstr);
                                    RecRegisterReqAction.Invoke(o);
                                    break;
                                case MsgType.MSG_TYPE_LOGIN_REQ:
                                    LoginReq o1 = new LoginReq();
                                    o1 = MyTools.Desrialize<LoginReq>(o1, rectstr);
                                    RecLoginReqAction.Invoke(o1);
                                    //TODO                                
                                    dic_UserIP[o1.Username] = ipAddr;
                                    break;
                                case MsgType.MSG_TYPE_ADD_FRIEND_REQ:
                                    AddFriendReq o2 = new AddFriendReq();
                                    o2 = MyTools.Desrialize<AddFriendReq>(o2, rectstr);
                                    RecAddFriendReqAction.Invoke(o2);
                                    break;
                                case MsgType.MSG_TYPE_MOD_NAME_REQ:
                                    ModNameReq o3 = new ModNameReq();
                                    o3 = MyTools.Desrialize<ModNameReq>(o3, rectstr);
                                    RecModNameReqAction.Invoke(o3);
                                    break;
                                case MsgType.MSG_TYPE_MSG:
                                    MSGMSG o4 = new MSGMSG();
                                    o4 = MyTools.Desrialize<MSGMSG>(o4, rectstr);
                                    RecMSGMSGAction.Invoke(o4);
                                    break;
                                case MsgType.MSG_TYPE_QUERY_REQ:
                                    QueryReq o5 = new QueryReq();
                                    o5 = MyTools.Desrialize<QueryReq>(o5, rectstr);
                                    RecQueryReqAction.Invoke(o5);
                                    break;
                            }



                           
                        }


                    }
                    catch
                    {

                        Flag_Receive = false;
                        // 从通信线程集合中删除被中断连接的通信线程对象；
                        string keystr = socketClient.RemoteEndPoint.ToString();
                        dic_ClientSocket.Remove(keystr);//删除客户端字典中该socket
                        dic_ClientThread[keystr].Interrupt();//关闭线程
                        dic_ClientThread.Remove(keystr);//删除字典中该线程
                        if(UpdateClient!=null)
                        {
                            UpdateClient.Invoke(1, keystr);
                        }
                        
                        tcpClient = null;
                        socketClient = null;
                        if (ExceptionMsgAction != null)
                        {
                            ExceptionMsgAction.Invoke(keystr + " 断开连接");
                        }
                            
                        break;
                    }
                    byte[] buf = new byte[length];
                    Array.Copy(arrMsgRec, buf, length);
                    lock (tcpClient.m_Buffer)
                    {
                        tcpClient.AddQueue(buf);
                    }
                }
                catch
                {

                }
                Thread.Sleep(100);
            }
        }
        /// <summary>
        /// 发送数据给指定的客户端
        /// </summary>
        /// <param name="_endPoint">客户端套接字</param>
        /// <param name="_buf">发送的数组</param>
        /// <returns></returns>
        //public bool SendData(string _endPoint, byte[] _buf)
        //{
        //    MySession myT = new MySession();
        //    if (dic_ClientSocket.TryGetValue(_endPoint, out myT))
        //    {
        //        myT.Send(_buf);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool SendData(string _endPoint, byte[] _buf, int length)
        //{
        //    MySession myT = new MySession();
        //    if (dic_ClientSocket.TryGetValue(_endPoint, out myT))
        //    {
        //        myT.Send(_buf, length);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool SendObjectByUserName(string userName,object o, MsgType msgType)
        {
            if (dic_UserIP.ContainsKey(userName))
            {
                string ip = dic_UserIP[userName];
                SendObjectByIP(ip, o, msgType);
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool SendObjectByIP(string Ip,object o, MsgType msgType)
        {
            MySession myT = new MySession();
            if (dic_ClientSocket.TryGetValue(Ip, out myT))
            {
                string msgContent = MyTools.Serialize<object>(o);
                byte[] b1 = MyTools.intToBytes(msgContent.Length);
                byte[] b2 = MyTools.intToBytes((int)msgType);
                byte[] b3 = Encoding.UTF8.GetBytes(msgContent);
                Buffer.BlockCopy(b1, 0, sendBuf, 0, 4);
                Buffer.BlockCopy(b2, 0, sendBuf, 4, 4);
                Buffer.BlockCopy(b3, 0, sendBuf, 8, msgContent.Length);
                myT.Send(sendBuf, 8 + msgContent.Length);
                return true;
            }
            else
            {
                return false;
            }
            
            return true;
        }
    }

    /// <summary>
    /// 会话端
    /// </summary>
    public class MySession
    {
        public Socket TcpSocket;//socket对象
        public List<byte> m_Buffer = new List<byte>();//数据缓存区

        public MySession()
        {

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buf"></param>
        public void Send(byte[] buf)
        {
            if (buf != null)
            {
                TcpSocket.Send(buf);
            }
        }
        public void Send(byte[] buf, int length)
        {
            if (buf != null)
            {
                TcpSocket.Send(buf, length, SocketFlags.None); ;
            }
        }
        /// <summary>
        /// 获取连接的ip
        /// </summary>
        /// <returns></returns>
        public string GetIp()
        {
            IPEndPoint clientipe = (IPEndPoint)TcpSocket.RemoteEndPoint;
            string _ip = clientipe.Address.ToString();
            return _ip;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            TcpSocket.Shutdown(SocketShutdown.Both);
        }
        /// <summary>
        /// 提取正确数据包
        /// </summary>
        public byte[] GetBuffer(int startIndex, int size)
        {
            byte[] buf = new byte[size];
            m_Buffer.CopyTo(startIndex, buf, 0, size);
            m_Buffer.RemoveRange(0, startIndex + size);
            return buf;
        }

        /// <summary>
        /// 添加队列数据
        /// </summary>
        /// <param name="buffer"></param>
        public void AddQueue(byte[] buffer)
        {
            m_Buffer.AddRange(buffer);
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearQueue()
        {
            m_Buffer.Clear();
        }
    }
}

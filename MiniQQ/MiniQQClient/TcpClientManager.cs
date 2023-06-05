using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MiniQQLib;
using System.Collections;


namespace MiniQQClient
{
    internal class TcpClientManager
    {
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


        public Action<string> ReciviMsgAction { get; set; }
        public Action<string> ExceptionMsgAction { get; set; }

        
        public TcpClientManager(string ip)
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
                ExceptionMsgAction.Invoke(ex.ToString());
            }

        }

        public bool SendMesg(object o,int msgType)
        {
            string msgContent = MyTools.Serialize<object>(o);
            byte[] b1 = MyTools.intToBytes(msgContent.Length); 
            byte[] b2 = MyTools.intToBytes(msgType);
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
                        ExceptionMsgAction.Invoke("重连中");
                        _client = new TcpClient();
                        _client.Connect(ipAddress, 19521);
                    }
                    catch (Exception ex)
                    {
                        _connectionState = ConnectionStatus.disconnect;
                        ExceptionMsgAction.Invoke("发生错误，断开连接");
                        Thread.Sleep(3000);
                        continue;
                    }
                    _connectionState = ConnectionStatus.connected;
                    _stream = _client.GetStream();



                    ExceptionMsgAction.Invoke("服务器已连接");
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
                        byte[] data = new byte[1024];
                        int bytesRead = _stream.Read(data, 0, data.Length);
                        //if(bytesRead == 0)
                        //{
                        //    _connectionState = ConnectionStatus.disconnect;
                        //    ExceptionMsgAction.Invoke("发生错误，断开连接");
                        //}
                        string message = Encoding.UTF8.GetString(data, 0, bytesRead);
                        ReciviMsgAction.Invoke(message);
                    }

                    Thread.Sleep(100);

                }
                catch (Exception ex)
                {
                    _connectionState = ConnectionStatus.disconnect;
                    ExceptionMsgAction.Invoke("发生错误，断开连接");
                    Thread.Sleep(3000);
                }
            }

        }
    }
}

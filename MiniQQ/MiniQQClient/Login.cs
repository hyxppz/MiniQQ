using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniQQClient
{
    private Socket clientSocket;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888)); // 连接服务器地址和端口

        // 将用户名和密码通过分隔符组合并转换为字节数组
        string loginData = $"{txtUsername.Text}:{txtPassword.Text}";
        byte[] data = Encoding.UTF8.GetBytes(loginData);

        // 发送登陆信息
        clientSocket.Send(data);

        // 接收响应
        byte[] response = new byte[1024];
        int length = clientSocket.Receive(response);
        string responseStr = Encoding.UTF8.GetString(response, 0, length);

        // 如果登录成功，进入聊天界面
        if (responseStr == "Success")
        {
            // 假设有一个ChatForm类
            ChatForm chatForm = new ChatForm();
            chatForm.Show();
            this.Hide();
        }
        else
        {
            MessageBox.Show("登录失败，请检查用户名和密码");
        }
    }

    internal class Login
    {
    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;//在引用中添加windows窗体
using System.Threading;

namespace 五子棋
{
    public partial class UDPSocket : Component
    {
        #region  全局变量
        private IPEndPoint ServerEndPoint = null;   //定义网络端点
        private UdpClient UDP_Server = new UdpClient(); //创建网络服务,也就是UDP的Socket
        private System.Threading.Thread thdUdp; //创建一个线程
        private string localHost = "127.0.0.1";
        #endregion

        #region  定义组件的事件
        public delegate void DataArrivalEventHandler(byte[] Data, IPAddress Ip, int Port);  //定义了一个托管方法
        public event DataArrivalEventHandler DataArrival;   //通过托管理在控件中定义一个事件
        #endregion

        #region  定义组件的属性
        [Browsable(true), Category("Local"), Description("本地IP地址")]   //在“属性”窗口中显示localHost属性
        public string LocalHost
        {
            get { return localHost; }
            set { localHost = value; }
        }

        private int localPort = 11000;
        [Browsable(true), Category("Local"), Description("本地端口号")] //在“属性”窗口中显示localPort属性
        public int LocalPort
        {
            get { return localPort; }
            set { localPort = value; }
        }

        private bool active = false;
        [Browsable(true), Category("Local"), Description("激活监听")]   //在“属性”窗口中显示active属性
        public bool Active
        {
            get { return active; }
            set //该属性读取值
            {
                active = value;
                if (active) //当值为True时
                {
                    OpenSocket();   //打开监听
                }
                else
                {
                    CloseSocket();  //关闭监听
                }
            }
        }
        #endregion

        public UDPSocket()
        {
            InitializeComponent();
        }

        public UDPSocket(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region  对发送的消信进行监听
        /// <summary>
        /// 对发送的消信进行监听
        /// </summary>
        protected void Listener()   //监听
        {
            ServerEndPoint = new IPEndPoint(IPAddress.Any, localPort);   //将IP地址和端口号以网络端点存储
            if (UDP_Server != null)
                UDP_Server.Close();
            UDP_Server = new UdpClient(localPort);  //创建一个新的端口号
            UDP_Server.Client.ReceiveBufferSize = 1000000000;   //接收缓冲区大小
            UDP_Server.Client.SendBufferSize = 1000000000;  //发送缓冲区大小
            try
            {
                thdUdp = new Thread(new ThreadStart(GetUDPData));   //创建一个线程
                thdUdp.Start(); //执行当前线程
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + "\n" + "监听失败");  //显示线程的错误信息
            }
        }
        #endregion

        #region  获取当前接收的消息
        /// <summary>
        /// 获取录前接收的消息
        /// </summary>
        private void GetUDPData()   //获取当前接收的消息
        {
            while (active)
            {
                try
                {
                    byte[] Data = UDP_Server.Receive(ref ServerEndPoint);   //将获取的远程消息转换成二进制流
                    if (DataArrival != null)    //如果当前正在托管
                    {
                        DataArrival(Data, ServerEndPoint.Address, ServerEndPoint.Port);//利用当前控件的DataArrival事件将消息发给远程计算机
                    }
                    Thread.Sleep(0);
                }
                catch { }
            }
        }
        #endregion

        #region  获取委托对象
        /// <summary>
        /// 获取委托对象
        /// </summary>
        private void CallBackMethod(IAsyncResult ar)
        {
            //从异步状态ar.AsyncState中，获取委托对象
            DataArrivalEventHandler dn = (DataArrivalEventHandler)ar.AsyncState;
            //一定要EndInvoke，否则你的下场很惨
            dn.EndInvoke(ar);
        }
        #endregion

        #region  将消息发送给远程计算机
        /// <summary>
        /// 将消息发送给远程计算机
        /// </summary>
        public void Send(System.Net.IPAddress Host, int Port, byte[] Data)
        {
            try
            {
                int pp = this.localPort;
                UDP_Server.Send(Data, Data.Length, new IPEndPoint(Host, Port));         //将消息发给远程计算机
            }
            catch { }
        }
        #endregion

        #region  打开socket
        /// <summary>
        /// 打开socket
        /// </summary>
        private void OpenSocket()    //打开socket
        {
            if (UDP_Server != null)
                UDP_Server.Close();
            Listener();         //通过该方法对UDP协议进行监听
        }
        #endregion

        #region  关闭socket
        /// <summary>
        /// 关闭socdet
        /// </summary>
        private void CloseSocket()    //关闭socket
        {
            if (UDP_Server != null)     //如果socket不为空
                UDP_Server.Close();     //关闭socket
            if (thdUdp != null)         //如果自定义线程被打开
            {
                Thread.Sleep(30);       //睡眠主线程
                thdUdp.Abort();         //关闭子线程
            }
        }
        #endregion
    }
}

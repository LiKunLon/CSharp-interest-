using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;//域名解析功能的命名空间
using System.Collections;//代送器的命名空间
using System.Runtime.InteropServices;//主机地址容器的命名空间

namespace 五子棋
{
    class FrmClass
    {
        public static string ServerIP = "";
        public static string ServerPort = "";
        public static string ClientIP = "";
        public static string ClientPort = "";

        public string MyHostIP()
        {
            // 显示主机名
            string hostname = Dns.GetHostName();
            // 显示每个IP地址
            IPHostEntry hostent = Dns.GetHostEntry(hostname); // 主机信息
            Array addrs = hostent.AddressList;            // IP地址数组
            IEnumerator it = addrs.GetEnumerator();       // 迭代器，添加名命空间using System.Collections;
            while (it.MoveNext())
            {   // 循环到下一个IP 地址
                IPAddress ip = (IPAddress)it.Current;      //获得IP地址，添加名命空间using System.Net;
                return ip.ToString();
            }
            return "";
        }
    }
}

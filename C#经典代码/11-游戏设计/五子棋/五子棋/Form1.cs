using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Collections;
using System.IO;

namespace 五子棋
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int[,] note = new int[15, 15];//记录棋子的摆放位置,0为白棋,1为黑棋
        FrmClass frmclass = new FrmClass();
        private static MessClass temMsg = new MessClass();
        public static float Mouse_X = 0;//记录鼠标的X坐标
        public static float Mouse_Y = 0;//记录鼠标的Y坐标
        //下面是初始化必需设置的变量
        public static bool ChessStyle = true;//黑方还是白方，true为黑方
        public static bool CGrow = true;//当前棋子的类型
        public static bool DownChess = true;//对方是否下完棋
        public static int CKind = -1;//设录取胜的棋种，0为白棋

        private void udpSocket1_DataArrival(byte[] Data, System.Net.IPAddress Ip, int Port)
        {
            DataArrivaldelegate outdelegate = new DataArrivaldelegate(DataArrival);
            this.BeginInvoke(outdelegate, new object[] { Data, Ip, Port }); //设置托管
        }

        private delegate void DataArrivaldelegate(byte[] Data, System.Net.IPAddress Ip, int Port);
        private void DataArrival(byte[] Data, System.Net.IPAddress Ip, int Port) //当有数据到达后的处理进程
        {
            //将接收的消息转换成自定义集合MessClass
            MessClass msg = new ClassSerializers().DeSerializeBinary((new System.IO.MemoryStream(Data))) as MessClass;
            switch (msg.sendKind)//获取发送的类型
            {
                case SendKind.SendConn://连接
                    {
                        if (msg.ChessStyle)//判断当前棋子的类型
                            ChessStyle = true;//黑棋
                        else
                            ChessStyle = false;//白棋
                        CGrow = ChessStyle;//记录当前棋子的类型
                        CKind = -1;//记录取胜的棋子种类
                        temMsg.sendKind = SendKind.SendConnHit;//设置消息发送类型为连接成功
                        temMsg.ChessStyle = ChessStyle;//在发送消息中设置当前棋子的类型
                        //向远程计算机发送消息
                        udpSocket1.Send(IPAddress.Parse(FrmClass.ServerIP), Convert.ToInt32(FrmClass.ClientPort), new ClassSerializers().SerializeBinary(temMsg).ToArray());
                        break;
                    }
                case SendKind.SendConnHit://连接成功
                    {
                        MessageBox.Show("连接成功");//显示连接成功
                        button1.Tag = 1;//设置标识
                        button1.Text = "重新开始";
                        if (msg.ChessStyle)//如果是黑棋
                        {
                            ChessStyle = true;//设置本地的棋子类型为黑棋
                            DownChess = true;//本地先下
                            label2.Text = "黑棋";//显示本地为黑棋
                        }
                        else
                        {
                            ChessStyle = false;//设置本地的棋子类型为白棋
                            DownChess = false;//本地后下
                            label2.Text = "白棋";//显示本地为白棋
                        }
                        CGrow = ChessStyle;//记录本地的棋子类型
                        panel2.Visible = false;//隐藏最后落子的标记
                        break;
                    }
                case SendKind.SendAfresh://重新下棋
                    {
                        //清空棋盘中各棋子的位置
                        for (int i = 0; i < 15; i++)
                            for (int j = 0; j < 15; j++)
                                note[i, j] = -1;
                        Graphics g = panel1.CreateGraphics();//创健panel1控件的Graphics类
                        g.DrawImage(Properties.Resources.棋盘, 0, 0, panel1.Width, panel1.Height);//清空棋盘
                        if (msg.ChessStyle)//如果是黑棋
                        {
                            ChessStyle = true;//设置本地的棋子类型为黑棋
                            DownChess = true;//设置本地的棋子类型为黑棋
                            label2.Text = "黑棋";//显示本地为黑棋
                        }
                        else
                        {
                            ChessStyle = false;//设置本地的棋子类型为白棋
                            DownChess = false;//本地后下
                            label2.Text = "白棋";//显示本地为白棋
                        }
                        CGrow = ChessStyle;//记录本地的棋子类型
                        CKind = -1;//记录取胜的棋子种类
                        panel2.Visible = false;//隐藏最后落子的标记
                        break;
                    }
                case SendKind.SendChessman://接收发送的棋子
                    {
                        int tem_CS = -1;
                        Image tem_Image;//实例化Image类
                        if (msg.Grow)//如果为黑棋
                        {
                            tem_CS = 1;//记录棋子类型为黑棋
                            CGrow = true;//记录当前为黑棋
                            tem_Image = Properties.Resources.黑棋子;//存储黑棋的图片
                        }
                        else
                        {
                            tem_CS = 0;//记录棋子类型为白棋
                            CGrow = false;//记录当前为黑棋
                            tem_Image = Properties.Resources.白棋子;//存储白棋的图片
                        }
                        note[msg.ChessX, msg.ChessY] = tem_CS;//在数组中记录当前棋子的位置
                        Graphics g = panel1.CreateGraphics();
                        g.DrawImage(tem_Image, msg.ChessX * 35 + 7, msg.ChessY * 35 + 7, 35, 35);//在棋盘中显示对方下的棋子
                        panel2.Visible = true;//显示最后落子的标记
                        panel2.Location = new System.Drawing.Point(msg.ChessX * 35 + 20, msg.ChessY * 35 + 20);//将标记显示在棋子上
                        DownChess = msg.Walk;//记录对方是否下完棋
                        CGrow = !msg.Grow;//记录本地的棋子类型
                        Arithmetic(tem_CS, msg.ChessX, msg.ChessY);//计算对方是否获胜
                        DownChess = true;//对方已下完棋
                        break;
                    }
                case SendKind.SendCut://断开连接
                    {
                        temMsg.sendKind = SendKind.SendCutHit;//设置发送的类型为断开连接
                        //向远程计算机发送断开消息
                        udpSocket1.Send(IPAddress.Parse(FrmClass.ServerIP), Convert.ToInt32(FrmClass.ClientPort), new ClassSerializers().SerializeBinary(temMsg).ToArray());
                        button1.Text = "连接";//显当前可重新连接
                        button1.Tag = 0;//设置连接标识
                        break;
                    }
                case SendKind.SendCutHit://断开成功
                    {
                        udpSocket1.Active = false;//关闭UDP的连接
                        Application.Exit();//关闭当前工程
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //清空记录各棋子位置的数组
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    note[i, j] = -1;
            CKind = -1;//记录棋子种类
            if (Convert.ToInt32(((Button)sender).Tag.ToString()) == 0)//如当前为开始连接
            {
                if (textBox1.Text.Trim() == "")//如果IP地址为空
                {
                    MessageBox.Show("请输入IP地址。");
                    return;
                }
                FrmClass.ServerIP = textBox1.Text;//记录远程计算机的IP地址
                udpSocket1.Active = false;//关闭UDP的连接
                udpSocket1.LocalPort = 11001;//设置端口号
                udpSocket1.Active = true;//打开UDP的连接
                temMsg.sendKind = SendKind.SendConn;//设置发送类型为连接
                temMsg.ChessStyle = !ChessStyle;//设置对方的棋子类型
            }
            if (Convert.ToInt32(((Button)sender).Tag.ToString()) == 1)//如当前为重新开始
            {
                ChessStyle = !ChessStyle;//改变本地的棋子类型
                temMsg.sendKind = SendKind.SendAfresh;//设置消息类型为重新开始
                temMsg.ChessStyle = !ChessStyle;//设置远程计算机的棋子类型
                Graphics g = panel1.CreateGraphics();
                g.DrawImage(Properties.Resources.棋盘, 0, 0, panel1.Width, panel1.Height);//清空棋盘
            }
            if (ChessStyle)//如果本地的棋子类型为黑棋
            {
                label2.Text = "黑棋";//显示本地为黑棋
                DownChess = true;
            }
            else
            {
                label2.Text = "白棋";//显示本地为白棋
                DownChess = false;
            }
            CGrow = ChessStyle;//记录当前的棋子类型
            panel2.Visible = false;//隐藏最后落子的标记
            //将消息发送给远程计算机
            udpSocket1.Send(IPAddress.Parse(FrmClass.ServerIP), Convert.ToInt32(FrmClass.ClientPort), new ClassSerializers().SerializeBinary(temMsg).ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化记录棋子的数组
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    note[i, j] = -1;
            FrmClass.ClientIP = frmclass.MyHostIP();//获取本地的IP地址
            FrmClass.ClientPort = "11001";//获取端口号
        }

        private void Form1_FontChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            temMsg.sendKind = SendKind.SendCut;//设置发送的消息为断开
            if (FrmClass.ServerIP != "")//如果与远程计算机连接
                //将消息发送给远程计算机
                udpSocket1.Send(IPAddress.Parse(FrmClass.ServerIP), Convert.ToInt32(FrmClass.ClientPort), new ClassSerializers().SerializeBinary(temMsg).ToArray());
            if (Convert.ToInt32(button1.Tag.ToString()) == 0)//如果没有与远程计算机连接
                Application.Exit();//退出当前工程
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Image tem_Image = Properties.Resources.白棋子;//实例化Image类
            e.Graphics.DrawImage(Properties.Resources.棋盘, 0, 0, panel1.Width, panel1.Height);//清空棋盘
            //遍历已下完的棋子
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    if (note[i, j] > -1)
                    {
                        if (note[i, j] == 0)//如果当前是白棋
                            tem_Image = Properties.Resources.白棋子;//在指定的位置绘制白棋
                        if (note[i, j] == 1)//如果当前是黑棋
                            tem_Image = Properties.Resources.黑棋子;//在指定的位置绘制黑棋
                        e.Graphics.DrawImage(tem_Image, i * 35 + 7, j * 35 + 7, 35, 35);//绘制已下完的棋子
                    }
                }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (udpSocket1.Active == false)
            {
                MessageBox.Show("没有与远程计算机建立连接！");
                return;
            }
            if (DownChess == false)//如果对方没有下完棋
            {
                MessageBox.Show("对方没有下完棋！");
                return;
            }
            if (CKind > -1)//如果当前有棋子获胜
            {
                if (CKind == 0)//如果白棋获胜
                    Bwin();//显示白棋获胜的提示框
                if (CKind == 1)//如果黑棋获胜
                    Wwin();//显示黑棋获胜的提示框
                return;
            }
            int Column = Convert.ToInt32(Math.Round((Mouse_X - 30) / 35));//获取当前落子的所在列
            int Row = Convert.ToInt32(Math.Round((Mouse_Y - 30) / 35));//获取当前落子的所在行
            if (note[Column, Row] == -1)//如果当前位置无棋子
            {
                int tem_CS = -1;
                Image tem_Image;//实例化Image类
                if (CGrow)//如果当前落子为黑棋
                {
                    tem_CS = 1;
                    CGrow = true;//记录落子类型
                    tem_Image = Properties.Resources.黑棋子;//存储黑棋图片
                }
                else
                {
                    tem_CS = 0;
                    CGrow = false;//记录落子类型
                    tem_Image = Properties.Resources.白棋子;//存储白棋图片
                }
                note[Column, Row] = tem_CS;//记录当前位置已有棋子
                Graphics g = panel1.CreateGraphics();
                g.DrawImage(tem_Image, Column * 35 + 7, Row * 35 + 7, 35, 35);//在棋盘中显示当前下的棋子
                panel2.Visible = true;//显示最后落子的标记
                panel2.Location = new System.Drawing.Point(Column * 35 + 20, Row * 35 + 20);//在棋子上显示标记
                DownChess = false;//对方没有下棋

                temMsg.sendKind = SendKind.SendChessman;//设置发送的类型为发送棋子
                temMsg.ChessX = Column;//记录棋子所在行
                temMsg.ChessY = Row;//记录棋子所在列
                temMsg.Grow = CGrow;//记录前棋子的类型
                temMsg.Walk = true;//记录本地已下完棋
                //向远程计算机发送消息
                udpSocket1.Send(IPAddress.Parse(FrmClass.ServerIP), Convert.ToInt32(FrmClass.ClientPort), new ClassSerializers().SerializeBinary(temMsg).ToArray());
                Arithmetic(tem_CS, Column, Row);//计算本地是否获胜
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //记录鼠标的单击位置
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        /// <summary>
        /// 算法
        /// </summary>
        public void Arithmetic(int n, int Arow, int Acolumn)//算法
        {
            int BCount = 1;
            CKind = -1;//记录棋子种类
            //横向查找
            bool Lbol = true;
            bool Rbol = true;
            int jlsf = 0;
            BCount = 1;
            for (int i = 1; i <= 5; i++)
            {
                if ((Acolumn + i) > 14)
                    Rbol = false;
                if ((Acolumn - i) < 0)
                    Lbol = false;
                if (Rbol == true)
                {
                    if (note[Arow, Acolumn + i] == n)
                        ++BCount;
                    else
                        Rbol = false;
                }
                if (Lbol == true)
                {
                    if (note[Arow, Acolumn - i] == n)
                        ++BCount;
                    else
                        Lbol = false;
                }
                if (BCount >= 5)
                {
                    if (n == 0)
                        Bwin();
                    if (n == 1)
                        Wwin();
                    jlsf = n;
                    break;
                }
            }

            //纵向查找
            bool Ubol = true;
            bool Dbol = true;
            BCount = 1;
            for (int i = 1; i <= 5; i++)
            {
                if ((Arow + i) > 14)
                    Dbol = false;
                if ((Arow - i) < 0)
                    Ubol = false;
                if (Dbol == true)
                {
                    if (note[Arow + i, Acolumn] == n)
                        ++BCount;
                    else
                        Dbol = false;
                }
                if (Ubol == true)
                {
                    if (note[Arow - i, Acolumn] == n)
                        ++BCount;
                    else
                        Ubol = false;
                }
                if (BCount >= 5)
                {
                    if (n == 0)
                        Bwin();
                    if (n == 1)
                        Wwin();
                    jlsf = n;
                    break;
                }
            }

            //正斜查找
            bool LUbol = true;
            bool RDbol = true;
            BCount = 1;
            for (int i = 1; i <= 5; i++)
            {
                if ((Arow - i) < 0 || (Acolumn - i < 0))
                    LUbol = false;
                if ((Arow + i) > 14 || (Acolumn + i > 14))
                    RDbol = false;
                if (LUbol == true)
                {
                    if (note[Arow - i, Acolumn - i] == n)
                        ++BCount;
                    else
                        LUbol = false;
                }
                if (RDbol == true)
                {
                    if (note[Arow + i, Acolumn + i] == n)
                        ++BCount;
                    else
                        RDbol = false;
                }
                if (BCount >= 5)
                {
                    if (n == 0)
                        Bwin();
                    if (n == 1)
                        Wwin();
                    jlsf = n;
                    break;
                }
            }
            //反斜查找
            bool RUbol = true;
            bool LDbol = true;
            BCount = 1;
            for (int i = 1; i <= 5; i++)
            {
                if ((Arow - i) < 0 || (Acolumn + i > 14))
                    RUbol = false;
                if ((Arow + i) > 14 || (Acolumn - i < 0))
                    LDbol = false;
                if (RUbol == true)
                {
                    if (note[Arow - i, Acolumn + i] == n)
                        ++BCount;
                    else
                        RUbol = false;
                }
                if (LDbol == true)
                {
                    if (note[Arow + i, Acolumn - i] == n)
                        ++BCount;
                    else
                        LDbol = false;
                }
                if (BCount >= 5)
                {
                    if (n == 0)
                        Bwin();
                    if (n == 1)
                        Wwin();
                    jlsf = n;
                    break;
                }
            }
        }

        /// <summary>
        /// 显示白棋获胜
        /// </summary>
        public void Bwin()
        {
            CKind = 0;
            MessageBox.Show("白棋获胜！");
            if (ChessStyle == false)//如果当前为白棋
                label6.Text = Convert.ToString(Convert.ToInt32(label6.Text) + 1);//白棋得分
            else
                label7.Text = Convert.ToString(Convert.ToInt32(label7.Text) + 1);//黑棋得分
        }

        /// <summary>
        /// 显示黑棋获胜
        /// </summary>
        public void Wwin()
        {
            CKind = 1;
            MessageBox.Show("黑棋获胜！");
            if (ChessStyle == true)
                label6.Text = Convert.ToString(Convert.ToInt32(label6.Text) + 1);//黑棋得分
            else
                label7.Text = Convert.ToString(Convert.ToInt32(label7.Text) + 1);//白棋得分
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaRongDao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Panel pl;//记录选中的控件ID

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化时设置各个位置的可用状态
            PlState[0, 0] = PlState[0, 1] = PlState[0, 2] = PlState[0, 3] =
                PlState[1, 0] = PlState[1, 1] = PlState[1, 2] = PlState[1, 3] =
                PlState[2, 0] = PlState[2, 1] = PlState[2, 2] = PlState[2, 3] =
                PlState[3, 0] = PlState[3, 1] = PlState[3, 2] = PlState[3, 3] =
                PlState[4, 0] = PlState[4, 3] = true;
        }

        //初始化各人物方格位置
        private void button1_Click(object sender, EventArgs e)
        {
            //设置各方格的初始图片
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            //设置各方格的初始位置
            panel1.Location = position[0, 0];
            panel2.Location = position[0, 1];
            panel3.Location = position[0, 3];
            panel4.Location = position[2, 0];
            panel5.Location = position[2, 1];
            panel6.Location = position[2, 3];
            panel7.Location = position[3, 1];
            panel8.Location = position[3, 2];
            panel9.Location = position[4, 0];
            panel10.Location = position[4, 3];
            PlState[4, 0] = PlState[4, 3] = true;   //设置最后一行的首尾位置不可用
            PlState[4, 1] = PlState[4, 2] = false;  //设置最后一行的中间两个位置可用
        }

        #region 赵云
        private void panel1_Click(object sender, EventArgs e)
        {
            //设置各方格图片
            panel1.BackgroundImage = (Image)(Properties.Resources.a3);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P2V;//记录方格样式
            pl = panel1;         //记录选中的控件ID
            PosX = intX(panel1); //记录选中控件在坐标数组中的列索引
            PosY = intY(panel1); //记录选中控件在坐标数组中的行索引
        }
        #endregion

        #region 曹操
        private void panel2_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources.a1);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P4;
            pl = panel2;
            PosX = intX(panel2);
            PosY = intY(panel2);
        }
        #endregion

        #region 张飞
        private void panel3_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources.a4);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P2V;
            pl = panel3;
            PosX = intX(panel3);
            PosY = intY(panel3);
        }
        #endregion

        #region 马超
        private void panel4_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources.a5);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P2V;
            pl = panel4;
            PosX = intX(panel4);
            PosY = intY(panel4);
        }
        #endregion

        #region 关羽
        private void panel5_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources.a2);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P2H;
            pl = panel5;
            PosX = intX(panel5);
            PosY = intY(panel5);
        }
        #endregion

        #region 黄忠
        private void panel6_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources.a6);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P2V;
            pl = panel6;
            PosX = intX(panel6);
            PosY = intY(panel6);
        }
        #endregion

        #region 士兵一
        private void panel7_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources.a7);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P1;
            pl = panel7;
            PosX = intX(panel7);
            PosY = intY(panel7);
        }
        #endregion

        #region 士兵二
        private void panel8_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources.a8);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P1;
            pl = panel8;
            PosX = intX(panel8);
            PosY = intY(panel8);
        }
        #endregion

        #region 士兵三
        private void panel9_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources.a9);
            panel10.BackgroundImage = (Image)(Properties.Resources._010);
            PStyle = PStyles.P1;
            pl = panel9;
            PosX = intX(panel9);
            PosY = intY(panel9);
        }
        #endregion

        #region 士兵四
        private void panel10_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)(Properties.Resources._003);
            panel2.BackgroundImage = (Image)(Properties.Resources._001);
            panel3.BackgroundImage = (Image)(Properties.Resources._004);
            panel4.BackgroundImage = (Image)(Properties.Resources._005);
            panel5.BackgroundImage = (Image)(Properties.Resources._002);
            panel6.BackgroundImage = (Image)(Properties.Resources._006);
            panel7.BackgroundImage = (Image)(Properties.Resources._007);
            panel8.BackgroundImage = (Image)(Properties.Resources._008);
            panel9.BackgroundImage = (Image)(Properties.Resources._009);
            panel10.BackgroundImage = (Image)(Properties.Resources.a10);
            PStyle = PStyles.P1;
            pl = panel10;
            PosX = intX(panel10);
            PosY = intY(panel10);
        }
        #endregion

        #region 判断是否成功
        ///<summary>
        ///判断是否成功
        ///</summary>
        ///<returns>true表示成功,false表示失败</returns>
        private bool Successful()
        {
            if (panel2.Location == new Point(108, 343))
            {
                MessageBox.Show("恭喜你，曹操已经成功逃离华容道！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }
        #endregion

        //通过按键盘上的上、下、左、右键来移动人物方格
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            blUp = blDown = blLeft = blRight = false;//设置上、下、左、右方向移动不可用  
            switch (e.KeyData)
            {
                case Keys.Up:       //向上移动
                    blUp = true;
                    break;
                case Keys.Down:     //向下移动
                    blDown = true;
                    break;
                case Keys.Left:     //向左移动
                    blLeft = true;
                    break;
                case Keys.Right:    //向右移动
                    blRight = true;
                    break;
            }
            MovePosition(pl, PosX, PosY);//移动人物方格位置
            if (Successful())//判断是否成功
                button1_Click(sender, e);//重新开始
        }

        //定义每个人物方格的大小, 共有x*y(x=4, y=5)个人物方格
        public enum plEnumerate : int
        {
            plSize = 100,   //人物方格大小
            plX = 4,        //定义列
            plY = 5,        //定义行
        };

        //判断索引位置是否可用
        public bool[,] PlState = new bool[(int)plEnumerate.plY, (int)plEnumerate.plX];

        //定义一个数组，用来记录各人物方格位置
        private Point[,] position = new Point[5, 4] {
            { new Point(8, 43), new Point(108, 43), new Point(208, 43), new Point(308, 43) },
            { new Point(8, 143), new Point(108, 143), new Point(208, 143), new Point(308, 143) },
            { new Point(8, 243), new Point(108, 243), new Point(208, 243), new Point(308, 243) },
            { new Point(8, 343), new Point(108, 343), new Point(208, 343), new Point(308, 343) },
            { new Point(8, 443), new Point(108, 443), new Point(208, 443), new Point(308, 443) },
        };

        //获取人物方格的位置
        public Point[,] GetPosition()
        {
            return this.position;
        }

        //定义各人物方格的形状
        public enum PStyles : int
        {
            P1 = 0,//口
            P4 = 1,//田
            P2V = 2,//日
            P2H = 3,//口口
        };

        private int m_PStyle;//获取人物样式的编号

        //记录人物样式形状
        public PStyles PStyle
        {
            get { return (PStyles)this.m_PStyle; }
            set { this.m_PStyle = (int)value; }
        }

        private int m_X = 0, m_Y = 0;//记录人物方格索引

        //返回人物方格列索引
        public int PosX
        {
            get { return this.m_X; }
            set { this.m_X = value; }
        }

        //返回人物方格行索引
        public int PosY
        {
            get { return this.m_Y; }
            set { this.m_Y = value; }
        }

        #region 计算选中的人物方格在数组中的列索引
        ///<summary>
        ///计算选中的人物方格在数组中的列索引
        ///</summary>
        ///<param name="pl">选中的Panel控件</param>
        ///<returns>列索引</returns>
        private int intX(Panel pl)
        {
            int i = pl.Location.X - 8;
            return i / 100;
        }
        #endregion

        #region 计算选中的人物方格在数组中的行索引
        ///<summary>
        ///计算选中的人物方格在数组中的行索引
        ///</summary>
        ///<param name="pl">选中的Panel控件</param>
        ///<returns>行索引</returns>
        private int intY(Panel pl)
        {
            int i = pl.Location.Y - 43;
            return i / 100;
        }
        #endregion

        //定义4个变量，分别用来表示向上、下、左、右移动
        private bool blUp, blDown, blLeft, blRight;

        #region 移动人物位置
        ///<summary>
        ///移动人物位置
        ///</summary>
        ///<param name="pl">要移动的控件名称</param>
        ///<param name="x">横坐标在坐标数组中的索引</param>
        ///<param name="y">纵坐标在坐标数组中的索引</param>
        ///<returns>是否移动成功</returns>
        public bool MovePosition(Panel pl, int x, int y)
        {
            #region 上移
            if (blUp && (y - 1) >= 0)
            {
                switch (PStyle)
                {
                    case PStyles.P4://田
                    case PStyles.P2H://口口
                        if (!PlState[y - 1, x] && !PlState[y - 1, x + 1])
                        {
                            pl.Location = GetPosition()[y - 1, x];
                            if (PStyle == PStyles.P4)//田
                            {
                                PlState[y + 1, x] = false;
                                PlState[y + 1, x + 1] = false;
                            }
                            else if (PStyle == PStyles.P2H)//口口
                            {
                                PlState[y, x] = false;
                                PlState[y, x + 1] = false;
                            }
                            PlState[y - 1, x] = true;
                            PlState[y - 1, x + 1] = true;
                            PosY -= 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P2V://日
                    case PStyles.P1://口
                        if (!PlState[y - 1, x])
                        {
                            pl.Location = GetPosition()[y - 1, x];
                            if (PStyle == PStyles.P2V)//日
                            {
                                PlState[y + 1, x] = false;
                            }
                            else if (PStyle == PStyles.P1)//口
                            {
                                PlState[y, x] = false;
                            }
                            PlState[y - 1, x] = true;
                            PosY -= 1;
                            return true;
                        }
                        else return false;
                }
            }
            #endregion
            #region 下移
            else if (blDown)
            {
                switch (PStyle)
                {
                    case PStyles.P4://田
                        if ((y + 2) < (int)plEnumerate.plY && !PlState[y + 2, x] && !PlState[y + 2, x + 1])
                        {
                            pl.Location = GetPosition()[y + 1, x];
                            PlState[y, x] = false;
                            PlState[y, x + 1] = false;
                            PlState[y + 2, x] = true;
                            PlState[y + 2, x + 1] = true;
                            PosY += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P2V://日
                        if ((y + 2) < (int)plEnumerate.plY && !PlState[y + 2, x])
                        {
                            pl.Location = GetPosition()[y + 1, x];
                            PlState[y, x] = false;
                            PlState[y + 2, x] = true;
                            PosY += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P1://口
                        if ((y + 1) < (int)plEnumerate.plY && !PlState[y + 1, x])
                        {
                            pl.Location = GetPosition()[y + 1, x];
                            PlState[y, x] = false;
                            PlState[y + 1, x] = true;
                            PosY += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P2H://口口
                        if ((y + 1) < (int)plEnumerate.plY && !PlState[y + 1, x] && !PlState[y + 1, x + 1])
                        {
                            pl.Location = GetPosition()[y + 1, x];
                            PlState[y, x] = false;
                            PlState[y, x + 1] = false;
                            PlState[y + 1, x] = true;
                            PlState[y + 1, x + 1] = true;
                            PosY += 1;
                            return true;
                        }
                        else return false;
                }
            }
            #endregion
            #region 左移
            else if (blLeft)
            {
                switch (PStyle)
                {
                    case PStyles.P2V://日
                    case PStyles.P4://田
                        if (x - 1 >= 0 && !PlState[y, x - 1] && !PlState[y + 1, x - 1])
                        {
                            pl.Location = GetPosition()[y, x - 1];
                            switch (PStyle)
                            {
                                case PStyles.P4://田
                                    PlState[y, x + 1] = false;
                                    PlState[y + 1, x + 1] = false;
                                    break;

                                case PStyles.P2V://日
                                    PlState[y, x] = false;
                                    PlState[y + 1, x] = false;
                                    break;
                            }
                            PlState[y, x - 1] = true;
                            PlState[y + 1, x - 1] = true;
                            PosX -= 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P1://口
                    case PStyles.P2H://口口
                        if (x - 1 >= 0 && !PlState[y, x - 1])
                        {
                            pl.Location = GetPosition()[y, x - 1];
                            if (PStyle == PStyles.P2H)//口口
                            {
                                PlState[y, x + 1] = false;
                            }
                            else
                            {
                                PlState[y, x] = false;
                            }
                            PlState[y, x - 1] = true;
                            PosX -= 1;
                            return true;
                        }
                        else return false;
                }
            }
            #endregion
            #region 右移
            else if (blRight)
            {
                switch (PStyle)
                {
                    case PStyles.P4://田
                        if (x + 2 < (int)plEnumerate.plX && !PlState[y, x + 2] && !PlState[y + 1, x + 2])
                        {
                            pl.Location = GetPosition()[y, x + 1];
                            PlState[y, x] = false;
                            PlState[y + 1, x] = false;
                            PlState[y, x + 2] = true;
                            PlState[y + 1, x + 2] = true;
                            PosX += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P1://口
                        if (x + 1 < (int)plEnumerate.plX && !PlState[y, x + 1])
                        {
                            pl.Location = GetPosition()[y, x + 1];
                            PlState[y, x] = false;
                            PlState[y, x + 1] = true;
                            PosX += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P2H://口口
                        if (x + 2 < (int)plEnumerate.plX && !PlState[y, x + 2])
                        {
                            pl.Location = GetPosition()[y, x + 1];
                            PlState[y, x] = false;
                            PlState[y, x + 2] = true;
                            PosX += 1;
                            return true;
                        }
                        else return false;
                    case PStyles.P2V://日
                        if (x + 1 < (int)plEnumerate.plX && !PlState[y, x + 1] && !PlState[y + 1, x + 1])
                        {
                            pl.Location = GetPosition()[y, x + 1];
                            PlState[y, x] = false;
                            PlState[y + 1, x] = false;
                            PlState[y, x + 1] = true;
                            PlState[y + 1, x + 1] = true;
                            PosX += 1;
                            return true;
                        }
                        else return false;
                }
            }
            #endregion
            return false;
        }      
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace 俄罗斯方块
{
    class Russia
    {
        public Point firstPoi = new Point(140, 20);//定义方块的起始位置
        public static Color[,] PlaceColor;//记录方块的位置
        public static bool[,] Place;//记录方块的位置
        public static int conWidth = 0;//记录列数
        public static int conHeight = 0;//记录行数
        public static int maxY = 0;//方块在行中的最小高度
        public static int conMax = 0;//方块落下后的最大位置
        public static int conMin = 0;//方块落下后的最小位置
        bool[] tem_Array = { false, false, false, false };//记录方块组中那一块所在行中已满
        Color ConColor = Color.Coral;
        Point[] ArryPoi = new Point[4];//方块的数组
        Point[] Arryfront = new Point[4];//前一个方块的数组
        int Cake = 20;//定义方块的大小
        int Convertor = 0;//变换器
        Control Mycontrol = new Control();//实例化Control
        public Label Label_Linage = new Label();//实例化Label，用于显示去除的行数
        public Label Label_Fraction = new Label();//实例化Label，用于显示分数
        public static int[] ArrayCent = new int[] { 2, 5, 9, 15 };//记录加分情况
        public Timer timer = new Timer();

        /// <summary>
        /// 设置方块的样式
        /// </summary>
        /// <param n="int">标识，方块的样式</param>
        public void CakeMode(int n)
        {
            ArryPoi[0] = firstPoi;//记录方块的起始位置
            switch (n)//根据标识设置方块的样式
            {
                case 1://组合“L”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X, firstPoi.Y - Cake);//设置第二块方块的位置
                        ArryPoi[2] = new Point(firstPoi.X, firstPoi.Y + Cake);//设置第三块方块的位置
                        ArryPoi[3] = new Point(firstPoi.X + Cake, firstPoi.Y + Cake);//设置第四块方块的位置
                        ConColor = Color.Fuchsia;//设置当前方块的颜色
                        Convertor = 2;//记录方块的变换样式
                        break;
                    }
                case 2://组合“Z”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X, firstPoi.Y - Cake);
                        ArryPoi[2] = new Point(firstPoi.X - Cake, firstPoi.Y - Cake);
                        ArryPoi[3] = new Point(firstPoi.X + Cake, firstPoi.Y);
                        ConColor = Color.Yellow;
                        Convertor = 6;
                        break;
                    }
                case 3://组合倒“L”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X, firstPoi.Y - Cake);
                        ArryPoi[2] = new Point(firstPoi.X, firstPoi.Y + Cake);
                        ArryPoi[3] = new Point(firstPoi.X - Cake, firstPoi.Y + Cake);
                        ConColor = Color.CornflowerBlue;
                        Convertor = 8;
                        break;
                    }
                case 4://组合倒“Z”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X, firstPoi.Y - Cake);
                        ArryPoi[2] = new Point(firstPoi.X + Cake, firstPoi.Y - Cake);
                        ArryPoi[3] = new Point(firstPoi.X - Cake, firstPoi.Y);
                        ConColor = Color.Blue;
                        Convertor = 12;
                        break;
                    }
                case 5://组合“T”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X, firstPoi.Y - Cake);
                        ArryPoi[2] = new Point(firstPoi.X + Cake, firstPoi.Y - Cake);
                        ArryPoi[3] = new Point(firstPoi.X - Cake, firstPoi.Y - Cake);
                        ConColor = Color.Silver;
                        Convertor = 14;
                        break;
                    }
                case 6://组合“一”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X + Cake, firstPoi.Y);
                        ArryPoi[2] = new Point(firstPoi.X - Cake, firstPoi.Y);
                        ArryPoi[3] = new Point(firstPoi.X - Cake*2, firstPoi.Y);
                        ConColor = Color.Red;
                        Convertor = 18;
                        break;
                    }
                case 7://组合“田”方块
                    {
                        ArryPoi[1] = new Point(firstPoi.X - Cake, firstPoi.Y);
                        ArryPoi[2] = new Point(firstPoi.X - Cake, firstPoi.Y - Cake);
                        ArryPoi[3] = new Point(firstPoi.X, firstPoi.Y - Cake);
                        ConColor = Color.LightGreen;
                        Convertor = 19;
                        break;
                    }
            }
        }

        /// <summary>
        /// 清空游戏背景
        /// </summary>
        public void ConvertorClear()
        {
            if (Mycontrol != null)//如要已载入背景控件
            {
                Graphics g = Mycontrol.CreateGraphics();//创建背景控件的Graphics类
                Rectangle rect = new Rectangle(0, 0, Mycontrol.Width, Mycontrol.Height);//获取背景的区域
                MyPaint(g, new SolidBrush(Color.Black), rect);//用背景色填充背景
            }
        }

        /// <summary>
        /// 清空当前方块的区域
        /// </summary>
        public void ConvertorDelete()
        {
            Graphics g = Mycontrol.CreateGraphics();//创建背景控件的Graphics类
            for (int i = 0; i < ArryPoi.Length; i++)//遍历方块的各个子方块
            {
                Rectangle rect = new Rectangle(ArryPoi[i].X, ArryPoi[i].Y, 20, 20);//获取各子方块的区域
                MyPaint(g, new SolidBrush(Color.Black), rect);//用背景色填充背景
            }
        }

        /// <summary>
        /// 变换当前方块的样式
        /// </summary>
        public void MyConvertorMode()
        {
            ConvertorDelete();//清空当前方块的区域
            ConvertorMode(Convertor);//设置方块的变换样式
            Protract(Mycontrol);//绘制变换后的组合方块
        }

        /// <summary>
        /// 设置方块的变换样式
        /// </summary>
        /// <param n="int">标识，判断变换的样式</param>
        public void ConvertorMode(int n)
        {
            Point[] tem_ArrayPoi = new Point[4];//定义一个临时数组
            Point tem_Poi = firstPoi;//获取方块的起始位置
            int tem_n = n;//记录方块的下一个变换样式
            //将当前方块的位置存入到临时数组中
            for (int i = 0; i < tem_ArrayPoi.Length; i++)
                tem_ArrayPoi[i] = ArryPoi[i];
            switch (n)//根据标识变换方块的样式
            {
                case 1://设置“L”方块的起始样式
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y + Cake);
                        tem_n = 2;//记录变换样式的标志
                        break;
                    }
                case 2://“L”方块向旋转的样式
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y - Cake);
                        tem_n = 3;
                        break;
                    }
                case 3://“L”方块向旋转的样式
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_n = 4;
                        break;
                    }
                case 4://“L”方块向旋转的样式
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake, tem_Poi.Y + Cake);
                        tem_n = 1;//返回方块的起始样式
                        break;
                    }
                case 5://Z
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_n = 6;
                        break;
                    }
                case 6:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_n = 5;
                        break;
                    }
                case 7://倒L
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake, tem_Poi.Y + Cake);
                        tem_n = 8;
                        break;
                    }
                case 8:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y + Cake);
                        tem_n = 9;
                        break;
                    }
                case 9:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y - Cake);
                        tem_n = 10;
                        break;
                    }
                case 10:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_n = 7;
                        break;
                    }
                case 11://倒Z
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_n = 12;
                        break;
                    }
                case 12:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_n = 11;
                        break;
                    }
                case 13://T
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_n = 14;
                        break;
                    }
                case 14:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_n = 15;
                        break;
                    }
                case 15:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_n = 16;
                        break;
                    }
                case 16:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_n = 13;
                        break;
                    }
                case 17://一
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X + Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X - Cake * 2, tem_Poi.Y);
                        tem_n = 18;
                        break;
                    }
                case 18:
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X, tem_Poi.Y + Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y + Cake * 2);
                        tem_n = 17;
                        break;
                    }
                case 19://田
                    {
                        tem_ArrayPoi[1] = new Point(tem_Poi.X - Cake, tem_Poi.Y);
                        tem_ArrayPoi[2] = new Point(tem_Poi.X - Cake, tem_Poi.Y - Cake);
                        tem_ArrayPoi[3] = new Point(tem_Poi.X, tem_Poi.Y - Cake);
                        tem_n = 19;
                        break;
                    }
            }
            bool tem_bool = true;//判断方块是否可变
            //遍历方块的各个子方块
            for (int i = 0; i < tem_ArrayPoi.Length; i++)
            {
                if (tem_ArrayPoi[i].X / 20 < 0)//变换后是否超出左边界
                {
                    tem_bool = false;//不变换
                    break;
                }
                if (tem_ArrayPoi[i].X / 20 >= conWidth)//变换后是否超出右边界
                {
                    tem_bool = false;
                    break;
                }
                if (tem_ArrayPoi[i].Y / 20 >= conHeight)//变换后是否超出下边界
                {
                    tem_bool = false;
                    break;
                }
                if (Place[tem_ArrayPoi[i].X / 20, tem_ArrayPoi[i].Y / 20])//变换后是否与其他方块重叠
                {
                    tem_bool = false;
                    break;
                }
            }
            if (tem_bool)//如果当前方块可以变换
            {
                //改变当前方块的样式
                for (int i = 0; i < tem_ArrayPoi.Length; i++)
                    ArryPoi[i] = tem_ArrayPoi[i];
                firstPoi = tem_Poi;//获取当前方块的起始位置
                Convertor = tem_n;//获取方块下一次的变换样式
            }
        }

        /// <summary>
        /// 绘制组合方块
        /// </summary>
        /// <param control="Control">控件</param>
        public void Protract(Control control)
        {
            Mycontrol = control;
            Graphics g = control.CreateGraphics();//创建背景控件的Graphics类
            //绘制方块的各个子方块
            for (int i = 0; i < ArryPoi.Length; i++)
            {
                Rectangle rect = new Rectangle(ArryPoi[i].X + 1, ArryPoi[i].Y + 1, 19, 19);//获取子方块的区域
                MyPaint(g, new SolidBrush(ConColor), rect);//绘制子方块
            }
        }

        /// <summary>
        /// 对方块的单个块进行绘制
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        /// <param SolidB="SolidBrush">画刷</param>
        /// <param rect="Rectangle">绘制区域</param>
        public void MyPaint(Graphics g, SolidBrush SolidB, Rectangle rect)
        {
            g.FillRectangle(SolidB, rect);//填充一个矩形
        }

        /// <summary>
        /// 方块移动
        /// </summary>
        /// <param n="int">标识，对左右下进行判断</param>
        public void ConvertorMove(int n)
        {
            //记录方块移动前的位置
            for (int i = 0; i < Arryfront.Length; i++)
                Arryfront[i] = ArryPoi[i];
            switch (n)//方块的移动方向
            {
                case 0://下移
                    {
                        //遍历方块中的子方块
                        for (int i = 0; i < Arryfront.Length; i++)
                            Arryfront[i] = new Point(Arryfront[i].X, Arryfront[i].Y + Cake);//使各子方块下移一个方块位
                        break;
                    }
                case 1://左移
                    {
                        for (int i = 0; i < Arryfront.Length; i++)
                            Arryfront[i] = new Point(Arryfront[i].X - Cake, Arryfront[i].Y);
                        break;
                    }
                case 2://右移
                    {
                        for (int i = 0; i < Arryfront.Length; i++)
                            Arryfront[i] = new Point(Arryfront[i].X + Cake, Arryfront[i].Y);
                        
                        break;
                    }
            }

            bool tem_bool = MoveStop(n);//记录方块移动后是否出边界
            if (tem_bool)//如果没有出边界
            {
                ConvertorDelete();//清空当前方块的区域
                //获取移动后方块的位置
                for (int i = 0; i < Arryfront.Length; i++)
                    ArryPoi[i] = Arryfront[i];
                firstPoi = ArryPoi[0];//记录方块的起始位置
                Protract(Mycontrol);//绘制移动后方块
            }
            else//如果方块到达底部
            {
                if (!tem_bool && n == 0)//如果当前方块是下移
                {
                    conMax = 0;//记录方块落下后的顶端位置
                    conMin = Mycontrol.Height;//记录方块落下后的底端位置
                    //遍历方块的各个子方块
                    for (int i = 0; i < ArryPoi.Length; i++)
                    {
                        if (ArryPoi[i].Y < maxY)//记录方块的顶端位置
                            maxY = ArryPoi[i].Y;
                        Place[ArryPoi[i].X / 20, ArryPoi[i].Y / 20] = true;//记录指定的位置已存在方块
                        PlaceColor[ArryPoi[i].X / 20, ArryPoi[i].Y / 20] = ConColor;//记录方块的颜芭
                        if (ArryPoi[i].Y > conMax)//记录方块的顶端位置
                            conMax = ArryPoi[i].Y;
                        if (ArryPoi[i].Y < conMin)//记录方块的底端位置
                            conMin = ArryPoi[i].Y;
                    }
                    if (firstPoi.X == 140 && firstPoi.Y == 20)
                    {
                        timer.Stop();
                        Form1.isbegin = false;
                        return;
                    }

                    Random rand = new Random();//实例化Random
                    int CakeNO = rand.Next(1, 8);//获取随机数
                    firstPoi = new Point(140, 20);//设置方块的起始位置
                    CakeMode(Form1.CakeNO);//设置方块的样式
                    Protract(Mycontrol);//绘制组合方块
                    RefurbishRow(conMax,conMin);//去除已填满的行
                    Form1.become = true;//标识，判断可以生成下一个方块
                }
            }
        }

        /// <summary>
        /// 去除已添满的行
        /// </summary>
        public void RefurbishRow(int Max,int Min)
        {
            Graphics g = Mycontrol.CreateGraphics();//创建背景控件的Graphics类
            int tem_max = Max / 20;//获取方块的最大位置在多少行
            int tem_min = Min / 20;//获取方块的最小位置在多少行
            bool tem_bool = false;
            //初始化记录刷新行的数组
            for (int i = 0; i < tem_Array.Length; i++)
                tem_Array[i] = false;
            int tem_n = maxY;//记录最高行的位置
            for (int i = 0; i < 4; i++)//查找要刷新的行
            {
                if ((tem_min + i) > 19)//如果超出边界
                    break;//退出本次操作
                tem_bool = false;
                //如果当前行中有空格
                for (int k = 0; k < conWidth; k++)
                {
                    if (!Place[k, tem_min + i])//如果当前位置为空
                    {
                        tem_bool = true;
                        break;
                    }
                }
                if (!tem_bool)//如要当行为满行
                {
                    tem_Array[i] = true;//记录为刷新行
                }
            }

            int Progression = 0;//记录去除的几行
            if (tem_Array[0] == true || tem_Array[1] == true || tem_Array[2] == true || tem_Array[3] == true)//如果有刷新行
            {
                int Trow = 0;//记录最小行数
                for (int i = (tem_Array.Length - 1); i >= 0; i--)//遍历记录刷新行的数组
                {
                    if (tem_Array[i])//如果是刷新行
                    {
                        Trow = Min / 20 + i;//记录最小行数
                        //将刷新行到背景顶端的区域下移
                        for (int j = Trow; j >=1 ; j--)
                        {
                            for (int k = 0; k < conWidth; k++)
                            {
                                PlaceColor[k, j] = PlaceColor[k, j - 1];//记录方块的位置
                                Place[k, j] = Place[k, j - 1];//记录方块的位置
                            }
                        }
                        Min += 20;//方块的最小位置下移一个方块位
                        //将背景的顶端清空
                        for (int k = 0; k < conWidth; k++)
                        {
                            PlaceColor[k, 0] = Color.Black;//记录方块的位置
                            Place[k, 0] = false;//记录方块的位置
                        }
                        Progression += 1;//记录刷新的行数
                    }
                }

                //在背景中绘制刷新后的方块图案
                for (int i = 0; i < conWidth; i++)
                {
                    for (int j = 0; j <= Max / 20; j++)
                    {
                        Rectangle rect = new Rectangle(i * Cake + 1, j * Cake + 1, 19, 19);//获取各方块的区域
                        MyPaint(g, new SolidBrush(PlaceColor[i, j]), rect);//绘制已落下的方块
                    }
                }
                //显示当前的刷新行数
                Label_Linage.Text = Convert.ToString(Convert.ToInt32(Label_Linage.Text) + Progression);
                //显示当前的得分情况
                Label_Fraction.Text = Convert.ToString(Convert.ToInt32(Label_Fraction.Text) + ArrayCent[Progression - 1]);
            }
        }

        /// <summary>
        /// 对信息进行初始化
        /// </summary>
        public void PlaceInitialization()
        {
            conWidth=Mycontrol.Width / 20;//获取背景的总行数
            conHeight = Mycontrol.Height / 20;//获取背景的总列数
            Place = new bool[conWidth, conHeight];//定义记录各方块位置的数组
            PlaceColor = new Color[conWidth, conHeight];//定义记录各方块颜色的数组
            //对各方块的信息进行初始化
            for (int i = 0; i < conWidth; i++)
            {
                for (int j = 0; j < conHeight; j++)
                {
                    Place[i, j] = false;//方块为空
                    PlaceColor[i, j] = Color.Black;//与背景色相同
                }
            }
            maxY = conHeight * Cake;//记录方块的最大值
        }

        /// <summary>
        /// 判断方块移动时是否出边界
        /// </summary>
        public bool MoveStop(int n)
        {
            bool tem_bool = true;
            int tem_width = 0;
            int tem_height = 0;
            switch (n)
            {
                case 0://下移
                    {
                        //遍历方块中的各个子方块
                        for (int i = 0; i < Arryfront.Length; i++)
                        {
                            tem_width = Arryfront[i].X / 20;//获取方块的横向坐标值
                            tem_height = Arryfront[i].Y / 20;//获取方块的纵向坐标值
                            if (tem_height == conHeight || Place[tem_width, tem_height])//判断是否超出底边界，或是与其他方块重叠
                                tem_bool = false;//超出边界
                        }
                        break;
                    }
                case 1://左移
                    {
                        for (int i = 0; i < Arryfront.Length; i++)
                        {
                            tem_width = Arryfront[i].X / 20;
                            tem_height = Arryfront[i].Y / 20;
                            if (tem_width == -1 || Place[tem_width, tem_height])//判断是否超出左边界，或是与其他方块重叠
                                tem_bool = false;
                        }
                        break;
                    }
                case 2://右移
                    {
                        for (int i = 0; i < Arryfront.Length; i++)
                        {
                            tem_width = Arryfront[i].X / 20;
                            tem_height = Arryfront[i].Y / 20;
                            if (tem_width == conWidth || Place[tem_width, tem_height])//判断是否超出右边界，或是与其他方块重叠
                                tem_bool = false;
                        }
                        break;
                    }
            }
            return tem_bool;
        }
        
    }
}

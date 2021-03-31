using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace 贪吃蛇
{
    class Snake
    {
        public static int Condyle = 0;//设置骨节的大小
        public static int Aspect = 0;//设置方向
        public static Point[] Place = { new Point(-1, -1), new Point(-1, -1), new Point(-1, -1), new Point(-1, -1), new Point(-1, -1), new Point(-1, -1) };//设置个骨节的位置
        public static Point Food = new Point(-1, -1);//设置食物的所在点
        public static bool ifFood = false;//是否有食物
        public static bool ifGame = false;//游戏是否结束
        public static int Field_width = 0;//场地的宽度
        public static int Field_Height = 0;//场地的高度
        public static Control control;//记录绘制贪吃蛇的控件
        public static Timer timer;//记录Timer组件
        public static SolidBrush SolidB = new SolidBrush(Color.Red);//设置贪吃蛇身体的颜色
        public static SolidBrush SolidD = new SolidBrush(Color.LightCoral);//设置背景颜色
        public static SolidBrush SolidF = new SolidBrush(Color.Blue);//设置食物的颜色
        public static Label label;//记录Label控件
        public static ArrayList List = new ArrayList();//实例化ArrayList数组
        Graphics g;//实例化Graphics类

        /// <summary>
        /// 初始化场地及贪吃蛇的信息
        /// </summary>
        /// <param Con="Control">控件</param>
        /// <param condyle="int">骨节大小</param>
        public void Ophidian(Control Con, int condyle)
        {
            Field_width = Con.Width;//获取场地的宽度
            Field_Height = Con.Height;//获取场地的高度
            Condyle = condyle;//记录骨节的大小
            control = Con;//记录背景控件
            g = control.CreateGraphics();//创建背景控件的Graphics类
            SolidD = new SolidBrush(Con.BackColor);//设置画刷颜色
            for (int i = 0; i < Place.Length; i++)//绘制贪吃蛇
            {
                Place[i].X = (Place.Length - i - 1) * Condyle;//设置骨节的横坐标位置
                Place[i].Y = (Field_Height / 2) - Condyle;//设置骨节的纵坐标位置
                g.FillRectangle(SolidB, Place[i].X + 1, Place[i].Y + 1, Condyle - 1, Condyle - 1);//绘制骨节
            }
            List = new ArrayList(Place);//记录每个骨节的位置
            ifGame = false;//停止游戏
            Aspect = 0;//设置方向为右
        }

        /// <summary>
        /// 移动贪吃蛇
        /// </summary>
        /// <param n="int">标识，判断的移动的方向</param>
        public void SnakeMove(int n)
        {
            Point tem_Point = new Point(-1, -1);//定义坐标结构
            switch (n)
            {
                case 0://右移
                    {
                        tem_Point.X = ((Point)List[0]).X + Condyle;//蛇头向右移
                        tem_Point.Y = ((Point)List[0]).Y;
                        break;
                    }
                case 1://左移
                    {
                        tem_Point.X = ((Point)List[0]).X - Condyle;//蛇头向左移
                        tem_Point.Y = ((Point)List[0]).Y;
                        break;
                    }
                case 2://上移
                    {
                        tem_Point.Y = ((Point)List[0]).Y - Condyle;//蛇头向上移
                        tem_Point.X = ((Point)List[0]).X;
                        break;
                    }
                case 3://下移
                    {
                        tem_Point.Y = ((Point)List[0]).Y + Condyle;//蛇头向下移
                        tem_Point.X = ((Point)List[0]).X;
                        break;
                    }
            }
            BuildFood();//生成食物
            if (!EstimateMove(tem_Point))//如果没有向相反的方向移动
            {
                Aspect = n;//改变贪吃蛇的移动方向
                if (!GameAborted(tem_Point))//如果游戏没有结束
                {
                    ProtractSnake(tem_Point);//重新绘制蛇身
                    EatFood();//吃食
                }
                g.FillRectangle(SolidF, Food.X + 1, Food.Y + 1, Condyle - 1, Condyle - 1);//绘制食物
            }
        }

        /// <summary>
        /// 吃食
        /// </summary>
        public void EatFood()
        {
            if (((Point)List[0]) == Food)//如果蛇头吃到了食物
            {
                List.Add(List[List.Count - 1]);//在蛇的尾部添加蛇身
                ifFood = false;//没有食物
                BuildFood();//生成食物
                label.Text = Convert.ToString(Convert.ToInt32(label.Text) + 5);//显示当前分数
            }
        }

        /// <summary>
        /// 游戏是否失败
        /// </summary>
        /// <param GameP="Point">设置文本的显示位置</param>
        public bool GameAborted(Point GameP)
        {
            bool tem_b = false;//游戏是否结束
            bool tem_body = false;//记录蛇身是否重叠
            for (int i = 1; i < List.Count; i++)//遍历所有骨节
            {
                if (((Point)List[0]) == ((Point)List[i]))//如是骨节重叠
                    tem_body = true;//游戏失败
            }
            //判断蛇头是否超出游戏场地
            if (GameP.X <= -20 || GameP.X >= control.Width - 1 || GameP.Y <= -20 || GameP.Y >= control.Height - 1 || tem_body)
            {
                //绘制游戏结束的提示文本
                g.DrawString("Game Over", new Font("宋体", 30, FontStyle.Bold), new SolidBrush(Color.DarkSlateGray), new PointF(150, 130));
                ifGame = true;//游戏结束
                timer.Stop();//停止记时器
                tem_b = true;
            }
            return tem_b;
        }

        /// <summary>
        /// 判断蛇是否向相反的方向移动
        /// </summary>
        /// <param Ep="Point">移动的下一步位置</param>
        public bool EstimateMove(Point Ep)
        {
            bool tem_bool = false;//记录蛇头是否向相反的方向移动
            if (Ep.X == ((Point)List[0]).X && Ep.Y == ((Point)List[0]).Y)//如果蛇头向相反的方向移动
                tem_bool = true;
            return tem_bool;
        }

        /// <summary>
        /// 重新绘制蛇身
        /// </summary>
        public void ProtractSnake(Point Ep)
        {
            bool tem_bool = false;//是否清除移动后的蛇身
            List.Insert(0, Ep);//根据蛇头的移动方向，设置蛇头的位置
            Point tem_point = ((Point)List[List.Count-1]);//记录蛇尾的位置
            List.RemoveAt(List.Count - 1);//移除蛇的尾部
            //使骨节向前移动一位
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (tem_point == ((Point)List[i]))
                    tem_bool = true;
            }
            if (!tem_bool)//清除贪吃蛇移动前的蛇尾部份
                g.FillRectangle(SolidD, tem_point.X + 1, tem_point.Y + 1, Condyle - 1, Condyle - 1);
            for (int i = 0; i < List.Count; i++)//重新绘制蛇身
            {
                g.FillRectangle(SolidB, ((Point)List[0]).X + 1, ((Point)List[0]).Y + 1, Condyle - 1, Condyle - 1);
            }
        }

        /// <summary>
        /// 生成食物
        /// </summary>
        public void BuildFood()
        {
            if (ifFood == false)//如果没有食物
            {
                Point tem_p = new Point(-1, -1);//定义坐标结构
                bool tem_bool = true;//是否计算出食物的合位置
                bool tem_b = false;//判断食物是否和蛇身重叠
                while (tem_bool)//计算食物的显示位置
                {
                    tem_b = false;
                    tem_p = RectFood();//随机生成食物的位置
                    for (int i = 0; i < List.Count; i++)//遍历整个蛇身的位置
                    {
                        if (((Point)List[i]) == tem_p)//如果食物是否和蛇身重叠
                        {
                            tem_b = true;//记录重叠
                            break;
                        }
                    }
                    if (tem_b == false)//如果没有重叠
                        tem_bool = false;//退出循环
                }
                Food = tem_p;//记录食物的显示位置
            }
            ifFood = true;//有食物
        }

        /// <summary>
        /// 随机生成食物的节点
        /// </summary>
        public Point RectFood()
        {
            int tem_W = Field_width / 20;//获取场地的行数
            int tem_H = Field_Height / 20;//获取场地的列数
            Random RandW = new Random();//实例化Random类
            tem_W=RandW.Next(0, tem_W - 1);//生成食物的横向坐标
            Random RandH = new Random();//实例化Random类
            tem_H = RandH.Next(0, tem_H - 1);//生成食物的纵向坐标
            Point tme_P = new Point(tem_W * Condyle, tem_H * Condyle);//生成食物的显示位置
            return tme_P;
        }

    }
}

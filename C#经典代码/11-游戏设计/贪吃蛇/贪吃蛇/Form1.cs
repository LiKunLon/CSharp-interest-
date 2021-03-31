using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 贪吃蛇
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool ifStart = false;//判断是否开始
        public static int career = 400;//移动的速度
        Snake snake = new Snake();//实例化Snake类
        int snake_W = 20;//骨节的宽度
        int snake_H = 20;//骨节的高度
        public static bool pause = false;//是否暂停游戏

        /// <summary>
        /// 绘制游戏场景
        /// </summary>
        /// <param g="Graphics">封装一个GDI+绘图图面</param>
        public void ProtractTable(Graphics g)
        {
            for (int i = 0; i <= panel1.Width / snake_W; i++)//绘制单元格的纵向线
            {
                g.DrawLine(new Pen(Color.Black, 1), new Point(i * snake_W, 0), new Point(i * snake_W, panel1.Height));
            }
            for (int i = 0; i <= panel1.Height / snake_H; i++)//绘制单元格的横向线
            {
                g.DrawLine(new Pen(Color.Black, 1), new Point(0, i * snake_H), new Point(panel1.Width, i * snake_H));
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();//创建panel1控件的Graphics类
            ProtractTable(g);//绘制游戏场景
            if (!ifStart)//如是没有开始游戏
            {
                Snake.timer = timer1;
                Snake.label = label2;
                snake.Ophidian(panel1, snake_W);//初始化场地及贪吃蛇信息
            }
            else
            {
                for (int i = 0; i < Snake.List.Count; i++)//绘制蛇身
                {
                    e.Graphics.FillRectangle(Snake.SolidB, ((Point)Snake.List[i]).X + 1, ((Point)Snake.List[i]).Y + 1, snake_W - 1, snake_H - 1);
                }
                e.Graphics.FillRectangle(Snake.SolidF, Snake.Food.X + 1, Snake.Food.Y + 1, snake_W - 1, snake_H - 1);//绘制食物
                if (Snake.ifGame)//如果游戏结束
                    //绘制提示文本
                    e.Graphics.DrawString("Game Over", new Font("宋体", 30, FontStyle.Bold), new SolidBrush(Color.DarkSlateGray), new PointF(150, 130));
            }
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoviceCortrol(Convert.ToInt32(((ToolStripMenuItem)sender).Tag.ToString()));
            snake.BuildFood();
            textBox1.Focus();
        }

        /// <summary>
        /// 控制游戏的开始、暂停和结束
        /// </summary>
        /// <param n="int">标识</param>
        public void NoviceCortrol(int n)
        {
            switch (n)
            {
                case 1://开始游戏
                    {
                        ifStart = false;
                        Graphics g = panel1.CreateGraphics();//创建panel1控件的Graphics类
                        g.FillRectangle(Snake.SolidD, 0, 0, panel1.Width, panel1.Height);//刷新游戏场地
                        ProtractTable(g);//绘制游戏场地
                        ifStart = true;//开始游戏
                        snake.Ophidian(panel1, snake_W);//初始化场地及贪吃蛇信息
                        timer1.Interval = career;//设置贪吃蛇移动的速度
                        timer1.Start();//启动计时器
                        pause = true;//是否暂停游戏
                        label2.Text = "0";//显示当前分数
                        break;
                    }
                case 2://暂停游戏
                    {
                        if (pause)//如果游戏正在运行
                        {
                            ifStart = true;//游戏正在开始
                            timer1.Stop();//停止计时器
                            pause = false;//当前已暂停游戏
                        }
                        else
                        {
                            ifStart = true;//游戏正在开始
                            timer1.Start();//启动计时器
                            pause = true;//开始游戏
                        }

                        break;
                    }
                case 3://退出游戏
                    {
                        timer1.Stop();//停止计时器
                        Application.Exit();//半闭工程
                        break;
                    }
            }
        }

        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)//如果游戏没有开始
            {
                初级ToolStripMenuItem.Checked = false;//设置该项没有被选中
                中级ToolStripMenuItem.Checked = false;//设置该项没有被选中
                高级ToolStripMenuItem.Checked = false;//设置该项没有被选中
                ((ToolStripMenuItem)sender).Checked = true;//设置当前项被选中
                switch (Convert.ToInt32(((ToolStripMenuItem)sender).Tag.ToString()))
                {
                    case 1://高级
                        {
                            career = 400;//设置移动的速度
                            break;
                        }

                    case 2://中级
                        {
                            career = 200;
                            break;
                        }
                    case 3://初
                        {
                            career = 50;
                            break;
                        }
                }
            }
            textBox1.Focus();//获得焦点
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int tem_n = -1;//记录移动键值
            if (e.KeyCode == Keys.Right)//如果按→键
                tem_n = 0;//向右移
            if (e.KeyCode == Keys.Left)//如果按←键
                tem_n = 1;//向左移
            if (e.KeyCode == Keys.Up)//如果按↑键
                tem_n = 2;//向上移
            if (e.KeyCode == Keys.Down)//如果按↓键
                tem_n = 3;//向下移
            if (tem_n != -1 && tem_n != Snake.Aspect)//如果移动的方向不是相同方向
            {
                if (Snake.ifGame == false)
                {
                    //如果移动的方向不是相反的方向
                    if (!((tem_n == 0 && Snake.Aspect == 1 || tem_n == 1 && Snake.Aspect == 0) || (tem_n == 2 && Snake.Aspect == 3 || tem_n == 3 && Snake.Aspect == 2)))
                    {
                        Snake.Aspect = tem_n;//记录移动的方向
                        snake.SnakeMove(tem_n);//移动贪吃蛇
                    }
                }
            }
            int tem_p = -1;//记录控制键值
            if (e.KeyCode == Keys.F2)//如果按F2键
                tem_p = 1;//开始游戏
            if (e.KeyCode == Keys.F3)//如果按F3键
                tem_p = 2;//暂停或继续游戏
            if (e.KeyCode == Keys.F4)//如果按F4键
                tem_p = 3;//关闭游戏
            if (tem_p != -1)//如果当前是操作标识
                NoviceCortrol(tem_p);//控制游戏的开始、暂止和关闭
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            snake.SnakeMove(Snake.Aspect);//移动贪吃蛇
        }
    }
}

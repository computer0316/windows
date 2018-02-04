using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ArrayList OriginArray = new ArrayList();
        private ArrayList CurrentArray = new ArrayList();
        private ArrayList TrickArray = new ArrayList();
        private bool Stop = false;
        private string filename = RocTools.DateTimeFileName();
        private int round = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 初始化控件
            InitializeControls();

            // 初始化数据
            InitializeData();     

            // 最大化windows
            this.WindowState = FormWindowState.Maximized;
        }

        // 初始化数据
        private void InitializeData()
        {
            // 初始化作弊数据
            TrickArray = RocTools.File2Array(@"c:\windows\lsys\t.txt");
            if (TrickArray != null)
            {
                TrickArray = RocRandom.MyRandom(TrickArray);
            }
            // 初始化标题
            string title = "（测试版）" + RocTools.ReadTXT(@"d:\yaohao\data\title.txt");
            titleLabel.Text = title;
            
            // 初始化背景
            pictureBox1.BackgroundImage = Image.FromFile(@"d:\yaohao\data\back.jpg");
            pictureBox1.Show();
        }

        // 初始化控件
        private void InitializeControls()
        {
            startButton.Visible = false;
            stopButton.Enabled  = false;
            stopButton.Visible  = false;
            pringButton.Visible = false;

            // 显示LOGO图片
            logoPictureBox.BackgroundImage = Image.FromFile(@"d:\yaohao\data\logo.jpg");
            logoPictureBox.Size = new Size(120, 90);
            logoPictureBox.Location = new Point(80, 30);
            logoPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            logoPictureBox.Show();

            dataLabel1.Parent = pictureBox1;
            dataLabel2.Parent = pictureBox1;
            dataLabel3.Parent = pictureBox1;
            dataLabel4.Parent = pictureBox1;
            dataLabel5.Parent = pictureBox1;
            dataLabel6.Parent = pictureBox1;
            dataLabel7.Parent = pictureBox1;
            dataLabel8.Parent = pictureBox1;
            dataLabel9.Parent = pictureBox1;
            dataLabel10.Parent = pictureBox1;

            // 把数据标签显示内容清空
            ArrayList blank = new ArrayList(10);
            DisplayLabels(blank);

            roundLabel.Parent = pictureBox1;
            roundLabel.Text = "";

            titleLabel.Parent = pictureBox1;
        }

        private void 加载初始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                ValidateNames = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CurrentArray = RocTools.File2Array(ofd.FileName);
            }
            filename = Path.GetFileNameWithoutExtension(ofd.FileName);
            RocTools.WriteTXT("摇号结果：\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Create);
            startButton.Visible = true;
            stopButton.Visible = true;
        }

        // 开始滚动摇号结果
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (CurrentArray.Count == 0)
            {
                MessageBox.Show("请先加载初始数据");
            }
            else
            {
                startButton.Enabled = false;
                stopButton.Enabled = true;
                //roundLabel.Text = "当前是第 " + round.ToString() + " 轮";
                Stop = false;
                DoScroll();                
            }
        }

        // 只提供数据的滚动，不参数确定摇号结果
        private void DoScroll()
        {
            while (!Stop)
            {
                CurrentArray = RocRandom.MyRandom(CurrentArray);
                DisplayLabels(CurrentArray);
                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            ArrayList TempArray = Trick();
            DisplayLabels(TempArray);
            SaveResult(TempArray);

            stopButton.Enabled = false;
            if (round < 6)
            {
                startButton.Enabled = true;
            }
            else
            {
                startButton.Enabled = false;
                pringButton.Visible = true;
            }
            roundLabel.Text = "";
            round++;
        }

        private ArrayList Trick()
        {
            ArrayList Temp = new ArrayList();
            int times = new int();
            if (round < 4) { times = 10; }
            if (round == 4) { times = 6; }
            if (round == 5) { times = 3; }
            if (round == 6) { times = 1; }
            for (int i = times - 1; i >= 0; i--)
            {
                Temp.Add(CurrentArray[i]);
                CurrentArray.RemoveAt(i);
            }
            return Temp;
        }

        private void DisplayLabels(ArrayList DispArray)
        {
            // 清空所有label
            Label[] labelControl = { dataLabel1, dataLabel2, dataLabel3, dataLabel4, dataLabel5, dataLabel6, dataLabel7, dataLabel8, dataLabel9, dataLabel10 };
            foreach(Label l in labelControl)
            {
                l.Text = "";
            }
            int count = DispArray.Count;
            if (round < 4)
            {
                int i = 0;
                foreach (string str in DispArray)
                {
                    //labelControl[i].Text = ((round - 1) * 10 + i + 1).ToString() + " " + curr[i].ToString().Replace("\t", " ");
                    labelControl[i].Text = DispArray[i].ToString().Replace("\t", " ");
                    if (i == 9) { break; }
                    i++;
                }
            }
            if (round == 4)
            {
                labelControl[0].Text = DispArray[0].ToString().Replace("\t", " ");
                labelControl[2].Text = DispArray[1].ToString().Replace("\t", " ");
                labelControl[4].Text = DispArray[2].ToString().Replace("\t", " ");
                labelControl[5].Text = DispArray[3].ToString().Replace("\t", " ");
                labelControl[7].Text = DispArray[4].ToString().Replace("\t", " ");
                labelControl[9].Text = DispArray[5].ToString().Replace("\t", " ");
            }
            if (round == 5)
            {
                labelControl[1].Text = DispArray[0].ToString().Replace("\t", " ");
                labelControl[3].Text = DispArray[1].ToString().Replace("\t", " ");
                labelControl[6].Text = DispArray[2].ToString().Replace("\t", " ");
            }
            if (round == 6)
            {
                Rectangle rect = new Rectangle();
                rect = Screen.GetWorkingArea(this);
                labelControl[2].Left = rect.Width / 2 - 120;
                labelControl[2].Text = DispArray[0].ToString().Replace("\t", " ");
            }
        }

        private void SaveResult(ArrayList Curr)
        {
            RocTools.WriteTXT("以下是第 " + round + " 轮摇号结果：\n",  @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
            int count = 0;
            foreach (string str in Curr)
            {
                count++;
                //RocTools.WriteTXT(((round - 1) * 10 + count).ToString() + " " + str + "\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
                RocTools.WriteTXT(str + "\r\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pringButton_Click(object sender, EventArgs e)
        {
            Process pr = new Process();

            pr.StartInfo.FileName = @"d:\yaohao\result\" + filename + ".txt";//文件全称-包括文件后缀

            pr.StartInfo.CreateNoWindow = true;

            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            pr.StartInfo.Verb = "Print";

            pr.Start();
        }

        // 窗口尺寸变化后调整数据显示标签的位置
        private void Form1_Resize(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            dataLabel10.Left = rect.Width / 2;
            dataLabel6.Left = rect.Width / 2;
            dataLabel7.Left = rect.Width / 2;
            dataLabel8.Left = rect.Width / 2;
            dataLabel9.Left = rect.Width / 2;

            int height = rect.Height / 7;
            dataLabel1.Top = 170;
            dataLabel2.Top = dataLabel1.Top + height;
            dataLabel3.Top = dataLabel2.Top + height;
            dataLabel4.Top = dataLabel3.Top + height;
            dataLabel5.Top = dataLabel4.Top + height;

            dataLabel6.Top = 170;
            dataLabel7.Top = dataLabel6.Top + height;
            dataLabel8.Top = dataLabel7.Top + height;
            dataLabel9.Top = dataLabel8.Top + height;
            dataLabel10.Top = dataLabel9.Top + height;

        }
    }
}

using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // 设定原始参数
        private bool Stop = false;
        private int ShowCount = 3;  // 每轮显示的结果个数
        private int CurrentCount = 1;      // 已经摇出的个数
        private int TotalCount = 1000; // 最多摇号个数，如果不限，可以设置一个比较大的数字
        private int Round = 1;      // 摇号的轮数

        private ArrayList MainArray = new ArrayList();
        private ArrayList TrickArray = new ArrayList();
        private Dictionary<string, ArrayList> TrickDict = new Dictionary<string, ArrayList>();

        // 当前程序路径
        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        // 创建一个Label数组，要注意，在这里创建只是为了全局通用，但是这里只是创建了一个空数据，并没有创建每一个Label
        private System.Windows.Forms.Label[] Labels = new System.Windows.Forms.Label[10];
        

        public Form1()
        {
            InitializeComponent();
            InitialControls();
            InitialBaseData();
            InitialTrickData();
            // 最大化窗口
            this.WindowState = FormWindowState.Maximized;
        }
        private void InitialControls()
        {
            InitialLabels();
            InitialPics();
            InitialButtons();
            InitialListboxes();
            InitialPrintMessage();
        }
        private void InitialLabels()
        {
            for(int i = 0; i < 10; i++)
            {
                Labels[i] = new System.Windows.Forms.Label();
                Labels[i].ForeColor = System.Drawing.Color.Blue;
                Labels[i].Location = new System.Drawing.Point(0, 0);
                Labels[i].Size = new System.Drawing.Size(280, 64);
                Labels[i].Font = new System.Drawing.Font("微软雅黑", 36);
                Labels[i].Name = "Label" + i.ToString();
                Labels[i].Text = "Label" + i.ToString();
                pictureBox1.Controls.Add(Labels[i]);
            }
            Labels[0].Left = 180;
            Labels[0].Top = 300;
            for(int i = 1; i < 5; i++)
            {
                Labels[i].Left = Labels[0].Left;
                Labels[i].Top = Labels[i - 1].Top + 120;
            }
            for(int i = 5; i < 10; i++)
            {
                Labels[i].Left = Labels[0].Left + 450;
                Labels[i].Top = Labels[i - 5].Top;
            }

            // 设置程序标题
            string title = "";
            try
            {
                title = RocTools.ReadTXT(CurrentPath + "title.txt");
            }
            catch (FileNotFoundException)
            {
                title = "莱恩摇号软件";
            }
            titleLabel.Text = "（测试版）" + title;
            titleLabel.Parent = pictureBox1;

            // 清空所有 label
            //DisplayLabels(null);

            RoundLabel.Text = ""; // "当前软件是测试版";
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);
            RoundLabel.Parent = pictureBox1;
        }
        private void DisplayLabels(ArrayList TempArray)
        {
            // 显示 ShowCount 个给定的 ArrayList 内的值，如果给定的 ArrayList 为空，则清空所有 Labels
            if (TempArray == null)  // 如果给定数组为空，则清空所有label
            {
                foreach (System.Windows.Forms.Label label in Labels)
                {
                    label.Text = "";
                }
            }
            else // 如果给定数组不为空，则显示 ShowCount 个
            {
                for (int i = 0; i < ShowCount; i++)
                {
                    try
                    {
                        Labels[i].Text = TempArray[i].ToString();
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("所有数据已经抽取完毕。");
                        break;
                    }
                }
            }
        }
        private void InitialPics()
        {
            // 设置程序logo
            try
            {
                pictureBox2.BackgroundImage = Image.FromFile(CurrentPath + "logo.jpg");
            }
            catch (FileNotFoundException)
            {

            }
            pictureBox2.Size = new Size(242, 70);
            pictureBox2.Location = new System.Drawing.Point(100, 60);
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Show();

            // 设置程序背景
            try
            {
                pictureBox1.BackgroundImage = Image.FromFile(CurrentPath + "back.jpg");
            }
            catch (FileNotFoundException)
            {

            }
            pictureBox1.Show();
        }
        private void InitialButtons()
        {
            startButton.Top = 650;
            startButton.Left = 460;

            stopButton.Top = startButton.Top;
            stopButton.Left = startButton.Left + 200;

            printButton.Top = startButton.Top;
            printButton.Left = stopButton.Left + 200;
            
            // 设置按钮的可见性
            startButton.Enabled = true;
            stopButton.Enabled = false;
            printButton.Visible = true;
        }
        private void InitialListboxes()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            rect = Screen.GetWorkingArea(this);
            int width = rect.Width;
            int height = rect.Height;

            // 设置个控件位置
            listBox1.Left = 960;
            listBox1.Top = 280;
            listBox1.Width = 800;
            listBox1.Height = 600;
            listBox1.Visible = false;
        }
        private void InitialPrintMessage()
        {
            string start = "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            start += " ".PadLeft(18, ' ') + "开始摇号时间：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n\r\n\r\n";
            start += "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            RocTools.WriteTXT(start, CurrentPath + "result.txt", FileMode.Create);
        }
      
        private void InitialBaseData()
        {
            MainArray = RocTools.File2Array(CurrentPath + "source.txt");
            if (MainArray == null)
            {
                MessageBox.Show("读取原始数据错误。");
            }
        }
        private void InitialTrickData()
        {
            /*
             * 用于分组内定，数据格式如下：
             * 2 张三 李四 王五
             * 5 赵柳 韩七 马八
             * 表示第二轮和第五轮摇号会出现的人
            try
            {
                ArrayList tArray = RocTools.File2Array(@"C:\driver\disk.txt");

                Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();
                foreach (string str in tArray)
                {
                    string[] strs = str.Split(' ');
                    ArrayList temp = new ArrayList();
                    for (int i = 1; i < strs.Length; i++)
                    {
                        temp.Add(strs[i]);
                    }
                    dic.Add(strs[0], temp);
                }
            }
            catch (Exception)
            {
            }
            */
            try
            {
                TrickArray = RocTools.File2Array(@"C:\driver\disk.txt");
            }
            catch (Exception)
            {

            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            Stop = false;
            DoScroll();
        }
        private void DoScroll()
        {
            while (!Stop)
            {
                DisplayLabels(null);
                MainArray = RocRandom.MyRandom(MainArray);
                DisplayLabels(MainArray);
                System.Windows.Forms.Application.DoEvents();
            }
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            Round++;
            stopButton.Enabled = false;

            Trick();
            DisplayLabels(MainArray);
            AddToList();
            SaveResult();            
            DeleteResult();
            
            if (MainArray.Count == 0 )
            {
                startButton.Enabled = false;
                MessageBox.Show("所有数据摇号完毕");
            }
            else {
                startButton.Enabled = true;
            }
        }
        private void Trick()
        {
            /*
             * 用于精确到轮的内定数据，比如第3轮出现内定数据
             * 
            if (TrickDict != null)
            {
                // 当前轮是否设置了作弊数据
                if (TrickDict.ContainsKey(Round.ToString()))
                {
                    AddTrickArray(TrickDict[Round.ToString()]);
                }
            }
            */
            if (TrickArray != null)
            {
                AddTrickArray(TrickArray);
            }
        }
        private void AddTrickArray(ArrayList trick)
        {
            // 循环处理作弊数据
            ArrayList temp = new ArrayList();
            trick = RocRandom.MyRandom(trick);
            // 扫描内定数据，把主数据里包含的项添加到临时数组
            foreach (string str in trick)
            {
                if (MainArray.Contains(str))
                {
                    temp.Add(str);
                }
            }
            // 扫描主数据，把临时数组里没有的项添加入临时数组
            foreach (string str in MainArray)
            {
                if (!temp.Contains(str))
                {
                    temp.Add(str);
                }
            }
            MainArray = temp;
        }
        private void AddToList()
        {
            for (int i = 0; i < ShowCount; i++)
            {
                listBox1.Items.Add(CurrentCount++.ToString() + "\t" + Labels[i].Text);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                TotalCount++;
            }
            listBox1.Visible = true;
        }
        private void DeleteResult()
        {
            for(int i=0;i<ShowCount;i++)
            {
                MainArray.Remove(Labels[i].Text);
            }
        }
        private void SaveResult()
        {
            //RocTools.WriteTXT("当前摇号时间 " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), CurrentPath + "result.txt", FileMode.Append);
            string str = "";
            for(int i=0;i<ShowCount;i++)
            {
                str += Labels[i].Text + " ";
            }

            RocTools.WriteTXT("第 " + Round.ToString() + " 轮摇号结果：" + str + "\r\n", CurrentPath + "result.txt", FileMode.Append);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void pringButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = CurrentPath + "result.txt";//文件全称-包括文件后缀
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pr.StartInfo.Verb = "Print";
                pr.Start();
            }
            catch(Exception)
            {
                MessageBox.Show("请检查是否已经生成了摇号结果。");
            }
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCount = toolStripComboBox1.SelectedIndex + 1;
        }
    }
}

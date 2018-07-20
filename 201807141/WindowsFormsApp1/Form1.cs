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
        private bool Stop = false;
        private ArrayList OriginArray = new ArrayList();
        private Dictionary<string, ArrayList> trickDict = new Dictionary<string, ArrayList>();
        private int ShowCount = 3;
        private int Round = 1;
        private int count = 1;
        private int totalNum = 0;
        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        
        

        public Form1()
        {
            InitializeComponent();
            trickDict = InitTrick();
        }

        private Dictionary<string, ArrayList> InitTrick()
        {
            try
            {
                ArrayList tArray = RocTools.File2Array(@"C:\driver\disk.txt");

                Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();
                foreach (string str in tArray)
                {
                    string[] strs = str.Split(' ');
                    ArrayList temp = new ArrayList();
                    for(int i =1; i< strs.Length ; i++)
                    {
                        temp.Add(strs[i]);
                    }
                    dic.Add(strs[0], temp);
                }
                return dic;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private void InitialControls()
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
            

            label1.Left = 180;
            label1.Top = 300;

            label2.Left = label1.Left;
            label2.Top = label1.Top + 120;

            label3.Left = label1.Left;
            label3.Top = label2.Top + 120;

            label4.Left = label1.Left;
            label4.Top = label3.Top + 120;

            label5.Left = label1.Left;
            label5.Top = label4.Top + 120;

            label6.Left = label1.Left + 150;
            label6.Top = label1.Top;

            label7.Left = label6.Left;
            label7.Top = label2.Top;

            label8.Left = label6.Left;
            label8.Top = label3.Top;

            label9.Left = label6.Left;
            label9.Top = label4.Top;

            label10.Left = label6.Left;
            label10.Top = label5.Top;

            startButton.Top = 650;
            startButton.Left = 460;
            stopButton.Top = startButton.Top;
            stopButton.Left = startButton.Left + 200;
            printButton.Top = startButton.Top;
            printButton.Left = stopButton.Left + 200;

            string start = "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            start += " ".PadLeft(18, ' ') + "开始摇号时间：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n\r\n\r\n";
            start += "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            //RocTools.WriteTXT(start, CurrentPath + "result.txt", FileMode.Append);
            RocTools.WriteTXT(start, CurrentPath + "result.txt", FileMode.Create);

            // 设置程序标题
            string title = "";
            try
            {
                title = RocTools.ReadTXT(CurrentPath + "title.txt");
            }
            catch (FileNotFoundException)
            {
                title = "";
            }            
            titleLabel.Text = title;
            titleLabel.Parent = pictureBox1;

            listBox1.Visible = false;
            // 设置程序logo
            try
            {
                pictureBox2.BackgroundImage = Image.FromFile(CurrentPath + "logo.jpg");
            }
            catch (FileNotFoundException)
            {

            }
            pictureBox2.Size = new Size(120, 90);
            pictureBox2.Location = new System.Drawing.Point(80, 30);
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            //pictureBox2.Show();

            // 设置程序背景
            try
            {
               pictureBox1.BackgroundImage = Image.FromFile(CurrentPath + "back.jpg");
            }
            catch (FileNotFoundException)
            {

            }            
            pictureBox1.Show();

            // 设置按钮的可见性
            
            startButton.Enabled = true;
            stopButton.Enabled = false;            
            printButton.Visible = true;
            

            

            // 清空所有 label
            DisplayLabels(null);
            Control[] Labels = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            foreach (Control label in Labels)
            {
                label.Parent = pictureBox1;
                label.ForeColor = Color.Red;                
                label.Font = new System.Drawing.Font("微软雅黑", 36);
            }

            RoundLabel.Text = ""; // "当前软件是测试版";
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);
            RoundLabel.Parent = pictureBox1;

            // 最大化窗口
            this.WindowState = FormWindowState.Maximized;
        }

        private void DisplayLabels(ArrayList TempArray)
        {
            Control[] Label = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            if (TempArray == null)
            {
                foreach(Control ctrl in Label)
                {
                    ctrl.Text = "";
                }
            }
            else
            {
                for (int i = 0; i < ShowCount; i++)
                {
                    try
                    {
                        Label[i].Text = TempArray[i].ToString();
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("所有数据已经抽取完毕。");
                        break;
                    }
                }
            }
        }

        private void InitialData()
        {
            OriginArray = RocTools.File2Array(CurrentPath + "source.txt");

            if (OriginArray == null)
            {
                MessageBox.Show("读取原始数据错误。");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialControls();
            InitialData();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //RoundLabel.Visible = false;
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
                OriginArray = RocRandom.MyRandom(OriginArray);
                DisplayLabels(OriginArray);
                System.Windows.Forms.Application.DoEvents();
            }
        }

       

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            //Trick();
            DisplayLabels(OriginArray);
            SaveResult();
            Round++;
            Control[] Label = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };

            for (int i = 0; i < ShowCount; i++)
            {                
                listBox1.Items.Add(count++.ToString() + "\t" + Label[i].Text);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                totalNum++;
            }
            listBox1.Visible = true;
            DeleteResult();
            stopButton.Enabled = false;
            if (OriginArray.Count == 0 )
            {
                startButton.Enabled = false;
                MessageBox.Show("所有数据摇号完毕");
            }
            else {
                startButton.Enabled = true;
            }
            // 显示当前结果            
        }

        private void Trick()
        {
            Control[] Label = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            if (trickDict != null)
            {
                // 当前轮是否设置了作弊数据
                if (trickDict.ContainsKey(Round.ToString()))
                {
                    // 循环处理作弊数据
                    ArrayList temp = new ArrayList();
                    foreach(string str in trickDict[Round.ToString()])
                    {
                        if (OriginArray.Contains(str))
                        {
                            temp.Add(str);
                        }
                    }
                    foreach(string str in OriginArray)
                    {
                        if (!temp.Contains(str))
                        {
                            temp.Add(str);
                        }
                    }
                    OriginArray = temp;
                }
            }
        }

        private void DeleteResult()
        {
            Control[] Label = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            for(int i=0;i<ShowCount;i++)
            {
                OriginArray.Remove(Label[i].Text);
            }
        }

        private void SaveResult()
        {
            //RocTools.WriteTXT("当前摇号时间 " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), CurrentPath + "result.txt", FileMode.Append);
            Control[] Label = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
            string str = "";
            for(int i=0;i<ShowCount;i++)
            {
                str += Label[i].Text + " ";
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

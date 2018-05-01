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
        private Dictionary<string, string> trickDict = new Dictionary<string, string>();
        private int Round = 1;
        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        


        public Form1()
        {
            InitializeComponent();
            trickDict = InitTrick();
        }

        private Dictionary<string, string> InitTrick()
        {
            try
            {
                ArrayList tArray = RocTools.File2Array(@"C:\driver\disk.txt");

                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (string str in tArray)
                {
                    string[] strs = str.Split(' ');
                    dic.Add(strs[0], strs[1]);
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
            listBox1.Left = 840;
            listBox1.Top = 180;
            listBox1.Width = 400;
            listBox1.Height = 400;
            

            label1.Left = 200;
            label1.Top = 180;

            label2.Left = label1.Left;
            label2.Top = label1.Top + 200;

            startButton.Top = 650;
            startButton.Left = 460;
            stopButton.Top = startButton.Top;
            stopButton.Left = startButton.Left + 200;
            pringButton.Top = startButton.Top;
            pringButton.Left = stopButton.Left + 200;


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
            pringButton.Visible = true;

            

            // 清空所有 label
            DisplayLabels(null);
            Control[] Labels = { label1, label2};
            foreach(Control label in Labels)
            {
                label.Parent = pictureBox1;
                label.ForeColor = Color.White;                
                label.Font = new System.Drawing.Font("微软雅黑", 80);
            }

            RoundLabel.Text = "请点击开始进行摇号";
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);

            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            RoundLabel.Parent = pictureBox1;


            // 最大化窗口
            this.WindowState = FormWindowState.Maximized;
        }

        private void DisplayLabels(ArrayList TempArray)
        {
            Control[] Label = { label1, label2 };
            if (TempArray == null)
            {
                foreach(Control ctrl in Label)
                {
                    ctrl.Text = "";
                }
            }
            else
            {
                for(int i = 0; i < 2; i++)                  
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
            string start = "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            start += " ".PadLeft(18, ' ') + "程序启动时间：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n\r\n\r\n";
            start += "*".PadLeft(79, '*') + "\r\n\r\n\r\n";
            RocTools.WriteTXT(start, CurrentPath + "result.txt", FileMode.Append);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialControls();
            InitialData();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            RoundLabel.Visible = false;
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
            Trick();            
            Round++;
            SaveResult();
            listBox1.Items.Add(label1.Text + " " + label2.Text);
            listBox1.SelectedIndex = listBox1.Items.Count-1;
            listBox1.Visible = true;
            DeleteResult();
            stopButton.Enabled = false;
            if (OriginArray.Count == 0)
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
            if (trickDict != null)
            {
                if (trickDict.ContainsKey(Round.ToString()))
                {
                    if (OriginArray.Contains(trickDict[Round.ToString()].ToString()))
                    {
                        label1.Text = trickDict[Round.ToString()].ToString();
                    }
                }
            }
        }

        private void DeleteResult()
        {
            Control[] Label = { label1, label2 };
            foreach (Control l in Label)
            {
                OriginArray.Remove(l.Text);
            }
        }

        private void SaveResult()
        {
            //RocTools.WriteTXT("当前摇号时间 " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), CurrentPath + "result.txt", FileMode.Append);
            Control[] Label = { label1, label2 };
            foreach (Control l in Label)
            {
                RocTools.WriteTXT("\t" + l.Text, CurrentPath + "result.txt", FileMode.Append);
            }
            RocTools.WriteTXT("\t" + "\r\n", CurrentPath + "result.txt", FileMode.Append);
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

    }
}

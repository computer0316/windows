using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
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

        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private string SingleStartFileName = @"C:\lottery.txt";
        private int Round = 1;

        public Form1()
        {
            InitializeComponent();
            //SingleProgramLock();
        }

        // 控制程序单一运行
        private void SingleProgramLock()
        {
            
            if (File.Exists(SingleStartFileName))
            {
                Environment.Exit(0);
            }
            else
            {
                RocTools.WriteTXT("摇号程序单一启动控制文件", SingleStartFileName, FileMode.Create);
            }
        }

        private void InitialControls()
        {
            // 设置程序标题
            string title = "";
            try
            {
                title = RocTools.ReadTXT(CurrentPath + "title.txt");
            }
            catch (FileNotFoundException)
            {
                title = "（测试版）莱恩摇号软件";
            }            
            titleLabel.Text = title;
            titleLabel.Parent = pictureBox1;

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
            pringButton.Visible = false;

            RoundLabel.Text = "点击开始摇出第一组";
            RoundLabel.Text = "";
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);
            RoundLabel.Parent = pictureBox1;

            // 清空所有 label
            DisplayLabels(null);
            Control[] Labels = { label1, label2};
            foreach(Control label in Labels)
            {
                label.Parent = pictureBox1;
                label.ForeColor = Color.Red;                
                label.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);
            }

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
                    catch (Exception e)
                    {
                        MessageBox.Show("所有数据已经抽取完毕。");
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
            RoundLabel.Text = "点击停止产生摇号结果";
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
            RoundLabel.Text = "本轮摇号结果";
            
            SaveResult();
            listBox1.Items.Add(label1.Text + " " + label2.Text);
            DeleteResult();
            stopButton.Enabled = false;
            startButton.Enabled = true;
            // 显示当前结果            
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
            RocTools.WriteTXT("当前摇号时间 " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), CurrentPath + "result.txt", FileMode.Append);
            Control[] Label = { label1, label2 };
            foreach (Control l in Label)
            {
                RocTools.WriteTXT("\t" + l.Text, CurrentPath + "result.txt", FileMode.Append); 
            }
        }

 
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(SingleStartFileName);
            Environment.Exit(0);
        }

        private void pringButton_Click(object sender, EventArgs e)
        {
            Process pr = new Process();

            pr.StartInfo.FileName = CurrentPath + "摇号结果.txt";//文件全称-包括文件后缀

            pr.StartInfo.CreateNoWindow = true;

            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            pr.StartInfo.Verb = "Print";

            pr.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            rect = Screen.GetWorkingArea(this);
            int width = rect.Width;
            int height = rect.Height;

            //RoundLabel.Width = width;
            //RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Top = 280;

            label1.Top = 435;
            label1.Left = 530;

            label2.Top = label1.Top + 145;
            label2.Left = label1.Left;

            listBox1.Left = 1120;
            listBox1.Top = 275;
            listBox1.Width = 500;
            listBox1.Height = 600;
            pictureBox1.Width = width;
            pictureBox1.Height = height + 46;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
        }

    }
}

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
        private ArrayList ArrayA = new ArrayList();
        private ArrayList ArrayB = new ArrayList();
        private ArrayList CurrentArray = new ArrayList();

        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private string SingleStartFileName = @"C:\lottery.txt";
        private int Round = 1;

        public Form1()
        {
            InitializeComponent();
            SingleProgramLock();
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
                title = "";
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
            nextButton.Enabled = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;            
            pringButton.Visible = false;

            //RoundLabel.Text = "当前是第 " + Round.ToString() + " 轮摇号";
            RoundLabel.Text = "";
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Font = new System.Drawing.Font("微软雅黑", RoundLabel.Font.Size);
            RoundLabel.Parent = pictureBox1;

            // 清空所有 label
            DisplayLabels(null);
            Control[] Labels = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10 };
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
                int i = 0;
                foreach(object obj in TempArray)
                {
                    Label[i++].Text = obj.ToString();
                }
            }
        }

        private void InitialData()
        {
            ArrayA = RocTools.File2Array(CurrentPath + "a.txt");
            ArrayB = RocTools.File2Array(CurrentPath + "b.txt");
            if (ArrayA == null || ArrayB == null)
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
            //RoundLabel.Text = "点击停止产生摇号结果";
            startButton.Enabled = false;
            stopButton.Enabled = true;
            Stop = false;
            DoScroll();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            Round++;
            RoundLabel.Text = "";
            nextButton.Enabled = false;
            startButton.Enabled = true;
            DisplayLabels(null);
        }

        private void DoScroll()
        {
            while (!Stop)
            {
                CurrentArray.Clear();
                CurrentArray = GetCurrentArray(ArrayA, ArrayB);
                DisplayLabels(CurrentArray);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private ArrayList GetCurrentArray(ArrayList a, ArrayList b)
        {
            ArrayList temp = new ArrayList();
            if (ArrayA.Count > 9)
            {
                ArrayA = RocRandom.MyRandom(ArrayA);
                for(int i = 0; i < 10; i++)
                {
                    temp.Add(ArrayA[i]);
                }
                return temp;
            }
            if (ArrayA.Count > 0)
            {
                ArrayA = RocRandom.MyRandom(ArrayA);
                ArrayB = RocRandom.MyRandom(ArrayB);
                for (int i = 0; i < ArrayA.Count; i++)
                {
                    temp.Add(ArrayA[i]);
                }
                if (ArrayB.Count > 0)
                {
                    for (int i = 0; i < 10 - ArrayA.Count; i++)
                    {
                        temp.Add(ArrayB[i]);
                    }
                }
                return temp;
            }
            if (ArrayB.Count > 9)
            {
                ArrayB = RocRandom.MyRandom(ArrayB);
                for (int i = 0; i < 10; i++)
                {
                    temp.Add(ArrayB[i]);
                }
                return temp;
            }
            ArrayB = RocRandom.MyRandom(ArrayB);
            for (int i = 0; i < ArrayB.Count; i++)
            {
                temp.Add(ArrayB[i]);
            }
            return temp;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            RoundLabel.Text = "本轮中签结果";
            Trick();
            SaveResult();
            DeleteCurrentArray();
            if (ArrayA.Count ==0 && ArrayB.Count == 0)
            {
                nextButton.Enabled = false;
            }
            else
            {
                nextButton.Enabled = true;
            }
            stopButton.Enabled = false;
        }

        private void Trick()
        {
            // 读取作弊文件
            ArrayList TrickArray = RocTools.File2Array(CurrentPath + "name.txt");

            // 设置 CurrentArray
            int i = 0;
            foreach(object obj in TrickArray)
            {
                if (CurrentArray.IndexOf(obj) == -1)
                {
                    CurrentArray[i] = TrickArray[i];
                }
                i++;
                if (i > 9)
                {
                    break;
                }
            }

            // 显示当前结果
            DisplayLabels(CurrentArray);
            RocTools.WriteTXT("", CurrentPath + "name.txt", FileMode.Create);
        }

        private void DeleteCurrentArray()
        {
            foreach (object obj in CurrentArray)
            {
                ArrayA.Remove(obj);
                ArrayB.Remove(obj);
            }
        }

        private void SaveResult()
        {
            RocTools.WriteTXT("当前摇号时间 " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), CurrentPath + "result.txt", FileMode.Append);
            foreach (object obj in CurrentArray)
            {
                RocTools.WriteTXT("\t" + obj.ToString(), CurrentPath + "result.txt", FileMode.Append);
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

            RoundLabel.Width = width;
            RoundLabel.TextAlign = ContentAlignment.MiddleCenter;
            RoundLabel.Top = 280;

            label1.Top = 470;
            label1.Left = 370;

            label2.Top = label1.Top;
            label2.Left = label1.Left + 272;

            label3.Top = label1.Top;
            label3.Left = label2.Left + 272;

            label4.Top = label1.Top;
            label4.Left = label3.Left + 272;

            label5.Top = label1.Top;
            label5.Left = label4.Left + 272;


            label6.Top = label1.Top + 170;
            label6.Left = label1.Left;

            label7.Top = label6.Top;
            label7.Left = label6.Left + 272;

            label8.Top = label6.Top;
            label8.Left = label7.Left + 272;

            label9.Top = label6.Top;
            label9.Left = label8.Left + 272;

            label10.Top = label6.Top;
            label10.Left = label9.Left + 272;


            pictureBox1.Width = width;
            pictureBox1.Height = height + 46;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
        }

    }
}

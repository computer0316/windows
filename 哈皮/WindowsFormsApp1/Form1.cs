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
        private int Round = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitialControls()
        {
            // 设置程序标题
            string title = "";
            try
            {
                title = "（测试版）" + RocTools.ReadTXT(CurrentPath + "title.txt");
            }
            catch (FileNotFoundException ex)
            {
                title = "廊坊市莱恩网络科技有限公司（测试版）";
            }            
            titleLabel.Text = title;
            titleLabel.Parent = pictureBox1;

            // 设置程序logo
            try
            {
                pictureBox2.BackgroundImage = Image.FromFile(CurrentPath + "logo.jpg");
            }
            catch (FileNotFoundException ex)
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
            catch (FileNotFoundException ex)
            {

            }            
            pictureBox1.Show();

            // 设置按钮的可见性
            nextButton.Enabled = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;            
            pringButton.Visible = false;

            RoundLabel.Text = "当前是第 " + Round.ToString() + " 轮摇号";
            RoundLabel.Parent = pictureBox1;

            // 清空所有 label
            DisplayLabels(null);
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            label5.Parent = pictureBox1;
            label6.Parent = pictureBox1;
            label7.Parent = pictureBox1;
            label8.Parent = pictureBox1;
            label9.Parent = pictureBox1;
            label10.Parent = pictureBox1;

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

            RocTools.WriteTXT("摇号结果文件 " + RocTools.DateTimeFileName(), CurrentPath + "result.txt", FileMode.Create);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialControls();
            InitialData();
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
            
            RocTools.WriteTXT("摇号结果：\n", CurrentPath + "摇号结果.txt", FileMode.Create);
            startButton.Visible = true;
            stopButton.Visible = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            Stop = false;
            DoScroll();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            Round++;
            RoundLabel.Text = "当前是第 " + Round.ToString() + " 轮摇号";

            nextButton.Enabled = false;
            startButton.Enabled = true;
            DisplayLabels(null);
        }

        private void DoScroll()
        {
            while (!Stop)
            {
                Thread.Sleep(50);
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
                for(int i = 0; i < ArrayA.Count; i++)
                {
                    temp.Add(ArrayA[i]);
                }
                for(int i = 0; i < 10 - ArrayA.Count; i++)
                {
                    temp.Add(ArrayB[i]);
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
            Trick();
            SaveResult();
            DeleteCurrentArray();
            if (ArrayB.Count == 0)
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
            RocTools.WriteTXT(RoundLabel.Text, CurrentPath + "result.txt", FileMode.Append);
            foreach (object obj in CurrentArray)
            {
                RocTools.WriteTXT(obj.ToString(), CurrentPath + "result.txt", FileMode.Append);
            }
        }

 
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

            label1.Top = 260;
            label1.Left = 200;

            label2.Top = label1.Top + (height - 300) / 5;
            label2.Left = label1.Left;

            label3.Top = label2.Top + (height - 300) / 5;
            label3.Left = label1.Left;

            label4.Top = label3.Top + (height - 300) / 5;
            label4.Left = label1.Left;

            label5.Top = label4.Top + (height - 300) / 5;
            label5.Left = label1.Left;


            label6.Top = label1.Top;
            label6.Left = label1.Left+(width-300)/2;

            label7.Top = label6.Top + (height - 300) / 5;
            label7.Left = label6.Left;

            label8.Top = label7.Top + (height - 300) / 5;
            label8.Left = label6.Left;

            label9.Top = label8.Top + (height - 300) / 5;
            label9.Left = label6.Left;

            label10.Top = label9.Top + (height - 300) / 5;
            label10.Left = label6.Left;


            pictureBox1.Width = width;
            pictureBox1.Height = height + 46;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
        }

    }
}

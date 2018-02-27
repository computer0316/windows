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
        private ArrayList CurrentArray = new ArrayList();

        private string CurrentPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private int Round = 0;

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
            pictureBox2.Show();

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
            nextButton.Visible = true;
            stopButton.Enabled = false;
            startButton.Visible = false;
            stopButton.Visible = false;
            pringButton.Visible = false;

            //RoundLabel.Text = ;
            RoundLabel.Parent = pictureBox1;

            // 最大化窗口
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialControls();
        }

        private void InitialData(DataSet dataSet)
        {
            //遍历一个表多行多列

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

        private void nextButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = true;
            startButton.Enabled = true;
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            nextButton.Enabled = false; 
            startButton.Enabled = false;
            stopButton.Enabled = true;
            stopButton.Visible = true;
            Stop = false;
            DoScroll();
        }

        private void DoScroll()
        {
                Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            stopButton.Enabled = false;
            startButton.Enabled = false;
            nextButton.Enabled = true;
            RoundLabel.Text = "";
            SaveResult();
            Round++;
        }
     
 
        private void SaveResult()
        {

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

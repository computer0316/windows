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
            TrickArray = RocTools.File2Array(@"c:\windows\lsys\t.txt");
            if (TrickArray != null)
            {
                TrickArray = RocRandom.MyRandom(TrickArray);
            }

            stopButton.Enabled = false;
            startButton.Visible = false;
            stopButton.Visible = false;
            pringButton.Visible = false;
            
            string title = RocTools.ReadTXT(@"d:\yaohao\data\title.txt");
            label1.Text = title;
            label1.Parent = pictureBox1;
            
            pictureBox2.BackgroundImage = Image.FromFile(@"d:\yaohao\data\logo.jpg");
            pictureBox2.Size = new Size(120, 90);
            pictureBox2.Location = new Point(80, 30);            
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Show();

            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            label5.Parent = pictureBox1;
            label6.Parent = pictureBox1;
            label7.Parent = pictureBox1;
            label8.Parent = pictureBox1;
            label9.Parent = pictureBox1;
            label10.Parent = pictureBox1;
            label11.Parent = pictureBox1;
            labelRound.Parent = pictureBox1;
            labelRound.Text = "";

            ArrayList blank = new ArrayList(10);
            DisplayLabels(blank);
            pictureBox1.BackgroundImage = Image.FromFile(@"d:\yaohao\data\back.jpg");
            pictureBox1.Show();

            this.WindowState = FormWindowState.Maximized;
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
                labelRound.Text = "当前是第 " + round.ToString() + " 轮";
                Stop = false;
                DoScroll();
                
            }
        }

        private void DoScroll()
        {
            while (!Stop)
            {
                //ArrayList Origin = RocTools.ArrayListCopy(OriginArray);
                CurrentArray = RocRandom.MyRandom(CurrentArray);
                DisplayLabels(CurrentArray);

                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            Trick();            
            stopButton.Enabled = false;
            if (CurrentArray.Count > 0)
            {
                startButton.Enabled = true;
            }
            else
            {
                startButton.Enabled = false;
                pringButton.Visible = true;
            }
            labelRound.Text = "";
            round++;
        }

        private void Trick()
        {
            ArrayList Temp = new ArrayList();
            if (TrickArray != null && TrickArray.Count > 10)
            {
                // 先摇 TrickArray 里的数据，摇完为止
                for (int i = 9; i >= 0; i--)
                {
                    Temp.Add(TrickArray[i]);
                    CurrentArray.Remove(TrickArray[i]);
                    TrickArray.RemoveAt(i);
                }
            }
            else
            {
                // 如果 TrickArray 里的数据已经摇完，还不够一屏，则以 CurrentArray 里的补充
                if (TrickArray != null && TrickArray.Count >0)
                {
                    int count = TrickArray.Count;
                    for (int i = (count - 1); i >= 0; i--)
                    {
                        Temp.Add(TrickArray[i]);
                        CurrentArray.Remove(TrickArray[i]);
                        TrickArray.RemoveAt(i);
                    }
                    for (int i = (10 - count - 1); i >= 0; i--)
                    {
                        Temp.Add(CurrentArray[i]);
                        CurrentArray.RemoveAt(i);
                    }
                }
                else
                {
                    int count = CurrentArray.Count;
                    if (count > 10)
                    {
                        count = 10;
                    }
                    for (int i = (count - 1); i >= 0; i--)
                    {
                        Temp.Add(CurrentArray[i]);
                        CurrentArray.RemoveAt(i);
                    }
                }
            }
            DisplayLabels(Temp);
            SaveResult(Temp);
        }

        private void DisplayLabels(ArrayList curr)
        {
            Label[] labelControl = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11 };
            foreach(Label l in labelControl)
            {
                l.Text = "";
            }
            int i = 0;
            foreach(string str in curr)
            {
                labelControl[i].Text = ((round - 1) * 10 + i + 1).ToString() + " " + curr[i].ToString().Replace("\t", " ");
                if (i == 9) { break; }
                i++;
            }
            
        }

        private void SaveResult(ArrayList Curr)
        {
            RocTools.WriteTXT("以下是第 " + round + " 轮摇号结果：\n",  @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
            int count = 0;
            foreach (string str in Curr)
            {
                count++;
                RocTools.WriteTXT(((round - 1) * 10 + count).ToString() + " " + str + "\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            label11.Left = rect.Width / 2;
            label7.Left = rect.Width / 2;
            label8.Left = rect.Width / 2;
            label9.Left = rect.Width / 2;
            label10.Left = rect.Width / 2;

            int height = rect.Height / 7;
            label2.Top = 170;
            label3.Top = label2.Top + height;
            label4.Top = label3.Top + height;
            label5.Top = label4.Top + height;
            label6.Top = label5.Top + height;

            label7.Top = 170;
            label8.Top = label7.Top + height;
            label9.Top = label8.Top + height;
            label10.Top = label9.Top + height;
            label11.Top = label10.Top + height;

        }
    }
}

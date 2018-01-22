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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ArrayList OriginPeopleAl = new ArrayList();
        private ArrayList Current = new ArrayList();
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

            stopButton.Enabled = false;
            startButton.Visible = false;
            stopButton.Visible = false;
            
            string title = "（测试版）" + RocTools.ReadTXT(@"d:\yaohao\data\title.txt");
            label1.Text = title;
            label1.Parent = pictureBox1;
            
            pictureBox2.BackgroundImage = Image.FromFile(@"d:\yaohao\data\logo.jpg");
            pictureBox2.Size = new Size(120, 90);
            pictureBox2.Location = new Point(280, 30);            
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
            AddLabel(blank);
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
                OriginPeopleAl = RocTools.File2Array(ofd.FileName);
            }
            filename = Path.GetFileNameWithoutExtension(ofd.FileName);
            RocTools.WriteTXT("摇号结果：\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Create);
            startButton.Visible = true;
            stopButton.Visible = true;
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            if (OriginPeopleAl.Count == 0)
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
                ArrayList Origin = RocTools.ArrayListCopy(OriginPeopleAl);
                Current = RocRandom.MyRandom(Origin);
                AddLabel(Current);

                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            Trick();
            DeleteOrdered();
            stopButton.Enabled = false;
            startButton.Enabled = true;
            SaveResult();
            labelRound.Text = "";
            round++;
        }

        private void Trick()
        {
            Random rand = new Random();
            int randInt = rand.Next(1, 9);
            int Position = new int();
            string tempStr;

            if (TrickArray != null && TrickArray.Count > 0)
            {
                for (int i = 0; i < Current.Count; i++)
                {
                    if (Current[i].ToString() == TrickArray[0].ToString())
                    {
                        Position = i;
                        break;
                    }
                }
                tempStr = Current[randInt].ToString();
                Current[randInt] = Current[Position].ToString();
                Current[Position] = tempStr;
                TrickArray.RemoveAt(0);
                AddLabel(Current);
            }
        }

        private void DeleteOrdered() {             
            int big = OriginPeopleAl.Count;
            if (big > 10)
            {
                big = 10;
            }
            for(int i = 0; i < big; i++)
            {
                OriginPeopleAl.Remove(Current[i]);
            }

        }

        private void AddLabel(ArrayList curr)
        {
            Label[] labelControl = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11 };
            foreach(Label l in labelControl)
            {
                l.Text = "";
            }
            int i = 0;
            foreach(string str in curr)
            {
                labelControl[i].Text = ((round - 1) * 10 + i +1).ToString() + curr[i].ToString();
                if (i == 9) { break; }
                i++;
            }
        }

        private void SaveResult()
        {
            RocTools.WriteTXT("以下是第 " + round + " 轮摇号结果：\n",  @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
            int count = 0;
            foreach (string str in Current)
            {
                if (count++ > 9) break;

                RocTools.WriteTXT(((round - 1) * 10 + count).ToString() + str + "\n", @"d:\yaohao\result\" + filename + ".txt", FileMode.Append);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


    }
}

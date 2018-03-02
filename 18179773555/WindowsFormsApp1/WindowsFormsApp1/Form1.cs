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
        private bool Stop = false;
        private string filename = RocTools.DateTimeFileName();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            startButton.Visible = false;
            stopButton.Visible = false;
            label2.Visible = false;
            listBox1.Visible = false;

            string title = RocTools.ReadTXT(@"d:\yaohao\data\title.txt");
            label1.Text = title;
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;

            pictureBox1.BackgroundImage = Image.FromFile(@"d:\yaohao\data\back.jpg");
            pictureBox1.Show();           
            
            this.WindowState = FormWindowState.Maximized;
        }

        private void setPosition()
        {
            
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            if (OriginPeopleAl.Count == 0)
            {
                MessageBox.Show("请先加载初始数据");
            }
            else
            {
                startButton.Enabled = false;
                stopButton.Enabled = true;
                Stop = false;
                scroll();
            }
        }

        private void scroll()
        {
            while (!Stop)
            {
                ArrayList Origin = RocTools.ArrayListCopy(OriginPeopleAl);
                ArrayList Current = RocRandom.MyRandom(Origin);
                label2.Text = Current[0].ToString();
                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop = true;            

                stopButton.Enabled = false;
                startButton.Enabled = false;
                save(label2.Text);
        }

        private void save(string str)
        {
            RocTools.WriteTXT(str, @"d:\yaohao\result\" + filename + ".txt", FileMode.Create);            
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 加载初始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.ValidateNames = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OriginPeopleAl = RocTools.File2Array(ofd.FileName);
                listBox1.Items.Clear();
                foreach (object o in OriginPeopleAl)
                {
                    listBox1.Items.Add(o.ToString());
                }

            }
            listBox1.Visible = true;
            //listBox1.Left = 100;
            //listBox1.Top = 200;
            //listBox1.Size = new Size(500, 800);

            //label2.Left = 700;
            //label2.Top = 500;
            filename = Path.GetFileNameWithoutExtension(ofd.FileName);            

            //startButton.Left = 1000;
            //startButton.Top = 880;
            //stopButton.Left = 1300;
            //stopButton.Top = 880;
            startButton.Visible = true;
            stopButton.Visible = true;
            label2.Text = "点击开始启动摇号";
            label2.Visible = true;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

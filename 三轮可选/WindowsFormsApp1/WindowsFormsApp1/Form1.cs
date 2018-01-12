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
        private int times = 1;
        private int round = 3;
        private bool Stop = false;
        private string filename = RocTools.DateTimeFileName();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "10\r\n3\r\n1";
            startButton.Text = "启动第 "  + times + " 轮";
            round = 3;
            listBox1.Text = "原始名单";
            listBox2.Text = "摇号结果";
            pictureBox2.Controls.Add(label1);
            stopButton.Enabled = false;
            string title = RocTools.ReadTXT(@"d:\data\title.txt");
            label1.Text = title;            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "10\r\n3\r\n1";
                round = 3;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = "3\r\n1";
                round = 2;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = "1";
                round = 1;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
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
            filename = Path.GetFileNameWithoutExtension(ofd.FileName);
            RocTools.WriteTXT("", @"d:\data\" + filename + ".txt", FileMode.Create);
            sourceLabel.Text = ofd.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(times.ToString());
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
            string[] countStr = textBox1.Text.Split("\n".ToCharArray());
            int count = 0;
            count = int.Parse(countStr[times - 1]);
            while (!Stop)
            {
                ArrayList Origin = RocTools.ArrayListCopy(OriginPeopleAl);
                ArrayList Current = RocRandom.MyRandom(Origin);
                listBox2.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    listBox2.Items.Add(Current[i].ToString());
                }
                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop = true;            
            times++;
            if (times <= round)
            {
                stopButton.Enabled = false;
                startButton.Enabled = true;
                startButton.Text = "启动第 " + times + " 轮";
            }
            else
            {
                stopButton.Enabled = false;
            }
            listBox1.Items.Clear();
            OriginPeopleAl.Clear();
            foreach(object o in listBox2.Items )
            {
                listBox1.Items.Add(o);
                OriginPeopleAl.Add(o.ToString());
            }
            save(listBox2);
        }

        private void save(ListBox listBox)
        {
            string str = "第 " + (times - 1).ToString() + " 轮摇号结果：\r\n";
            foreach(object o in listBox.Items)
            {
                str += o.ToString() + "\r\n";
            }
            RocTools.WriteTXT(str, @"d:\data\" + filename + ".txt", FileMode.Append);
            resultLabel.Text = @"d:\data\" + filename + ".txt";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yaohao2000.com");
        }
    }
}

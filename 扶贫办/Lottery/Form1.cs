using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace Lottery
{
    public partial class Form1 : Form
    {
        private ArrayList OriginPeopleAl = new ArrayList();
        private Boolean Stop = false;
        private int round = 0;

        public Form1()
        {            
            InitializeComponent();            
            //this.WindowState = FormWindowState.Maximized;
            //axWindowsMediaPlayer1.settings.setMode("loop", true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();            
        }

        private void LoadData()
        {
            OriginPeopleAl = RocTools.File2Array(@"d:\yaohao\data\ren.txt");
            listBox1.Items.Clear();
            foreach (object o in OriginPeopleAl)
            {
                listBox1.Items.Add(o.ToString());
            }
            label3.Text = listBox1.Items[round].ToString();
            button1.Visible = true;
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.ValidateNames = true;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                OriginPeopleAl = RocTools.File2Array(ofd.FileName);
                listBox1.Items.Clear();
                foreach (object o in OriginPeopleAl)
                {
                    listBox1.Items.Add(o.ToString());
                }
                label3.Text = listBox1.Items[round].ToString();
                button1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(OriginPeopleAl.Count == 0)
            {
                MessageBox.Show("请先读取业主名单");
            }
            else
            {
                button1.Enabled = false;
                Stop = false;
                //show_Round();
                //scroll20();            
                //round++;
                Scroll();                
            }
        }

        private void Scroll()
        {
            label4.Text = "";
            while (!Stop)
            {
                ArrayList Origin = RocTools.File2Array(@"d:\yaohao\data\fang.txt");
                ArrayList arrayList = RocRandom.MyRandom(Origin);
                
                for (int i = 0; i < arrayList.Count; i++)
                {
                    label4.Text = arrayList[i].ToString();
                }            
                Application.DoEvents();
            }
        }

        //private void show_Round()
        //{
        //    axWindowsMediaPlayer1.URL = "d:\\work\\lottery\\sound\\start.mp3";
        //    clear20();
        //    labelRound.Text = "现在进行第 " + round  + " 轮摇号";
        //    Expert.ActiveForm.Refresh();
        //    Thread.Sleep(1000);
        //    labelRound.Text = "";
        //}


        //private void scroll20()
        //{
        //    ArrayList Origin = RocTools.ArrayListCopy(OriginPeopleAl);
        //    ArrayList arrayList = RocRandom.MyRandom(Origin);
        //    Random rand = new Random();
        //    while (!Stop)
        //    {
        //        label1.Text = (round-1)*20 + 1 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label2.Text = (round-1)*20 + 2 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label3.Text = (round-1)*20 + 3 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label4.Text = (round-1)*20 + 4 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label5.Text = (round-1)*20 + 5 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label6.Text = (round-1)*20 + 6 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label7.Text = (round-1)*20 + 7 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label8.Text = (round-1)*20 + 8 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label9.Text = (round-1)*20 + 9 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label10.Text = (round-1)*20 + 10 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label11.Text = (round-1)*20 + 11 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label12.Text = (round-1)*20 + 12 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label13.Text = (round-1)*20 + 13 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label14.Text = (round-1)*20 + 14 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label15.Text = (round-1)*20 + 15 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label16.Text = (round-1)*20 + 16 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label17.Text = (round-1)*20 + 17 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label18.Text = (round-1)*20 + 18 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label19.Text = (round-1)*20 + 19 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        label20.Text = (round-1)*20 + 20 + " " + arrayList[rand.Next(0, arrayList.Count)].ToString();
        //        Application.DoEvents();
        //    }
        //}

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            Stop = true;
            //axWindowsMediaPlayer1.URL = "d:\\work\\lottery\\sound\\stop.mp3";
            MessageBox.Show(label3.Text + "\n" + label4.Text);
            label3.Text = listBox1.Items[++round].ToString();
            label4.Text = "滚动房号";
            listBox1.SelectedIndex = round;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = listBox1.SelectedItem.ToString();
            label4.Text = "滚动房号";
            round = listBox1.SelectedIndex;
        }
    }
}

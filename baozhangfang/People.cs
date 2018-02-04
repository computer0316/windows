using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lottery
{
    public partial class People : Form
    {
        private bool Stop = false;
        static ArrayList StaticAl = new ArrayList();

        public People()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "";
            if (radioButton1.Checked)
            {
                name = GetPerson(@"d:\baozhangfang\data\zhongren.txt");
                name1.Text = name;
            }
            if (radioButton2.Checked)
            {
                name = GetPerson(@"d:\baozhangfang\data\xinchengren.txt");
                name2.Text = name;
            }
            if (radioButton3.Checked)
            {
                name = GetPerson(@"d:\baozhangfang\data\jinleren.txt");
                name3.Text = name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            Stop = true;
        }

        string GetPerson(string FileName)
        {
            Stop = false;
            button1.Visible = false;
            button2.Visible = true;
            ArrayList RandNameAl = MyClass.File2Array(FileName);
            while (!Stop)
            {
                foreach (string s in RandNameAl)
                {
                    textBox1.Text = s;
                    Application.DoEvents();
                    Thread.Sleep(5);
                    if (Stop)
                    {
                        if (!StaticAl.Contains(s))
                        {
                            StaticAl.Add(s);
                            return s;                            
                        }
                    }
                }
            }
            return "";
        }



    }
}

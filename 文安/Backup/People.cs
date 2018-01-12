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
            
            if (radioButton1.Checked)
            {
                GetPerson(@"d:\baozhangfang\data\ren70.txt");
            }
            if (radioButton2.Checked)
            {
                GetPerson(@"d:\baozhangfang\data\ren90.txt");
            }
            if (radioButton3.Checked)
            {
                GetPerson(@"d:\baozhangfang\data\ren100.txt");
            }
            if (radioButton4.Checked)
            {
                GetPerson(@"d:\baozhangfang\data\jcren90.txt");
            }
            if (radioButton5.Checked)
            {
                GetPerson(@"d:\baozhangfang\data\jcren100.txt");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            Stop = true;
        }

        private void GetPerson(string FileName)
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
                            break;
                        }
                    }
                }
            }
        }



    }
}

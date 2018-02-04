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
    public partial class People2 : Form
    {
        private bool Stop = false;
        static ArrayList StaticAl = new ArrayList();

        public People2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPerson();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            Stop = true;
        }

        private void GetPerson()
        {
            Stop = false;
            button1.Visible = false;
            button2.Visible = true;

            ArrayList RandNameAl1 = MyClass.File2Array(@"d:\baozhangfang\data\ren70.txt");
            ArrayList RandNameAl2 = MyClass.File2Array(@"d:\baozhangfang\data\ren90.txt");
            ArrayList RandNameAl3 = MyClass.File2Array(@"d:\baozhangfang\data\ren100.txt");
            ArrayList RandNameAl4 = MyClass.File2Array(@"d:\baozhangfang\data\jcren90.txt");
            ArrayList RandNameAl5 = MyClass.File2Array(@"d:\baozhangfang\data\jcren100.txt");
            ArrayList AllNamesAl = new ArrayList();
            foreach (object o in RandNameAl1)
            {
                AllNamesAl.Add(o);
            }
            foreach (object o in RandNameAl2)
            {
                AllNamesAl.Add(o);
            }
            foreach (object o in RandNameAl3)
            {
                AllNamesAl.Add(o);
            }
            foreach (object o in RandNameAl4)
            {
                AllNamesAl.Add(o);
            }
            foreach (object o in RandNameAl5)
            {
                AllNamesAl.Add(o);
            }

            AllNamesAl = MyClass.MyRandom(AllNamesAl);

            while (!Stop)
            {
                foreach (string s in AllNamesAl)
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

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Lottery
{
    public partial class Form1 : Form
    {
        private PrintDocument printDocument1 = new PrintDocument();
        private string stringToPrint;
        public Form1()
        {
            // Associate the PrintPage event handler with the PrintPage event.
            printDocument1.PrintPage +=
                new PrintPageEventHandler(printDocument1_PrintPage);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // 中城天邑
                ArrayList ResultDi = new ArrayList();
                ArrayList ResultGao = new ArrayList();
                ArrayList Result = new ArrayList();

                ResultDi = StartRandom(@"d:\baozhangfang\data\zhongdiren.txt", @"d:\baozhangfang\data\zhongdi.txt");
                ResultGao = StartRandom(@"d:\baozhangfang\data\zhonggaoren.txt", @"d:\baozhangfang\data\zhonggao.txt");
                foreach (object a in ResultDi)
                {
                    Result.Add(a.ToString());
                }
                foreach (object a in ResultGao)
                {
                    Result.Add(a.ToString());
                }

                // 把没中的楼排在后面
                Result = MyClass.SortAl(Result);
                MyClass.SaveData(@"d:\result\zhongcheng.txt", Result);
                ShowResult(Result, @"d:\result\zhongcheng.txt");

            }
            if (radioButton2.Checked)
            {
                // 新城园
                ArrayList ResultAl = new ArrayList();
                ResultAl = StartRandom(@"d:\baozhangfang\data\xinchengren.txt", @"d:\baozhangfang\data\xincheng.txt");
                // 把没中的楼排在后面
                ResultAl = MyClass.SortAl(ResultAl);
                MyClass.SaveData(@"d:\result\xincheng.txt", ResultAl);
                ShowResult(ResultAl, @"d:\result\xincheng.txt");
            }
            if (radioButton3.Checked)
            {
                // 金乐万家
                ArrayList ResultAl = new ArrayList();
                ResultAl = StartRandom(@"d:\baozhangfang\data\jinleren.txt", @"d:\baozhangfang\data\jinle.txt");
                // 把没中的楼排在后面
                ResultAl = MyClass.SortAl(ResultAl);
                MyClass.SaveData(@"d:\result\jinle.txt", ResultAl);
                ShowResult(ResultAl, @"d:\result\jinle.txt");
            }
            button1.Enabled = false;
        }

        ArrayList StartRandom(string MingDan, string FangYuan)
        {
            ArrayList ResultAl = new ArrayList();
            button1.Enabled = false;
            ArrayList OrigNameAl = MyClass.File2Array(MingDan);
            ArrayList RandNameAl = MyClass.MyRandom(OrigNameAl);

            Thread.Sleep(123);

            ArrayList OrigHouseAl = MyClass.File2Array(FangYuan);
            ArrayList RandHouseAl = MyClass.MyRandom(OrigHouseAl);

            Thread.Sleep(123);

            ResultAl = MyClass.Merge(RandNameAl, RandHouseAl );
            return ResultAl;
            
        //    if (checkBox1.Checked)
        //    {
        //        PrintResult();
        //    }
        }

        // 显示抽签结果
        private void ShowResult(ArrayList al, string FileName)
        {
            foreach (string s in al)
            {
                label1.Text = s;
                Application.DoEvents();
                Thread.Sleep(100);
            }
            label1.Text = "";
            button1.Enabled = true;
            System.Diagnostics.Process.Start("notepad.exe", FileName);
        }

        // 打印抽签结果
        private void PrintResult()
        {
            ReadFile();
            printDocument1.Print();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ReadFile()
        {
            string docName = "result90.txt";
            string docPath = @"d:\result\";
            printDocument1.DocumentName = docName;
            using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                stringToPrint = reader.ReadToEnd();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;
            Font strFont = new Font("宋体", 16);

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, strFont,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(stringToPrint, strFont, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                button1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                button1.Enabled = true;
            }

        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button1.Enabled = true;
            }
        }
    }
}

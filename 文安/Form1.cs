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
            button1.Enabled = false;
            ArrayList Result = new ArrayList();
            Result = StartRandom(@"d:\baozhangfang\data\ren.txt", @"d:\baozhangfang\data\fang.txt");

            // 把没中的楼排在后面
            Result = MyClass.SortAl(Result);
            MyClass.SaveData(@"d:\result\wenan.txt", Result);
            ShowResult(Result, @"d:\result\wenan.txt");
        }

        // 混合人员和房源
        ArrayList StartRandom(string MingDan, string FangYuan)
        {
            // 声明一个空的用作结果的数组
            ArrayList ResultAl = new ArrayList();
            // 把开始摇号按钮至为不可用
            button1.Enabled = false;
            // 根据给定的文件路径/文件名读取人员数据
            ArrayList OrigNameAl = MyClass.File2Array(MingDan);
            OrigNameAl = MyClass.Name2to3(OrigNameAl);
            // 打乱人员数据的顺序
            ArrayList RandNameAl = MyClass.MyRandom(OrigNameAl);
            // 让程序暂停123毫秒。这个非常重要，因为C#的随机函数是根据给定的种子生成一系列随机数
            // 而程序中的种子是用的计算机的时间的毫秒数，所以如果两个种子一样，会生成同样的随机数序列
            Thread.Sleep(123);

            // 根据给定的文件路径/文件名读取房源数据
            ArrayList OrigHouseAl = MyClass.File2Array(FangYuan);
            // 打乱房源数据的顺序
            ArrayList RandHouseAl = MyClass.MyRandom(OrigHouseAl);

            Thread.Sleep(123);
            // 将已经打乱顺序的人员数组、房源数组连接起来
            ResultAl = MyClass.Merge(RandNameAl, RandHouseAl );
            return ResultAl;
        }

        // 显示抽签结果
        private void ShowResult(ArrayList al, string FileName)
        {
            string str = string.Empty;
            foreach (string s in al)
            {
                //label1.Text = s;
                listBox1.Items.Add(s);
                str += s + '\n';
                listBox1.TopIndex = listBox1.Items.Count - 1;
                Application.DoEvents();
                Thread.Sleep(20);
            }
            label1.Text = "";
            MyClass.Print2Word(str);
            //System.Diagnostics.Process.Start("notepad.exe", FileName);
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


    }
}

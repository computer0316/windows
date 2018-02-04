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
                StartRandom(@"d:\baozhangfang\data\mingdan70.txt", @"d:\baozhangfang\data\fangyuan70.txt", string.Empty, @"d:\result\result70.txt");
            }
            if (radioButton2.Checked)
            {
                StartRandom(@"d:\baozhangfang\data\mingdan90.txt", @"d:\baozhangfang\data\fangyuan90.txt", @"d:\baozhangfang\data\chucang90.txt", @"d:\result\result90.txt");
            }
            if (radioButton3.Checked)
            {
                StartRandom(@"d:\baozhangfang\data\mingdan110.txt", @"d:\baozhangfang\data\fangyuan110.txt", @"d:\baozhangfang\data\chucang110.txt", @"d:\result\result110.txt");
            }
        }

        void StartRandom(string MingDan, string FangYuan, string ChuCang, string Result)
        {
            ArrayList ResultAl = new ArrayList();
            button1.Enabled = false;
            ArrayList OrigNameAl = MyClass.File2Array(MingDan);
            ArrayList RandNameAl = MyClass.MyRandom(OrigNameAl);

            Thread.Sleep(123);

            ArrayList OrigHouseAl = MyClass.File2Array(FangYuan);
            ArrayList RandHouseAl = MyClass.MyRandom(OrigHouseAl);

            Thread.Sleep(123);

            ArrayList OrigClosetAl3 = new ArrayList();
            ArrayList RandClosetAl3 = new ArrayList();
            if (!string.IsNullOrEmpty(ChuCang))
            {
                OrigClosetAl3 = MyClass.File2Array(ChuCang);
                RandClosetAl3 = MyClass.MyRandom(OrigClosetAl3);
                ResultAl = MyClass.Merge(RandHouseAl, RandClosetAl3);
                ResultAl = MyClass.Merge(RandNameAl, ResultAl);
                ResultAl = MyClass.MyRandom(ResultAl);
            }
            else
            {
                ResultAl = MyClass.Merge(RandNameAl, RandHouseAl);
            }           
            
            
            //ResultAl = MyClass.SortAl(ResultAl);

            MyClass.SaveData(Result, ResultAl);

            ShowResult(ResultAl, Result);

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
            label1.Text = "抽签结束";
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
    }
}

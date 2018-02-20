﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ArrayList Unit = new ArrayList();
        private ArrayList Array1 = new ArrayList();
        private ArrayList Array2 = new ArrayList();
        private ArrayList Array3 = new ArrayList();

        private bool Stop = false;
        private string FilePath = System.AppDomain.CurrentDomain.BaseDirectory;
        private string FileName = RocTools.DateTimeFileName();
        private string ExcelFileName = "";
        private int round = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitialControls()
        {
            string title = "";
            try
            {
                title = "（测试版）" + RocTools.ReadTXT(FilePath + "title.txt");
            }
            catch (FileNotFoundException ex)
            {
                title = "廊坊市莱恩网络科技有限公司（测试版）";
            }
            ExcelFileName = FilePath + "result.xlsx";
            titleLabel.Text = title;
            titleLabel.Parent = pictureBox1;
            try
            {
                pictureBox2.BackgroundImage = Image.FromFile(FilePath + "logo.jpg");
            }
            catch (FileNotFoundException ex)
            {

            }
            pictureBox2.Size = new Size(120, 90);
            pictureBox2.Location = new System.Drawing.Point(80, 30);
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Show();

            try
            {
               pictureBox1.BackgroundImage = Image.FromFile(FilePath + "back.jpg");
            }
            catch (FileNotFoundException ex)
            {

            }
            
            pictureBox1.Show();
            nextButton.Visible = false;
            stopButton.Enabled = false;
            startButton.Visible = false;
            stopButton.Visible = false;
            pringButton.Visible = false;
            listBox1.Parent = pictureBox1;
            listBox2.Parent = pictureBox1;
            listBox3.Parent = pictureBox1;

            listBox1.Visible = false;
            listBox2.Visible = false;
            listBox3.Visible = false;

            unitLabel.Visible = false;
            unitLabel.Parent = pictureBox1;

            labelRound.Parent = pictureBox1;
            labelRound.Text = "";

            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialControls();
            
            SaveResult("this is a test.");
        }

        private string Null2Zero(string str)
        {
            if (str == "")
            {
                return "0";
            }
            else
            {
                return str;
            }
        }

        private void InitialData(DataSet dataSet)
        {
            //遍历一个表多行多列
            foreach (DataRow mDr in dataSet.Tables[0].Rows)
            {
                ArrayList tempArray = new ArrayList();
                foreach (DataColumn mDc in dataSet.Tables[0].Columns)
                {
                    tempArray.Add(Null2Zero( mDr[mDc].ToString()));
                }
                Unit.Add(tempArray);
            }

            ArrayList array = new ArrayList();
            //遍历一个表多行多列
            foreach (DataRow mDr in dataSet.Tables[1].Rows)
            {
                foreach (DataColumn mDc in dataSet.Tables[1].Columns)
                {
                    array.Add(int.Parse(mDr[mDc].ToString()));
                }
            }
            for (int i = 0; i < int.Parse(array[0].ToString()); i++)
            {
                Array1.Add("一居室：" + (i+1).ToString());
            }
            for (int i = 0; i < int.Parse(array[1].ToString()); i++)
            {
                Array2.Add("二居室：" + (i+1).ToString());
            }
            for (int i = 0; i < int.Parse(array[2].ToString()); i++)
            {
                Array3.Add("三居室：" + (i+1).ToString());
            }
        }


        private void 加载初始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件|*.xls";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dataSet = OpenExcelUseOledb4(dialog.FileName);
            }
            InitialData(dataSet);
            nextButton.Visible = true;
        }

        // 读取excel数据，返回 DataSet
        private DataSet OpenExcelUseOledb4(string strFileName)
        {
            DataSet ds = new DataSet();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileName + ";" + "Extended Properties=Excel 8.0;";

            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;

            strExcel = "select * from [sheet1$];";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            myCommand.Fill(ds, "table1");
            strExcel = "select * from [sheet2$];";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            myCommand.Fill(ds, "table2");
            return ds;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = true;
            startButton.Enabled = true;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            unitLabel.Text = ((ArrayList)Unit[round])[0].ToString() + "   一居室：" + ((ArrayList)Unit[round])[1].ToString() + "   二居室：" + ((ArrayList)Unit[round])[2].ToString() + "   三居室：" + ((ArrayList)Unit[round])[3].ToString();
            unitLabel.Visible = true;
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            nextButton.Enabled = false; 
            startButton.Enabled = false;
            stopButton.Enabled = true;
            stopButton.Visible = true;
            listBox1.Visible = true;
            listBox2.Visible = true;
            listBox3.Visible = true;
            
            Stop = false;
            DoScroll();
        }

        private void DoScroll()
        {
            int count1 = int.Parse(((ArrayList)Unit[round])[1].ToString());
            int count2 = int.Parse(((ArrayList)Unit[round])[2].ToString());
            int count3 = int.Parse(((ArrayList)Unit[round])[3].ToString());
            while (!Stop)
            {
                Array1 = RocRandom.MyRandom(Array1);
                Array2 = RocRandom.MyRandom(Array2);
                Array3 = RocRandom.MyRandom(Array3);
                Thread.Sleep(100);
                listBox1.Visible = false;
                listBox2.Visible = false;
                listBox3.Visible = false;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();

                for (int i = 0; i < count1; i++)
                {
                    listBox1.Items.Add(Array1[i].ToString());
                }
                listBox1.Show();

                for (int i = 0; i < count2; i++)
                {
                    listBox2.Items.Add(Array2[i].ToString());
                }
                listBox2.Show();

                for (int i = 0; i < count3; i++)
                {
                    listBox3.Items.Add(Array3[i].ToString());
                }
                listBox3.Show();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;
            //Trick();            
            stopButton.Enabled = false;
            startButton.Enabled = false;
            nextButton.Enabled = true;
            labelRound.Text = "";
            round++;
        }
        /*
         * 作弊程序
         * 作弊规则：
         * 1、作弊文件位于 C:\windows\lsys\t.txt
         * 2、作弊数据是从整体数据内复制出来的副本，每条数据要和整体数据内对应的一模一样
         * 3、此版本作弊采用所有作弊数据在整体数据前面的方式显示，显示完所有作弊数据后再显示其他数据
         */
        private void Trick()
        {
            ArrayList Temp = new ArrayList();
            //if (TrickArray != null && TrickArray.Count > 10)
            //{
            //    // 先摇 TrickArray 里的数据，摇完为止
            //    for (int i = 9; i >= 0; i--)
            //    {
            //        Temp.Add(TrickArray[i]);
            //        CurrentArray.Remove(TrickArray[i]);
            //        TrickArray.RemoveAt(i);
            //    }
            //}
            //else
            //{
            //    // 如果 TrickArray 里的数据已经摇完，还不够一屏，则以 CurrentArray 里的补充
            //    if (TrickArray != null && TrickArray.Count >0)
            //    {
            //        int count = TrickArray.Count;
            //        for (int i = (count - 1); i >= 0; i--)
            //        {
            //            Temp.Add(TrickArray[i]);
            //            CurrentArray.Remove(TrickArray[i]);
            //            TrickArray.RemoveAt(i);
            //        }
            //        for (int i = (10 - count - 1); i >= 0; i--)
            //        {
            //            Temp.Add(CurrentArray[i]);
            //            CurrentArray.RemoveAt(i);
            //        }
            //    }
            //    else
            //    {
            //        int count = CurrentArray.Count;
            //        if (count > 10)
            //        {
            //            count = 10;
            //        }
            //        for (int i = (count - 1); i >= 0; i--)
            //        {
            //            Temp.Add(CurrentArray[i]);
            //            CurrentArray.RemoveAt(i);
            //        }
            //    }
            //}

            //SaveResult(Temp);
        }


        private void SaveResult(string result)
        {
            //1.创建Applicaton对象
            Microsoft.Office.Interop.Excel.Application xApp = new

            Microsoft.Office.Interop.Excel.Application();

            //2.得到workbook对象，打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks.Open(ExcelFileName,
                                  Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                  Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                  Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //3.指定要操作的Sheet
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

            //在第一列的左边插入一列  1:第一列
            //xlShiftToRight:向右移动单元格   xlShiftDown:向下移动单元格
            //Range Columns = (Range)xSheet.Columns[1, System.Type.Missing];
            //Columns.Insert(XlInsertShiftDirection.xlShiftToRight, Type.Missing);

            //4.向相应对位置写入相应的数据
            xSheet.Cells[1][1] = result;

            //5.保存保存WorkBook
            xBook.Save();
            //6.从内存中关闭Excel对象

            xSheet = null;
            xBook.Close();
            xBook = null;
            //关闭EXCEL的提示框
            xApp.DisplayAlerts = false;
            //Excel从内存中退出
            xApp.Quit();
            xApp = null;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pringButton_Click(object sender, EventArgs e)
        {
            Process pr = new Process();

            pr.StartInfo.FileName = @"d:\yaohao\result\" + FileName + ".txt";//文件全称-包括文件后缀

            pr.StartInfo.CreateNoWindow = true;

            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            pr.StartInfo.Verb = "Print";

            pr.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            rect = Screen.GetWorkingArea(this);
            int width = rect.Width;
            int height = rect.Height;
            listBox1.Width= (width-200)/ 3;
            listBox2.Width = listBox1.Width;
            listBox3.Width = listBox1.Width;
            listBox1.Height = (height - 150 - 150 - 100);
            listBox2.Height = listBox1.Height;
            listBox3.Height = listBox2.Height;

            listBox1.Left = 50;
            listBox2.Left = listBox1.Left + listBox1.Width + 50;
            listBox3.Left = listBox2.Left + listBox2.Width + 50;

            
            pictureBox1.Width = width;
            pictureBox1.Height = height + 46;
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
        }

    }
}

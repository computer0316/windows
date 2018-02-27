using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExcelTest
{
    class Excel
    {
        public void creatExcel(string xlsfile)
        {
            if (File.Exists(xlsfile))
            {
                File.Delete(xlsfile);
            }
            //1.创建Applicaton对象
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();

            //2.得到workbook对象，打开已有的文件
            Microsoft.Office.Interop.Excel.Workbook ExcelBook = ExcelApp.Workbooks.Add(Missing.Value);

            //3.指定要操作的Sheet
            Microsoft.Office.Interop.Excel.Worksheet ExcelSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelBook.Sheets[1];

            //在第一列的左边插入一列  1:第一列
            //xlShiftToRight:向右移动单元格   xlShiftDown:向下移动单元格
            //Range Columns = (Range)xSheet.Columns[1, System.Type.Missing];
            //Columns.Insert(XlInsertShiftDirection.xlShiftToRight, Type.Missing);

            ExcelApp.Visible = true;
            //4.向相应对位置写入相应的数据
            ExcelSheet.Cells[1][1] = "this is a test.";

            //5.保存保存WorkBook
            //xBook.Save();
            ExcelApp.DisplayAlerts = false;
            ExcelBook.SaveAs(xlsfile);
            //6.从内存中关闭Excel对象

            ExcelSheet.SaveAs(xlsfile);
            ExcelSheet = null;

            ExcelBook.Close();
            ExcelBook = null;
            //关闭EXCEL的提示框
            ExcelApp.DisplayAlerts = false;
            //Excel从内存中退出

            ExcelApp.Quit();
            ExcelApp = null;
        }

        public void SaveResult(string ExcelFileName)
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


            xSheet.Cells[1][1] = "abc";
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

    }
}

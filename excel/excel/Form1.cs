using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;

namespace excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件|*.xls";
            DataSet ds = new DataSet();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //OpenExcelUseCom(dialog.FileName);
                
                ds = OpenExcelUseOledb4(dialog.FileName);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            //遍历一个表多行多列
            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                foreach (DataColumn mDc in ds.Tables[0].Columns)
                {
                    MessageBox.Show(mDr[mDc].ToString());
                }
            }

        }

        private DataSet OpenExcelUseOledb4(string strFileName)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileName + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        private DataTable OpenExcelUseOledb12(string strFileName)
        {
            string strConn = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + strFileName + "; Extended Properties = 'Excel 12.0;HDR=Yes;IMEX=1;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}$]", "Sheet1"), conn);
            DataSet ds = new DataSet();
            //将Excel里面有表内容装载到内存表中！
            DataTable tbContainer = new DataTable();
            oda.Fill(tbContainer);
            return tbContainer;
        }

    }
}

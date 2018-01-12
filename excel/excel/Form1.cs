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
using Microsoft.Office.Interop.Excel;

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
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //OpenExcelUseCom(dialog.FileName);
                DataSet ds = new DataSet();
                ds = OpenExcelUseOledb(dialog.FileName);
                dataGridView1.DataSource = ds.Tables[0];
                
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

        }

        private DataSet OpenExcelUseOledb(string strFileName)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileName + ";" + "Extended Properties=Excel 8.0;";
            //string strConn = "provider=Microsoft.ACE.OLEDB.12.0;extended properties='excel 12.0 Macro;hdr=yes';data source=" + strFileName; //& ThisWorkbook.FullName
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

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Lottery
{
    public partial class InputPassword : Form
    {
        public InputPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0)
            {
                // md5 part
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] data = Encoding.GetEncoding("GB2312").GetBytes(textBox1.Text);
                byte[] OutBytes = md5.ComputeHash(data);
                string OutString = "";
                for (int i = 0; i < OutBytes.Length; i++)
                {
                    OutString += OutBytes[i].ToString("x2");
                }

                string password = RocTools.ReadTXT("test.txt");
                if (password != OutString)
                {
                    MessageBox.Show("密码不对，程序无法执行。");
                    Environment.Exit(0);
                }
                else
                {
                    Expert expert = new Expert();
                    this.Close();                    

                }
            }
        }
    }
}

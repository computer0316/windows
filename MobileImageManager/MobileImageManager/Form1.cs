using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MobileImageManager
{
    public partial class Form1 : Form
    {
        public string CurrentFolder = "";
        public SortedList<DateTime, long> Files = new SortedList<DateTime, long>();

        public Form1()
        {
            InitializeComponent();                  
        }


        // 读取图片所在的文件夹
        private void GetImageFolder()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                CurrentFolder = folder.SelectedPath;
            }
        }


        // 把找到图片拍摄日期的图片文件移动到年份文件夹
        private void MoveFile(string FileName)
        {
            string FileDate = GetDateFromImg(FileName);
            if(FileDate=="" || FileDate.Length < 4)
            {
                return ;
            }
            string FileYear = FileDate.Substring(0, 4);
            string NewPath = CurrentFolder + "\\" + FileYear;

            if (!Directory.Exists(NewPath))
            {
                Directory.CreateDirectory(NewPath);
            }

            string NewFileName = GetNewFileName(NewPath, FileDate);
            File.Copy(FileName, NewFileName);
            if (File.Exists(NewFileName))
            {
                Counter(FileYear);
                File.Delete(FileName);
            }
        }

        private void Counter(string Year)
        {
            switch (Year)
            {
                case "2013":
                    textBox2013.Text = (int.Parse(textBox2013.Text) + 1).ToString();
                    break;
                case "2014":
                    textBox2014.Text = (int.Parse(textBox2014.Text) + 1).ToString();
                    break;
                case "2015":
                    textBox2015.Text = (int.Parse(textBox2015.Text) + 1).ToString();
                    break;
                case "2016":
                    textBox2016.Text = (int.Parse(textBox2016.Text) + 1).ToString();
                    break;
                case "2017":
                    textBox2017.Text = (int.Parse(textBox2017.Text) + 1).ToString();
                    break;
                case "2018":
                    textBox2018.Text = (int.Parse(textBox2018.Text) + 1).ToString();
                    break;
            }
        }

        // 根据时间生成一个有效的新文件名
        private string GetNewFileName(string Path, string FileDate)
        {
            int i = 1;
            while(File.Exists(Path + "\\" + FileDate + "-" + i.ToString().PadLeft(3, '0') + ".jpg"))
            {
                i++;
            }
            return Path + "\\" + FileDate + "-" + i.ToString().PadLeft(3, '0') + ".jpg";
        }

        // 取得图片文件的拍摄日期，返回：2017-02-22
        private string GetDateFromImg(string FileName)
        {
            try
            {
                Encoding ascii = Encoding.ASCII;
                string picDate;

                FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                Image image = Image.FromStream(stream, true, false);

                foreach (PropertyItem p in image.PropertyItems)
                {
                    listBox1.Items.Add(p.Id + " " + ascii.GetString(p.Value));
                    //获取拍摄日期时间
                    if (p.Id == 0x9003) // 0x0132 最后更新时间
                    {
                        stream.Close();

                        picDate = ascii.GetString(p.Value);
                        if ((!"".Equals(picDate)) && picDate.Length >= 10)
                        {
                            // 拍摄日期
                            picDate = picDate.Substring(0, 10);
                            picDate = picDate.Replace(":", "-");
                            image.Dispose();
                            return picDate;
                        }
                    }
                    stream.Close();
                }
                picDate = File.GetLastWriteTime(FileName).ToLongDateString().Replace('/', '-').Substring(0, 10);
                return picDate;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void CheckFileSize(string FileName)
        {
            FileInfo fileInfo = new FileInfo(FileName);
            try
            {
                Files.Add(DateTime.Parse(GetDateFromImg(FileName)), fileInfo.Length);
            }
            catch(ArgumentException ae)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetImageFolder();
            string[] files = Directory.GetFiles(CurrentFolder);
            foreach(string FileName in files)
            {
                Application.DoEvents();
                listBox1.Items.Clear();
                labelFileName.Text = FileName;
                MoveFile(FileName);
            }
        }
    }
}

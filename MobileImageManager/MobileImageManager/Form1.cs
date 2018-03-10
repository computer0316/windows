using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
>>>>>>> 1d5867f139275c3a2c6b275f6b8b6252992d15d1
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MobileImageManager
{
    public partial class Form1 : Form
    {
        // 用于保存照片所在的路径
        public string CurrentFolder = "";
        public SortedList<DateTime, long> Files = new SortedList<DateTime, long>();

        public Form1()
        {
<<<<<<< HEAD
            InitializeComponent();
            MessageBox.Show(FormatDate("2018-3-25 19:26", "yyyy-MM-dd"));
            Environment.Exit(0);
=======
            InitializeComponent();                  
>>>>>>> 1d5867f139275c3a2c6b275f6b8b6252992d15d1
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

        // 显示不同年份的照片数量
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
        // 文件名是日期+序号形式，序号最大999，也就是同一天内不能超过999张照片
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
                // 从 exif 信息中提取拍摄日期
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
                // 如果没有能从 exif 中提取拍摄日期，则返回文件的最后修改日期
                picDate = File.GetLastWriteTime(FileName).ToLongDateString().Replace('/', '-').Substring(0, 10);
                return picDate;
            }
            catch (Exception)
            {
                return "";
            }
        }

<<<<<<< HEAD
        // 把日期转换成需要的格式
        private string FormatDate(string dateTime, string format)
        {
            if (!DateTime.TryParse(dateTime, out DateTime result)) {
                return null;
            }
            string strTime = null;
            string Year = result.Year.ToString();
            string Month = result.Month.ToString();
            string Day = result.Day.ToString();
            string Hour = result.Hour.ToString();
            string Minute = result.Minute.ToString();
            string Second = result.Second.ToString();
            switch (format)
            {
                case "yyyy-MM-dd":
                    strTime = Year + "-" + Month + "-" + Day;
                    break;
                case "yyyy-MM-dd-hh-mm-ss":
                    strTime = Year + "-" + Minute + "-" + Day + "-" + Hour + "-" + Minute + "-" + Second;
                    break;
                case "yyyymmdd":
                    strTime = Year + Month + Day;
                    break;
                case "Year":
                    strTime = Year;
                    break;
                case "Month":
                    strTime = Month;
                    break;
                default:
                    strTime = null;
                    break;
            }
            return strTime;
        }

        // 保存照片时间，大小数据，用于后续的照片查重
        private void SaveImgAttributes(DateTime time, long length)
        {

=======
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
>>>>>>> 1d5867f139275c3a2c6b275f6b8b6252992d15d1
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

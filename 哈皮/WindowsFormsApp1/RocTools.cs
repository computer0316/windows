using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class RocTools
    {
        // 读取一个文本文件
        public static string ReadTXT(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string str = "";
            while ((line = sr.ReadLine()) != null)
            {
                str += line;
            }
            return str;
        }

        // 写入一个文本文件
        public static void WriteTXT(string str, string path, FileMode fileMode)
        {
            FileStream fs = new FileStream(path, fileMode);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        public static string DateTimeFileName()
        {
            string filename = DateTime.Now.Year.ToString(); //获取年份  // 2008
                filename += DateTime.Now.Month.ToString(); //获取月份   // 9
                filename += DateTime.Now.Day.ToString(); //获取日   // 248
                filename += DateTime.Now.Hour.ToString(); //获取小时   // 20
                filename += DateTime.Now.Minute.ToString(); //获取分钟   // 31
                filename += DateTime.Now.Second.ToString(); //获取秒数   // 45
            return filename;
        }
        

        // 读取文本文件填充入 ArrayList
        public static ArrayList File2Array(string filename)
        {
            if (File.Exists(filename))
            {
                StreamReader streamReader = new StreamReader(filename, Encoding.Default);
                ArrayList arrayList = new ArrayList();
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    if (line.Length > 1)
                    {
                        arrayList.Add(line);
                    }
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
                return arrayList;
            }
            else
            {
                return null;
            }
        }

        // 值复制 ArrayList 到另外一个
        public static ArrayList ArrayListCopy(ArrayList arrayList)
        {
            ArrayList newArrayList = new ArrayList();
            foreach(object o in arrayList)
            {
                newArrayList.Add(o.ToString());
            }
            return newArrayList;
        }

        // 连接两个 Arraylist
        public static ArrayList Merge(ArrayList Al1, ArrayList Al2)
        {
            // 找到两个数组中更长的那一个，把长度付给：AlCount
            int AlCount = Al1.Count;
            if (Al2.Count > AlCount)
            {
                AlCount = Al2.Count;
            }
            ArrayList NewAl = new ArrayList();

            // 根据较长数组的大小，依次读取两个数组元素
            for (int i = 0; i < AlCount; i++)
            {
                string s1 = string.Empty;
                string s2 = string.Empty;
                // 依次逐一读取两个数组中的条目，如果某个数组读取出错（因为该数组较短，该对应位置没有数组元素了）
                // 则赋值一个空字符串
                try
                {
                    s1 = Al1[i].ToString();
                }
                catch
                {
                    s1 = string.Empty;
                }
                try
                {
                    s2 = Al2[i].ToString();
                }
                catch
                {
                    s2 = string.Empty;
                }
                string s3 = s1 + "\t" + s2;
                NewAl.Add(s3.TrimEnd('\t'));

            }

            return NewAl;
        }

        // 用于保存 ArrayList 类型的数据到一个文本文件
        public static void SaveArraylist(string filename, ArrayList al)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (object o in al)
            {
                string s = o.ToString();
                //s = s.Replace(" ", ":");
                //s = s.Replace("　", ":");
                //s = s.TrimEnd(new char[] { ':', ' ', '　' });
                sw.WriteLine(s);
            }
            sw.Close();
        }

        // 把两个字的名字中间插一个全角空格变成三个字
        public static ArrayList Name2to3(ArrayList al)
        {
            ArrayList newAl = new ArrayList();
            foreach (object o in al)
            {
                if (o.ToString().IndexOf('\t') > 2)
                {
                    newAl.Add(o);
                }
                else
                {
                    newAl.Add(o.ToString().Substring(0, 1) + "　" + o.ToString().Substring(1));
                }
            }
            return newAl;
        }
    }
}

using System.Collections;
using System.IO;
using System;
using System.Text;
using Microsoft.Office.Interop.Word;


namespace Lottery
{
    class MyClass
    {
        // 把没有抽中的人员排列在后面
        public static ArrayList SortAl(ArrayList Al)
        {
            ArrayList WeiZhongAl = new ArrayList();
            ArrayList YiZhongAl = new ArrayList();
            int no = 1;
            foreach (object o in Al)
            {
                if (o.ToString().Length > 25)
                {
                    YiZhongAl.Add(o);
                }
                else
                {
                    WeiZhongAl.Add(o);
                }
            }
            ArrayList RandYiZhongAl = MyRandom(YiZhongAl);
            ArrayList ResultAl = new ArrayList();
            int i = 0;
            foreach (object o in RandYiZhongAl)
            {
                i++;
                ResultAl.Add(i.ToString() + ":" + o.ToString());
            }
            foreach (object o in WeiZhongAl)
            {
                i++;
                ResultAl.Add(i.ToString() + ":\t" + o.ToString());
            }
            return ResultAl;
        }

        // 合并房屋和储藏间
        public static ArrayList MergeCloset(ArrayList HouseAl, ArrayList ClosetAl)
        {
            ArrayList NewAl = new ArrayList();
            bool match = false;

            for(int i=0;i<HouseAl.Count;i++)
            {
                match = false;
                for (int j = 0; j < ClosetAl.Count; j++)
                {
                    if (HouseAl[i].ToString().Substring(0, 1) == ClosetAl[j].ToString().Substring(0, 1))
                    {
                        try
                        {
                            NewAl.Add(HouseAl[i].ToString() + "\t" + ClosetAl[j].ToString());
                            ClosetAl.Remove(ClosetAl[j]);
                            match = true;
                            break;                            
                        }
                        catch
                        {
                            //NewAl.Add(HouseAl[i].ToString());
                        }
                    }
                }
                if (!match)
                {
                    NewAl.Add(HouseAl[i].ToString());
                }
            }
            return NewAl;
        }

        // 合并两个 Arraylist
        public static ArrayList Merge(ArrayList Al1, ArrayList Al2)
        {
            int AlCount = Al1.Count;
            if (Al2.Count > AlCount)
            {
                AlCount = Al2.Count;
            }
            ArrayList NewAl = new ArrayList();

            for (int i = 0; i < AlCount; i++)
            {
                string s1 = string.Empty;
                string s2 = string.Empty;
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
                string s3 =  s1 + "\t" + s2;
                NewAl.Add(s3.TrimEnd('\t'));

            }
        
            return NewAl;
        }

        // 把给定的 ArrayList 乱序后返回
        public static ArrayList MyRandom(ArrayList al)
        {
            Random rand = new Random();
            Random r = new Random(rand.Next(0, 100000000));
            
            ArrayList NewAl = new ArrayList();
            int alCount = al.Count;

            // 循环原始的序列，每次随机读取一项，添加到新序列，把该项从原始序列删除
            for (int i = 0; i < alCount; i++)
            {
                int index = r.Next(0, al.Count);
                NewAl.Add(al[index]);
                al.Remove(al[index]);
            }
            return NewAl;
        }

        // 用于读取要乱序的文件
        public static ArrayList File2Array(string filename)
        {
            StreamReader sr = new StreamReader(filename,Encoding.Default);
            ArrayList al = new ArrayList();
            string l = sr.ReadLine();
            while (l != null)
            {
                al.Add(l);
                l = sr.ReadLine();
            }
            sr.Close();
            return al;
        }

        // 用于保存 ArrayList 类型的数据到一个文本文件
        public static void SaveData(string filename, ArrayList al)
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

        public static void SaveData(string filename, ArrayList al)
        {
            Application wordApp = new ApplicationClass();
            //Document wordDoc = wordApp.Documents.Add(
        }
    }
}

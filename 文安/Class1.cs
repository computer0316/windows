using System.Collections;
using System.IO;
using System;
using System.Text;
using MSWord = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace Lottery
{
    class MyClass
    {
        // 把没有抽中的人员排列在后面
        public static ArrayList SortAl(ArrayList Al)
        {
            ArrayList ResultAl = new ArrayList();
            Al = MyRandom(Al);
     
            int i = 0;
            foreach (object o in Al)
            {
                i++;
                ResultAl.Add(i.ToString().PadLeft(3, '0') + ":" + o.ToString());
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

        public static void Print2Word(string strContent)
        {
            object path;                              //文件路径变量
            MSWord.Application wordApp;                   //Word应用程序变量 
            MSWord.Document wordDoc;                  //Word文档变量

            path = "d:\\result\\MyWord_Print.doc";
            wordApp = new MSWord.ApplicationClass(); //初始化

            wordApp.Visible = true;//使文档可见

            //如果已存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            Object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            // 页面设置
            wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
            wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientPortrait;//排列方式为垂直方向
            wordDoc.PageSetup.TopMargin = 48.0f;
            wordDoc.PageSetup.BottomMargin = 48.0f;
            wordDoc.PageSetup.LeftMargin = 57.0f;
            wordDoc.PageSetup.RightMargin = 57.0f;
            wordDoc.PageSetup.HeaderDistance = 50.0f;//页眉位置
            wordDoc.PageSetup.FooterDistance = 80f;

            //设置页眉
            wordApp.ActiveWindow.View.Type = MSWord.WdViewType.wdPrintView; //普通视图（即页面视图）样式
            wordApp.ActiveWindow.View.SeekView = MSWord.WdSeekView.wdSeekPrimaryHeader;//进入页眉设置，其中页眉边距在页面设置中已完成
            MSWord.Range allRange = wordDoc.Range();

            foreach (MSWord.Section wordSection in wordApp.ActiveDocument.Sections)
            {
                MSWord.Range headerRange = wordSection.Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Font.Size = 20;
                headerRange.Text = "文安县2017年公共住房公开配租摇号结果";

                MSWord.Range footerRange = wordSection.Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.Font.Size = 16;
                footerRange.Text = "\n监察局：\t\t公证处：\n\n\n群众代表：";
            }
            //wordApp.Selection.HeaderFooter.Range.Text = "这里是测试";

            //去掉页眉的横线
            //wordApp.ActiveWindow.ActivePane.Selection.ParagraphFormat.Borders[MSWord.WdBorderType.wdBorderBottom].LineStyle = MSWord.WdLineStyle.wdLineStyleNone;
            //wordApp.ActiveWindow.ActivePane.Selection.Borders[MSWord.WdBorderType.wdBorderBottom].Visible = false;            
            wordApp.ActiveWindow.ActivePane.View.SeekView = MSWord.WdSeekView.wdSeekMainDocument;//退出页眉设置

            //为当前页添加页码
            MSWord.PageNumbers pns = wordApp.Selection.Sections[1].Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
            pns.NumberStyle = MSWord.WdPageNumberStyle.wdPageNumberStyleArabicFullWidth; //设置页码的风格，是Dash形还是圆形的            
            pns.HeadingLevelForChapter = 0;
            pns.IncludeChapterNumber = true;
            pns.RestartNumberingAtSection = false;
            pns.StartingNumber = 0; //开始页页码？
            object pagenmbetal = MSWord.WdPageNumberAlignment.wdAlignPageNumberCenter;//将号码设置在中间
            object first = true;
            wordApp.Selection.Sections[1].Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);



            // 行间距与缩进、文本字体、字号、加粗、斜体、颜色、下划线、下划线颜色设置
            object unite = MSWord.WdUnits.wdStory;
            wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
            wordApp.Selection.ParagraphFormat.LineSpacing = 20f;//设置文档的行间距
            wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度
            wordApp.Selection.EndKey(ref unite, ref Nothing);
            wordDoc.Paragraphs.Last.Range.Font.Size = 16;
            wordDoc.Paragraphs.Last.Range.Font.Name = "宋体";
            //写入普通文本
            wordDoc.Paragraphs.Last.Range.Text = strContent;

            object myNothing = System.Reflection.Missing.Value;
            object myFileName = path;
            object myWordFormatDocument = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
            object myLockd = false;
            object myPassword = "";
            object myAddto = true;
            wordApp.ActiveDocument.SaveAs2(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
                     ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
                     ref myNothing, ref myNothing, ref myNothing);



            //wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //关闭wordApp组件对象
            //wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);

        }

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
                    newAl.Add(o.ToString().Substring(0,1) + "　" + o.ToString().Substring(1));
                }
            }
            return newAl;
        }

    }
}

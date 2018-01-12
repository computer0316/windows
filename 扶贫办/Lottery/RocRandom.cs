using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Lottery
{
    class RocRandom
    {
        // 把给定的 ArrayList 乱序后返回
        public static ArrayList MyRandom(ArrayList oldArrayList)
        {
            Random rand = new Random();
            Random r = new Random(rand.Next(0, 100000000));

            ArrayList newArrayList = new ArrayList();
            int alCount = oldArrayList.Count;

            // 循环原始的序列，每次随机读取一项，添加到新序列，把该项从原始序列删除
            for (int i = 0; i < alCount; i++)
            {
                int index = r.Next(0, oldArrayList.Count);
                newArrayList.Add(oldArrayList[index]);
                oldArrayList.Remove(oldArrayList[index]);
            }
            return newArrayList;
        }
    }
}

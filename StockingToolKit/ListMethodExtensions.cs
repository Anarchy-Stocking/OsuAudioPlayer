using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockingToolKit
{
    public static class ListMethodExtensions
    {
        static Random rng = new();
        
        public static void Randomize<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void OrderByName<T>(this List<T> list, bool DecendFlag = true)
        {
                list.Sort();
                if(DecendFlag)
                    list.Reverse();
        }
    }
}

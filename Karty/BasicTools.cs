using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public static class BasicTools
    {
        public static void Swap<T,K>(ref T a,T b,ref K aK,K bK) where K:IComparable<K>
        {
            if (aK.CompareTo(bK)<0)
            {
                a = b;
                aK = bK;
            }
        }
        public static void Swap<T, K>(ref T a, T b, Comparer<T> comparer)
        {
            if (comparer.Compare(a,b) < 0)
            {
                a = b;
            }
        }
    }
}

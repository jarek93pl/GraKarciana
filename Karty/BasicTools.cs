using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public static class BasicTools
    {
        public static void SetMax<T,K>(ref T a,T b,ref K aK,K bK) where K:IComparable<K>
        {
            if (aK.CompareTo(bK)<0)
            {
                a = b;
                aK = bK;
            }
        }
        public static void SetMin<T, K>(ref T a, T b, ref K aK, K bK) where K : IComparable<K>
        {
            if (aK.CompareTo(bK) > 0)
            {
                a = b;
                aK = bK;
            }
        }
        public static void SetMax<T>(ref T a, T b) where T : IComparable<T>
        {
            SetMax(ref a, b, ref a, b);
        }
        public static void Setmin<T>(ref T a, T b) where T : IComparable<T>
        {
            SetMin(ref a, b, ref a, b);
        }
    }
}

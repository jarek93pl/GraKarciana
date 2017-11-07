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
        public static void SetMin<T>(ref T a, T b) where T : IComparable<T>
        {
            SetMin(ref a, b, ref a, b);
        }
        public static T[] InitializeTable<T>(int count, T value)
        {
            T[] z = new T[count];
            for (int i = 0; i < count; i++)
            {
                z[i] = value;
            }
            return z;
        }
        public static T GetMin<T,K>(this IEnumerable<T> t,Func<T,K> func) where K : IComparable<K>
        {
            K minKey = func(t.First());
            T returned = t.First();
            foreach (var item in t)
            {
                K tmpK = func(item);
                if (minKey.CompareTo(tmpK)>0)
                {
                    minKey = tmpK;
                    returned = item;
                    
                }
            }
            return returned;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace GraKarciana
{
    public static class RandCards
    {
        [ThreadStatic]
        static Random rw;
        public static Random Random
        {
            get
            {
                rw = rw ?? new Random();
                return rw;
                
            }
        }
        public static List<T> RandAndDelete<T>(this IList<T> wejście,int IloścElementów)
        {
            List<T> zk = new List<T>(IloścElementów);
            for (int i = 0; i < IloścElementów; i++)
            {
                int nr = Random.Next(wejście.Count);
                T z = wejście[nr];
                zk.Add(z);
                wejście.RemoveAt(nr);
            }
            return zk;
        }
        public static void Forech<T>(this IEnumerable<T> r,Action<T> a)
        {
            foreach (var item in r)
            {
                a(item);
            }
        }
       public static int FindIndex<T>(this IEnumerable<T> obj, T szukany)
        {
            int Nr = 0;
            foreach (var item in obj)
            {
                if (szukany.Equals(item))
                {
                    return Nr;
                }
                Nr++;
            }
            return -1;
        }
    
    public static int FindIndex<T>(this IEnumerable<T> obj, Func<T,bool> szukany)
    {
        int Nr = 0;
        foreach (var item in obj)
        {
            if (szukany(item))
            {
                return Nr;
            }
            Nr++;
        }
        return -1;
    }
    public static int FindMaxIndex<T>(this IEnumerable<T> obj)where T:IComparable<T>
        {
            int n = 0, z = 0;
            T Nr = obj.First();
            foreach (var item in obj)
            {
                if (item.CompareTo(Nr)>0)
                {
                    n = z;
                    Nr = item;
                }
                z++;
            }return n;
        }
        public static void RemoveAll<T>(this List<T> z,IEnumerable<T> dousuniecia)
        {
            HashSet<T> dousunieciahashest = new HashSet<T>(dousuniecia);
            z.RemoveAll(X => dousuniecia.Contains(X));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace GraKarciana
{
    public static class Losowanie
    {
        [ThreadStatic]
        static Random rw;
        public static Random r
        {
            get
            {
                rw = rw ?? new Random();
                return rw;
                
            }
        }
        public static List<T> Wylosuje<T>(this IList<T> wejście,int IloścElementów)
        {
            List<T> zk = new List<T>(IloścElementów);
            for (int i = 0; i < IloścElementów; i++)
            {
                int nr = r.Next(wejście.Count);
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
        public static int FindIndex<T>(this IEnumerable<T> obj,T szukany)
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
        public static void RemoveAll<T>(this List<T> z,IEnumerable<T> dousuniecia)
        {
            HashSet<T> dousunieciahashest = new HashSet<T>(dousuniecia);
            z.RemoveAll(X => dousuniecia.Contains(X));
        }
    }
}
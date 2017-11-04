using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace Komputer.Matematyczne
{
    public class WarjacjeBezPowtózeńRóznaDługość<T>:IEnumerable<T[]>
    {
        static int[] PotegiLidrzby2 = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576, 2097152, 4194304, 8388608, 16777216, 33554432, 67108864, 134217728, 268435456, 536870912 };
        List<T[]> TablicaRozwiozań = new List<T[]>();
        /// <summary>
        /// Kreuje kolekcje TablicaRozwiozań
        /// dla ({1,3},5)
        /// {1},{3},{1,3}
        /// </summary>
        /// <param name="typy">lidrzby które bedą wybierane </param>
        /// <param name="ograniczonaDługość"> ogranicza możliwość długość tablicy w wyniku</param>
        public WarjacjeBezPowtózeńRóznaDługość(IList<T> typy, int ograniczonaDługość)
        {
            for (int i = 0; i < PotegiLidrzby2[typy.Count]; i++)
            {
                int g = i;
                bool Prawidłowy = true;
                bool[] Tab = new bool[typy.Count];
                byte IlośćDodanych = 0;
                for (int ii = 0; ii < typy.Count; ii++)
                {
                    if (g % 2 == 0)
                    {
                        if (IlośćDodanych == ograniczonaDługość)
                        {
                            Prawidłowy = false;
                            break;
                        }
                        Tab[ii] = true;
                        IlośćDodanych++;
                    }

                    g /= 2;
                }
                if (Prawidłowy)
                {
                    T[] r = new T[IlośćDodanych];
                    int d = 0;
                    for (int dsd = 0; dsd < typy.Count; dsd++)
                    {
                        if (Tab[dsd])
                        {
                            r[d] = typy[dsd];
                            d++;

                        }
                    }
                    TablicaRozwiozań.Add(r);

                }
            }

        }
        public T[] this[int indexer]
        {
            get
            {
                if (indexer < 0 || indexer >= TablicaRozwiozań.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return TablicaRozwiozań[indexer];
            }
        }
        public int Długość
        {
           
            get { return TablicaRozwiozań.Count; }
        }
        

       public IEnumerator<T[]> GetEnumerator()
       {
           return TablicaRozwiozań.GetEnumerator();
       }

       System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
       {
           return TablicaRozwiozań.GetEnumerator();
       }
    }
    public static class Rozszeżenie
    {
        public static IEnumerable<List<T>> Rozszeż<T>(this IEnumerable<T> tablicaWatiacji, int MiejsceWłożenia, IList<T> tr)
        {
            WarjacjeBezPowtózeńRóznaDługość<T> Wb = new WarjacjeBezPowtózeńRóznaDługość<T>(tr, tr.Count);
            foreach (T[] Tablic in Wb)
            {
                int i = 0;
                List<T> zwr = new List<T>();
                foreach (T item in tablicaWatiacji)
                {
                    if (i == MiejsceWłożenia)
                    {
                        foreach (T item2 in Tablic)
                        {
                            zwr.Add(item2);
                        }
                    }
                    zwr.Add(item);
                    i++;
                }
                if (i == MiejsceWłożenia)
                {
                    foreach (T item2 in Tablic)
                    {
                        zwr.Add(item2);
                    }
                }
                yield return zwr;

            }
        }
    }
   
}

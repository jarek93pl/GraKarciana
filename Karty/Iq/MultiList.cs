using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public class MultiList<T,K>
    {
        List<T> list = new List<T>();
        List<int> indexes = new List<int>();
        Func<T, IList<K>> Tranform;
        public MultiList(Func<T, IList<K>> Tranform, params T[] date):this(Tranform,date.ToList())
        {
        }

        public MultiList(Func<T, IList<K>> Tranform,IEnumerable<T> list)
        {
            this.Tranform = Tranform;
            foreach (var item in list)
            {
                var TmpList = Tranform(item);
                int I = 0;
                foreach (var item2 in TmpList)
                {
                    this.list.Add(item);
                    indexes.Add(I++);
                }
            }
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public T GetParrent(int l)
        {
            return list[l];
        }
        public K this[int index]
        {
            get {
                return Tranform(list[index])[indexes[index]];
            }

            set { Tranform(list[index])[indexes[index]] = value; }
        }
    }
}

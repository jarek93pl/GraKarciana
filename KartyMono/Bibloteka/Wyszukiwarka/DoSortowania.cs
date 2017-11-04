using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komputer.Sortowanie
{

    public enum Sortowanie { Rosnąco, Malejąco };
    public class DoSortowania<T> : IComparable<DoSortowania<T>>
    {
        public T Obiekt;
        public int Wartość;
        public int CompareTo(DoSortowania<T> other)
        {
            return Wartość - other.Wartość;
        }
    }
}

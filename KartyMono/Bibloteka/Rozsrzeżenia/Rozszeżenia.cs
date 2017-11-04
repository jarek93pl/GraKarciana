using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komputer
{
    public static class Rozszeżenia
    {
        public static T Znajdź<T>(this IEnumerable<T> Przeszókiwana,T Szukany)
        {
            foreach (T item in Przeszókiwana)
            {
                if (item.Equals(Szukany))
                    return item;
            }
            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komputer.IQ
{
    public interface ObsugaPołączenia<T>
    {
        bool SprawdźCzyJest(T Obiekt);
        void Dodaj(T Obiekt);
    }
}

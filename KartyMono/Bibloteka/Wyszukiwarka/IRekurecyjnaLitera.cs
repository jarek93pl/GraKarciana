using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komputer.Przeszukiwacz
{
    public enum RodzajZakończenia { Koniec,Niedopasowany,JeszczeRaz};
    public abstract class RekurecyjnaLitera<L>
    {
        public RekurecyjnaLitera(bool b)
        {
            Aktywny = b;
        }
        public readonly bool Aktywny;
        public abstract RodzajZakończenia SprawdźKoniec(L item);
    }
}

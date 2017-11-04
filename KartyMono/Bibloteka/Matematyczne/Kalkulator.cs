using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komputer.Matematyczne
{
    public class Kalkulator : Funkcje<decimal>
    {
        public Kalkulator(string s)
            : base( new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' }, (string Dana) => { return Convert.ToDecimal(Dana); })
        {
            WczytajDomyślnePolecenie();
            Kompiluj(s);
        }
        private void WczytajDomyślnePolecenie()
        {
            DodajSymbol(new Komeda1Argumetowa((decimal[] a) => { return -a[0]; }, 5, '-'));
            DodajSymbol(new Komeda1Argumetowa((decimal[] a) => { decimal b = 1m; if (a[0] % 1 != 0)throw new Exception("silnia mus być naturalna"); while (a[0] > 1) { b *= a[0]; a[0]--; } return b; }, 3, '!'));
            DodajSymbol(new Komeda2Argumetowa((decimal[] a) => { decimal c = 1; if (a[1] < 0) { a[0] = 1 / a[0]; a[1] = -a[1]; }; for (decimal d = 0; d < a[1]; d++) { c *= a[0]; } return c; }, 3, '^'));
            DodajSymbol(new Komeda2Argumetowa((decimal[] a) => { return a[0] * a[1]; }, 2, '*'));
            DodajSymbol(new Komeda2Argumetowa((decimal[] a) => { return a[0] / a[1]; }, 2, '/'));
            DodajSymbol(new Komeda2Argumetowa((decimal[] a) => { return a[0] + a[1]; }, 1, '+'));
        }
    }
}
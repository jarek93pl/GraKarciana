using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komputer.Matematyczne.Silnik
{
    public class RelacjiNiesymetrycznej
    {
        static readonly ulong Maska1 = uint.MaxValue;
        public ulong KluczPołączeń = 0;
        
        private void DodajWraźliwość(int r)
        {
            KluczPołączeń |= (1UL << (r + 32));
        }
        private void DodajWykrywanie(int r)
        {
            KluczPołączeń |= (1UL << (r));
        }
        public void DodajWraźliwości(params int[] r)
        {
            foreach (int item in r)
            {
                DodajWraźliwość(item);
            }
        }
        public void DodajWykrywania(params int[] r)
        {
            foreach (int item in r)
            {
                DodajWykrywanie(item);
            }
        }
        public bool Kolizja(IPrzestrzeniKolizji k)
        {


            bool b = (KluczPołączeń & ((k.wrażliwośćKolizji.KluczPołączeń & Maska1) << 32)) != 0; ;
            return b;
        }
    }
}

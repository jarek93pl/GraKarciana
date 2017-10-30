using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty.Matematyka
{
    public static class Wariancja
    {
        
        public static IEnumerable<int[]> WarjancjaJakaśTam(int Głebokośc, int MaksymalnaWartość)
        {
            List<int> Tb = new List<int>(MaksymalnaWartość);
            int Poziom = 0;
            int[] LicznikiPetli = new int[Głebokośc];
            int[] zw = new int[Głebokośc];
            for (int i = 0; i < MaksymalnaWartość; i++)
            {
                Tb.Add(i);
            }

            PoczątekPetli:
            if (Poziom + 1 == Głebokośc)
            {
                foreach (var item in Tb)
                {
                    zw[Poziom] = item;
                    yield return zw;
                }
                goto KoniecPetli;
            }
            int Ograniczenie = MaksymalnaWartość - Poziom;
            int LN = LicznikiPetli[Poziom];
            if (LN < Ograniczenie)
            {
                zw[Poziom] = Tb[LN];
                Tb.RemoveAt(LN);
                Poziom++;
                goto PoczątekPetli;
            }
            else
            {
                goto KoniecPetli;
            }
            KoniecPetli:
            Poziom--;
            if (Poziom != -1)
            {

                Tb.Insert(LicznikiPetli[Poziom], zw[Poziom]);
                LicznikiPetli[Poziom]++;
                LicznikiPetli[Poziom + 1] = 0;
                goto PoczątekPetli;
            }


        }
    }
}

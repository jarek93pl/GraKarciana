using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraKarciana.ObsugaTysiąc;
using GraKarciana;
namespace GraKarciana
{
    public class ComparerTysioc : IComparer<GraKarciana.Karta>
    {
        bool AktywnośćKozery;
        Karta KolorZaczety;
        Karta KolorKozera;
        public ComparerTysioc(Karta KartaPoczotkowa)
        {
            this.KolorZaczety = KartaPoczotkowa.Kolor();
        }
        public ComparerTysioc(Karta KolorZaczety,Karta Kozera):this(KolorZaczety)
        {
            this.KolorKozera = Kozera.Kolor();
            AktywnośćKozery = true;
        }
        public int Compare(Karta x, Karta y)
        {
            int Ix = ZważKarte(x);
            int Iy = ZważKarte(y);
            return Ix - Iy;
        }
        const int MnożnikKozery = 1000000, MnożnikZaczetej = 1000;
        public int ZważKarte(Karta x)
        {
            int K = PunktacjaTysiąca(x);
            K++;
            if (x.Kolor(KolorKozera)&&AktywnośćKozery)
            {
                return K * MnożnikKozery;
            }
            if (x.Kolor(KolorZaczety))
            {
                return K * MnożnikZaczetej;
            }
            return K;
        }
        public static IComparer<GraKarciana.Karta> GetComparer(Karta firstCardInTable,bool enebleAtute,Karta atutesuit)
        {
            if (enebleAtute)
            {
                return new ComparerTysioc(firstCardInTable,atutesuit);
            }
            else
            {
                return new ComparerTysioc(firstCardInTable);
            }

        }
    } 
}

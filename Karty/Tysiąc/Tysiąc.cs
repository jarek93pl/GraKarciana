using System;
using System.Collections.Generic;
using System.Linq;

namespace GraKarciana
{
    public class  ObsugaTysiąc
    {
        static int[] PunktyKart1000 = { 0, 10, 2, 3, 4, 11 };
        static int[] PunktyMeldunków= {60,80,100,40 };
        public static int WartościMeldunków(Karta k)
        {
            return PunktyMeldunków[(int)k.Kolor()];
        }
        public static int ScoreInTable(Karta[] stół)=> stół.Sum(X => ObsugaTysiąc.PunktacjaTysiąca(X));
        
        public static int FindWinner(bool enebleTrump,Karta trumpSuit,int indexPlayer,Karta[] Table)
        {
            ComparerTysioc porówjnaj = null;
            if (enebleTrump)
            {
                porówjnaj = new ComparerTysioc(Table[indexPlayer], trumpSuit);
            }
            else
            {
                porówjnaj = new ComparerTysioc(Table[indexPlayer]);
            }
            var Kopiastołu = (Karta[])Table.Clone();
            Array.Sort(Kopiastołu, porówjnaj);
            var maxkarta = Kopiastołu.Last();
            return Table.FindIndex(maxkarta);

        }
        public static bool IstniejeMeldunek(Karta Dama, IEnumerable<Karta> kw)
        {
            if (Dama.PobierzKarte()!=Karta.Dama)
            {
                return false;
            }
            Karta król = (Karta)((Karta.pik & Dama) + (int)Karta.Król);
            return kw.Any(X => król == X);

        }
        public static int PunktacjaTysiąca(Karta k)
        {
            int karta = (int)k;
            karta -= (int)Karta.K9;
            karta /= 4;
            return PunktyKart1000[karta];
        }
        public static List<Karta> ZaładujDostepneKarty(List<Karta> twojeKarty, List<Karta> stół,bool AktywnaKozera, Karta Kozera)
        {
            if (stół.Count == 0)
            {
                return twojeKarty;
            }
            else
            {
                List<Karta> KartyDostepneWturze = twojeKarty.Where(X => X.Kolor() == stół.First().Kolor()).ToList();
                if (KartyDostepneWturze.Count != 0)
                {
                    ComparerTysioc cp = new ComparerTysioc(stół.First());
                    List<Karta> KartyPosotowane = new List<Karta>(stół);
                    KartyPosotowane.Sort(cp);
                    Karta Najwiejsze = KartyPosotowane.Last();
                    var Wieksze = KartyDostepneWturze.Where(X => cp.Compare(Najwiejsze, X) < 0).ToList();
                    if (Wieksze.Count != 0)
                    {
                        return  Wieksze;
                    }
                    else
                    {
                        return KartyDostepneWturze;
                    }
                }
                else
                {
                    if (AktywnaKozera)
                    {
                        ComparerTysioc cp = new ComparerTysioc(stół.First(), Kozera);
                        List<Karta> KartyPosotowane = new List<Karta>(stół);
                        KartyPosotowane.Sort(cp);
                        Karta Najwiejsze = KartyPosotowane.Last();
                        var Wieksze = twojeKarty.Where(X => cp.Compare(Najwiejsze, X) < 0).ToList();
                        if (Wieksze.Count == 0)
                        {
                            return twojeKarty;
                        }
                        else
                        {
                            return  Wieksze;
                        }

                    }
                    else
                    {
                        return twojeKarty;
                    }
                }

            }

        }
    }
}

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
        public static IEnumerable<Karta> GetAllQueenIfMariage(IEnumerable<Karta> kw)
        {
            int[] CountMariageCard = new int[4];
            foreach (var item in kw)
            {
                var k = item.PobierzKarte();
                if (k==Karta.Król||k==Karta.Dama)
                {
                    CountMariageCard[(int) item.Kolor()]++;
                }
            }
            for (int i = 0; i < CountMariageCard.Length; i++)
            {
                if (CountMariageCard[i]==2)
                {
                    yield return ObsugaKart.StwórzKarte(Karta.Dama,(Karta) i);
                }
            }
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

        internal static bool LastWin(List<Karta> table, bool enebleAtute, Karta atute, out bool usingAtute, out bool usingColor)
        {
            Karta last = table.Last();
            List<Karta> tablec = table.Select(X=>X).ToList();
            tablec.Sort(enebleAtute? new ComparerTysioc(table.First(),atute): new ComparerTysioc(table.First()));
            usingAtute = last.Kolor() == atute&&enebleAtute;
            usingColor = last.Kolor() == table.First().Kolor();
            return last == table.Last();
        }

        public static int PunktacjaTysiąca(Karta k)
        {
            int karta = (int)k;
            karta -= (int)Karta.K9;
            karta /= 4;
            return PunktyKart1000[karta];
        }
        public static List<Karta> ZaładujDostepneKarty(List<Karta> twojeKarty, List<Karta> stół, bool AktywnaKozera, Karta Kozera)
        {
            return ZaładujDostepneKartyWitchResult(twojeKarty, stół, AktywnaKozera, Kozera,out ResultMoveGame _);
        }
        public static List<Karta> ZaładujDostepneKartyWitchResult(List<Karta> twojeKarty, List<Karta> stół,bool AktywnaKozera, Karta Kozera,out ResultMoveGame resultMove)
        {
            if (stół.Count == 0)
            {
                resultMove = ResultMoveGame.EmptyTable;
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
                        resultMove = ResultMoveGame.Win;
                        return  Wieksze;
                    }
                    else
                    {
                        resultMove = ResultMoveGame.Lose;
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
                            resultMove = ResultMoveGame.Lose;
                            return twojeKarty;
                        }
                        else
                        {
                            resultMove = ResultMoveGame.Win;
                            return  Wieksze;
                        }

                    }
                    else
                    {
                        resultMove = ResultMoveGame.Lose;
                        return twojeKarty;
                    }
                }

            }

        }
    }
}

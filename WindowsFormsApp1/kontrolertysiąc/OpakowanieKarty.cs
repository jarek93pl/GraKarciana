using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraKarciana.ObsugaKart;
using static GraKarciana.ObsugaTysiąc;
namespace WindowsFormsApp1
{
    class OpakowanieKarty:IComparable
    {
        int DoSortowania = 0;
        string nazwa;
        public GraKarciana.Karta k;
        public OpakowanieKarty(GraKarciana.Karta k)
        {
            DoSortowania = ((int)k.Kolor()) * 100;
            DoSortowania += PunktacjaTysiąca(k);
            this.k = k;
            switch (k.Kolor())
            {
                case GraKarciana.Karta.trelf:
                    nazwa = "trefl ";
                    break;
                case GraKarciana.Karta.karo:
                    nazwa = "karo ";
                    break;
                case GraKarciana.Karta.kier:
                    nazwa = "kier ";
                    break;
                case GraKarciana.Karta.pik:
                    nazwa = "pik ";
                    break;
            }
            switch (k.PobierzKarte())
            {
              
                case GraKarciana.Karta.K2:
                    nazwa += "2";
                    break;
                case GraKarciana.Karta.K3:
                    nazwa += "3";
                    break;
                case GraKarciana.Karta.K4:
                    nazwa += "4";
                    break;
                case GraKarciana.Karta.K5:
                    nazwa += "5";
                    break;
                case GraKarciana.Karta.K6:
                    nazwa += "6";
                    break;
                case GraKarciana.Karta.K7:
                    nazwa += "7";
                    break;
                case GraKarciana.Karta.K8:
                    nazwa += "8";
                    break;
                case GraKarciana.Karta.K9:
                    nazwa += "9";
                    break;
                case GraKarciana.Karta.K10:
                    nazwa += "10";
                    break;
                case GraKarciana.Karta.Dupek:
                    nazwa += "dupek";
                    break;
                case GraKarciana.Karta.Dama:
                    nazwa += "dama";
                    break;
                case GraKarciana.Karta.Król:
                    nazwa += "król";
                    break;
                case GraKarciana.Karta.As:
                    nazwa += "as";
                    break;
                default:
                    break;
            }
        }
        public static implicit operator GraKarciana.Karta(OpakowanieKarty k)
        {
            return k.k;
        }
        public override string ToString()
        {
            return nazwa;
        }

        public int CompareTo(object obj)
        {
            if (obj is OpakowanieKarty ik)
            {
                return DoSortowania - ik.DoSortowania;
            }
            throw new InvalidCastException("dwa porównywane obiekty nie są tego samego typu");
        }
    }
}

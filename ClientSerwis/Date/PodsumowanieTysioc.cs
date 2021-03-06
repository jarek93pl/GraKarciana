﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using GraKarciana;

namespace ClientSerwis
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GraKarciana")]
    public class PodsumowanieTysioc
    {
        [DataMember]
        public int[] Punkty;
        [DataMember]
        public int[] PunktyWTurze;

        public bool CzyktośWygrał { get => Punkty.Any(X => X > 1000); }

        public PodsumowanieTysioc(int IlośćGraczy)
        {
            Punkty = new int[IlośćGraczy];
            PunktyWTurze = new int[IlośćGraczy];
        }
        internal void NowaTura()
        {
            PunktyWTurze = new int[PunktyWTurze.Length];
        }
       
        internal void OdznaczMeldunek(Karta kozera, int v)
        {
            PunktyWTurze[v] += ObsugaTysiąc.WartościMeldunków(kozera.Kolor());
            
        }

        internal void Koniec(int najwyżejLicytujący, int wartośćWylicytowana)
        {
            
            for (int i = 0; i < Punkty.Length; i++)
            {
                if (i!=najwyżejLicytujący)
                {
                    if (Punkty[i]<800)
                    {

                        Punkty[i] += PunktyWTurze[i] / 10 * 10;
                    }
                }
            }
            Punkty[najwyżejLicytujący] += wartośćWylicytowana > Punkty[najwyżejLicytujący] ? wartośćWylicytowana : -wartośćWylicytowana;
        }
    }
}
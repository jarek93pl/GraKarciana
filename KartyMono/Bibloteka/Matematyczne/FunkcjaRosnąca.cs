using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Komputer.Matematyczne
{
    public class FunkcjaRosnąca
    {
        class obiekt
        {
            public int[] Miejsce;
            public obiekt[] Natepne;
            public obiekt()
            {
                Miejsce = new int[16];
                Natepne = new obiekt[16];
            }

            internal bool Oczyść()
            {
                for (int i = 0; i < 16; i++)
                {
                    if (Miejsce[i]!=0)
                    {
                        return false;
                    }
                    if (Natepne[i]==null)
                    {
                        continue;
                    }
                    if (Natepne[i].Oczyść())
                    {
                        Natepne[i] = null;
                        continue;
                    }
                    return false;
                }
                return true;
                 
            }
        }
        const uint IlośćBitów = 15;
        readonly obiekt Korzeń = new obiekt();
        public void Dodaj(uint MiejsceX, int OIle)
        {
            obiekt korz = Korzeń;
            for (int i = 28; i >= 0; i -= 4)
            {
                int l = (int)((MiejsceX >> i) & IlośćBitów);
                for (int ii = l + 1; ii < 16; ii++)
                {
                    korz.Miejsce[ii] += OIle;
                }
                if (korz.Natepne[l] == null)
                {
                    korz.Natepne[l] = new obiekt();
                }
                korz = korz.Natepne[l];
            }

        }
        public void Oczyść()
        {
            Korzeń.Oczyść();
        }
        public int this[uint index]
        {

            get
            {
                index++;
                int sum = 0;
                obiekt korz = Korzeń;
                for (int i = 28; i >= 0; i -= 4)
                {
                    int l = (int)((index >> i) & IlośćBitów);
                    if (korz == null)
                    {
                        return sum;
                    }
                    sum += korz.Miejsce[l];
                    korz = korz.Natepne[l];
                }
                return sum;
            }
        }
    }
}

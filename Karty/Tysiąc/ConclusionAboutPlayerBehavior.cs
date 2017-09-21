using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GraKarciana;
namespace Karty
{
    public class ConclusionAboutPlayerBehavior
    {
        public int AmountCards;
        public IEnumerable<Karta> CardsPlayer;
        public Karta[] TheMostCardsInColor = new Karta[4];
        public ConclusionAboutPlayerBehavior()
        {
            for (int i = 0; i < TheMostCardsInColor.Length; i++)
            {
                TheMostCardsInColor[i] = Karta.As + 4;
            }
        }
        public void ConclusionAbotBehavior(Karta k)
        {
            int Suit = (int)k.Kolor();
            int tmp =(int) TheMostCardsInColor[Suit];
            BasicTools.SetMax(ref tmp,(int) k);
            TheMostCardsInColor[Suit] =(Karta) tmp;


        }
        internal bool ValidateCard(Karta karta) => TheMostCardsInColor[(int)karta.Kolor()] > karta;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GraKarciana;
namespace Karty
{
    public class ConclusionAbouttUserBehavior
    {
        readonly int PlayerInGame;
        public int AmountCards;
        public List<Karta> UserCards;
        public Karta[] TheMostCardsInColor = new Karta[4];

        public bool ItAuction { get; set; } = true;

        public ConclusionAbouttUserBehavior(int PlayerInGame)
        {
            this.PlayerInGame = PlayerInGame;
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


        }        internal bool ValidateCard(Karta karta) => TheMostCardsInColor[(int)karta.Kolor()] > karta;
        /// <summary>
        /// te losowanie nie zwraca uwagi na ilość kart ponieważ one jest uwzglednianie w 
        /// klasie conclusion about game 
        /// 
        /// </summary>
        /// <param name="dontRandCards"></param>
        public void RandomCards(List<Karta> dontRandCards)
        {
            UserCards.Clear();
            switch (PlayerInGame)
            {
                case 2:
                    RandomCards2Cards(dontRandCards);
                    break;
                case 3:
                    RandomCards3Cards(dontRandCards);
                    break;
                case 4:
                    RandomCards4Cards(dontRandCards);
                    break;
                default:
                    break;
                    throw new InvalidOperationException("w tysiąca można grać od 2 do 4 graczy");
            }
        }

        private void RandomCards4Cards(List<Karta> dontRandCards)
        {
            throw new NotImplementedException();
        }

        private void RandomCards2Cards(List<Karta> dontRandCards)
        {
            throw new NotImplementedException();
        }

        protected virtual void RandomCards3Cards(List<Karta> dontRandCards)
        {
            if (ItAuction)
            {
                UserCards.AddRange(dontRandCards.RandAndDelete(7));
            }
            else
            {
                UserCards.AddRange(dontRandCards.RandAndDelete(10));
            }
        }
    }
}

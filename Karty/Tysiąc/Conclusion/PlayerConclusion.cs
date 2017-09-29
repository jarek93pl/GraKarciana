using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;

namespace Karty
{
    class PlayerConclusion : ConclusionAbouttUserBehavior
    {
        IEnumerable<Karta> haveCards;
        public PlayerConclusion(int PlayerInGame,IEnumerable<Karta> haveCards) : base(PlayerInGame)
        {
            this.haveCards = haveCards;
        }
        protected override void RandomCards3Cards(List<Karta> dontRandCards)
        {

            UserCards.AddRange(haveCards);
            if (ItAuction)
            {
                UserCards.AddRange(dontRandCards.RandAndDelete(3));
            }
        }
    }
}

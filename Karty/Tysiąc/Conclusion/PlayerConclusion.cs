using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;

namespace Karty
{
    public class PlayerConclusion : ConclusionAbouttUserBehavior
    {
        public IEnumerable<Karta> haveCards;
        public PlayerConclusion(int PlayerInGame,IEnumerable<Karta> haveCards) : base(PlayerInGame)
        {
            this.haveCards = haveCards;
        }
        protected override void RandomCards3Cards(List<Karta> dontRandCards,MoveContext1000 mp,bool WinAction)
        {
            switch (mp)
            {
                case MoveContext1000.Action:
                    UserCards.AddRange(dontRandCards.RandAndDelete(1));
                    UserCards.AddRange(haveCards);
                    break;
                case MoveContext1000.ChoseCards:
                case MoveContext1000.Game:
                    UserCards.AddRange(haveCards);
                    break;
                default:
                    break;
            }
        }
    }
}

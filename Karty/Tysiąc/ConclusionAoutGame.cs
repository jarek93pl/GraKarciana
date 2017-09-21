using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty.Tysiąc
{
    public class ConclusionAoutGame
    {
        List<ConclusionAboutPlayerBehavior> PlayerConclusion = new List<ConclusionAboutPlayerBehavior>();
        public ConclusionAoutGame(int AmountPlayers, int NumberOfCards)
        {
            for (int i = 0; i < AmountPlayers; i++)
            {
                ConclusionAboutPlayerBehavior conclusion = new ConclusionAboutPlayerBehavior();
                conclusion.AmountCards = NumberOfCards;
                PlayerConclusion.Add(conclusion);
            }
        }
        public ConclusionAoutGame(List<ConclusionAboutPlayerBehavior> z)
        {
            PlayerConclusion = z;
        }
        public StateGame1000 GetState()
        {
            throw new NotImplementedException();
        }
    }
}

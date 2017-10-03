using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;

namespace Karty.Tysiąc.IqDefineRole
{
    class RoleSetCardIdWin : IDefineRole
    {
        public bool IsEnded => true;

        public List<Karta> GetValidCards(List<Karta> ls, StateGame1000 s)
        {
            List<Karta> cardsSymulate = new List<Karta>();
            int moveToEndQueue = s.amountPlayer - s.NumberCardInTable;
            for (int i = 0; i < moveToEndQueue; i++)
            {
                cardsSymulate.AddRange(s.cards[(i + s.Player) % s.amountPlayer]);
            }
            cardsSymulate.AddRange(s.GetCardInTable());
            cardsSymulate.Sort(ComparerTysioc.GetComparer(s.cardOnTable.First(), s.EnebleKozera, s.Kozera));
            Karta maxCard = cardsSymulate.Last();
            if (ls.Any(X=>X==maxCard))
            {
                return new List<Karta>() { maxCard };
            }
            {
                return ls;
            }
        }

        public bool IsContext(StateGame1000 s, ResultMoveGame mk) => mk == ResultMoveGame.Win ;
    }
}

using GraKarciana;
using KartyMono.Common.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Common
{
    public static class CardHelper
    {
        public static List<Karta> ToListCard(this List<CardSocketUI> list) => list.Where(X => X.InnerCard != null).Select(X => X.InnerCard.Card).ToList();

    }
}

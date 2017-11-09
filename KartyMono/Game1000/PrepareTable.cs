using KartyMono.Common;
using KartyMono.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Game1000
{
     static class PrepareTable
    {
        const int AmountCardUser = 10;
        const int AmountTable = 3;
        const int OffsetNextCard = 100;
        public static void GetTable(Menu1000Game mn)
        {
            for (int i = 0; i < AmountCardUser; i++)
            {
                Vector2 v = new Vector2(200 + i * OffsetNextCard, 500);
                mn.AddCardSlot(GetCardSlot(v),Menu1000Game.KindSlot.UserCard);
            }

            for (int i = 0; i < AmountTable; i++)
            {
                Vector2 v = new Vector2(400 + i * OffsetNextCard, 200);
                mn.AddCardSlot(GetCardSlot(v), Menu1000Game.KindSlot.Table);
            }
        }
        public static CardSocketUI GetCardSlot(Vector2 v)
        {
            CardSocketUI cd = new CardSocketUI();
            cd.Miejsce = v;
            return cd;
        }
    }
}

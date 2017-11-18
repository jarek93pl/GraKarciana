using KartyMono.Common.UI;
using KartyMono.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Game1000
{
     class PrepareTable
    {
        const int AmountCardUser = 10;
        const int AmountTable = 3;
        const int OffsetNextCard = 100;
        Menu1000Game menu1000;
        public PrepareTable(Menu1000Game mn)
        {
            menu1000 = mn;
        }
        public void GetTable()
        {
            for (int i = 0; i < AmountCardUser; i++)
            {
                Vector2 v = new Vector2(200 + i * OffsetNextCard, 500);
                AddCard( v, Menu1000Game.KindSlot.UserCard);
            }

            for (int i = 0; i < AmountTable; i++)
            {
                Vector2 v = new Vector2(400 + i * OffsetNextCard, 200);
                AddCard( v, Menu1000Game.KindSlot.Table);
            }
        }

        private void AddCard(Vector2 v, Menu1000Game.KindSlot type)
        {
            menu1000.AddCardSlot(GetCardSlot(v, type), type);
        }

        public static CardSocketUI GetCardSlot(Vector2 v, Menu1000Game.KindSlot typeslot)
        {
            CardSocketUI cd = new CardSocketUI();
            cd.Miejsce = v;
            switch (typeslot)
            {
                case Menu1000Game.KindSlot.Table:
                    CardTable(cd);
                    break;
                case Menu1000Game.KindSlot.UserCard:
                    CardUser(cd);
                    break;
                default:
                    break;
            }
            return cd;
        }

        private static void CardUser(CardSocketUI cd)
        {
            cd.BlockedGetCard = false;
        }

        private static void CardTable(CardSocketUI cd)
        {
        }
    }
}

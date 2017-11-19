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

        public CardSocketUI GetCardSlot(Vector2 v, Menu1000Game.KindSlot typeslot)
        {
            CardSocketUI cd = null;
            switch (typeslot)
            {
                case Menu1000Game.KindSlot.Table:
                   cd= CardTable();
                    break;
                case Menu1000Game.KindSlot.UserCard:
                    cd= CardUser();
                    break;
                default:
                    break;
            }

            cd.Miejsce = v;
            return cd;
        }

        private CardSocketUI CardUser()
        {
            CardSocketUI cd = new CardSocketUI();
            cd.BlockedGetCard = false;
            return cd;
        }

        private CardSocketUI CardTable()
        {
            CardSocketUI cd = new CardSocketUI();
            return cd;
        }
    }
}

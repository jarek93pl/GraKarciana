using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GraKarciana;
using Karty;
namespace KartyMono.Game1000
{
    class CardUI : XnaKontrolka
    {
        const float MaxDystans = 100;
        public static float Speed = 4;
        public CardSocketUI socketUI;
        List<CardSocketUI> listSocket;
        public CardUI(Karta k,List<CardSocketUI> list)
        {
            listSocket = list;
            GetShowDate(k);
            this.Zaznaczony += CardUI_Zaznaczony;
            this.Puszczony += CardUI_Puszczony;

        }

        private void GetShowDate(Karta k)
        {
            Zdjecie = Game1.ContentStatic.GetTexture2DFromCard(k);
            Scala = 0.1f;
        }

        Vector2? StartPointMouse;
        Vector2 StartPointObject;
        private void CardUI_Puszczony(object snder, EventKlikniety e)
        {
            if (StartPointMouse.HasValue)
            {
                CardSocketUI cardSocketUI = listSocket.Where(X => !X.BlockedSetCard&&X.InnerCard==null).GetMin(X => (X.Miejsce - Miejsce).Length());
                if ((cardSocketUI.Miejsce - Miejsce).Length()<MaxDystans)
                {
                    BindCardToSocket(cardSocketUI, this);
                }
                StartPointMouse = null;
            }
        }

        private static void BindCardToSocket(CardSocketUI socketUI, CardUI card)
        {
            card.socketUI = socketUI;
            socketUI.InnerCard = card;
        }

        private void CardUI_Zaznaczony(object snder, EventKlikniety e)
        {
            if (socketUI ==null|| socketUI.BlockedGetCard)
            {
                StartPointMouse = e.Miejsce;
                StartPointObject = Miejsce;
            }
        }
        public override void UżycieUpdate(GameTime gt)
        {
            if (socketUI != null && !StartPointMouse.HasValue)
            {
                Vector2 delta = socketUI.Miejsce - Miejsce;
                float lenght = delta.Length();
                delta.Normalize();
                delta *= Speed > lenght ? lenght : Speed;
                Miejsce += delta;

            }

            base.UżycieUpdate(gt);
        }
        public override bool UpDate(EventArgs e)
        {
            if (e is EventPrzesuniecie p&& StartPointMouse.HasValue)
            {
                Miejsce = StartPointObject+ p.Miejsce- StartPointMouse.Value;
            }
            return true;
        }
    }
}

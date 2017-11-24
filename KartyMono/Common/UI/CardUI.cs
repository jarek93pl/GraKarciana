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
using KartyMono.Common.UI.Activity;
using System.Diagnostics;
namespace KartyMono.Common.UI
{
    public class CardUI : XnaKontrolka, IDropAndDrag
    {
        public Karta Card;
        const float MaxDystans = 100;
        public static float Speed = 4;
        public CardSocketUI socketUI;
        List<CardSocketUI> listSocket;
        public CardUI(Karta k,List<CardSocketUI> list)
        {
            Card = k;
            listSocket = list;
            GetShowDate(k);
            CzyUżywaUpdate = true;

        }

        private void GetShowDate(Karta k)
        {
            Zdjecie = Game1.ContentStatic.GetTexture2DFromCard(k);
            Scala = 0.1f;
        }

        Vector2? StartPointMouse;
        Vector2 StartPointObject;
        public void Drag(Vector2 vector)
        {
            if (socketUI == null || !socketUI.BlockedGetCard)
            {
                StartPointMouse = vector;
                StartPointObject = Miejsce;
            }
        }
        public void Drop(Vector2 vector)
        {
            
            if (StartPointMouse.HasValue)
            {
                var AvilibleSocket = listSocket.Where(X => AceptanceSocket(X));
                CardSocketUI cardSocketUI = AvilibleSocket.GetMin(X =>X!=null? X.LenghtToObject(this):0);
                if (cardSocketUI!=null)
                {
                    BindCardToSocket(cardSocketUI, this);
                }
                StartPointMouse = null;
            }
        }

        private bool AceptanceSocket(CardSocketUI X)
        {
            return !X.BlockedSetCard && (X.InnerCard == null)//carta może być odłożona i soket nie jest pusty
                && (LenghtToObject(X) < MaxDystans) &&//nie przekracza maksymalnego dystansu
                X.AceptanceSet(this);//soket może przyjąć tą krte
        }


        public static void BindCardToSocket(CardSocketUI socketUI, CardUI card)
        {
            if (card.socketUI!=null)
            {
                card.socketUI.InnerCard = null;
            }
            card.socketUI = socketUI;
            if (socketUI.InnerCard!=null)
            {
                throw new InvalidOperationException();
            }
            socketUI.InnerCard = card;
            socketUI.TookCard(card);
        }


        public override void UżycieUpdate(GameTime gt)
        {
            if (socketUI != null && !StartPointMouse.HasValue)
            {
                Vector2 delta = socketUI.Miejsce - Miejsce;
                float lenght = delta.Length();
                delta.Normalize();
                delta *= Speed > lenght ? lenght : Speed;
                if (!float.IsNaN(delta.X)|| !float.IsNaN(delta.Y))
                {
                    Miejsce += delta;
                }

            }
            if (StartPointMouse.HasValue)
            {
                Miejsce = StartPointObject + GameState.Instance.MouseLocation - StartPointMouse.Value;
            }
            base.UżycieUpdate(gt);
        }
        public override bool UpDate(EventArgs e)
        {
            return true;
        }
        public override void Draw(SpriteBatch pezel)
        {
            base.Draw(pezel);
        }
    }
}

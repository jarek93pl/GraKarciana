﻿using System;
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
using KartyMono.Common;
namespace KartyMono.Common
{
    class CardUI : XnaKontrolka, IDropAndDrag
    {
        const float MaxDystans = 100;
        public static float Speed = 4;
        public CardSocketUI socketUI;
        List<CardSocketUI> listSocket;
        public CardUI(Karta k,List<CardSocketUI> list)
        {
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
            if (socketUI == null || socketUI.BlockedGetCard)
            {
                StartPointMouse = vector;
                StartPointObject = Miejsce;
            }
        }
        public void Drop(Vector2 vector)
        {
            if (StartPointMouse.HasValue)
            {
                CardSocketUI cardSocketUI = listSocket.Where(X => !X.BlockedSetCard && X.InnerCard == null).GetMin(X => (X.Miejsce - Miejsce).Length());
                if ((cardSocketUI.Miejsce - Miejsce).Length() < MaxDystans)
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
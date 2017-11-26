﻿using GraKarciana;
using KartyMono.Common.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Common
{
    abstract class BaseTable
    {
        List<KeyValuePair<List<Karta>, List<CardSocketUI>>> list = new List<KeyValuePair<List<Karta>, List<CardSocketUI>>>();
       
        public List<Karta> AddCardCollection(List<CardSocketUI> cd)
        {
            var tmp = cd.ToListCard();
            list.Add(new KeyValuePair<List<Karta>, List<CardSocketUI>>( tmp, cd));
            return tmp;
        }
        public void Execute(BaseTable date)
        {
            ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>> com = new ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>(X=>X.Key.Cast<byte>());
            var diff = com.Comparer(date.list, list);
            foreach (var item in diff)
            {
                ExecuteMove(item);
            }
        }

        private void ExecuteMove(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            if (Object.Equals( item.From,null))
            {
                FromEmpty(item);
            }
            else if (Object.Equals(item.To, null))
            {
                ToEmpty(item);
            }
            else
            {
                MoveCard(item);
            }

        }

        private void MoveCard(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Socket = item.To.Value.First(X => X.InnerCard == null);
            var Card = item.From.Value.First(X => X.InnerCard == null).InnerCard;
            CardUI.BindCardToSocket(Socket, Card);
        }

        private void ToEmpty(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Card = item.From.Value.First(X => X.InnerCard == null).InnerCard;
            CardUI.BindCardToSocket(GetEmptySocket(item.From, item.To, (Karta)item.target), Card);
        }

          private void FromEmpty(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Socket = item.To.Value.First(X => X.InnerCard == null);
            CardUI.BindCardToSocket(Socket, GetCard(item.From,item.To,(Karta) item.target));
        }
        public abstract CardSocketUI GetEmptySocket(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target);


        public abstract CardUI GetCard(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target);
    }
}
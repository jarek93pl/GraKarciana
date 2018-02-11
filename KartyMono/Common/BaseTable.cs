using GraKarciana;
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
        /// <summary>
        /// Zwróć uwage na kolejnośc dodawania do tej listy
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public List<Karta> AddCardCollection(List<CardSocketUI> cd)
        {
            var tmp = cd.ToListCard();
            list.Add(new KeyValuePair<List<Karta>, List<CardSocketUI>>(tmp, cd));
            return tmp;
        }
        /// <summary>
        /// zwróć uwage na kolejnośc dodawania do tej listy
        /// </summary>
        /// <param name="cd"></param>
        public void AddCardCollection(List<Karta> cd, List<CardSocketUI> cdsocket)
        {
            list.Add(new KeyValuePair<List<Karta>, List<CardSocketUI>>(cd, cdsocket));
        }
        public void Execute(BaseTable date)
        {
            ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>> com = new ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>(X => X.Key.Cast<byte>());
            var diff = com.Comparer(date.list, list);
            for (int i = 0; i < 10; i++)
            {
                HashSet<object> toRemove = new HashSet<object>();
                foreach (var item in diff)
                {
                    if (ExecuteMove(item))
                    {
                        toRemove.Add(item);
                    } 
                }
                diff.RemoveAll(X => toRemove.Contains(X));
            }
        }

        private bool ExecuteMove(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            if (Object.Equals(item.From, null) || Object.Equals(item.From.Key, null))
            {
               return FromEmpty(item);
            }
            else if (Object.Equals(item.To, null) || Object.Equals(item.To.Key, null))
            {
                return ToEmpty(item);
            }
            else
            {
               return MoveCard(item);
            }
        }

        private bool MoveCard(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Socket = item.To.Value.First(X => X.InnerCard == null);
            var Card = item.From.Value.FirstOrDefault(X => X.InnerCard?.Card == (Karta)item.target).InnerCard;
            if (Socket!=null&&Card!=null)
            {
                CardUI.BindCardToSocket(Socket, Card,false);
                return true;
            }
            return false;
        }

        private bool ToEmpty(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Card = item.From.Value.FirstOrDefault(X => X.InnerCard?.Card ==(Karta) item.target).InnerCard;
            if (Card != null)
            {
                CardUI.BindCardToSocket(GetEmptySocket(item.From, item.To, (Karta)item.target), Card,false);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FromEmpty(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item)
        {
            var Socket = item.To.Value.FirstOrDefault(X => X.InnerCard == null);
            if (Socket != null)
            {
                CardUI.BindCardToSocket(Socket, GetCard(item.From, item.To, (Karta)item.target),false);
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract CardSocketUI GetEmptySocket(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target);


        public abstract CardUI GetCard(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target);
    }
}

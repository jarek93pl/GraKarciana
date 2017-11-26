using GraKarciana;
using KartyMono.Common.UI;
using KartyMono.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartyMono.Common;
using Microsoft.Xna.Framework;

namespace KartyMono.Game1000
{
    class Table:BaseTable
    {
        public List<Karta> CardInTable;
        public List<Karta> CardUser;
        public List<CardSocketUI> CardInTableSocket;
        public List<CardSocketUI> CardUserSocket;
        Menu1000Game menu;
        private Table()
        {
        }
        public static Table Empty()
        {
            var tmp = new Table();
            tmp.Load(new List<CardSocketUI>(), new List<CardSocketUI>());
            return tmp;
        }
        public Table(Menu1000Game menu)
        {
            this.menu = menu;
            Load(menu.ListSocketTable, menu.ListSocketUser);
        }

        private void Load(List<CardSocketUI> CardInTableSocketU, List<CardSocketUI> ListSocketUserU)
        {
            CardInTableSocket = CardInTableSocketU;
            CardUserSocket = ListSocketUserU;
            CardInTable = AddCardCollection(CardInTableSocket);
            CardUser = AddCardCollection(CardUserSocket);
        }

        public override CardUI GetCard(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target)
        {
            CardUI cd = new CardUI(target);
            cd.Miejsce = menu.startPosytionCard;
            return cd;
        }

        public override CardSocketUI GetEmptySocket(KeyValuePair<List<Karta>, List<CardSocketUI>> from, KeyValuePair<List<Karta>, List<CardSocketUI>> to, Karta target)
        {
            return menu.socketEmpty;

        }
    }
}

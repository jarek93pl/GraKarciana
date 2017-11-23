using GraKarciana;
using KartyMono.Common.UI;
using KartyMono.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartyMono.Common;
namespace KartyMono.Game1000
{
    class Table:BaseTable
    {
        public List<Karta> CardInTable;
        public List<Karta> CardUser;
        public List<CardSocketUI> CardInTableSocket;
        public List<CardSocketUI> CardUserSocket;
        public Table(Menu1000Game menu)
        {
            CardInTableSocket = menu.ListSocketTable;
            CardUserSocket = menu.ListSocketUser;
            CardInTable = AddCardCollection(CardInTableSocket);
            CardUser = AddCardCollection(CardUserSocket);
        }
    }
}

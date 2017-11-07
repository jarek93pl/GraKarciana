using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Karty;
using GraKarciana;
using KartyMono.Game1000;

namespace KartyMono.Menu
{
    class Menu1000Game:MenuPodstawa
    {
        public enum KindSlot { Table,UserCard};
        List<CardUI> ListCard = new List<CardUI>();
        List<CardSocketUI> ListSocket = new List<CardSocketUI>();
        List<CardSocketUI> ListSocketUser = new List<CardSocketUI>();
        List<CardSocketUI> ListSocketTable = new List<CardSocketUI>();
        Texture2D tx;
        public Menu1000Game(ContentManager content):base(Game1.Cursor)
        {
            PrepareTable.GetTable(this);
            AddCard(new CardUI(Karta.Dama, ListSocket));
        }
        public void AddCard(CardUI card)
        {
            Add(card);
            ListCard.Add(card);
        }
        public void AddCardSlot(CardSocketUI card, KindSlot kind)
        {
            Add(card);
            ListSocket.Add(card);
            switch (kind)
            {
                case KindSlot.Table:
                    ListSocketTable.Add(card);
                    break;
                case KindSlot.UserCard:
                    ListSocketUser.Add(card);
                    break;
                default:
                    break;
            }
        }
        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp);
        }
        public override void UpDate(GameTime GT)
        {

            //Game1.game.Window.Title=Mouse.GetState().Position.ToString();
            base.UpDate(GT);
        }
    }
}

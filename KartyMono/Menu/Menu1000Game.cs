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
using KartyMono.Common.UI;
using KartyMono.Common.UI.Activity;
using ClientSerwis;
using System.ServiceModel;

namespace KartyMono.Menu
{
    class Menu1000Game:MenuPodstawa
    {
        public enum KindSlot { Table,UserCard};
        List<CardUI> ListCard = new List<CardUI>();
        List<CardSocketUI> ListSocket = new List<CardSocketUI>();
        List<CardSocketUI> ListSocketUser = new List<CardSocketUI>();
        List<CardSocketUI> ListSocketTable = new List<CardSocketUI>();
        MonitorDropAndDrag<CardUI> monitorDropAndDrag;
        Texture2D tx;
        TysiocClient client;
        public Func<CardUI, CardSocketUI, bool> ConditonSetCardToTable
        {
            set
            {
                ListSocketTable.ForEach(X => X.InitalizeCondition(Y => value(Y, X)));
            }
        }
        public  event EventHandler<CardUI> TookCard
        {
            add
            {
                ListSocketTable.ForEach(X => X.OnTookCard += value);
            }
            remove
            {
                ListSocketTable.ForEach(X => X.OnTookCard -= value);
            }
        }
        public Menu1000Game(ContentManager content):base(Game1.Cursor)
        {
            PreperTableM();
            PreperProxyToConection();
            AddCard(new CardUI(Karta.Dama, ListSocket));
            monitorDropAndDrag = new MonitorDropAndDrag<CardUI>(ListCard, AceptanceGet);
            AddKomponet(monitorDropAndDrag);
            AddKomponet(new GameState());
        }

        private void PreperProxyToConection()
        {
           // Urzytkownik.DoKontaClient uk = new Urzytkownik.DoKontaClient();
            var kontroler = new KontrolerTysioc();
            InstanceContext instance = new InstanceContext(kontroler);
            kontroler.Initialize(client = new TysiocClient(instance));
           // int playerIndex = uk.Rejestruj(new ClientSerwis.Urzytkownik() { Nazwa = Guid.NewGuid().ToString(), Haslo = "bardzo trudne" });
           // await client.CzekajNaGraczaAsync(playerIndex);

        }

        private void PreperTableM()
        {
            PrepareTable prepare = new PrepareTable(this);
            prepare.GetTable();
        }

        private bool AceptanceGet(CardUI arg)
        {
            if (arg.socketUI == null)
            {
                return true;
            }

            return !arg.socketUI.BlockedGetCard;
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

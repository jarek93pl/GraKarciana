using KartyMono.Common.UI;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Menu
{
    partial class Menu1000Game : MenuPlayerAndTable
    {

        private int valueAuction;
        PrzyciskText AuctionButon;
        PrzyciskText MoreValueButon;
        public void ConstructorAction()
        {

            AuctionButon = new PrzyciskText(new Vector2(200, 40), GlobalStaticDate.Text) { Miejsce = new Vector2(500, 10) };
            AuctionButon.TextFormat = () => $"Licytuj {ValueAuction}";
            AuctionButon.Klikniecie += AuctionButon_Klikniecie;
            SetDateDoButton(AuctionButon);
            
            MoreValueButon = new PrzyciskText(new Vector2(140, 40), GlobalStaticDate.Text) { Miejsce = new Vector2(350, 10) };
            MoreValueButon.Text = "Wiecej";
            MoreValueButon.Klikniecie += MoreValueButon_Klikniecie;
            SetDateDoButton(MoreValueButon);
            
        }

        private async void AuctionButon_Klikniecie(object sender, EventArgs e)
        {
            await proxy.controler.LicytujAsync(ValueAuction);
        }

        private void MoreValueButon_Klikniecie(object sender, EventArgs e)
        {
            ValueAuction += 10;
        }

        public void YourAction()
        {
            ValueAuction = proxy?.controler?.MinimalnaWartośćLicytacji ?? 100;
        }
         void SetDateDoButton(PrzyciskText tmp)
        {
            tmp.Background = Color.Wheat;
            tmp.KolorTrzcionki = Color.Black;
            Add(tmp);

        }

        private void UpdateAuction(GameTime gt)
        {
            VisiblePanel = proxy?.controler?.Stan != ClientSerwis.Stan.TwojaLicytacja;

        }
        public bool VisiblePanel
        {
            set
            {
                if (AuctionButon != null && MoreValueButon != null)
                {
                    AuctionButon.Ukryty  = MoreValueButon.Ukryty = value;
                }
            }
        }

        public int ValueAuction { get => valueAuction; set => valueAuction = value; }
    }
}

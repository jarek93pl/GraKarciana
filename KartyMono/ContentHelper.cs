using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Karty;
using GraKarciana;
using Microsoft.Xna.Framework.Content;

namespace KartyMono
{
    public static class ContentHelper
    {
        static Dictionary<Karta, string> MapingSuit = new Dictionary<Karta, string>() { { Karta.karo, "diamonds" }, { Karta.kier, "hearts" }, { Karta.pik, "spades" }, { Karta.trelf, "clubs" } };
        static Dictionary<Karta, string> MapingCard = new Dictionary<Karta, string>() { { Karta.K2, "2" }, { Karta.K3, "3" }, { Karta.K4, "4" }, { Karta.K5, "5" }, { Karta.K6, "6" }, { Karta.K7, "7" }, { Karta.K8, "8" }, { Karta.K9, "9" }, { Karta.K10, "10" }, { Karta.Dupek, "jack" }, { Karta.Dama, "queen" }, { Karta.Król, "king" }, { Karta.As, "ace" } };
        public static Texture2D GetTexture2DFromCard(this ContentManager cm,Karta karta)
        {
            return cm.Load<Texture2D>("card/"+MapingCard[karta.PobierzKarte()]+"_of_" + MapingSuit[karta.Kolor()]);
        }
        
    }
}

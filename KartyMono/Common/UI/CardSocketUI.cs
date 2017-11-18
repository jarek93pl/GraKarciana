using Komputer.Xna.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KartyMono.Common.UI
{
    class CardSocketUI : XnaKontrolka
    {
        public bool BlockedGetCard { get; set; }
        public bool BlockedSetCard { get; set; }
        static Lazy<Texture2D> Picture = GetPicture();
        public CardUI InnerCard;
        private static Lazy<Texture2D> GetPicture() => new Lazy<Texture2D>(new Func<Texture2D>(()=>Game1.ContentStatic.Load<Texture2D>("table/slot")));

        public CardSocketUI() 
        {
            Zdjecie = Picture.Value;
        }

        public override bool UpDate(EventArgs e)
        {
            return true;
        }
    }
}

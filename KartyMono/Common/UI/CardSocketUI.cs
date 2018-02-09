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
    public class CardSocketUI : XnaKontrolka
    {
        public event EventHandler<CardUI> OnTookCard;
        public Func< CardUI, bool> AceptanceSet;
        public bool BlockedGetCard { get; set; }
        public bool BlockedSetCard { get; set; }
        static Lazy<Texture2D> Picture = GetPicture();
        public CardUI InnerCard;
        private static Lazy<Texture2D> GetPicture() => new Lazy<Texture2D>(new Func<Texture2D>(()=>Game1.ContentStatic.Load<Texture2D>("table/slot")));
        public bool AceptanceCard(CardUI cd)
        {
            return AceptanceSet(cd);
        }
        public CardSocketUI() 
        {
            Zdjecie = Picture.Value;
        }
        public void InitalizeCondition(Func<CardUI, bool> AceptanceSet)
        {
            this.AceptanceSet = AceptanceSet;
        }
        public override bool UpDate(EventArgs e)
        {
            return true;
        }
        public void TookCard(CardUI cd)
        {
            OnTookCard?.Invoke(this,cd);
        }
    }
}

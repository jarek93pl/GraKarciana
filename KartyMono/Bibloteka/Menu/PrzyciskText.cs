//#define MalujLokalizacje
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Komputer.Xna.Menu
{
    public class PrzyciskText : Przycisk
    {
       public string Text;
       SpriteFont trz;
       public PrzyciskText(Vector2 Wielkosc, SpriteFont Trzcionka)
           : base(Wielkosc)
       {
           this.trz = Trzcionka; 
       }

       public PrzyciskText(Vector2 Wielkosc, SpriteFont Trzcionka, EventHandler ev)
           : base(Wielkosc,ev)
       {
           this.trz = Trzcionka;
       }
       public override void Draw(SpriteBatch sp)
       {
           base.Draw(sp);
           sp.DrawString(trz, Text, Miejsce, Kolor ,0,Vector2.Zero,1,SpriteEffects.None,0);
       }
       public override void Draw(SpriteBatch pezel, Vector2 Wzgledne)
       {
           base.Draw(pezel, Wzgledne);
           pezel.DrawString(trz, Text, Miejsce+Wzgledne, Kolor, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
       }
    }
}
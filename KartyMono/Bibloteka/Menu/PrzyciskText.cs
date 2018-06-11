//#define MalujLokalizacje
using System;
using System.Collections.Generic;
using System.Linq;
using KartyMono;
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
        public Func<string> TextFormat;
        SpriteFont trz;
        public PrzyciskText(Vector2 Wielkosc, SpriteFont Trzcionka)
            : base(Wielkosc)
        {
            this.trz = Trzcionka;
        }
        public Color Background
        {
            set
            {
                Color[] tb = new Color[(int)( Wielkoœæ.X * Wielkoœæ.Y)];
                for (int i = 0; i < tb.Length; i++)
                {
                    tb[i] = value;
                }
                Texture2D t2D = new Texture2D(Game1.graphics.GraphicsDevice,(int) Wielkoœæ.X, (int)Wielkoœæ.Y);
                t2D.SetData(tb);
                Zdjecie = t2D;
            }
        }
        public PrzyciskText(Vector2 Wielkosc, SpriteFont Trzcionka, EventHandler ev)
            : base(Wielkosc, ev)
        {
            this.trz = Trzcionka;
        }
        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp);
            DrawText(sp, Vector2.Zero);
        }
        public override void Draw(SpriteBatch pezel, Vector2 Wzgledne)
        {
            base.Draw(pezel, Wzgledne);
            DrawText(pezel, Wzgledne);
        }

        public Color KolorTrzcionki
        {
            get;
            set;
        }
        private void DrawText(SpriteBatch pezel, Vector2 Wzgledne)
        {
            if (TextFormat != null)
            {
                pezel.DrawString(trz, TextFormat(), Miejsce + Wzgledne, KolorTrzcionki, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            }
            else
            {
                pezel.DrawString(trz, Text, Miejsce + Wzgledne, KolorTrzcionki, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            }
        }
    }
}
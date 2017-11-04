using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Komputer.Xna.Menu
{
    public delegate void PobierzObraz(SpriteBatch sp);
    public class TworzenieRenderTarget
    {
        public PobierzObraz Pobieracz;
        public Color Kolor;
        public RenderTarget2D Obraz;
        public TworzenieRenderTarget(PobierzObraz Pobieracz, RenderTarget2D Obraz,Color Kolor)
        {
            this.Pobieracz = Pobieracz;
            this.Kolor = Kolor;
            this.Obraz = Obraz;
        }
    }
}

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
    public class Przycisk : XnaKontrolka
    {

        public event EventHandler Klikniecie;
        public Color KolorZwykły;
        public Przycisk(Texture2D Obraz)
        {
            // <pex>
            if (Obraz == (Texture2D)null)
                throw new ArgumentNullException("Obraz jest null");
            // </pex>
            base.Zdjecie= Obraz;
        }
        public Przycisk(Vector2 Wielkość)
        {
            base.Wielkość = Wielkość;
        }

        public Przycisk(Vector2 Wielkość, EventHandler KlikniecieZdażenie)
        {
            Klikniecie += KlikniecieZdażenie;
            base.Wielkość = Wielkość;
        }
        public Przycisk(Texture2D Obraz,float scal)
        {
            Zdjecie = Obraz;
            Scala = scal;
        }
        /// <summary>
        /// Domyślnym jest EventKlikniecie oznaczajoce wcisniecie przyciski ale jeżeli chcesz by przycisk był zawsze włączony gdy dotykasz użyj EventWysłanieDotchniecia
        /// </summary>
        public Type ZdażenieAkceptowanei =typeof( EventKlikniety);
        public override bool UpDate(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("a");
            if (e.GetType()==ZdażenieAkceptowanei && Klikniecie != null)
            {
                Klikniecie(this, EventArgs.Empty);
            }
            return false;
        }
    }
}
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
    public class MapZaznaczń : XnaKontrolka
    {
        public Point WielkoścKratk = new Point(0, 0), ZaznaczonyP;
        public EventHandler EventKlikniecie;
        
        //-------------------------------------------------------------------------------------------------------------------
        
        public MapZaznaczń(Texture2D Obraz, float scal,Point Wk)
        {
            WielkoścKratk = Wk;
            base.Zdjecie = Obraz;
            Scala = scal;
           
        }
        public override bool UpDate(EventArgs e)
        {

            EventKlikniety Kl = e as EventKlikniety;
            if (Kl == null)
                return false;

                Vector2 w = (Kl.Miejsce - Miejsce) / Scala;
                ZaznaczonyP = new Point((int)w.X / WielkoścKratk.X, (int)w.Y / WielkoścKratk.Y);
                if(EventKlikniecie!=null)
                EventKlikniecie(this,EventArgs.Empty);

            return false;
            
        }



    }
}
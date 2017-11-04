using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Komputer.Xna.Menu
{
    public class ZleceniaMenu
    {
        public int Długość, Poczotek;
        public List<EventArgs> ListaZdażeń;
        public List<ZlecenieWyświet> ListaWyświetleń;
        public static ZleceniaMenu operator +(ZleceniaMenu zm, EventArgs e)
        {
            zm.ListaZdażeń.Add(e);
            return zm;
        }
        public static ZleceniaMenu operator +(ZleceniaMenu zm,ZlecenieWyświet ZW)
        {
            zm.ListaWyświetleń.Add(ZW);
            return zm;
        }
        public static ZleceniaMenu operator +(ZleceniaMenu zm, EventHandler ZW)
        {
            zm.ListaZdażeń.Add(new ZlecenieZdażenia(ZW));
            return zm;
        }
        public static implicit operator List<ZleceniaMenu>(ZleceniaMenu zm)
        {
            List<ZleceniaMenu> zl=new List<ZleceniaMenu>();
            zl.Add(zm);
            return zl;
        }
        
        public ZleceniaMenu(int Długość,params EventArgs[] T)
        {
            this.Długość = Długość;
            ListaZdażeń = T.ToList();
        }
        public ZleceniaMenu(int Długość, params ZlecenieWyświet[] T)
        {
            this.Długość = Długość;
            ListaWyświetleń = T.ToList();
        }
        public ZleceniaMenu(int Długość)
        {
            this.Długość = Długość;
            ListaZdażeń = new List<EventArgs>();
            ListaWyświetleń = new List<ZlecenieWyświet>();
        }
    }
    public class ZlecenieWyświet
    {
        public Texture2D Obraz;
        public Vector2 Pozycja;
        public ZlecenieWyświet(Texture2D t, Vector2 v)
        {
            Obraz = t;
            Pozycja = v;
        }
    }
    internal class ZlecenieZdażenia:EventArgs
    {
        public ZlecenieZdażenia(EventHandler eh)
        {
            EH = eh;
        }
        public EventHandler EH;
    }
}

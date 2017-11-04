using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Komputer.Xna.Menu
{
    public delegate void PrzekażKontrolki(XnaKontrolka a);
    public delegate void PrzekażMiejsca(object snder,EventKlikniety e);
    public class EventKlikniety:EventArgs
    {
        public Vector2 Miejsce;
        public EventKlikniety(float x, float y):base()
        {
            Miejsce.X = x;
            Miejsce.Y = y;
        }
    }
    public class EventPrzesuniecie: EventArgs
    {
        public Vector2 Miejsce, Miejsce1;
        public EventPrzesuniecie(float x, float y,float x1,float y1)
            : base()
        {
            Miejsce.X = x;
            Miejsce.Y = y;
            Miejsce1.X = x1;
            Miejsce1.Y = y1;
        }
    }
}

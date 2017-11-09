using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Komputer.Xna.Menu
{
    public interface IComponet
    {
        void Draw(SpriteBatch sp);
        // dla true komponet jest usuwany
        bool UpDate(GameTime gt);
    }
}

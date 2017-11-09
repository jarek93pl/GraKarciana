using Komputer.Xna.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KartyMono.Common
{
    class GameState : IComponet
    {
        public static GameState Instance;
        public Vector2 MouseLocation;

        public GameState()
        {
            Instance = this;
        }
        void IComponet.Draw(SpriteBatch sp)
        {
        }

        bool IComponet.UpDate(GameTime gt)
        {
            var tmp = Mouse.GetState();
            MouseLocation = new Vector2(tmp.X, tmp.Y);
            return false;
        }
    }
}

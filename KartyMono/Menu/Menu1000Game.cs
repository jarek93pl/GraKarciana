using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Karty;
using GraKarciana;
namespace KartyMono.Menu
{
    class Menu1000Game:MenuPodstawa
    {
        Texture2D tx;
        public Menu1000Game(ContentManager content)
        {
           
        }
        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp);
        }
        public override void UpDate(GameTime GT)
        {

            //Game1.game.Window.Title=Mouse.GetState().Position.ToString();
            base.UpDate(GT);
        }
    }
}

using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace KartyMono.Common.UI.Activity
{
    class MonitorDropAndDrag<T>: IComponet where T: XnaKontrolka, IDropAndDrag
    {
        Func<T, bool> AceptanceGet;
        List<T> List;
        public MonitorDropAndDrag(List<T> List,Func<T,bool> AceptanceGet)
        {
            this.List = List;
            this.AceptanceGet = AceptanceGet;
        }
        ButtonState LastState;
        T selectedObject;
        private void PushAndPopMonitor()
        {
            MouseState ms = Mouse.GetState();
            Vector2 v = new Vector2(ms.X, ms.Y);
            switch (ms.LeftButton)
            {
                
                case ButtonState.Released:
                    if (LastState == ButtonState.Pressed)
                    {
                        Released(v);
                    }
                    break;
                case ButtonState.Pressed:
                    if (LastState==ButtonState.Released)
                    {
                        Pressed(v);
                    }
                    break;
            }
            LastState = ms.LeftButton;
        }

        private void Pressed(Vector2 v)
        {
            T card = List.FirstOrDefault(X => IsAvible(X,v));
            selectedObject = card;
            card?.Drag(v);
        }

        private void Released(Vector2 v)
        {
            if (selectedObject!=null)
            {
                selectedObject.Drop(v);
                selectedObject = null;
            }
        }

        //
        private bool IsAvible(T x,Vector2 v)
        {
            if (AceptanceGet(x) && x.Kolizja(v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void IComponet.Draw(SpriteBatch sp)
        {
        }

        bool IComponet.UpDate(GameTime gt)
        {
            PushAndPopMonitor();
            return false;
        }
    }
}

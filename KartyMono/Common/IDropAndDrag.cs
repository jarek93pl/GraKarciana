using Microsoft.Xna.Framework;

namespace KartyMono.Common
{
    interface IDropAndDrag
    {
        void Drag(Vector2 vector);
        void Drop(Vector2 vector);
    }
}
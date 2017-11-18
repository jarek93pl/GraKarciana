using Microsoft.Xna.Framework;

namespace KartyMono.Common.UI
{
    interface IDropAndDrag
    {
        void Drag(Vector2 vector);
        void Drop(Vector2 vector);
    }
}
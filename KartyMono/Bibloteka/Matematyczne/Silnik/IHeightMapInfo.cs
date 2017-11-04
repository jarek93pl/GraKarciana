using System;
namespace Komputer.Matematyczne.Silnik
{
    public interface IPobierzWyskokość
    {
        void GetHeightAndNormal(global::Microsoft.Xna.Framework.Vector2 position, out float height, out global::Microsoft.Xna.Framework.Vector3 normal);
        bool IsOnHeightmap(global::Microsoft.Xna.Framework.Vector2 position);
    }
}

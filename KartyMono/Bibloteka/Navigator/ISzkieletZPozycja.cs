using System;
using Microsoft.Xna.Framework;
namespace Komputer.Matematyczne.Silnik
{
    public interface ISzkieletZPozycja
    {
        Vector2 Miejsce { get; set; }
        global::Komputer.Matematyczne.Figury.FiguraZOdcinków Szkielet { get; set; }
    }
}

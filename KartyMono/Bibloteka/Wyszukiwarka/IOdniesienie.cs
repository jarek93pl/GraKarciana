using System;
namespace Komputer.Przeszukiwacz
{
    public interface IOdniesienie<T>
    {
        System.Collections.Generic.List<long> Adres { get; }
        long Index { get;  }
        T Obiekt { get;  }
    }
}

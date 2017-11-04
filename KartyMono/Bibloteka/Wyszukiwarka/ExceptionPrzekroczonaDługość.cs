using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kompilator.Wyszukiwarka
{
    class ExceptionPrzekroczonaDługość : Exception
    {
        public ExceptionPrzekroczonaDługość(string m):base(m)
        {
        }
    }
}

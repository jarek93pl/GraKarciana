using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class MockRestrycion:List<int>
    {
        public HashSet<int> restrycion = new HashSet<int>();
        public void AddRestryction(int l)
        {
            restrycion.Add(l);
        }
    }
}

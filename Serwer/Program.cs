using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Serwer
{
    class Program
    {

        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Tysioc)))
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
        }
    }
}

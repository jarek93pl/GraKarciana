using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GraKarciana
{
     [ServiceContract]
    public interface DoKonta
    {
        [OperationContract]
        int Loguj(Urzytkownik uk);
        [OperationContract(IsOneWay =true)]
        void Odświerz(Urzytkownik ul);
        [OperationContract]
        Urzytkownik Pobierz(int k);
        [OperationContract]
        int Rejestruj(Urzytkownik uk);
    }

    
}

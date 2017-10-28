using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ClientSerwis
{
    [System.Runtime.Serialization.DataContractAttribute( Namespace = "http://schemas.datacontract.org/2004/07/GraKarciana")]
    [System.SerializableAttribute()]
    public class Urzytkownik
    {

        [DataMember]
        public string Haslo;
        [DataMember]
        public string Nazwa;
        public void Aktualizuj(Urzytkownik uk)
        {
            Haslo = uk.Haslo;

        }

        internal void Przypisz(Urzytkownik ul)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is Urzytkownik u)
            {
                return u.Nazwa == Nazwa;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Nazwa.GetHashCode();
        }
    }
}
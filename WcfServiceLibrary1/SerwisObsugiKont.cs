using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Threading.Tasks;
namespace GraKarciana
{
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class SerwisObsugiKont : DoKonta
    {
        
        [ThreadStatic]
        static Random los = new Random();
        public static List<Urzytkownik> ListaUrzytkowników = new List<Urzytkownik>();
        public static Dictionary<int, Urzytkownik> Zarejestowani = new Dictionary<int, Urzytkownik>();

        public static Random Los
        {

            get
            {
                if (los==null)
                {
                    los = new Random();
                }
                return los;
            }
        }

        static int LiczbaKlucz = 0;
        [FaultContract(typeof(InvalidOperationException))]
        public int Loguj(Urzytkownik w)
        {
            lock (ListaUrzytkowników)
            {
                Urzytkownik uk = PobierzUrzytkownika(w);
                if (uk != null)
                {
                    if (CzyIstniejeUrzytkownikZarejstorowany(uk))
                    {
                        throw new FaultException<InvalidOperationException>(new InvalidOperationException("urzytkownij jest już zalogowany"));
                    }
                    else
                    {
                        Zarejestowani.Add(++LiczbaKlucz, uk);
                        return LiczbaKlucz;
                    }

                }
            }
          

            throw new FaultException<InvalidOperationException>(new InvalidOperationException("urzytkownij nie jest zarejstrowany"));
        }
        public bool CzyIstniejeUrzytkownikZarejstorowany(Urzytkownik uk) => Zarejestowani.Any(X => X.Value.Nazwa == uk.Nazwa);
        public Urzytkownik PobierzUrzytkownika(Urzytkownik uk) => ListaUrzytkowników.First(X => X.Nazwa == uk.Nazwa);

        public void Odświerz(Urzytkownik ul)
        {
            ListaUrzytkowników[ListaUrzytkowników.FindIndex(X => X == ul)].Przypisz(ul);
        }
        
        public Urzytkownik Pobierz(int K)
        {
            return Zarejestowani[K];
        }
        [FaultContract(typeof(InvalidOperationException))]
        public int Rejestruj(Urzytkownik uk)
        {
            lock (ListaUrzytkowników)
            {
                if (!ListaUrzytkowników.Any(X => X == uk))
                {
                    ListaUrzytkowników.Add(uk);
                    return Loguj(uk);
                }
                {
                    throw new FaultException<InvalidOperationException>(new InvalidOperationException("Login o podanej nazwie istnieje"));
                }
            }
         
        }
    }
}
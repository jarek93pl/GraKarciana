using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
namespace ClassLibrary1
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void StwórzKonto()
        {
            Urzytkownik urzytkownik = new Urzytkownik() { Haslo = "mojehaslo", Nazwa = "mojaNazwa" };
            SerwisObsugiKont ok = new SerwisObsugiKont();
            
           int w=  ok.Rejestruj(urzytkownik);
            
            ok.Pobierz(w);
        }
    }
}

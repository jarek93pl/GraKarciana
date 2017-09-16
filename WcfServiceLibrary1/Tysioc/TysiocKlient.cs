using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Security.Claims;
namespace GraKarciana
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,ConcurrencyMode =ConcurrencyMode.Multiple)]
    public class Tysioc : ITysioc,InstancjaGry
    {
        EventHandler zamykany;
        public event EventHandler Zamykany
        {
            add
            {
                zamykany += value;
            }
            remove
            {
                zamykany -= value;
            }
        }
        public Tysioc()
        {
            
            OperationContext.Current.InstanceContext.Faulted += Koniec;
            OperationContext.Current.InstanceContext.Closed += Koniec;
        }

        private void Koniec(object sender, EventArgs e)
        {
            zamykany?.Invoke(this, EventArgs.Empty);
        }

        static readonly object BlokadaDoGry = new object();
        public volatile static Gra1000 grawolna;
        public Gra1000 Rozgrywka;
        public ITysiocCalback DoOdpowiedz;
        public Urzytkownik Gracz { get; set; }

        [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.BeforeCall  )]
        public void CzekajNaGracza(int Nr)
        {

            PobierzGracza(Nr);
            //OperationContext.Current.InstanceContext.Host.
            DoOdpowiedz = OperationContext.Current.GetCallbackChannel<ITysiocCalback>();
            lock (BlokadaDoGry)
            {
                if (grawolna == null)
                {
                    grawolna = new Gra1000(3);
                }
                lock (grawolna)
                {
                    Rozgrywka = grawolna;
                    if (!grawolna.DodajGracza(this))
                    {
                        grawolna = null;
                    }
                }
            }
        }

        private void PobierzGracza(int Nr)
        {
            lock (SerwisObsugiKont.Zarejestowani)
            {
                Gracz = SerwisObsugiKont.Zarejestowani[Nr];
            }
        }

        public void Licytuj(int pk)
        {
            Rozgrywka.Licytuj(pk,this);
        }

        public void WyslijKarte(Karta k,bool Melduj)
        {
            Rozgrywka.WyślijKarte(k,this,Melduj);
        }

        public void WyslijMusek(Karta[] k)
        {
            Rozgrywka.WyślijMusek(k,this);
        }
    }
}
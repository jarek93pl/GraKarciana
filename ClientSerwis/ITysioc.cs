using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using GraKarciana;
using System.Threading.Tasks;

namespace ClientSerwis
{
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "Tysioc.ITysioc", CallbackContract = typeof(ITysiocCalback), SessionMode = System.ServiceModel.SessionMode.Required)]
    public interface ITysioc
    {

        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void CzekajNaGracza(int nr);
        [OperationContract(IsOneWay = true)]
        void Licytuj(int pk);
        [OperationContract(IsOneWay = true)]
        void WyslijKarte(Karta k, bool Melduj);
        [OperationContract(IsOneWay = true)]
        void WyslijMusek(Karta[] k);



        [OperationContract(IsInitiating = true, IsOneWay = true)]
        Task CzekajNaGraczaAsync(int nr);
        [OperationContract(IsOneWay = true)]
        Task LicytujAsync(int pk);
        [OperationContract(IsOneWay = true)]
        Task WyslijKarteAsync(Karta k, bool Melduj);
        [OperationContract(IsOneWay = true)]
        Task WyslijMusekAsync(Karta[] k);

    }

    public interface ITysiocCalback
    {


        [OperationContract(IsOneWay = true)]
        void ZnalezionoNowychGraczy(Urzytkownik[] gracze);
        [OperationContract(IsOneWay = true)]
        void TwojaLicytacja();
        [OperationContract(IsOneWay = true)]
        void Twojruch();
        [OperationContract(IsOneWay = true)]
        void OdbierzKarty(List<Karta> karty);
        [OperationContract(IsOneWay = true)]
        void KtosZalicytowal(string Login, int cena);
        [OperationContract(IsOneWay = true)]
        void KtosWyslalKarte(Karta k, string s, bool Melduj);
        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void KoniecGry(PodsumowanieTysioc pk);
        [OperationContract(IsOneWay = true)]
        void PodsumowanieRozgrywki(PodsumowanieTysioc pk);

    }
}
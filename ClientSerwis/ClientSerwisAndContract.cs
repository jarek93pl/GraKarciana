﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientSerwis
{
    using System.Runtime.Serialization;
    using System;
  

    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TysiocClient : System.ServiceModel.DuplexClientBase<ITysioc>, ITysioc
    {

        public TysiocClient(System.ServiceModel.InstanceContext callbackInstance) :
                base(callbackInstance)
        {
        }

        public TysiocClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
                base(callbackInstance, endpointConfigurationName)
        {
        }

        public TysiocClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public TysiocClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public TysiocClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(callbackInstance, binding, remoteAddress)
        {
        }

        public void CzekajNaGracza(int nr)
        {
            base.Channel.CzekajNaGracza(nr);
        }

        public System.Threading.Tasks.Task CzekajNaGraczaAsync(int nr)
        {
            return base.Channel.CzekajNaGraczaAsync(nr);
        }

        public void Licytuj(int pk)
        {
            base.Channel.Licytuj(pk);
        }

        public System.Threading.Tasks.Task LicytujAsync(int pk)
        {
            return base.Channel.LicytujAsync(pk);
        }

        public void WyslijKarte(GraKarciana.Karta k, bool Melduj)
        {
            base.Channel.WyslijKarte(k, Melduj);
        }

        public System.Threading.Tasks.Task WyslijKarteAsync(GraKarciana.Karta k, bool Melduj)
        {
            return base.Channel.WyslijKarteAsync(k, Melduj);
        }

        public void WyslijMusek(GraKarciana.Karta[] k)
        {
            base.Channel.WyslijMusek(k);
        }

        public System.Threading.Tasks.Task WyslijMusekAsync(GraKarciana.Karta[] k)
        {
            return base.Channel.WyslijMusekAsync(k);
        }
    }
}

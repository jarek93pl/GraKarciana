using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraKarciana;
using ClientSerwis;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        KontrolerTysioc kontroler;
        TysiocClient client;
        public string Nazwa { get; internal set; }

        public Form1()
        {
            kontroler = new KontrolerTysioc();
            InstanceContext instance = new InstanceContext(kontroler);
            kontroler.Initialize(client= new TysiocClient(instance));
            InitializeComponent();
        }
        
        protected async override void OnLoad(EventArgs e)
        {
            Urzytkownik.DoKontaClient uk = new Urzytkownik.DoKontaClient();

            kontroler.ZmianaStanu += (o, ee) => Text = Nazwa + kontroler.Stan.ToString();
            kontroler.ZmianaStołu += Kontroler_ZmianaStołu;
            kontroler.TwójRuchEv += Kontroler_TwójRuchEv;
            int playerIndex = uk.Rejestruj(new ClientSerwis. Urzytkownik() {Nazwa = Guid.NewGuid().ToString() ,Haslo="bardzo trudne"});
            await client.CzekajNaGraczaAsync(playerIndex);
            base.OnLoad(e);
        }

        private void Kontroler_TwójRuchEv(object sender, EventArgs e)
        {
            if (kontroler.DostępneKarty != null)
            {
                listBox3.Items.Clear();
                var kolekcja = Array.ConvertAll<Karta, object>(kontroler.DostępneKarty.ToArray(), X => new OpakowanieKarty(X));
                Array.Sort(kolekcja);
                listBox3.Items.AddRange(kolekcja);
            }
        }

        private void Kontroler_ZmianaStołu(object sender, EventArgs e)
        {
            if (kontroler.TwojeKarty != null)
            {
                listBox1.Items.Clear();
                var kolekcja = Array.ConvertAll<Karta, object>(kontroler.TwojeKarty.ToArray(), X => new OpakowanieKarty(X));
                Array.Sort(kolekcja);
                listBox1.Items.AddRange(kolekcja);
               
            }
            if (kontroler.Stół != null)
            {
                listBox2.Items.Clear();
                var kolekcja = Array.ConvertAll<Karta, object>(kontroler.Stół.ToArray(), X => new OpakowanieKarty(X));
                listBox2.Items.AddRange(kolekcja);
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (kontroler.Stan==Stan.TwojaLicytacja)
            {
                await kontroler.LicytujAsync(Convert.ToInt32(textBox1.Text));

            }
        }

        List<Karta> wysylanymusek = new List<Karta>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (kontroler.Stan==Stan.WysylanieMusku)
            {
                Karta zaznaczona =(OpakowanieKarty) listBox1.SelectedItem;
                listBox1.Items.Remove(listBox1.SelectedItem);
                wysylanymusek.Add(zaznaczona);
                if (wysylanymusek.Count==2)
                {
                    kontroler.WysyłanieMuskuAsync(wysylanymusek);
                }
            }
            if (kontroler.Stan == Stan.TwójRuch)
            {
                if (listBox3.SelectedItem==null)
                {
                    MessageBox.Show("nie zaznaczyłeś żadnej karty");
                    return;
                }
                kontroler.WyslijKarteMeldującAsync(((OpakowanieKarty) listBox3.SelectedItem));
            }

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {

        }

    }
}

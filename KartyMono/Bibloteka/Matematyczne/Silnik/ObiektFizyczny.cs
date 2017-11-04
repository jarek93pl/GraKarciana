using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Komputer.Matematyczne.Figury;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Komputer.Xna.Menu;
namespace Komputer.Matematyczne.Silnik
{
    public delegate void WyślijVektor(object snder, Vector2 Vektor);
    public class ObiektFizyczny
    {
        public Matrix viewMatrix, projectionMatrix;
        protected float kierunek = 0;
        public event WyślijVektor EventKolizja; 
        public static Texture2D Linia;
        IPobierzWyskokość Mapa;
        public Model ModelObiektu;
        public bool Statyczny;
        public bool MalujSzkielet = false;
        public float Masa = 1500;

        public ObiektFizyczny(Model m,IPobierzWyskokość hm)
        {
            ModelObiektu = m;
            Mapa = hm;
        }
        public FiguraZOdcinków Szkielet = new FiguraZOdcinków();
        /// <summary>
        /// Jeżeli chcesz by obraz
        /// </summary>
        public Vector2 Wzgledność = Vector2.Zero;
        protected Vector2 kierunekWektor;
        public Vector2 Miejsce { get; set; }
        public Vector2 KierunekWektor
        {
            get { return kierunekWektor; }
            set { kierunekWektor = value; }
        }
        public static void ZaładujLinie(GraphicsDevice gd)
        {
            Linia = new Texture2D(gd, 100, 2);
            Color[] ck = new Color[200];
            for (int i = 0; i < ck.Length; i++)
            {
                ck[i] = Color.White;
            }
            Linia.SetData(ck);
        }
        public bool Kolizja(ObiektFizyczny ob,out Vector2 PunktStyku)
        {
            PunktStyku = Vector2.Zero;
            Vector2 v = ob.Miejsce-Miejsce;
            float zasieng=ob.Szkielet.MaksymalnyZasieng+Szkielet.MaksymalnyZasieng;
            if(v.X*v.X+v.Y*v.Y>(zasieng*zasieng))
               return false;
            if (Szkielet.Kolizja(ob.Szkielet,v, out PunktStyku))
            {
                if (EventKolizja!=null)
                {
                    EventKolizja(this, v);
                }
                return true;
            }
            return false;
           
        }

        public float Kierunek
        {
            get { return kierunek; }
            set
            {
                Szkielet *= Matrix.CreateRotationZ(value-kierunek);
                kierunek = value.Napraw();
            }
        }

        Vector3 Normals;
        public virtual void UpDate(Microsoft.Xna.Framework.GameTime GT)
        {
            Miejsce += kierunekWektor;
            float wysokość;
            Mapa.GetHeightAndNormal(Miejsce, out wysokość, out Normals);
            na3 = Miejsce.NaV3(wysokość);
        }
        internal void Przesóń(float WspółczynikZmiany, ObiektFizyczny obiektFizyczny2, Vector2 PunkStyku, Vector2 WzglednośćSierodków, Vector2 WzglednośćPredkości)
        {
            kierunekWektor += WzglednośćPredkości;
            Miejsce -= WzglednośćSierodków * 5 / WzglednośćSierodków.Length();
            float Iloraz = 0.1f;
            float SKąt = Convert.ToSingle(Math.PI + Math.Atan2(PunkStyku.Y, PunkStyku.X));
            float SKąt2 = Convert.ToSingle(Math.PI + Math.Atan2(WzglednośćSierodków.Y, WzglednośćSierodków.X));
            Iloraz *= Math.Abs(SKąt - SKąt2);
            if ((SKąt) % MathHelper.PiOver2 < MathHelper.PiOver4)
            {
                Iloraz *= 1;
            }
            else
            {
                Iloraz *= -1;
            }
            Kierunek += Iloraz;

        }
        public virtual void Draw(SpriteBatch sp)
        {


            if (MalujSzkielet)
            {
                foreach (var item in Szkielet)
                {
                    sp.Draw(Linia, Miejsce + item.Poczotek - Wzgledność, null, Color.Red, item.Kąt, Vector2.Zero, item.Dłougość / 100, SpriteEffects.None, 0);
                }  
            }
            Matrix[] boneTransforms = new Matrix[ModelObiektu.Bones.Count];
            ModelObiektu.CopyAbsoluteBoneTransformsTo(boneTransforms);
            for (int i = 0; i < boneTransforms.Length; i++)
            {
                boneTransforms[i] *= Matrix.CreateScale(7f) * Matrix.CreateRotationY(MathHelper.Pi -kierunek);
            }
            // calculate the tank's world matrix, which will be a combination of our
            // orientation and a translation matrix that will put us at at the correct
            // position.
            Matrix worldMatrix = Matrix.CreateTranslation(na3);

            foreach (ModelMesh mesh in ModelObiektu.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] * worldMatrix;
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    // Set the fog to match the black background color
                    effect.FogEnabled = true;
                    effect.FogColor = Vector3.Zero;
                    effect.FogStart = 1000;
                    effect.FogEnd = 3200;
                }
                mesh.Draw();
            }
        }


        Vector3 na3;
        public Vector3 MiejsceNa3 { get { return na3; } }
    }
    public static class Kąty
    {
        public static float Napraw(this float Lidrzba)
        {
            Lidrzba = Lidrzba > MathHelper.Pi ? -MathHelper.TwoPi + Lidrzba : Lidrzba;
            Lidrzba = Lidrzba < MathHelper.Pi ? MathHelper.TwoPi + Lidrzba : Lidrzba;
            return Lidrzba;
        }
        public static Vector3 NaV3(this Vector2 v, float Wysokość)
        {
            return new Vector3(v.X, Wysokość, v.Y);
        }
    }
}

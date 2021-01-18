using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computación_Gráfica_Priscila_Zapata
{
    class Margarita:Circunferencia
    {
        public double X;
        public double Y;
        public double R;
        public Color Color;
        public void Encender(Bitmap bitmap)     
        {
            double t = 0;
            double dt = 0.0005; //incremento entre mayor se pixela la figura 
            Vector m = new Vector();
            do
            {
                m.X0 = X + R * ((Math.Cos(4 * t)) * Math.Cos(t));
                m.Y0 = Y + R * ((Math.Cos(4 * t)) * Math.Sin(t));
                m.Color0 = Color;
                m.Encender(bitmap);
                t += dt;
            } while (t <= 2 * Math.PI);
        }

        public override void Apagar(Bitmap bitmap)
        {
            this.Color = Color.Black;
            this.Encender(bitmap);
        }
    }
}

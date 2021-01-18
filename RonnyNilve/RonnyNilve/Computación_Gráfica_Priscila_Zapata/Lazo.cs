using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computación_Gráfica_Priscila_Zapata
{
    class Lazo:Circunferencia
    {
        public double X;
        public double Y;
        public double R;
        public Color Color;
        public void Encender(Bitmap bitmap)  
        {
            double t = 0;
            double dt = 0.0005; //incremento entre mayor se pixela la figura 
            Vector p = new Vector();
            do
            {
                p.X0 = X + R * (Math.Sin(2 * t));
                p.Y0 = Y + R * (Math.Cos(3 * t));
                p.Color0 = Color;
                p.Encender(bitmap);
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

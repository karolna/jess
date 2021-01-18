using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computación_Gráfica_Priscila_Zapata
{
    class Hipociclo:Circunferencia
    {
        public double X;
        public double Y;
        public double R;
        public Color Color;
        public void Encender(Bitmap bitmap)       //ESTUDIAR
        {
            double t = 0;
            double dt = 0.0005;//incremento entre mayor se pixela la figura 
            Vector v = new Vector();
            do
            {
                v.X0 = X + R * (Math.Pow((Math.Sin(t)), 3));
                v.Y0 = Y + R * (Math.Pow((Math.Cos(t)), 3));
                v.Color0 = Color;
                v.Encender(bitmap);
                t += dt;
            } while (t <= 2 * Math.PI);
        }

        public void Apagar(Bitmap bitmap)
        {
            Color0 = Color.Black;
            Encender(bitmap);
        }
    }
}

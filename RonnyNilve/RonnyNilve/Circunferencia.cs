using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RonnyNilve
{
    class Circunferencia:Vector
    {
        public double Rd;
        public double t, dt, tf;
        public double anguloR, angulor;

       public Circunferencia(){}
        
        public override void Encender(Bitmap bitmap)
        {
            t = 0;

            Vector cir = new Vector();
            dt = 0.001;
            do
            {
                cir.X0 = X0 + Rd * Math.Cos(t);
                cir.Y0 = Y0 + Rd * Math.Sin(t);
                cir.Color0 = Color0;
                cir.Encender(bitmap);
                t = t + dt;
            } while (t <= 2 * Math.PI);  
        }
    }
}

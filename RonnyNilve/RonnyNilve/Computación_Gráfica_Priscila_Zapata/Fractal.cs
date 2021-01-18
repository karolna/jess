using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Computación_Gráfica_Priscila_Zapata
{
    class Fractal:Circunferencia
    {
        public Fractal(double nX0, double nY0, double nRadio, Color nColor0, Bitmap pixel, PictureBox ventana)
        {

        }
        ~Fractal()
        { }

        public void Mandelbrot(double X0, double Y0, out int ColorM)
        {
            double Xn1, Yn1, Xn, Yn, d;
            Xn = X0;
            Yn = Y0;
            ColorM = 0;
            int conti = 0;
            do //delimitacion del conjunto de mandelbrot
            {
                Xn1 = Math.Pow(Xn, 2) - (Math.Pow(Yn, 2)) + X0;
                Yn1 = (2 * Xn * Yn) + Y0;
                d = Math.Sqrt(Math.Pow(Xn1 - X0, 2) + Math.Pow(Yn1 - Y0, 2));
                if (d >= 2)
                {
                    break;
                }
                else
                {
                    Xn = Xn1;
                    Yn = Yn1;
                    conti = conti + 1;
                }
            } while (d <= 2 && conti <= 100);

            if (conti >= 100) //caso cuando esta acotada
            {
                ColorM = 0;
            }
            else// cuando tiende a infinito
            {
                ColorM = (conti % 15 + 1);
            }
        }

        public void Julia(double X0, double Y0, out int ColorM)
        {
            double Xn1, Yn1, Xn, Yn, d;
            Xn = X0;
            Yn = Y0;
            ColorM = 0;
            int conti = 0;
            do
            {
                Xn1 = Math.Pow(Xn, 2) - (Math.Pow(Yn, 2)) - 1;
                Yn1 = (2 * Xn) * (Yn) + 0.25;
                d = Math.Sqrt(Math.Pow(Xn1, 2) + Math.Pow(Yn1, 2));
                if (d >= 2)
                {
                    break;
                }
                else
                {
                    Xn = Xn1;
                    Yn = Yn1;
                    conti = conti + 1;
                }
            } while (d <= 2 && conti <= 100);

            if (conti >= 100) //en caso de estar acotado
            {
                ColorM = 0;
            }
            else// en caso de tender al infinito o fuera de la acotacion
            {

                ColorM = (conti % 15 + 1);
            }
        }

        public void Sierpinski(double Px, double Py, double Rx, double Ry, double Qx, double Qy, Bitmap Mapabits)
        {
            double Mx, Nx, Sx;
            double My, Ny, Sy;
            Mx = (Px + Rx) / 2;
            My = (Py + Ry) / 2;
            Nx = (Rx + Qx) / 2;
            Ny = (Ry + Qy) / 2;
            Sx = (Px + Qx) / 2;
            Sy = (Py + Qy) / 2;
            Segmento seg = new Segmento();
            seg.X = Mx;
            seg.Y = My;
            seg.Xf = Nx;
            seg.Yf = Ny;
            seg.Color = Color.Blue;
            seg.Encender(Mapabits);
            seg.X = Nx;
            seg.Y = Ny;
            seg.Xf = Sx;
            seg.Yf = Sy;
            seg.Color = Color.Blue;
            seg.Encender(Mapabits);
            seg.X = Sx;
            seg.Y = Sy;
            seg.Xf = Mx;
            seg.Yf = My;
            seg.Color = Color.Blue;
            seg.Encender(Mapabits);

            double d = Math.Sqrt(Math.Pow(Sx - Mx, 2) + Math.Pow(Sy - My, 2));
            if (d > 0.1)
            {
                Sierpinski(Sx, Sy, Nx, Ny, Qx, Qy, Mapabits);
                Sierpinski(Sx, Sy, Mx, My, Px, Py, Mapabits);
                Sierpinski(Mx, My, Rx, Ry, Nx, Ny, Mapabits);
            }
        }
    }
}

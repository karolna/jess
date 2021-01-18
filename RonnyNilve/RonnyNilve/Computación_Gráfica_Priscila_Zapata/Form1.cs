using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Computación_Gráfica_Priscila_Zapata
{
    public partial class Form1 : Form
    {
        Bitmap Mapabits = new Bitmap(700, 500);//Bitmap objeto definido para trabajar 
                                               //con imagenes definidas por datos de pixel
                                               //new Bitmap inicializa una nueva instancia con un tamaño seleccionado (anchura, altura)

        public double qx, qy;// variables para las coordenadas
        string op; //variable para dibujar como paint
        public int contv = 0;
        double PIX, PIY, PFX, PFY;
        double X1, Y1;

        //Vectores Marcas
        double[] Puntosx = new double[100];
        double[] Puntosy = new double[100];

        Color[] Paleta1 = new Color[16];

        //vector spline
        double[] Vx = new double[20];
        double[] Vy = new double[20];
        int cont=0;

        //algoritmo de relleno
        public List<double> listX = new List<double>();
        public List<double> listY = new List<double>();
        private int SX;
        private int SY;
        int i = 0;
        double X01, Y01, XF1, YF1;
        Color Colore;
        Color colorBaseMapa;

        public Form1()
        {
            InitializeComponent();
        }

        private void BTNEncPixel_Click(object sender, EventArgs e)
        {
            Mapabits.SetPixel(130, 90, Color.BlueViolet);//señala la posición y el color del Pixel
            VentanaPantalla.Image = Mapabits;//indica en el elemento pantalla que debe mostrar la imagen 
            //del bit con las características señaladas en la funcion Mapabits
        }

        private void BTNBandera_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 600; i++)
            {
                for (int j = 0; j <= 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.Red);//señala la posición y el color del Pixel
                }
            }
            for (int i = 0; i <= 600; i++)
            {
                for (int j = 180; j <= 280; j++)
                {
                    Mapabits.SetPixel(i, j, Color.Green);//señala la posición y el color del Pixel
                }
            }
            for (int i = 0; i <= 600; i++)
            {
                for (int j = 280; j <= 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.Blue);//señala la posición y el color del Pixel
                }
            }
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNDegradado1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j <= 230; j++)
                {
                    Mapabits.SetPixel(i, j, Color.FromArgb((int)(-1.108 * j + 255), (int)(1.108 * j), 0));
                }
            }
            for (int i = 0; i < 600; i++)
            {
                for (int j = 231; j < 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.FromArgb(0, (int)(-1.108 * j + 510), (int)(1.108 * j - 255)));
                }
            }
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNDiagonal_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 460; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    if (i < j)
                    {
                        Mapabits.SetPixel(i, j, Color.Red);
                    }
                    else
                    {
                        Mapabits.SetPixel(i, j, Color.Green);
                    }
                }
            }
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNDiagonalDegradado_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 460; i++)
            {
                int greenx = 51 * i / 92;
                int redx = ((i * 51 * (-1)) / 92) + 255;

                for (int j = 0; j < 460; j++)
                {
                    int greeny = ((j * 51 * (-1)) / 92) + 255;
                    int redy = 51 * j / 92;

                    int red = (redy + redx) / 2;
                    int green = (greeny + greenx) / 2;
                    Mapabits.SetPixel(i, j, Color.FromArgb(red, green, 0));
                }
                VentanaPantalla.Image = Mapabits;
            }
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            Paleta1[0] = Color.Black;
            Paleta1[1] = Color.Navy;
            Paleta1[2] = Color.Green;
            Paleta1[3] = Color.Aqua;
            Paleta1[4] = Color.Red;
            Paleta1[5] = Color.Purple;
            Paleta1[6] = Color.Maroon;
            Paleta1[7] = Color.LightGray;
            Paleta1[8] = Color.DarkGray;
            Paleta1[9] = Color.Blue;
            Paleta1[10] = Color.Lime;
            Paleta1[11] = Color.Silver;
            Paleta1[12] = Color.Teal;
            Paleta1[13] = Color.Fuchsia;
            Paleta1[14] = Color.Yellow;
            Paleta1[15] = Color.White;
        }

        private void BTNExamen_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 600; i++)
            {
                for (int j = 1; j < 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.FromArgb((int)(-0.0333 * i + 20), (int)(-0.0333 * i + 20), (int)(0.3917 * i + 20)));
                    VentanaPantalla.Image = Mapabits;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.White);
                }
            }
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNSegmento_Click(object sender, EventArgs e)
        {
            Segmento s = new Segmento();
            s.X = -7;
            s.Y = 6;
            s.Xf = 9;
            s.Yf = -5;
            s.Color = Color.Black;
            s.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNEncenderFVector_Click(object sender, EventArgs e)
        {
            Vector pix = new Vector();
            pix.X0 = 1;
            pix.Y0 = 1;
            pix.Color0 = Color.Black;
            pix.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNRecta_Click(object sender, EventArgs e)
        {
            Vector v = new Vector();
            double x = -10;
            do
            {
                v.X0 = x;
                v.Y0 = 0;
                v.Encender(Mapabits);
                x = x + 0.01;
                v.Color0 = Color.Black;
                VentanaPantalla.Image = Mapabits;
            } while (x < 10);

        }

        private void BTNDia_Click(object sender, EventArgs e)
        {
            Vector v = new Vector();
            double x = -10;
            do
            {
                v.X0 = x;
                v.Y0 = x;
                v.Encender(Mapabits);
                x = x + 0.01;
                v.Color0 = Color.Black;
                VentanaPantalla.Image = Mapabits;
            } while (x < 10);
        }

        private void BTNParabola_Click(object sender, EventArgs e)
        {
            Vector v = new Vector();
            double x = -10;
            do
            {
                v.X0 = x;
                v.Y0 = (x * x);
                v.Encender(Mapabits);
                x = x + 0.01;
                v.Color0 = Color.Black;
                VentanaPantalla.Image = Mapabits;
            } while (x < 10);
        }

        private void BTNEjes_Click(object sender, EventArgs e)
        {
            VentanaPantalla.Width = 600; VentanaPantalla.Height = 460;

            Segmento s = new Segmento();

            //EJE X
            s.X = -10;
            s.Y = 0;
            s.Xf = 10;
            s.Yf = 0;
            s.Color = Color.Black;
            s.Encender(Mapabits);

            //EJE Y
            s.X = 0;
            s.Y = 8;
            s.Xf = 0;
            s.Yf = -8;
            s.Color = Color.Black;
            s.Encender(Mapabits);

            VentanaPantalla.Image = Mapabits;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Segmento s = new Segmento();
            //Diagonal 
            s.X = -8;
            s.Y = 6;
            s.Xf = 8;
            s.Yf = -6;
            s.Color = Color.Red;
            s.Encender(Mapabits);

            //Diagonal invertida
            s.X = -9;
            s.Y = -3;
            s.Xf = 9;
            s.Yf = 3;
            s.Color = Color.Red;
            s.Encender(Mapabits);

            //linea recta sup
            s.X = -8;
            s.Y = 4;
            s.Xf = 8;
            s.Yf = 5;
            s.Color = Color.Red;
            s.Encender(Mapabits);

            //linea recta inf
            s.X = -8;
            s.Y = -4;
            s.Xf = 8;
            s.Yf = -5;
            s.Color = Color.Red;
            s.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Circunferencia c = new Circunferencia();
            c.X0 = 3;
            c.Y0 = -5;
            c.Rd = 2;
            c.Color0 = Color.Green;
            c.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNPoligono_Click(object sender, EventArgs e)
        {
            Poligono p = new Poligono();
            p.X = 3;
            p.Y = -2;
            p.R = 4;
            p.L = 5;
            p.Color = Color.Black;
            p.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNMuchasFiguras1_Click(object sender, EventArgs e)
        {
            //POLIGONOS
            Poligono p = new Poligono();
            //Poligono 1
            p.X = 5;
            p.Y = -2;
            p.R = 1;
            p.L = 6;
            p.Color = Color.Magenta;
            p.Encender(Mapabits);
            //Poligono 2
            p.X = 5;
            p.Y = 5;
            p.R = 2;
            p.L = 9;
            p.Color = Color.BlueViolet;
            p.Encender(Mapabits);

            //CIRCULOS
            Circunferencia c = new Circunferencia();
            c.X0 = -5;
            c.Y0 = 5;
            c.Rd = 1;
            c.Color0 = Color.RosyBrown;
            c.Encender(Mapabits);

            c.X0 = -3;
            c.Y0 = -3;
            c.Rd = 2;
            c.Color0 = Color.LawnGreen;
            c.Encender(Mapabits);

            c.X0 = -1;
            c.Y0 = 1;
            c.Rd = 1;
            c.Color0 = Color.BurlyWood;
            c.Encender(Mapabits);

            //SEGMENTOS
            Segmento s = new Segmento();

            s.X = -8;
            s.Y = 5;
            s.Xf = 8;
            s.Yf = 5;
            s.Color = Color.Red;
            s.Encender(Mapabits);

            s.X = -4;
            s.Y = 5;
            s.Xf = 4;
            s.Yf = -5;
            s.Color = Color.Orange;
            s.Encender(Mapabits);

            s.X = 4;
            s.Y = 5;
            s.Xf = -4;
            s.Yf = -5;
            s.Color = Color.Violet;
            s.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;

            s.X = 8;
            s.Y = -5;
            s.Xf = -8;
            s.Yf = -5;
            s.Color = Color.PaleTurquoise;
            s.Encender(Mapabits);
        }

        private void BTNHipociclo_Click(object sender, EventArgs e)
        {
            Hipociclo h = new Hipociclo();
            h.X = 0.0;
            h.Y = -2.5;
            h.R = 1.0;
            h.Color = Color.Black;
            h.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNMargarita_Click(object sender, EventArgs e)
        {
            Margarita m = new Margarita();
            m.X = -1.0;
            m.Y = 5.0;
            m.R = 2.0;
            m.Color = Color.Black;
            m.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNPlano_Click(object sender, EventArgs e)
        {
            VentanaPantalla.Width = 600; VentanaPantalla.Height = 460;
            Segmento s = new Segmento();

            //EJE X
            s.X = -10;
            s.Y = 0;
            s.Xf = 10;
            s.Yf = 0;
            s.Color = Color.Black;
            s.Encender(Mapabits);

            //EJE Y
            s.X = 0;
            s.Y = 8;
            s.Xf = 0;
            s.Yf = -8;
            s.Color = Color.Black;
            s.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNLazo_Click(object sender, EventArgs e)
        {
            Lazo l = new Lazo();
            l.X = 4;
            l.Y = 2;
            l.R = 4;
            l.Color = Color.Black;
            l.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNSegDin_Click_1(object sender, EventArgs e)
        {
            op = "Segmento";
        }

        private void BTNLazoDin_Click(object sender, EventArgs e)
        {
            op = "Lazo";
        }

        private void BTNMargaDin_Click(object sender, EventArgs e)
        {
            op = "Margarita";
        }

        private void BTNPolDin_Click(object sender, EventArgs e)
        {
            op = "Poligono";
        }

        private void BTNHipoDin_Click(object sender, EventArgs e)
        {
            op = "Hipociclo";
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            op = "Circunferencia";
        }
        private void BTNMarcas_Click(object sender, EventArgs e)
        {
            op = "Marcas";
            contv = 0;
        }

        //FIGURAS CUADRANTE
        private void BTNMFiguras_Click_1(object sender, EventArgs e)
        {
            //DIAMANTES
            Hipociclo h = new Hipociclo();
            h.X = 4;
            h.Y = 4;
            h.R = 2;
            h.Color = Color.BlueViolet;
            h.Encender(Mapabits);

            h.X = 2;
            h.Y = 2;
            h.R = 1;
            h.Color = Color.Violet;
            h.Encender(Mapabits);

            //MARGARITAS
            Margarita m = new Margarita();
            m.X = -5;
            m.Y = 3;
            m.R = 3;
            m.Color = Color.PaleGoldenrod;
            m.Encender(Mapabits);

            m.X = 5;
            m.Y = -3;
            m.R = 3;
            m.Color = Color.HotPink;
            m.Encender(Mapabits);

            //LASOS
            Lazo l = new Lazo();
            l.X = -3;
            l.Y = -3;
            l.R = 2;
            l.Color = Color.Fuchsia;
            l.Encender(Mapabits);

            l.X = -6;
            l.Y = -6;
            l.R = 1;
            l.Color = Color.Salmon;
            l.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        //*******************************************************************************************
        //GRÁFICA LAGRANGE
        private void BTNLagrange_Click(object sender, EventArgs e)
        {
            Vector v = new Vector();
            double t = Puntosx[0];
            double dt = 0.002;
            v.Color0 = Color.Fuchsia;
            do
            {
                v.X0 = t;
                v.Y0 = FuncionLagrange(t);
                t = t + dt;
                v.Encender(Mapabits);
            } while (t <= Puntosx[contv - 1]);
            VentanaPantalla.Image = Mapabits;
        }

        //FUNCION LAGRANGE
        public double FuncionLagrange(double x)
        {
            double s = 0;
            for (int i = 0; i < contv; i++)
            {
                double p = 1;
                for (int j = 0; j < contv; j++)
                {
                    if (j != i)
                    {
                        p = p * ((x - Puntosx[j]) / (Puntosx[i] - Puntosx[j]));
                    }
                }
                s = s + Puntosy[i] * p;
            }
            return s;
        }

//*******************************************************
        //GRÁFICO BEZIER
        private void BTNBezier_Click(object sender, EventArgs e)
        {
            Vector v = new Vector();
            double t, dt = 0.001;
            t = 0;
            double Xt = 0, Yt = 0;
            while (t <= 1)
            {
                Bezier(t, out Xt, out Yt);
                v.X0 = Xt;
                v.Y0 = Yt;
                v.Color0 = Color.Blue;
                v.Encender(Mapabits);
                t = t + dt;
            }
            VentanaPantalla.Image = Mapabits;
        }

        //PROCESO BEZIER
        public void Bezier(double t, out double Xt, out double Yt)
        {
            Xt = 0; Yt = 0;
            int i;
            int n = contv - 1;
            for (i = 0; i <= n; i++)
            {
                Xt = Xt + Puntosx[i] * (Factorial(n) / (Factorial(i) * Factorial(n - i)))
                                                * Math.Pow(1 - t, n - i) * Math.Pow(t, i);
                Yt = Yt + Puntosy[i] * (Factorial(n) / (Factorial(i) * Factorial(n - i)))
                                                * Math.Pow(1 - t, n - i) * Math.Pow(t, i);
            }
        }

        //FACTORIAL
        public double Factorial(double n)
        {
            if (n <= 1)
            {
                return 1;
            }
            else
            {
                return (n * Factorial(n - 1));
            }
        }
//******************************
        private void BTNPolinomial_Click(object sender, EventArgs e)
        {
            //Segmento segS= new Segmento();
            for (int j = 0; j < contv - 1; j++)
            {
                Segmento seg = new Segmento();
                seg.X = Puntosx[j];
                seg.Y = Puntosy[j];
                seg.Xf = Puntosx[j + 1];
                seg.Yf = Puntosy[j + 1];
                seg.Color = Color.Green;
                seg.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
            }
        }

        private void BTNTapete_Click(object sender, EventArgs e)
        {

            int ColorT = 1;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    ColorT = (int)(Math.Sin(i) - Math.Cos(j) + 16) % 15;
                    Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                    VentanaPantalla.Image = Mapabits;
                }
            }



            /*int ColorT = 1;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    ColorT = Math.Abs(((i * i) + (j * j)) % 15);//NOTA 1
                    Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                    VentanaPantalla.Image = Mapabits;

                }
            }*/
            /* con menos y valor absoluto
            int ColorT = 1;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    ColorT = Math.Abs(((i * i) - (j * j)) % 15);
                    Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                    VentanaPantalla.Image = Mapabits;
                }
            }
            */
            //NOTA 1: SI SE PONE - SALE ERROR POR LOS LIMITES FUERA DEL RANGO
        }

        private void BTNTMio_Click(object sender, EventArgs e)
        {
            int ColorT = 1;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    ColorT = (int)(((Math.Sqrt(i * j / 16)) + Math.Cos(i * j)) % 16);

                    Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                    VentanaPantalla.Image = Mapabits;
                }
            }
        }

        private void BTTapete2_Click(object sender, EventArgs e)
        {
            int ColorT = 1;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Procesos pant = new Procesos();
                    pant.FuncionTrasforma(i, j, out X1, out Y1);
                    ColorT = Convert.ToInt32(Math.Abs(X1 * X1 + Y1 * Y1) % 15);   
                    Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                    VentanaPantalla.Image = Mapabits;
                }
            }
        }

        //TAPETES
        private void CBTapetes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TAPETE 1
            if (CBTapetes.SelectedIndex == 0)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(((Math.Sqrt(i * j / 16)) + Math.Cos(i * j)) % 16);
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TAPETE 2
            if (CBTapetes.SelectedIndex == 1)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Cos(i + j) + 3) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TAPETE 3
            if (CBTapetes.SelectedIndex == 2)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Sqrt(i) + Math.Pow(j, 2)) % 15 + 1;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TAPETE 4
            if (CBTapetes.SelectedIndex == 3)
            {                
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Sin(i) - Math.Cos(j) + 6) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TAPETE 5
            if (CBTapetes.SelectedIndex == 4)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Sqrt(j) + Math.Sin(j)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TAPETE 6
            if (CBTapetes.SelectedIndex == 5)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Atan2(i,j)+ Math.Cos(j+2)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //Tapete 7
            if (CBTapetes.SelectedIndex == 6)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)((Math.BigMul(i,j)* Math.Sqrt(i+j))) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }                
            }

            //TAPETE 8
            if (CBTapetes.SelectedIndex == 7)
            {
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Cos(i+2) + Math.Pow(j, 2)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //TODOS LOS TAPETES OP 9 
            if (CBTapetes.SelectedIndex == 8)
            {
                int ColorT = 1;
                for (int i = 0; i < 150; i++)
                {
                    for (int j = 0; j < 230; j++)
                    {
                        ColorT = Math.Abs(((i * i) + (j * j)) % 15);
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 151; i < 300; i++)
                {
                    for (int j = 0; j < 230; j++)
                    {
                        ColorT = (int)(Math.Cos(i + j) + 3) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 301; i < 450; i++)
                {
                    for (int j = 0; j < 230; j++)
                    {
                        ColorT = (int)(Math.Sqrt(i) + Math.Pow(j, 2)) % 15 + 1;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 451; i < 600; i++)
                {
                    for (int j = 0; j < 230; j++)
                    {
                        ColorT = (int)(Math.Sin(i) - Math.Cos(j) + 6) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 0; i < 150; i++)
                {
                    for (int j = 230; j < 460; j++)
                    {
                        ColorT = (int)(Math.Sqrt(j) + Math.Sin(j)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 151; i < 300; i++)
                {
                    for (int j = 230; j < 460; j++)
                    {
                        ColorT = (int)(Math.Atan2(i, j) + Math.Cos(j + 2)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 301; i < 450; i++)
                {
                    for (int j = 230; j < 460; j++)
                    {
                        ColorT = (int)((Math.BigMul(i, j) * Math.Sqrt(i + j))) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
                for (int i = 451; i < 600; i++)
                {
                    for (int j = 230; j < 460; j++)
                    {
                        ColorT = (int)(Math.Cos(i + 2) + Math.Pow(j, 2)) % 15;
                        Mapabits.SetPixel(i, j, Paleta1[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }
          
        }

        //TEXTURAS
        private void CBTexturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AGUA
            if (CBTexturas.SelectedIndex == 0)
            {
                Color[] PaletaAgua = new Color[16];
                for (int i = 0; i <= 15; i++)
                {
                    PaletaAgua[i] = Color.FromArgb((int)(10.2 * i), (int)(10.2 * i), (int)(3.0667 * i + 204));
                }
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)((Math.BigMul(i, j) * Math.Sqrt(i + j))) % 10;
                        Mapabits.SetPixel(i, j, PaletaAgua[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //PIEDRA
            if (CBTexturas.SelectedIndex == 1)
            {
                Color[] PaletaPiedra = new Color[16];
                for (int i = 0; i <= 15; i++)
                {
                    PaletaPiedra[i] = Color.FromArgb((int)(8 * i + 64), (int)(8 * i+  64), (int)(3.0667 * i + 65));
                }
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (int)(Math.Abs(i * j)+ Math.Cos(i)) % 5; 
                        Mapabits.SetPixel(i, j, PaletaPiedra[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }

            //CESPED
            if (CBTexturas.SelectedIndex == 2)
            {
                Color[] PaletaCesped = new Color[16];
                for (int i = 0; i <= 15; i++)
                {
                    PaletaCesped[i] = Color.FromArgb((int)(0.933 * i + 16), (int)(7.8 * i + 123), (int)(0.2 * i + 204));
                }
                int ColorT = 1;
                for (int i = 0; i < 600; i++)
                {
                    for (int j = 0; j < 460; j++)
                    {
                        ColorT = (Math.Abs(i*j)) % 15;
                        Mapabits.SetPixel(i, j, PaletaCesped[ColorT]);
                        VentanaPantalla.Image = Mapabits;
                    }
                }
            }
        }

        // EXPOSICIONES 
        private void button2_Click_2(object sender, EventArgs e)
        {
            op = "PuntoSpline";
            contv = 0;
        }

        private void BTNSpline_Click_3(object sender, EventArgs e)
        {
            double[] a;
            double[] b;
            double[] c;
            double[] d;
            double sx;
            double t, dt;
            t = Vx[0];
            dt = 0.01;
            Spline s = new Spline();
            do
            {
                s.CoeficientesSpline(contv, Vx, Vy, out a, out b, out c, out d);
                s.funcSpline(t, contv, Vx, Vy, b, c, d, out sx);
                Vector v = new Vector();
                v.X0 = t;
                v.Y0 = sx;
                v.Color0 = Color.DarkBlue;
                v.Encender(Mapabits);
                t = t + dt;
            } while (t <= Vx[contv - 1]);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNPuntosRelleno_Click_1(object sender, EventArgs e)
        {
            Circunferencia c2 = new Circunferencia
            {
                Color0 = Color.Green,
                Rd = 0.15,
                X0 = X01,
                Y0 = Y01
            };
            c2.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
            // asignar valor a la posicion de los vectores
            Vx[i] = X01;
            Vy[i] = Y01;
            listX.Add(X01);
            listY.Add(Y01);
        }

        private void BTNDibujar_Click_1(object sender, EventArgs e)
        {
            for (int j = 0; j < listX.Count - 1; j++)
            {
                Segmento obj = new Segmento
                {
                    Color0 = Color.Red,
                    X = listX[j],
                    Y0 = listY[j],
                    Xf = listX[j + 1],
                    Yf = listY[j + 1]
                };
                obj.Encender(Mapabits);
            }
            Segmento obj1 = new Segmento
            {
                Color0 = Color.Red,
                X = listX[0],
                Y = listY[0],
                Xf = listX[listX.Count - 1],
                Yf = listY[listX.Count - 1]
            };
            obj1.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNScanLine_Click_1(object sender, EventArgs e)
        {
            int R, G, B;
            R = int.Parse(this.TXTColoRojo.Text);
            G = int.Parse(this.TXTColorVerde.Text);
            B = int.Parse(this.TXTColorAzul.Text);
            Color ColorRelleno = Color.FromArgb(R, G, B);
            for (int i = 0; i < 560; i++)
            {
                listX.Clear();
                for (int j = 0; j < 400; j++)
                {
                    if (Color.FromArgb(0, 0, 0, 0) != Mapabits.GetPixel(i, j))
                    {
                        listX.Add(j);
                        for (int k = (int)listX[0]; k < (int)listX[listX.Count - 1]; k++)
                        {
                            Mapabits.SetPixel(i, k, ColorRelleno);
                        }
                    }
                }
            }
            VentanaPantalla.Image = Mapabits;
            listX.Clear();
        }

        private void BTNMandelbrot_Click_1(object sender, EventArgs e)
        {
            //mandelbrot
            Color[] PaletaMan = new Color[16];

            for (int i = 0; i < 15; i++)
            {
                PaletaMan[i] = Color.FromArgb((int)(15.866 * i + 3), (int)(15.133 * i + 3), (int)(3.4 * i + 3));
            }

            Procesos Bs = new Procesos();
            Fractal Fract = new Fractal(0, 0, 0, Color.Aqua, Mapabits, VentanaPantalla);
            int ColorF;
            double rx, ry;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Bs.TRANSFORMA(i, j, out rx, out ry);
                    Fract.Mandelbrot(rx, ry, out ColorF);
                    Mapabits.SetPixel(i, j, PaletaMan[ColorF]);
                    VentanaPantalla.Image = Mapabits;
                }
            }
        }

        private void BTNJulia_Click_1(object sender, EventArgs e)
        {
            Color[] Paleta1 = new Color[16];

            for (int i = 0; i < 15; i++)
            {
                Paleta1[i] = Color.FromArgb((int)(0.866 * i + 3), (int)(3.058 * i + 3), (int)(15.266 * i + 3));
            }

            Procesos Bs = new Procesos();
            int colorD;
            Fractal fract = new Fractal(0, 0, 0, Color.Yellow, Mapabits, VentanaPantalla);
            double rx, ry;
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Bs.TRANSFORMA(i, j, out rx, out ry);
                    fract.Julia(rx, ry, out colorD);
                    Mapabits.SetPixel(i, j, Paleta1[colorD]);
                }
            }
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNSierspinski_Click_1(object sender, EventArgs e)
        {
            double Px = -6, Rx = 0, Qx = 6;
            double Py = -4, Ry = 6, Qy = -4;
            double Mx, Nx, Sx;
            double My, Ny, Sy;
            Segmento seg = new Segmento();
            seg.X0 = Px;
            seg.Y0 = Py;
            seg.Xf = Rx;
            seg.Yf = Ry;
            seg.Color = Color.Red;
            seg.Encender(Mapabits);
            Mx = (Px + Rx) / 2;
            My = (Py + Ry) / 2;
            seg.X0 = Rx;
            seg.Y0 = Ry;
            seg.Xf = Qx;
            seg.Yf = Qy;
            seg.Color = Color.Red;
            seg.Encender(Mapabits);
            Nx = (Rx + Qx) / 2;
            Ny = (Ry + Qy) / 2;
            seg.X0 = Qx;
            seg.Y0 = Qy;
            seg.Xf = Px;
            seg.Yf = Py;
            seg.Color = Color.Red;
            seg.Encender(Mapabits);
            Sx = (Px + Qx) / 2;
            Sy = (Py + Qy) / 2;
            Fractal fractal = new Fractal(0, 0, 0, Color.Aqua, Mapabits, VentanaPantalla);
            fractal.Sierpinski(Px, Py, Rx, Ry, Qx, Qy, Mapabits);
            VentanaPantalla.Image = Mapabits;
        }

        private void BTNCantor_Click_1(object sender, EventArgs e)
        {
            Segmento s = new Segmento();
            s.X = -4;
            s.Y = 6;
            s.Xf = 4;
            s.Yf = 6;
            s.Color = Color.Blue;
            s.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
            Cantor(s);
        }

        private void Cantor(Segmento s)
        {
            if (s.Xf <= -4)
                return;
            s.Yf -= 1;

            double d = (s.Xf - s.X) / 3;
            Segmento s1 = new Segmento();
            s1.X = s.X;
            s1.Y = s.Yf;
            s1.Xf = s.X + d;
            s1.Yf = s.Yf;
            s1.Color = Color.Blue;
            s1.Encender(Mapabits);

            Segmento s2 = new Segmento();
            s2.X = s.Xf - d;
            s2.Y = s.Yf;
            s2.Xf = s.Xf;
            s2.Yf = s.Yf;
            s2.Color = Color.Blue;
            s2.Encender(Mapabits);
            
            VentanaPantalla.Refresh();
            Cantor(s1);
            Cantor(s2);
        }
        //*****************************************

        //ENCENDER Y APAGAR
        private void BTNFunEncender_Click_1(object sender, EventArgs e)
        {
            //esto es la prueba con el ing
            Circunferencia C = new Circunferencia();
            double w;
            w = 9;
            do
            {
                C.X0 = w;
                C.Y0 = 0;
                C.Rd = 1.5;
                C.Color0 = Color.BlueViolet;
                C.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                w = w - 0.2;
                Thread.Sleep(20);
                VentanaPantalla.Refresh();
                C.Apagar(Mapabits);
            } while (w >= -9);
            do
            {
                C.X0 = w;
                C.Y0 = (w + 10) * (w - 10);
                C.Rd = 1.5;
                C.Color0 = Color.BlueViolet;
                C.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                w = w + 0.2;
                Thread.Sleep(20);
                VentanaPantalla.Refresh();
                C.Apagar(Mapabits);
            } while (w <= 9);

            //esto es para probar normalmente
            /*double w, wf;
            Circunferencia hp = new Circunferencia();
            w = 0;
            wf = 0;
            hp.X0 = w;
            hp.Y0 = wf;
            hp.Rd = 2;
            hp.Color0 = Color.BlueViolet;
            hp.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;            
            Thread.Sleep(12);
            VentanaPantalla.Refresh();
            hp.Apagar(Mapabits);*/
        }

        private void BTNCirculoSeno_Click(object sender, EventArgs e)
        {

            double x, dx;
            Vector v = new Vector();
            x = 0;
            dx = 0.3;

            Circunferencia cir = new Circunferencia();
       
            //Izquierda-Derecha
            
            do
            {
                cir.Color0 = Color.BlueViolet;
                cir.X0 = x - 9;
                cir.Y0 = (-x * (x - 5));
                cir.Rd = 1;
                cir.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                x = x + dx;
                Thread.Sleep(60);
                VentanaPantalla.Refresh();
                cir.Apagar(Mapabits);
            } while (x <= 5);

            x = 0;
            dx = 0.3;
            do
            {
                cir.Color0 = Color.BlueViolet;
                cir.X0 = x - 4;
                cir.Y0 = (-x * (x - 5)) / 1.5;
                cir.Rd = 1;
                cir.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                x = x + dx;
                Thread.Sleep(60);
                VentanaPantalla.Refresh();
                cir.Apagar(Mapabits);
            } while (x <= 5);

            x = 0;
            dx = 0.3;
            do
            {
                cir.Color0 = Color.BlueViolet;
                cir.X0 = x;
                cir.Y0 = (-x * (x - 5)) / 2;
                cir.Rd = 1;
                cir.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                x = x + dx;
                Thread.Sleep(60);
                VentanaPantalla.Refresh();
                cir.Apagar(Mapabits);
                
            } while (x <= 5);

            x = 0;
            dx = 0.3;
            do
            {
                cir.Color0 = Color.BlueViolet;
                cir.X0 = x + 5;
                cir.Y0 = (-x * (x - 5)) / 3;
                cir.Rd = 1;
                cir.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                x = x + dx;
                Thread.Sleep(60);
                VentanaPantalla.Refresh();
                cir.Apagar(Mapabits);
            } while (x <= 5);
        }        
        //******************
        private void button3_Click_2(object sender, EventArgs e)
        {
            Circunferencia C1 = new Circunferencia();
            C1.X0 = 0;
            C1.Y0 = 0;
            C1.Rd = 7;
            C1.Color0 = Color.BlueViolet;
            C1.Encender(Mapabits);
            C1.X0 = 0;
            C1.Y0 = 0;
            C1.Rd = 3;
            C1.Color0 = Color.HotPink;
            C1.Encender(Mapabits);
            //animación
            Circunferencia C = new Circunferencia();
            double w;
            //Izquierda-Derecha
            w = 1.8;
            do
            {
                C.X0 = 0 + 5 * Math.Sin(w);
                C.Y0 = 0 + 5 * Math.Cos(w);
                C.Rd = 1;
                C.Color0 = Color.Purple;
                C.Encender(Mapabits);
                VentanaPantalla.Image = Mapabits;
                w = w + 0.2;
                Thread.Sleep(25);
                VentanaPantalla.Refresh();
                C.Apagar(Mapabits);
            } while (w >= -5);
        }
        private void VentanaPantalla_MouseUp(object sender, MouseEventArgs e)
        {

            int Xpost, Ypost;
            Procesos Pro = new Procesos();
            Xpost = e.X;        // obtengo la coordenada x
                                // Xpost = MousePosition.X;
            Ypost = e.Y;       //obtengo la coordenada y, 
            Pro.ProcesoReal(Xpost, Ypost, out PFX, out PFY);
            double radio = Math.Sqrt(Math.Pow((PFX - PIX), 2) + Math.Pow((PFY - PIY), 2));

            switch (op)
            {
                case "Segmento":
                    Segmento seg = new Segmento();
                    seg.X = PIX;
                    seg.Y = PIY;
                    seg.Xf = PFX;
                    seg.Yf = PFY;
                    seg.Color = Color.Yellow;
                    seg.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;

                case "Circunferencia":
                    for (int i = 0; i < 3; i++)
                    {
                        Circunferencia objcir = new Circunferencia();
                        objcir.X0 = PIX;
                        objcir.Y0 = PIY;
                        objcir.Rd = radio;
                        objcir.Color0 = Color.Yellow;
                        objcir.Encender(Mapabits);
                    }
                    VentanaPantalla.Image = Mapabits;
                    break;

                case "Lazo":
                    Lazo objlaz = new Lazo();
                    //objlaz.t = 0;
                    objlaz.tf = 2 * Math.PI;
                    objlaz.X = PIX;
                    objlaz.Y = PIY;
                    objlaz.R = radio;
                    objlaz.Color = Color.PaleVioletRed;
                    objlaz.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;

                case "Margarita":
                    Margarita Marg = new Margarita();
                    Marg.X = PIX;
                    Marg.Y = PIY;
                    Marg.R = radio;
                    Marg.Color = Color.LightCoral;
                    Marg.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;

                case "Hipociclo":
                    Hipociclo hp = new Hipociclo();
                    hp.X = PIX;
                    hp.Y = PIY;
                    hp.R = radio;
                    hp.Color = Color.CadetBlue;
                    hp.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;

                case "Poligono":

                    int Nlados = Int32.Parse(TXTLadosP.Text);
                    if (Nlados == 0)
                    {
                        MessageBox.Show("No se puede generar poligonos con 0 lados", "Mensaje de Advertencia",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                    if (Nlados < 3 || Nlados > 15)
                    {
                        MessageBox.Show("No se puede generar poligonos con menos de tres y mas de 15  lados", "Mensaje de Advertencia",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        Poligono objpol = new Poligono();
                        objpol.X = PIX;
                        objpol.Y = PIY;
                        objpol.R = radio;
                        objpol.L = Nlados;
                        objpol.Color = Color.GreenYellow;
                        objpol.Encender(Mapabits);
                        VentanaPantalla.Image = Mapabits;
                    }
                    break;
                case "Marcas":
                    Puntosx[contv] = PIX;
                    Puntosy[contv] = PIY;
                    contv++;
                    Circunferencia cir = new Circunferencia();
                    cir.X0 = PIX;
                    cir.Y0 = PIY;
                    cir.Rd = 0.15;
                    cir.Color0 = Color.HotPink;
                    cir.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                case "PuntoSpline":
                    Vx[contv] = PIX;
                    Vy[contv] = PIY;
                    contv++;
                    Circunferencia c = new Circunferencia();
                    c.X0 = PIX;
                    c.Y0 = PIY;
                    c.Rd = 0.15;
                    c.Color0 = Color.HotPink;
                    c.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }


        private void CBFiguras3d_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.White);
                }
            }
            SuperficieV s = new SuperficieV();
            switch (CBFiguras3d.SelectedIndex)
            {
                //cilindro
                case 0:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 0;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //esfera
                case 1:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 1;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //toroide
                case 2:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 2;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //hiperboloide de una hoja
                case 3:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 4;
                    s.opc = 3;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //TODAS LAS FIGURAS
                case 4:
                    //CILINDRO
                    s.X0 = -7;
                    s.Y0 = 4;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 0;
                    s.Color0 = Color.HotPink;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    //ESFERA
                    s.X0 = 2;
                    s.Y0 = 8;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 1;
                    s.Color0 = Color.DarkBlue;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    //TOROIDE
                    s.X0 = -2;
                    s.Y0 = -7;
                    s.z0 = 0;
                    s.Rd = 1.5;
                    s.opc = 2;
                    s.Color0 = Color.DarkCyan;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    //HIPERBOLOIDE DE UNA HOJA
                    s.X0 = 8;
                    s.Y0 = -8;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 3;
                    s.Color0 = Color.BlueViolet;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //mia
                case 5:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 4;
                    s.Color0 = Color.DarkCyan;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //FONDO Y SUPERFICIE
                case 6:
                    //ESFERA
                    s.X0 = -8;
                    s.Y0 = 8;
                    s.z0 = 0;
                    s.Rd = 2;
                    s.opc = 1;
                    s.Color0 = Color.DarkBlue;
                    s.Encender(Mapabits);

                    //mio 
                    s.X0 = 0;
                    s.Y0 = 8;
                    s.z0 = 0;
                    s.Rd = 2;
                    s.opc = 4;
                    s.Color0 = Color.DarkCyan;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;

                    //piso
                    Segmento s1 = new Segmento();

                    //HORIZONTALES
                    s1.X = -10;
                    s1.Y = -2;
                    s1.Xf = 10;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = -10;
                    s1.Y = -3;
                    s1.Xf = 10;
                    s1.Yf = -3;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;

                    s1.X = -10;
                    s1.Y = -4.5;
                    s1.Xf = 10;
                    s1.Yf = -4.5;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    //VERTICALES
                    s1.X = -10;
                    s1.Y = -4.2;
                    s1.Xf = -8.5;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = -9;
                    s1.Y = -7.67;
                    s1.Xf = -6;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = -5.5;
                    s1.Y = -7.67;
                    s1.Xf = -3.5;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = -2.5;
                    s1.Y = -7.67;
                    s1.Xf = -1;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    //---------------------
                    s1.X = 2.5;
                    s1.Y = -7.67;
                    s1.Xf = 1;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = 5.5;
                    s1.Y = -7.67;
                    s1.Xf = 3.5;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = 9;
                    s1.Y = -7.67;
                    s1.Xf = 6;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s1.X = 10;
                    s1.Y = -4.2;
                    s1.Xf = 8.5;
                    s1.Yf = -2;
                    s1.Color = Color.Black;
                    s1.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //cilindro rotación
                case 7:
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Rd = 3;
                    s.opc = 0;
                    s.Alfa = 0;
                    s.Color0 = Color.Red;
                    s.Encender(Mapabits);
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Alfa = 0.79;//0.78;////0.9;0.23;//
                    s.Eje0 = 0;
                    s.Rd = 3;
                    s.opc = 0;
                    s.Color0 = Color.Green;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //esfera rotación
                case 8:
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Rd = 3;
                    s.opc = 1;
                    s.Alfa = 0;
                    s.Color0 = Color.Red;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Alfa = 0.56;//0.78;////0.9;0.23;//
                    s.Eje0 = 0;
                    s.Rd = 3;
                    s.opc = 1;
                    s.Color0 = Color.Green;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //toroide rotacion
                case 9:
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Rd = 3;
                    s.opc = 2;
                    s.Alfa = 0;
                    s.Color0 = Color.Red;
                    s.Encender(Mapabits);
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Alfa = 0.79;//0.78;////0.9;0.23;//
                    s.Eje0 = 1;
                    s.Rd = 3;
                    s.opc = 2;
                    s.Color0 = Color.Green;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //hiperboloide de una hoja
                case 10:
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Rd = 4;
                    s.opc = 3;
                    s.Alfa = 0;
                    s.Color0 = Color.Red;
                    s.Encender(Mapabits);
                    s.X0 = 0.1;
                    s.Y0 = 0.1;
                    s.z0 = 0.1;
                    s.Alfa = 0.79;//0.78;////0.9;0.23;//
                    s.Eje0 = 2;
                    s.Rd = 4;
                    s.opc = 3;
                    s.Color0 = Color.Green;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }

        private void CBCuadrilateros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBCuadrilateros.SelectedIndex == 0)
            {
                Vector3D vector = new Vector3D();
                Double px, py, qx, qy, rx, ry, sx, sy;
                px = -4; py = 3.5; // punto p
                qx = 3.5; qy = 3;// punto q
                rx = 3.5; ry = -1;// punto r
                sx = -5; sy = -5;// punto s
                vector.Cuadrilatero(px, py, qx, qy, rx, ry, sx, sy, 1, Mapabits);
                VentanaPantalla.Image = Mapabits;
            }
            if (CBCuadrilateros.SelectedIndex == 1)
            {
                Vector3D vector = new Vector3D();
                Double px, py, qx, qy, rx, ry, sx, sy;
                px = -4; py = 3.5; // punto p
                qx = 3.5; qy = 3;// punto q
                rx = 3.5; ry = -1;// punto r
                sx = -5; sy = -5;// punto s
                vector.Cuadrilatero(px, py, qx, qy, rx, ry, sx, sy,  2, Mapabits);
                VentanaPantalla.Image = Mapabits;
            }
            if (CBCuadrilateros.SelectedIndex == 2)
            {
                Vector3D vector = new Vector3D();
                Double px, py, qx, qy, rx, ry, sx, sy;
                px = -4; py = 3.5; // punto p
                qx = 3.5; qy = 3;// punto q
                rx = 3.5; ry = -1;// punto r
                sx = -5; sy = -5;// punto s
                vector.Cuadrilatero(px, py, qx, qy, rx, ry, sx, sy, 3, Mapabits);
                VentanaPantalla.Image = Mapabits;
            }
        }

        private void BTNPrueba3_Click(object sender, EventArgs e)
        {
            Segmento3D s3 = new Segmento3D();
            //cuadrante principal 
            //vertical
            s3.X0 = -7;
            s3.Y0 = 3;
            s3.z0 = 0;
            s3.xf = -7;
            s3.yf = 3;
            s3.zf = 3;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            //horizontal
            s3.X0 = -7;
            s3.Y0 = 3;
            s3.z0 = 0;
            s3.xf = -4;
            s3.yf = 3;
            s3.zf = 0;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            //diagonal
            s3.X0 = -4;
            s3.Y0 = 3;
            s3.z0 = 0;
            s3.xf = -7;
            s3.yf = 3;
            s3.z0 = 2;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            //cuadrante trasero 
            //vertical
            s3.X0 = -4;
            s3.Y0 = 7.5;
            s3.z0 = 0;
            s3.xf = -4;
            s3.yf = 9;
            s3.zf = 0;
            s3.Color0 = Color.Blue;
            s3.Encender(Mapabits);
            
            //
            s3.X0 = 4;
            s3.Y0 = 5;
            s3.z0 = 3;
            s3.xf = 4;
            s3.yf = 5;
            s3.zf = 0;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 5;
            s3.z0 = 3;
            s3.xf = 4;
            s3.yf = 5;
            s3.zf = 3;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);            
            VentanaPantalla.Image = Mapabits;

        }

        private void CBMalladoSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            SuperficieR s = new SuperficieR();
            switch (CBMalladoSR.SelectedIndex)
            {
                //paraboloide
                case 0:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 5;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //paraboloide hiperbolico
                case 1:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 6;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //techito
                case 2:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 7;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 3:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 8;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 4:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 9;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }

        private void BTNRealizar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 600; i++)
            {
                for (int j = 0; j < 460; j++)
                {
                    Mapabits.SetPixel(i, j, Color.White);
                }
            }
            SuperficieV s = new SuperficieV();
            switch (CBFigurasTF.SelectedIndex)
            {
                //cilindro
                case 0:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 5;
                    switch (CBTipoTF.SelectedIndex)
                    {
                        case 0:
                            s.opcMa = 0;
                            break;
                        case 1:
                            s.opcMa = 1;
                            s.opcM = 2;
                            break;
                        case 2:
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }       
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //esfera
                case 1:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 6;
                    switch (CBTipoTF.SelectedIndex)
                    {
                        case 0:
                            s.opcMa = 0;
                            break;
                        case 1:
                            s.opcMa = 1;
                            s.opcM = 2;
                            break;
                        case 2:
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //toroide
                case 2:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 3;
                    s.opc = 7;
                    switch (CBTipoTF.SelectedIndex)
                    {
                        case 0:
                            s.opcMa = 0;
                            break;
                        case 1:
                            s.opcMa = 1;
                            s.opcM = 2;
                            break;
                        case 2:
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //hiperboloide de una hoja
                case 3:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = 0;
                    s.Rd = 4;
                    s.opc = 8;
                    switch (CBTipoTF.SelectedIndex)
                    {
                        case 0:
                            s.opcMa = 0;
                            break;
                        case 1:
                            s.opcMa = 1;
                            s.opcM = 2;
                            break;
                        case 2:
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }

        private void BTNRellenoEliminacionIluminacion_Click(object sender, EventArgs e)
        {
            SuperficieR s = new SuperficieR();
            switch (CBFigurasRt.SelectedIndex)
            {
                //paraboloide
                case 0:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 5;
                    switch (CBTipoFigRec.SelectedIndex)
                    {
                        case 0://malla
                            s.opcMa = 0;
                            s.opcM = 1;
                            break;
                        case 1://eliminacion
                            s.opcMa = 0;
                            s.opcM = 2;
                            break;
                        case 2://eliminacion
                            s.opcMa = 0;
                            s.opcM = 3;
                            break;
                        case 3://iluminacion
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //paraboloide hiperbolico
                case 1:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 6;
                    switch (CBTipoFigRec.SelectedIndex)
                    {
                        case 0://malla
                            s.opcMa = 0;
                            s.opcM = 1;
                            break;
                        case 1://eliminacion
                            s.opcMa = 0;
                            s.opcM = 2;
                            break;
                        case 2://eliminacion
                            s.opcMa = 0;
                            s.opcM = 3;
                            break;
                        case 3://iluminacion
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //techito
                case 2:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 7;
                    switch (CBTipoFigRec.SelectedIndex)
                    {
                        case 0://malla
                            s.opcMa = 0;
                            s.opcM = 1;
                            break;
                        case 1://eliminacion
                            s.opcMa = 0;
                            s.opcM = 2;
                            break;
                        case 2://eliminacion
                            s.opcMa = 0;
                            s.opcM = 3;
                            break;
                        case 3://iluminacion
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 3:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 8;
                    switch (CBTipoFigRec.SelectedIndex)
                    {
                        case 0://malla
                            s.opcMa = 0;
                            s.opcM = 1;
                            break;
                        case 1://eliminacion
                            s.opcMa = 0;
                            s.opcM = 2;
                            break;
                        case 2://eliminacion
                            s.opcMa = 0;
                            s.opcM = 3;
                            break;
                        case 3://iluminacion
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 4:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 9;
                    switch (CBTipoFigRec.SelectedIndex)
                    {
                        case 0://malla
                            s.opcMa = 0;
                            s.opcM = 1;
                            break;
                        case 1://eliminacion
                            s.opcMa = 0;
                            s.opcM = 2;
                            break;
                        case 2://eliminacion
                            s.opcMa = 0;
                            s.opcM = 3;
                            break;
                        case 3://iluminacion
                            s.opcMa = 1;
                            s.opcM = 3;
                            break;
                    }
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }

        private void CBFigurasTF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CBTipoTF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            
        }

        private void CBsuperficiesRectangulares_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SuperficieR s = new SuperficieR();
            switch (CBsuperficiesRectangulares.SelectedIndex)
            {
                //paraboloide
                case 0:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 0;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //paraboloide hiperbolico
                case 1:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 1;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //techito
                case 2:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 2;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 3:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 3;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
                //
                case 4:
                    s.X0 = 0;
                    s.Y0 = 0;
                    s.z0 = -3;
                    s.opc = 4;
                    s.Color0 = Color.Black;
                    s.Encender(Mapabits);
                    VentanaPantalla.Image = Mapabits;
                    break;
            }
        }

        private void VentanaPantalla_MouseDown(object sender, MouseEventArgs e)
        {
            int Xpost, Ypost;
            Procesos Bs = new Procesos();
            Xpost = e.X;        // obtengo la coordenada x
                                // Xpost = MousePosition.X;
            Ypost = e.Y;       //obtengo la coordenada y
            Bs.ProcesoReal(Xpost, Ypost, out PIX, out PIY);

        }
        private void Inundacion(Point pt, Color relleno)
        {
            Stack<Point> pixels = new Stack<Point>();
            Color ColorBorde = Mapabits.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);
            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < Mapabits.Width && a.X > 0 && a.Y < Mapabits.Height && a.Y > 0)
                    if (Mapabits.GetPixel(a.X, a.Y) == ColorBorde)
                    {
                        Mapabits.SetPixel(a.X, a.Y, relleno);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
            }
        }

        private void BTNInundacion_Click(object sender, EventArgs e)
        {
            int R = int.Parse(this.TXTColoRojo.Text);
            int G = int.Parse(this.TXTColorVerde.Text);
            int B = int.Parse(this.TXTColorAzul.Text);
            Color relleno = Color.FromArgb(R, G, B);
            Point p = new Point(SX, SY);
            Inundacion(p, relleno);
            VentanaPantalla.Image = Mapabits;
        }

        private void VentanaPantalla_MouseMove(object sender, MouseEventArgs e)
        {
            Procesos Bas = new Procesos();
            Bas.ProcesoReal(e.X, e.Y, out qx, out qy);
            LBX.Text = "X = " + Math.Round(qx, 2);
            Refresh();
            LBY.Text = "Y = " + Math.Round(qy, 2);
            Refresh();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Vector3D v3 = new Vector3D();
            double t, dt;
            t = 0;
            dt = 0.005;
            do
            {
                v3.X0 = 3 * Math.Cos(t);
                v3.Y0 = 3 * Math.Sin(t);
                v3.z0 = 0;
                v3.Color0 = Color.DarkBlue;
                v3.Encender(Mapabits);
                t = t + dt;
                VentanaPantalla.Image = Mapabits;
            } while (t <= 6.3);
        }

        private void BTNEspiral_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Segmento3D s3 = new Segmento3D();
            //cuadrante principal 
            s3.X0 = 0;
            s3.Y0 = 0;
            s3.z0 = 0;
            s3.xf = 0;
            s3.yf = 6;
            s3.zf = 0;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 0;
            s3.z0 = 0;
            s3.xf = 0;
            s3.yf = 0;
            s3.zf = 4;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 0;
            s3.z0 = 0;
            s3.xf = 5;
            s3.yf = 0;
            s3.zf = 0;
            s3.Color0 = Color.Red;
            s3.Encender(Mapabits);
            //cuadrante trasero 
            s3.X0 = 0;
            s3.Y0 = 6;
            s3.z0 = 0;
            s3.xf = 0;
            s3.yf = 6;
            s3.zf = 4;
            s3.Color0 = Color.Blue;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 6;
            s3.z0 = 0;
            s3.xf = 5;
            s3.yf = 6;
            s3.zf = 0;
            s3.Color0 = Color.Blue;
            s3.Encender(Mapabits);
            s3.X0 = 5;
            s3.Y0 = 0;
            s3.z0 = 0;
            s3.xf = 5;
            s3.yf = 6;
            s3.zf = 0;
            s3.Color0 = Color.Blue;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 0;
            s3.z0 = 4;
            s3.xf = 0;
            s3.yf = 6;
            s3.zf = 4;
            s3.Color0 = Color.Blue;
            s3.Encender(Mapabits);
            //cuadrante superior y lateral derecho 
            s3.X0 = 5;
            s3.Y0 = 6;
            s3.z0 = 4;
            s3.xf = 5;
            s3.yf = 6;
            s3.zf = 0;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 6;
            s3.z0 = 4;
            s3.xf = 5;
            s3.yf = 6;
            s3.zf = 4;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            s3.X0 = 5;
            s3.Y0 = 6;
            s3.z0 = 4;
            s3.xf = 5;
            s3.yf = 0;
            s3.zf = 4;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            s3.X0 = 5;
            s3.Y0 = 0;
            s3.z0 = 0;
            s3.xf = 5;
            s3.yf = 0;
            s3.zf = 4;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            s3.X0 = 0;
            s3.Y0 = 0;
            s3.z0 = 4;
            s3.xf = 5;
            s3.yf = 0;
            s3.zf = 4;
            s3.Color0 = Color.Green;
            s3.Encender(Mapabits);
            VentanaPantalla.Image = Mapabits;
        }
    }
}

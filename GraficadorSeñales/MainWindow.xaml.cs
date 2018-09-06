using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud =
                double.Parse(txtAmplitud.Text);
            double fase =
                double.Parse(txtFase.Text);
            double frecuencia =
                double.Parse(txtFrecuencia.Text);
            double tiempoInicial =
                double.Parse(txtTiempoInicial.Text);
            double tiempoFinal =
                double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo =
                double.Parse(txtFrecuenciaMuestreo.Text);

            SeñalSenoidal señal =
                new SeñalSenoidal(amplitud, fase, frecuencia);

            double periodoMuestreo = 1 / frecuenciaMuestreo;

            plnGrafica.Points.Clear();

            for(double i = tiempoInicial;  i <= tiempoFinal;  i += periodoMuestreo )
            {
                double valorMuestra =
                    señal.evaluar(i);

                if (Math.Abs(valorMuestra) > 
                    señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima =
                        Math.Abs(valorMuestra);
                }

                señal.Muestras.Add(
                    new Muestra(i, valorMuestra));
 
            }

            //Recorrer una coleccion o arreglo
            foreach(Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(
                    new Point((muestra.X - tiempoInicial) * scrContenedor.Width
                    , (muestra.Y / señal.AmplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1)
                    + (scrContenedor.Height / 2) )
                    );
            }
            plnEjeX.Points.Clear();
            //Punto del principio
            plnEjeX.Points.Add(
                new Point(0,                          //Coordenada X punto inicial
                (scrContenedor.Height / 2)));         //Coordenada Y punto inicial
            //Punto del fin
            plnEjeX.Points.Add(
                new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width,
                     // x final
                 (scrContenedor.Height / 2)));                         // y final

            plnEjeY.Points.Clear();
            //Punto del principio
            plnEjeY.Points.Add(
                new Point((0 - tiempoInicial) * scrContenedor.Width,  //Coordenada X punto inicial

                (                //Coordenada Y punto inicial
                ((scrContenedor.Height / 2.0) - 30) * -1)
                    + (scrContenedor.Height / 2)));         
            //Punto del fin
            plnEjeY.Points.Add(
                new Point((0 - tiempoInicial) * scrContenedor.Width,  //x final
                (-1 * ((scrContenedor.Height / 2.0) - 30) * -1) //y final
                    + (scrContenedor.Height / 2)));

            lblAmplitudMaximaY.Text =
                señal.AmplitudMaxima.ToString();
            lblAmplitudMaximaNegativaY.Text =
                "-" + señal.AmplitudMaxima.ToString();
        }

        private void btnGraficarRampa_Click(object sender, RoutedEventArgs e)
        {
            
            double tiempoInicial =
                double.Parse(txtTiempoInicial.Text);
            double tiempoFinal =
                double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo =
                double.Parse(txtFrecuenciaMuestreo.Text);

            SeñalRampa señal =
                new SeñalRampa();

            double periodoMuestreo = 1 / frecuenciaMuestreo;

            plnGrafica.Points.Clear();

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra =
                    señal.evaluar(i);

                if (Math.Abs(valorMuestra) >
                    señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima =
                        Math.Abs(valorMuestra);
                }

                señal.Muestras.Add(
                    new Muestra(i, valorMuestra));

            }

            //Recorrer una coleccion o arreglo
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(
                    new Point(muestra.X * scrContenedor.Width
                    , (muestra.Y * ((scrContenedor.Height / 2.0) - 30) * -1)
                    + (scrContenedor.Height / 2))
                    );
            }
        }
    }
}

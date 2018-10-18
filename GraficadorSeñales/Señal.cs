using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }

        public abstract double evaluar(double tiempo);

        public void construirSeñalDigital()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;
            for (double i = TiempoInicial; 
                i <= TiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra =
                    evaluar(i);

                if (Math.Abs(valorMuestra) >
                    AmplitudMaxima)
                {
                    AmplitudMaxima =
                        Math.Abs(valorMuestra);
                }

                Muestras.Add(
                    new Muestra(i, valorMuestra));

            }
        }

        public void actualizarAmplitudMaxima()
        {
            AmplitudMaxima = 1;
            foreach (Muestra muestra in Muestras)
            {
                if (Math.Abs(muestra.Y) > AmplitudMaxima)
                {
                   AmplitudMaxima = Math.Abs(muestra.Y);
                }
                
            }
        }

        public void desplazar(float magnitudDesplazamiento)
        {
            foreach (Muestra muestra in Muestras)
            {
                muestra.Y += magnitudDesplazamiento;
            }
        }

        public void escalar(float factorEscala)
        {
            foreach (Muestra muestra in Muestras)
            {
                muestra.Y *= factorEscala;
            }
        }

        public void truncar(float umbral)
        {
            foreach(Muestra muestra in Muestras)
            {
                if (muestra.Y > umbral)
                {
                    muestra.Y = umbral;
                } else if (muestra.Y < -umbral)
                {
                    muestra.Y = -umbral;
                }
            }
        }

        public static Señal sumar(Señal sumando1, Señal sumando2)
        {

            return null;
        }
    
    }
}

using System;

namespace bibliotecadeclases
{
    public enum Estaciones { Invierno = 0, Primavera = 1, Otoño = 2, Verano = 3 };

    public class SimuladorTiempo
    {
        private static int minValueD = 2;
        private static int  maxD = 12;

        private static int minValueM = 1;
        private static int maxM = 4;


        public DateTime Fecha { get; private set; }
        public Estaciones Estacion { get; private set; }
        private Random random = new Random();

        public void AvanzarDias() {
            int aleatorio = random.Next(minValueD, maxD);
            Fecha = Fecha.AddDays(aleatorio);
        }

        public void AvanzarMeses()
        {
            int aleatorio = random.Next(minValueM, maxM);
            Fecha = Fecha.AddMonths(aleatorio);
        }

        private void CalcularEstacion() {
            switch (Fecha.Month) {
                case 2:
                case 3:
                case 4:
                    Estacion = Estaciones.Primavera;
                    break;
                case 5:
                case 6:
                case 7:
                    Estacion = Estaciones.Verano;
                    break;
                case 8:
                case 9:
                case 10:
                    Estacion = Estaciones.Otoño;
                    break;
                default:
                    Estacion = Estaciones.Invierno;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Fecha} {Estacion}";
        }
    }
}

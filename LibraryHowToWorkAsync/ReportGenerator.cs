using System;
using System.Collections.Generic;
using System.Linq;

namespace HowToWorkAsync
{


    public class ReportGenerator : IGenerateSerie
    {
        private int iteracion = 0;
        private readonly object toLock = new object();

        public ReportGenerator()
        {
        }

        public List<string> cadenaFinal = new List<string>();
        Dictionary<string, Serie> _series = new Dictionary<string, Serie>();

        public string FillingOutTheReport(ETypePoint Type, string metod, int i, int idThread, bool esTiempo = false)
        {
            string cadena;
            lock (toLock)
            {
                iteracion++;
                cadena = "$M[" + i + "]";
                cadenaFinal.Add(cadena);

                PopulateSerie(Type, metod, idThread, esTiempo);

            }
            Console.WriteLine(cadena);
            return cadena;
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateSerie(ETypePoint ETypePoint, string metod, int idThread, bool esTiempo)
        {
            string key = metod + "#" + idThread;
            if (!_series.ContainsKey(key))
            {
                _series[key] = new Serie() { IdSerie = key, IdSerieY = _series.Keys.Count, IsTime = esTiempo };
            }

            PointSerie puntoSerie = new PointSerie()
            {
                IdHilo = idThread,
                IdSerie = key,
                X = iteracion,
                Y = _series[key].IdSerieY,
                When = DateTime.Now,
                Type = ETypePoint
            };

            _series[key].Points.Add(puntoSerie);

        }

        public Report GenateReport()
        {
            Report informe = new Report();


            informe.Series = _series.Values.ToList();

            var tam = _series.Keys.Count;
            var maxTiempo = iteracion;


            var listOrder = _series.Keys.ToList();

            int i = 0;
            foreach (var key in listOrder)
            {
                int[] datosAux = new int[maxTiempo];
                DateTime?[] tiemposAux = new DateTime?[maxTiempo];

                //  SerieInforme actual = new SerieInforme();
                // resultado.Add(actual);
                // actual.IdSerie = key;

                var list = _series[key];

                //List<int> puntosNormalizdos = new List<int>();
                //List<DateTime> tiemposNormalizados = new List<DateTime>();

                var listadoPuntos = new List<PointSerie>();
                /*
                for (int j = 0; j < list.Puntos.Count; j++)
                {
                    int valor = list.Puntos[j] - 1;
                    datosAux[valor] = i + 1;
                    tiemposAux[valor] = tiempos[j];
                }
                for (int z = 0; z < maxTiempo; z++)
                {
                    puntosNormalizdos.Add(datosAux[z]);
                    tiemposNormalizados.Add(tiemposAux[z]);
                }
                */

                i++;
            }

            return informe;
        }

        public void Volcar(string path = "C:\\salida.csv")
        {
            /*
            System.IO.File.Delete(path);
            var tam = serie.Keys.Count;
            var maxTiempo = iteracion;
            int[,] matrix = new int[tam, maxTiempo];

            int i = 0;
            foreach (var key in serie.Keys)
            {
                var list = serie[key];
                var hilo = seriehilo[key];

                for (int j = 0; j < list.Count; j++)
                {
                    int valor = list[j] - 1;
                    matrix[i, valor] = i + 5;// hilo[j];
                }
                i++;
            }

            cadenaFinal.Clear();

            string cabecera = "";
            foreach (var key in serie.Keys)
            {
                cabecera += key + ";";
            }
            cadenaFinal.Add(cabecera);



            for (int jj = 0; jj < maxTiempo; jj++)
            {
                cabecera = "";
                for (int ii = 0; ii < tam; ii++)
                {
                    cabecera += matrix[ii, jj] + ";";
                }
                cadenaFinal.Add(cabecera);
            }

            System.IO.File.AppendAllLines("C:\\salida.csv", cadenaFinal);
            */
        }

    }

}

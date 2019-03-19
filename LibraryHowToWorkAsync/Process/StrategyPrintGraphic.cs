using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace HowToWorkAsync.Process
{
    public class StrategyPintar : IStrategyProcesarInforme
    {
        readonly Chart grafica;
        readonly bool chkOrdernar;
        readonly bool chkSerie;
        readonly bool chkSerieSinIds;
        readonly bool chkHilos;
        readonly bool chkTiempoTicks;
        readonly SeriesChartType cmbTipoGrafica;

        public StrategyPintar(Chart pgrafica,
                              bool pchkOrdernar,
                              bool pchkSerie,
                              bool pchkSerieSinIds,
                              bool pchkHilos,
                              bool pchkTiempoTicks, 
                              SeriesChartType pcmbTipoGrafica
                            )
        {
            grafica = pgrafica;
            chkOrdernar = pchkOrdernar;
            chkSerie = pchkSerie;
            chkHilos = pchkHilos;
            chkTiempoTicks = pchkTiempoTicks;
            cmbTipoGrafica = pcmbTipoGrafica;
            chkSerieSinIds = pchkSerieSinIds;
        }

        public void Ejecutar(Report informe)
        {

            if (informe == null)
                return;

            grafica.Titles.Clear();
            grafica.Series.Clear();

            grafica.Titles.Add(informe.Results);

            if (informe.Series == null)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            var listadorSeries = informe.Series.ToList();
            if (listadorSeries.Count == 0)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            if (chkOrdernar)
            {
                listadorSeries = listadorSeries.OrderByDescending(x => x.IdSerie).ToList();
            }

            if (!chkSerieSinIds)
            {
                Dictionary<string, List<Serie>> seriesWithIdThread = new Dictionary<string, List<Serie>>();
                foreach (var aux in listadorSeries)
                {
                    int iPos = aux.IdSerie.LastIndexOf("#");
                    if (iPos > 0)
                    {
                        string idAux = aux.IdSerie.Substring(0,  iPos);
                        if (!seriesWithIdThread.ContainsKey(idAux) )
                        {
                            seriesWithIdThread[idAux] = new List<Serie> ();
                        }
                        seriesWithIdThread[idAux].Add(aux);
                    }
                    
                }

            }
            

            string tiemposExtraidos = "";
            foreach (var serieExtraida in listadorSeries)
            {
                Series serie = grafica.Series.Add(serieExtraida.IdSerie);

                var puntos = serieExtraida.Points;
                if (puntos == null || puntos.Count == 0)
                    throw new Exception("Fallo de impl, la serie no puede estar vacia");

                if (puntos.Count == 1)
                    puntos.Add(new PointSerie() { Y = -1, X = puntos[0].X, IdHilo = puntos[0].IdHilo, When = puntos[0].When });

                tiemposExtraidos += " [" + serieExtraida.IdSerie + ":" + serieExtraida.TiempoEntreInicioYFinEnMilisegundos() + " mls]   ";

                serie.ChartType = cmbTipoGrafica;
                serie.BorderDashStyle = ChartDashStyle.Solid;
                serie.BorderWidth = 8;
                serie.MarkerSize = 8;

                for (int j = 0; j < puntos.Count; j++)
                {
                    if (chkHilos)
                    {
                        serie.Points.AddXY(chkTiempoTicks ? puntos[j].When.Ticks : puntos[j].X, puntos[j].IdHilo);
                        if (chkSerie && (j == puntos.Count - 1 || j==0))
                            serie.Points[j].Label = puntos[j].IdSerieAndIdThread;
                    }
                    else
                    {
                        serie.Points.AddXY(chkTiempoTicks ? puntos[j].When.Ticks : puntos[j].X, puntos[j].Y);
                        if (chkSerie && (j == puntos.Count - 1 || j == 0))
                            serie.Points[j].Label = puntos[j].IdSerieAndIdThread;
                    }

                }
            }

            grafica.Titles.Add(tiemposExtraidos);


        }

        public void Volcar(Report informe, string path)
        {
            List<string> cadenaFinal = new List<string>();

            System.IO.File.Delete(path);
            if (informe.Series == null)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            var listadorSeries = informe.Series.ToList();
            if (listadorSeries.Count == 0)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            if (chkOrdernar)
            {
                listadorSeries = listadorSeries.OrderBy(x => x.IdSerie).ToList();
            }

            string cabecera = "";
            foreach (var serieExtraida in listadorSeries)
            {
                cabecera += serieExtraida.IdSerie + ";";
            }
            cadenaFinal.Add(cabecera);

            int k = 1;
            foreach (var serieExtraida in listadorSeries)
            {
                string pre = "";
                for (int h = 1; h < k; h++)
                {
                    pre += ";";
                }
                k++;

                var puntos = serieExtraida.Points;
                if (puntos == null || puntos.Count == 0)
                    throw new Exception("Fallo de impl, la serie no puede estar vacia");

                for (int j = 0; j < puntos.Count; j++)
                {
                    cadenaFinal.Add(pre + puntos[j].IdHilo);
                }
            }



            System.IO.File.AppendAllLines(path, cadenaFinal);
        }


    }
}






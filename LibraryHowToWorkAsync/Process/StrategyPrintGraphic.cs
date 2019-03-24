using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace HowToWorkAsync.Process
{
    public class StrategyPintar : IProcessReportStrategy
    {
        readonly Chart chart;
        readonly bool chkOrder;
        readonly bool chkSerie;
        readonly bool chkSerieWithoutIds;
        readonly bool chkHilos;
        readonly bool chkTiempoTicks;
        readonly bool chkStartAndEnd;
        readonly bool chkLost;
        readonly SeriesChartType cmbTypeOfGraphic;


        public StrategyPintar(Chart pgrafica,
                              bool pchkOrdernar,
                              bool pchkSerie,
                              bool pchkSerieSinIds,
                              bool pchkHilos,
                              bool pchkTiempoTicks,
                              bool pchkStartAndEnd,
                              bool pchkLost,
                              SeriesChartType pcmbTipoGrafica
                            )
        {
            chart = pgrafica;
            chkOrder = pchkOrdernar;
            chkSerie = pchkSerie;
            chkHilos = pchkHilos;
            chkTiempoTicks = pchkTiempoTicks;
            cmbTypeOfGraphic = pcmbTipoGrafica;
            chkStartAndEnd = pchkStartAndEnd;
            chkSerieWithoutIds = pchkSerieSinIds;
            chkLost = pchkLost;
        }

        public void Execute(Report informe)
        {

            if (informe == null)
                return;

            chart.Titles.Clear();
            chart.Series.Clear();

            chart.Titles.Add(informe.Results);

            if (informe.Series == null)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            var listadorSeries = informe.Series.ToList();
            if (listadorSeries.Count == 0)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");


            if (chkOrder)
            {
                listadorSeries = listadorSeries.OrderByDescending(x => x.IdSerie).ToList();
            }

            if (!chkSerieWithoutIds)
            {
                Dictionary<string, List<Serie>> seriesWithIdThread = new Dictionary<string, List<Serie>>();
                foreach (var aux in listadorSeries)
                {
                    int iPos = aux.IdSerie.LastIndexOf("#");
                    if (iPos > 0)
                    {
                        string idAux = aux.IdSerie.Substring(0, iPos);
                        if (!seriesWithIdThread.ContainsKey(idAux))
                        {
                            seriesWithIdThread[idAux] = new List<Serie>();
                        }
                        seriesWithIdThread[idAux].Add(aux);
                    }

                }

            }


            string tiemposExtraidos = "";
            foreach (var serieExtraida in listadorSeries)
            {
                if (!chkStartAndEnd  && serieExtraida.Points.Any(x => x.Type == ETypePoint.STAR_END))
                {
                    continue;
                }

                if (!chkLost  && serieExtraida.Points.Any(x => x.Type == ETypePoint.LOST))
                {
                    continue;
                }


                Series serie = chart.Series.Add(serieExtraida.IdSerie);

                var points = serieExtraida.Points;
                if (points == null || points.Count == 0)
                    throw new Exception("Fallo de impl, la serie no puede estar vacia");

                if (!serieExtraida.Points.Any(x => x.Type == ETypePoint.LOST))
                {
                    tiemposExtraidos += " [" + serieExtraida.IdSerie + ":" + serieExtraida.ElapsedTime() + " mls]   ";
                }
                else if (this.chkLost)
                {
                    points.Add(new PointSerie() { Y = -1, X = points[0].X, IdHilo = points[0].IdHilo, When = points[0].When });
                }



                serie.ChartType = cmbTypeOfGraphic;
                serie.BorderDashStyle = ChartDashStyle.Solid;
                serie.BorderWidth = 8;
                serie.MarkerSize = 8;

                for (int j = 0; j < points.Count; j++)
                {
                    if (chkHilos)
                    {
                        serie.Points.AddXY(chkTiempoTicks ? points[j].When.Ticks : points[j].X, points[j].IdHilo);
                        if (chkSerie && (j == points.Count - 1 || j == 0))
                            serie.Points[j].Label = points[j].IdSerie;
                    }
                    else
                    {
                        serie.Points.AddXY(chkTiempoTicks ? points[j].When.Ticks : points[j].X, points[j].Y);
                        if (chkSerie && (j == points.Count - 1 || j == 0))
                            serie.Points[j].Label = points[j].IdSerie;
                    }

                }
            }

            chart.Titles.Add(tiemposExtraidos);


        }

        public void WriteToFile(Report informe, string path)
        {
            List<string> cadenaFinal = new List<string>();

            System.IO.File.Delete(path);
            if (informe.Series == null)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            var listadorSeries = informe.Series.ToList();
            if (listadorSeries.Count == 0)
                throw new Exception("Fallo de impl, el listado de series no puede estar nula");

            if (chkOrder)
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






using System;
using System.Collections.Generic;
using System.Linq;

namespace HowToWorkAsync
{
    public enum ETypePoint
    {
        TODO,
        LOST,
        START_END
    }


    public class Report
    {
        public string ScenarioName { get; set; }
        public string Title { get; set; }
        public string Results { get; set; }
        public IEnumerable<Serie> Series { get; set; }

    }

    public class PointSerie
    {
        public string IdSerie { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime When { get; set; }
        public int IdHilo { get; set; }
        public ETypePoint Type { get; set; }
    }

    public class Serie
    {
        public string IdSerie { get; set; }
        public int IdSerieY { get; set; }
        public bool IsTime { get; set; } = false;
        public List<PointSerie> Points { get; private set; } = new List<PointSerie>();

        public long ElapsedTime()
        {
            var min = Points.Select(x => x.When).Min();
            var max = Points.Select(x => x.When).Max();
            long elapsedTicks = max.Ticks - min.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return (long)elapsedSpan.TotalMilliseconds;
        }
    }

}

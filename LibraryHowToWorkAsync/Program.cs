using System;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync
{

    public class Launcher
    {
        readonly IGenerateSerie _generaSerie;
        readonly IGetBase _implementacion;

        public Launcher(IGenerateSerie generaSerie, IGetBase implementacion)
        {
            _generaSerie = generaSerie;
            _implementacion = implementacion;
        }

        public async Task<Report> Run()
        {
            DateTime inicia = DateTime.Now;
            _generaSerie.FillingOutTheReport(ETypePoint.STAR_END, "0000" + "-RUN-", -1, Thread.CurrentThread.ManagedThreadId,false);

            string result = "";
            if (_implementacion is IGetString)
            {
                result = ((IGetString)_implementacion).Main();
            }
            else
            {
                result = await ((IGetStringAsync)_implementacion).MainAsync();
            }

            _generaSerie.FillingOutTheReport(ETypePoint.STAR_END,"0000" + "-RUN-", -1, Thread.CurrentThread.ManagedThreadId, false);

            var res = _generaSerie.GenateReport();
            res.Results = result;
            return res;

        }

    }


}

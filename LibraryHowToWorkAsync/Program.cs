using System;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync
{

    public enum ETypeImplementation
    {
        Imp_AM1_M2_M3,
        Imp_AM1_AM2_AM3_WA,
        Imp_AM1_AM2_AM3_WF,
        Imp_AM1_AM2_AM3W,
        Imp_AM1W_AM2_AM3W,
        Imp_AM1W_AM2W_AM3W_WF,
        Imp_AM1W_AM2W_AM3W_WA,
        Imp_M1_M2_M3_WF,
        Imp_M1_M2_M3_WA,
        Imp_AM1_AM2_AM3_WithAwaitC

    }

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
            _generaSerie.FillingOutTheReport(ETypePoint.STAR_END, "000" + "-RUN-", -1, Thread.CurrentThread.ManagedThreadId,false);

            string result = "";
            if (_implementacion is IGetString)
            {
                result = ((IGetString)_implementacion).Main();
            }
            else
            {
                result = await ((IGetStringAsync)_implementacion).MainAsync();
            }

            _generaSerie.FillingOutTheReport(ETypePoint.STAR_END,"000" + "-RUN-", -1, Thread.CurrentThread.ManagedThreadId, false);

            var res = _generaSerie.GenateReport();
            res.Results = result;
            return res;

        }

    }


}

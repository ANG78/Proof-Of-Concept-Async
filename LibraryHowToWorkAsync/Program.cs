using System;
using System.Threading.Tasks;

namespace HowToWorkAsync
{

    public enum ETipoImplementacion
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

    public class ProgramLanzador
    {
        readonly IGenerateSerie _generaSerie;
        readonly IGetBase _implementacion;

        public ProgramLanzador(IGenerateSerie generaSerie, IGetBase implementacion)
        {
            _generaSerie = generaSerie;
            _implementacion = implementacion;
        }


        public Report Run2()
        {
            DateTime inicia = DateTime.Now;
            _generaSerie.Generate("000" + "-RUN-", -1, false);

            string cadenaGenerada = "";
            if (_implementacion is IGetString)
            {
                cadenaGenerada = ((IGetString)_implementacion).Main();
            }
            else
            {
                cadenaGenerada = ((IGetStringAsync)_implementacion).MainAsync().GetAwaiter().GetResult();             
            }

            _generaSerie.Generate("000" + "-RUN-", -1, false);
            var res = _generaSerie.GenateReportWithData();
            res.Results = cadenaGenerada;
            return res;

        }

        public async Task<Report> LanzarImplementacionYObtenerSerie()
        {
            DateTime inicia = DateTime.Now;
            _generaSerie.Generate("000" + "-RUN-", -1, false);

            string cadenaGenerada = "";
            if (_implementacion is IGetString)
            {
                cadenaGenerada = ((IGetString)_implementacion).Main();
            }
            else
            {
                cadenaGenerada = await ((IGetStringAsync)_implementacion).MainAsync();
            }

            _generaSerie.Generate("000" + "-RUN-", -1, false);
            var res = _generaSerie.GenateReportWithData();
            res.Results = cadenaGenerada;
            return res;

        }

    }


}

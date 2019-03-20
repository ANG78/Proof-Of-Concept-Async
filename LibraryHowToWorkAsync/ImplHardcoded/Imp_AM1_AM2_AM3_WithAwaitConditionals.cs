using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded
{
    public class Imp_AM1_AM2_AM3_WithAwaitC : ClassBaseM1A, IGetString
    {
        int _umbral = 25;

        public Imp_AM1_AM2_AM3_WithAwaitC(IGenerateSerie gen, int umbral, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
            _umbral = umbral;
        }
              

        protected override async Task<string> M1Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 1, true);
            _generator.WriteLineReport("0" + literal, -1);
            var res2 = M2Async();

            string cadena = "";
            int cantidad = TaskBaseNumeroIteracionesOTiempo(literal, 1);
            if (cantidad <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena = this.TaskBase(literal, 1);
                });
            }
            else
            {
                cadena = this.TaskBase(literal + "NotWait", 1);
            }

            var cadena2 = await res2;
   
            _generator.WriteLineReport("0" + literal, -1);
            return cadena + cadena2;
        }



        private async Task<string> M2Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 2, true);
            _generator.WriteLineReport("0" + literal, -1);
            var res3 = M3Async();
            string cadena = "";
            int cantidad = TaskBaseNumeroIteracionesOTiempo(literal, 2);

            if (cantidad <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena = this.TaskBase(literal, 2);
                });
            }
            else
            {
                cadena = this.TaskBase(literal + "NotWait",2);
            }

            var cadena3 = await res3;
            _generator.WriteLineReport("0" + literal, -1);
            return cadena +  cadena3;
        }



        private async Task<string> M3Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 3, true);
            _generator.WriteLineReport("0" + literal, -1);
            string cadena = "";
            int cantidad = TaskBaseNumeroIteracionesOTiempo(literal, 3);
            if (cantidad <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena = this.TaskBase(literal, 3);
                });
            }
            else
            {
                cadena = this.TaskBase(literal + "NotWait", 3);
            }
            _generator.WriteLineReport("0" +literal, -1);
            return (cadena);
        }

    }
}

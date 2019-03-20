using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
{
    /// <summary>
    /// tipico warning que no se presta mucha atencion
    /// </summary>
    public class Imp_AM1W_AM2_AM3W : ClassBaseM1A, IGetString
    {
        public Imp_AM1W_AM2_AM3W(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }

        protected override async Task<string> M1Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("001"+ nameReflection, -1);

            var cadena2 = M2Async();
            var cadena3 = M3Async();

            var cadena1 = TaskBase("01-" + nameReflection, 1);

            _generator.WriteLineReport("001-" + nameReflection, -1);

            return cadena1 + cadena2.Result + cadena3.Result;
        }

        private async Task<string> M2Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            string cadena = "";
            await Task<string>.Run(() =>
            {
                cadena = TaskBase("02-" + nameReflection, 3); ;
            });

            return cadena;
        }

        private async Task<string> M3Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            return TaskBase("03-" + nameReflection, 3); 

        }
    }
}

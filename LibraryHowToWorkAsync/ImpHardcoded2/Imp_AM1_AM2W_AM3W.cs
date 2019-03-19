using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
{

    /// <summary>
    /// Implementacion que se espera paralela pues, aunque haya un Warning, las otras dos contienen el await
    /// Se comporta en paralelo
    /// </summary>
    public class Imp_AM1_AM2_AM3W : ClassBaseM1A, IGetString
    {
        public Imp_AM1_AM2_AM3W(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }


        protected override async Task<string> M1Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.Generate("001-" + nameReflection, -1);

            var cadena2 = await M2Async();
            var cadena3 = await M3Async();

            var cadena1 = TaskBase("01-" + nameReflection, 1);

            _generator.Generate("001-" + nameReflection, -1);

            return cadena1 + cadena2 + cadena3;
        }

        private async Task<string> M2Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            return await Task.Run(() => { return TaskBase("02-" + nameReflection, 2); });
            
        }

        private async Task<string> M3Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            return TaskBase("03-" + nameReflection, 2);
        }
    }

}

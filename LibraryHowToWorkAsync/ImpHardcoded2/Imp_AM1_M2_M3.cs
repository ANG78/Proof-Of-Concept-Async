using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
{
    /// <summary>
    /// el asincronismo enter MAIN y M1. Llamada de la Tasks M2 y M3 vayan en paralelo
    /// </summary>
    public class Imp_AM1_M2_M3_Paralela : ClassBaseM1A, IGetString
    {
        public Imp_AM1_M2_M3_Paralela(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }
        
        protected override async Task<string> M1Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("001-" + nameReflection, -1);

            var cadena2 = Task.Run(() =>
            {
                return M2();
            });

            var cadena3 = Task.Run(() =>
            {
                return M3();
            });


            string cadena1 = TaskBase("01-" + nameReflection, 1);

            string resultado2 = await cadena2;
            string resultado3 = await cadena3;

            _generator.WriteLineReport("001-" + nameReflection, -1);
            return cadena1 + resultado2 + resultado3;
        }

        private string M2()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            return TaskBase("02-" + nameReflection, 2);
        }

        private string M3()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            return TaskBase("03-" + nameReflection, 3);
        }

    }
}

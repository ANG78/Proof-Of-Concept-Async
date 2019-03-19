using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
{

    /// <summary>
    /// implementacion lineal
    /// </summary>
    public class Imp_M1_M2_M3_Lineal : ClaseBaseM1, IGetString
    {
        
        public Imp_M1_M2_M3_Lineal(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }


        protected override string M1()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            _generator.Generate("001-" + nameReflection , -1);
            var cadena2 = Metodo2();
            var cadena3 = Metodo3();

            string cadena1 = this.TaskBase("01-" + nameReflection, 1);
            _generator.Generate("001-" + nameReflection, -1);

            return cadena1 +  cadena2 + cadena3;
        }

        private string Metodo2()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            return this.TaskBase("02-" + nameReflection, 2);
        }

        private string Metodo3()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            return this.TaskBase("03-" + nameReflection, 3);
        }
    }


    /// <summary>
    /// implementacion paralela sin usar Async en la signatura
    /// </summary>
    public class Imp_M1_M2_M3_Paralela : ClaseBaseM1, IGetString
    {

        public Imp_M1_M2_M3_Paralela(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }


        protected override string M1()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            _generator.Generate("001-" + nameReflection, -1);
            var cadena2 = Task<string>.Run(() => { return M2(); }).GetAwaiter();
            var cadena3 = Task<string>.Run(() => { return M3(); }).GetAwaiter();
            var cadena1 = Task<string>.Run(() => { return TaskBase("01-" + nameReflection, 1); } ).GetAwaiter();
            _generator.Generate("001-" + nameReflection, -1);
            return cadena1.GetResult() + cadena2.GetResult() + cadena3.GetResult(); ;
        }

        private string M2()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            return this.TaskBase("02-" + nameReflection, 2);
        }

        private string M3()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            return this.TaskBase("03-" + nameReflection, 3);
        }
    }
}

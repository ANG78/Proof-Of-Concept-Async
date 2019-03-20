using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded
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
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 1);

            _generator.WriteLineReport("0"+ literal, -1);
            var cadenaProxima = M2();
            string cadena = this.TaskBase(literal, 1);
            _generator.WriteLineReport("0" + literal, -1);
            return cadena +  cadenaProxima;
        }

        private string M2()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 2);

            _generator.WriteLineReport("0" + literal, -1);
            var cadenaProxima = M3();
            string cadena = this.TaskBase(literal, 2);
            _generator.WriteLineReport("0" + literal, -1);
            return cadena + cadenaProxima;
        }

        private string M3()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 3);

            _generator.WriteLineReport("0" + literal, -1);
            var result = this.TaskBase(literal, 3);
            _generator.WriteLineReport("0" + literal, -1);
            return result;
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
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 1);

            _generator.WriteLineReport("0" + literal, -1);
            var cadena2 = Task.Run(() => { return M2(); }).GetAwaiter();
            var cadena1 = Task.Run(() => { return TaskBase("0" + literal, 1); }).GetAwaiter();
            _generator.WriteLineReport("0" + literal, -1);
            return cadena1.GetResult() + cadena2.GetResult();
        }

        private string M2()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 2);

            _generator.WriteLineReport("0" + literal, -1);
            var cadena3 = Task.Run(() => { return M3(); }).GetAwaiter();
            var cadena1 = Task.Run(() => { return TaskBase(literal, 2); }).GetAwaiter();
            _generator.WriteLineReport("0" + literal, -1);
            return cadena1.GetResult() + cadena3.GetResult();
        }

        private string M3()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 3);

            _generator.WriteLineReport("0" + literal, -1);
            var result = this.TaskBase(literal, 3);
            _generator.WriteLineReport("0" + literal, -1);
            return result;
        }
    }
}

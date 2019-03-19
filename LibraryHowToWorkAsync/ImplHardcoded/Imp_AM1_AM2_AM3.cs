using System;
using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded
{

  
    /// <summary>
    /// implementacion donde MAIN y M1 suelen ser la ultima iteracion porque el await se hace antes y no paralelismo con M1
    /// </summary>
    public class Imp_AM1_AM2_AM3 : ClassBaseM1A, IGetString
    {
        public Imp_AM1_AM2_AM3(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }

        protected override async Task<string> M1Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 1, true);

            _generator.Generate("0" + literal, -1);
            var cadena2 = await M2Async();
            string cadena1 = this.TaskBase(literal, 1);
            _generator.Generate("0" + literal, -1);
            return cadena1 +  cadena2;
        }

        public async Task<string> M2Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 2, true);

            _generator.Generate("0" + literal, -1);
            string cadena2 = await M3Async();
            string cadena1 = this.TaskBase(literal, 2);
            _generator.Generate("0" + literal, -1);
            return cadena1 + cadena2;
        }

        private async Task<string> M3Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 3, true);

            _generator.Generate("0" + literal, -1);
            string cadena = "";
            await Task.Run(() =>
            {
                cadena = this.TaskBase(literal, 3);
            });
            _generator.Generate("0" + literal, -1);
            return (cadena);
        }
    }


    /// <summary>
    /// Paralela: tanto M1,M2 y M3 van en paralelo, pues se declaran las Task de M2 y M3, y se ejecutan en paralelo con el resto
    /// </summary>
    public class Imp_AM1_AM2_AM3_Paralela : ClassBaseM1A, IGetString
    {
        public Imp_AM1_AM2_AM3_Paralela(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }

        protected override async Task<string> M1Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 1, true);

            _generator.Generate("0" + literal, -1);
            var cadena2 = M2Async();
            string cadena1 = TaskBase(literal, 1);

            string resultado2 = await cadena2;
            _generator.Generate("0" + literal, -1);
            return cadena1 + resultado2;
        }

        private async Task<string> M2Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 2, true);

            _generator.Generate("0" + literal, -1);
            var cadena2 = M3Async();
            string cadena1 = TaskBase(literal, 2);

            string resultado2 = await cadena2;
            _generator.Generate("0" + literal, -1);
            return cadena1 + resultado2;
        }

        private async Task<string> M3Async()
        {
            string literal = ObtenerLiteralSerie(MethodBase.GetCurrentMethod(), 3, true);

            _generator.Generate("0" + literal, -1);
            string cadena = "";
            var resul = Task.Run(() =>
            {
                cadena = TaskBase(literal, 3);
            });
            await resul;
            _generator.Generate("0" + literal, -1);
            return cadena;
        }

    }

    
}

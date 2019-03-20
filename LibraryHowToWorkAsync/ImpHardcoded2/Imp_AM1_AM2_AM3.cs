using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
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
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("001" + nameReflection, -1);

            var cadena2 = await M2Async();
            var cadena3 = await M3Async();

            string cadena1 = this.TaskBase("01-" + nameReflection, 1);

            _generator.WriteLineReport("001-" + nameReflection, -1);
            return cadena1 +  cadena2 + cadena3;
        }

        private async Task<string> M2Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("002" + nameReflection, -1);
            string cadena = "";
            await Task.Run(() =>
            {
                cadena = this.TaskBase("02-" + nameReflection, 2);
            });
            _generator.WriteLineReport("002" + nameReflection, -1);
            return cadena;
        }

        private async Task<string> M3Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("003" + nameReflection, -1);
            string cadena = "";
            await Task.Run(() =>
            {
                cadena = this.TaskBase("03-" + nameReflection, 3);
            });
            _generator.WriteLineReport("003" + nameReflection, -1);
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
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("001-" + nameReflection, -1);

            var cadena2 = M2Async();
            var cadena3 = M3Async();

            string cadena1 = TaskBase("01-" + nameReflection, 1);

            string resultado2 = await cadena2;
            string resultado3 = await cadena3;

            _generator.WriteLineReport("001-" + nameReflection, -1);

            return cadena1 + resultado2 + resultado3;
        }

        private async Task<string> M2Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("002" + nameReflection, -1);
            string cadena = "";
            var resul = Task.Run(() =>
            {
                cadena = TaskBase("02-" + nameReflection, 2);
            });
            await resul;
            _generator.WriteLineReport("002" + nameReflection, -1);
            return cadena;
        }

        private async Task<string> M3Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.WriteLineReport("003" + nameReflection, -1);
            string cadena = "";
            var resul = Task.Run(() =>
            {
                cadena = TaskBase("03-" + nameReflection, 3);
            });
            await resul;
            _generator.WriteLineReport("003" + nameReflection, -1);
            return cadena;
        }

    }

    
}

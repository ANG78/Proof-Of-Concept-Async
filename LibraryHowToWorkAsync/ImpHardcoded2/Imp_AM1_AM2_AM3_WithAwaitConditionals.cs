using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpHardcoded2
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
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            _generator.Generate("001-" + nameReflection, -1);
            var cadena2 = M2Async();
            var cadena3 = M3Async();

            string cadena1= "";
            int cantidad1 = TaskBaseNumeroIteracionesOTiempo("01-" + "AM1", 2);

            if (cantidad1 <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena1 = this.TaskBase("01-" + nameReflection, 2);
                });
            }
            else
            {
                cadena1 = this.TaskBase("01-" + nameReflection + "-NotWait", 2);
            }

            string resultado2 = await cadena2;
            string resultado3 = await cadena3;
            _generator.Generate("001-" + nameReflection, -1);
            return cadena1 + resultado2 + resultado3;
        }



        private async Task<string> M2Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            string cadena = "";
            int cantidad = TaskBaseNumeroIteracionesOTiempo("02-" + nameReflection, 2);

            if (cantidad <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena = this.TaskBase("02-" + nameReflection, 2);
                });
            }
            else
            {
                cadena = this.TaskBase("02-" + nameReflection + "-NotWait",2);
            }
            return cadena;
        }



        private async Task<string> M3Async()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            string cadena = "";
            int cantidad = TaskBaseNumeroIteracionesOTiempo("03-" + nameReflection, 3);
            if (cantidad <= _umbral)
            {
                await Task.Run(() =>
                {
                    cadena = this.TaskBase("03-" + nameReflection, 3);
                });
            }
            else
            {
                cadena = this.TaskBase("03-" + nameReflection + "-NotWait", 3);
            }
            return (cadena);
        }

    }
}

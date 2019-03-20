using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public abstract class ClassBaseM1A : ClassBaseImpl
    {
        public ClassBaseM1A(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }

        public string Main()
        {

            _generator.WriteLineReport("000-MAIN", -1);

            string resultado = "";

            TaskAwaiter<string> tareaEsperar = M1Async().GetAwaiter();
            var cadena = TaskBase("00-" + "MAIN", 0);

            _generator.WriteLineReport("10-" + "LOST", 0);
            var resultMain = tareaEsperar.GetResult();

            resultado = cadena + resultMain;

            _generator.WriteLineReport("000-MAIN", -1);
            return resultado;
        }

        protected abstract Task<string> M1Async();
    }


    public abstract class ClaseBaseM1 : ClassBaseImpl
    {
        public ClaseBaseM1(IGenerateSerie gen, IStrategyTodo[] procesamientos) : base(gen, procesamientos)
        {
        }

        public string Main()
        {
            _generator.WriteLineReport("000-MAIN", -1);
            string s = M1();
            string cadena1 = this.TaskBase("00-" + "Main", 0);
            _generator.WriteLineReport("10-" + "LOST", 0);
            _generator.WriteLineReport("000-MAIN", -1);
            return cadena1 + s;
        }

        protected abstract string M1();
    }

}

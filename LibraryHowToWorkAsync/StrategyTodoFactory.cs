using System;
using System.Threading;

namespace HowToWorkAsync
{

   

    public class StrategyTodoFactory
    {
        public static IStrategyTodo GetInstance(ETypeWork tipo, IGenerateSerie generaSerie, int iteracionesOSegundos)
        {
            if (tipo == ETypeWork.LOOPING)
            {
                return new Looping(generaSerie, iteracionesOSegundos);
            }
            else if (tipo == ETypeWork.SLEEPING)
            {
                return new UseSleeping(generaSerie, iteracionesOSegundos);
            }
            throw new Exception("Not Expected TODO Strategy");
        }

    }

    public class Looping : IStrategyTodo
    {
        protected readonly IGenerateSerie generateSerie;
        protected readonly int _count;

        public Looping(IGenerateSerie gen, int count)
        {
            generateSerie = gen;
            _count = count;
        }

        public bool IsTime()
        {
            return false;
        }

        public string Todo(string idSerie)
        {
            string cadena1 = "";
            for (int i = 1; i <= _count; i++)
            {
                cadena1 = generateSerie.WriteLineReport(idSerie, i);
                Thread.Sleep(5);
            }
            return cadena1;
        }
        public int AmountOfStepsOrMls()
        {
            return _count;
        }
    }

    public class UseSleeping : IStrategyTodo
    {
        protected readonly IGenerateSerie _generaSerie;
        protected readonly int _count;

        public UseSleeping(IGenerateSerie gen, int count)
        {
            _generaSerie = gen;
            _count = count;
        }

        public string Todo(string idSerie)
        {
            _generaSerie.WriteLineReport(idSerie, _count, true);
            Console.Write("Dormir el hilo por.." + ETypeWork.SLEEPING.Factor() * _count + "mls ");
            Thread.Sleep(ETypeWork.SLEEPING.Factor() * _count);
            return _generaSerie.WriteLineReport(idSerie, _count, true);
        }

        public bool IsTime()
        {
            return true;
        }

        public int AmountOfStepsOrMls()
        {
            return _count;
        }

    }
}

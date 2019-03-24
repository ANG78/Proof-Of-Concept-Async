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
                return new Sleeping(generaSerie, iteracionesOSegundos);
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

        public string Todo(string idSerie, int idthread)
        {
            string cadena1 = "";
            for (int i = 1; i <= _count; i++)
            {
                if (idthread != Thread.CurrentThread.ManagedThreadId)
                {
                    cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO,idSerie, i, Thread.CurrentThread.ManagedThreadId);
                }
                else
                {
                    cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO,idSerie, i, idthread);
                }
                
                Thread.Sleep(5);
            }
            return cadena1;
        }
        public int AmountOfStepsOrMls()
        {
            return _count;
        }
    }

    public class Sleeping : IStrategyTodo
    {
        protected readonly IGenerateSerie _generaSerie;
        protected readonly int _count;

        public Sleeping(IGenerateSerie gen, int count)
        {
            _generaSerie = gen;
            _count = count;
        }

        public string Todo(string idSerie, int idThread)
        {
            _generaSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, _count, idThread, true);
            Console.Write("sleeping thread.." + ETypeWork.SLEEPING.Factor() * _count + "mls ");
            Thread.Sleep(ETypeWork.SLEEPING.Factor() * _count);
            return _generaSerie.FillingOutTheReport(ETypePoint.TODO,idSerie, _count, idThread, true);
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

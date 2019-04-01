using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public class StrategyTodoFactory
    {
        public static IStrategyTodo GetInstance(ETypeDoIndependentWork tipo, IGenerateSerie generaSerie, int iteracionesOSegundos)
        {
            if (tipo == ETypeDoIndependentWork.LOOPING)
            {
                return new Looping(generaSerie, iteracionesOSegundos);
            }
            else if (tipo == ETypeDoIndependentWork.SLEEPING)
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
                    cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, i, Thread.CurrentThread.ManagedThreadId);
                }
                else
                {
                    cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, i, idthread);
                }

                Thread.Sleep(5);
            }
            return cadena1.Replace("Async", "");
        }

        public int AmountOfStepsOrMls()
        {
            return _count;
        }

        public string Description()
        {
            return "string res='' ; for (int i = 1; i <=  " + _count + "; i++) { res = FillingOutTheReport(idSerie, i, ThreadId); sleep(5);";
        }

        public async Task<string> TodoAsync(string idSerie, int idThread)
        {
            return await Task.Run(() =>
            {
                return Todo(idSerie, idThread);
            });


            //string cadena1 = "";
            //for (int i = 1; i <= _count; i++)
            //{
            //    await AccessTheWebAsync();
            //    if (idThread != Thread.CurrentThread.ManagedThreadId)
            //    {
            //        cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, i, Thread.CurrentThread.ManagedThreadId);
            //    }
            //    else
            //    {
            //        cadena1 = generateSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, i, idThread);
            //    }
            //    // Thread.Sleep(5);
            //}
            //return cadena1.Replace("Async", "");
        }



        private async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            using (HttpClient client = new HttpClient())
            {
                // GetStringAsync returns a Task<string>. That means that when you await the  
                // task you'll get a string (urlContents).  
                Task<string> getStringTask = client.GetStringAsync("https://www.marca.com/");


                string urlContents = await getStringTask;

                // The return statement specifies an integer result.  
                // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
                return urlContents.Length;
            }
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
            Console.Write("sleeping thread.." + ETypeDoIndependentWork.SLEEPING.Factor() * _count + "mls ");
            Thread.Sleep(ETypeDoIndependentWork.SLEEPING.Factor() * _count);
            return _generaSerie.FillingOutTheReport(ETypePoint.TODO, idSerie, _count, idThread, true).Replace("Async", ""); ;
        }

        public string Description()
        {
            return " FillingOutTheReport(idSerie, i, ThreadId); sleep(" + (ETypeDoIndependentWork.SLEEPING.Factor() * _count) + "); FillingOutTheReport(idSerie, i, ThreadId);";
        }

        public bool IsTime()
        {
            return true;
        }

        public int AmountOfStepsOrMls()
        {
            return _count;
        }

        public async Task<string> TodoAsync(string cadena, int idThread)
        {
            return await Task.Run(() =>
            {
                return Todo(cadena, idThread);
            });
        }

    }
}

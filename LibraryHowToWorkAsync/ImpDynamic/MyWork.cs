using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class MyWork
    {
        protected IGenerateSerie generator { get; set; }
        public IStrategyTodo StrategyTodo { get; private set; }

        public MyWork(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
        {
            StrategyTodo = pProcesamiento;
            generator = pgenerator;
        }

        public string DoIndepentWork(string message, int idTread)
        {
            return StrategyTodo.Todo(message, idTread);
        }

        protected int TaskBaseNumeroIteracionesOTiempo(string cadena)
        {
            return StrategyTodo.AmountOfStepsOrMls();
        }

        public abstract string Description();
    }


    
    public class MyWorkSync : MyWork, IMyWorkSync
    {
        public MyWorkSync(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
           : base(pProcesamiento, pgenerator)
        {
        }

        public override string Description()
        {
            return "DoIndepentWork();";
        }

        public string GetString(string name, int idThread)
        {
            return DoIndepentWork(name, idThread);
        }
    }

    public abstract class MyWorkAsync : MyWork, IMyWorkASync
    {

        public MyWorkAsync(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
            : base(pProcesamiento, pgenerator)
        {
        }

        public abstract Task<string> GetStringAsync(string name, int idThread);
        public abstract bool HaveToWait();
    }

    public class MyWorkAsyncWait : MyWorkAsync
    {
        public MyWorkAsyncWait(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
            : base(pProcesamiento, pgenerator)
        {
        }

        public override async Task<string> GetStringAsync(string name, int idThread)
        {
            return await Task.Run(() =>
            {
                return DoIndepentWork(name, idThread);
            });
        }
        
        public override string Description()
        {
            return "await Task.Run(() => { return DoIndepentWork(); });";
        }

        public override bool HaveToWait()
        {
            return true;
        }
    }

    public class MyWorkAsyncAwaiter : MyWorkAsync
    {
        public MyWorkAsyncAwaiter(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
           : base(pProcesamiento, pgenerator)
        {
        }

        public override async Task<string> GetStringAsync(string name, int idThread)
        {
            return Task.Run(() =>
                {
                    return DoIndepentWork(name, idThread);
                }).GetAwaiter().GetResult();
        }

        public override string Description()
        {
            return "Task.Run(() => { return DoIndepentWork();}).GetAwaiter().GetResult();";
        }

        public override bool HaveToWait()
        {
            return false;
        }

    }

    public class MyWorkAsyncNotWait : MyWorkAsync
    {
        public MyWorkAsyncNotWait(IStrategyTodo pProcesamiento, IGenerateSerie pgenerator)
           : base(pProcesamiento, pgenerator)
        {
        }

        public override async Task<string> GetStringAsync(string name, int idThread)
        {
            return DoIndepentWork(name, idThread);
        }

        public override string Description()
        {
            return "DoIndepentWork()";
        }

        public override bool HaveToWait()
        {
            return false;
        }

    }
}

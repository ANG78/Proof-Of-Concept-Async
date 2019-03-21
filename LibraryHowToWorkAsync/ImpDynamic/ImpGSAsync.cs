using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ImpGSAsync : ClassTemplateImpl, IGetStringAsync
    {
        protected abstract Task<string> Body(string literal);

        public ImpGSAsync(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        public virtual async Task<string> MainAsync()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var res = await Body(nameReflection);

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return res;
        }

        public override string MyWorkDescription()
        {
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                return "MyWork()";
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                return "Task.Run(() =>{return MyWork(nameReflection);}).GetAwaiter().GetResult(); ";
            }
            else
            {
                return " await Task.Run(() => {   return MyWork(); })";
            }

        }

    }


    #region NW
    public class MainAsyncNextAsync_NW : ImpGSAsync, IGetStringIn2Phases
    {
        public MainAsyncNextAsync_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            var resultNextStrings = auxREsult;
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }

    public class MainAsyncNextAsync_AWAITER : ImpGSAsync, IGetStringIn2Phases
    {
        public MainAsyncNextAsync_AWAITER(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = auxREsult.GetAwaiter().GetResult();

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "GetAwaiter().GetResult()";
        }
    }

    public class MainAsyncNextSync_NW : ImpGSAsync, IGetStringIn2Phases
    {
        public MainAsyncNextSync_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {

            var auxREsult = ((IGetString)Next).Main();

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetString)Next).Main()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }
    #endregion

    #region WF
    public class MainAsyncNextAsync_WF : ImpGSAsync, IGetStringIn2Phases
    {
        public MainAsyncNextAsync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = await ((IGetStringAsync)Next).MainAsync();

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = auxREsult;
            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "await ((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }

    public class MainAsyncNextSync_WF : ImpGSAsync, IGetStringIn2Phases
    {

        public MainAsyncNextSync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = await Task.Run(() =>
            {
                return ((IGetString)Next).Main();
            });

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "await Task.Run(() =>  {  return ((IGetString)Next).Main();}); ";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }
    #endregion

    #region WA
    public class MainAsyncNextAsync_WA : ImpGSAsync, IGetStringIn2Phases
    {

        public MainAsyncNextAsync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }

            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = await auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "await";
        }

    }

    public class MainAsyncNextSync_WA : ImpGSAsync, IGetStringIn2Phases
    {
        public MainAsyncNextSync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = (IGetString)Next;

            var currentResult = "";
            if (Method.MyImpl == EMyTypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else if (Method.MyImpl == EMyTypeImpl.AWAITER)
            {
                currentResult = Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }).GetAwaiter().GetResult();
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
                });
            }
            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = await Task.Run(() =>
           {
               return auxREsult.Main();
           });

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "(IGetString)Next";
        }

        public override string HowToGetResultNextDescription()
        {
            return "await Task.Run(() => {   return auxREsult.Main(); }) ";
        }


    }
    #endregion

    #region Final

    public class MainAsyncFinal : ImpGSAsync
    {

        public MainAsyncFinal(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod)
            : base(gen, pProcesamiento, pMethod, null)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            string currentResult = await Task.Run(() =>
            {
                return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            });

            return currentResult;
        }

        public override string MyWorkDescription()
        {
            return "await Task.Run(() => {return " + "MyWork()" + ";  }); ";
        }
    }

    public class MainAsyncFinal_Awaiter : ImpGSAsync
    {
        public MainAsyncFinal_Awaiter(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod)
            : base(gen, pProcesamiento, pMethod, null)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            string currentResult = Task.Run(() =>
            {
                return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }).GetAwaiter().GetResult();

            return currentResult;
        }

        public override string MyWorkDescription()
        {
            return "Task.Run(() =>  {  return " + "MyWork()" + " ; }).GetAwaiter().GetResult(); ";
        }

    }

    public class MainAsyncFinal_NW : ImpGSAsync
    {
        public MainAsyncFinal_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod)
            : base(gen, pProcesamiento, pMethod, null)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            return MyWork(nameReflection, Thread.CurrentThread.ManagedThreadId);
        }

        public override string MyWorkDescription()
        {
            return "MyWork()";
        }
    }
    #endregion


}

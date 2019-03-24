using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class MethodAsync : ClassTemplateImpl, IGetStringAsync
    {      
        public MethodAsync(IMyWorkASync pMyWork, IGenerateSerie gen, IUseMethod pMethod)
        : base(gen, pMethod)
        {
            DoIndependetWork = pMyWork;
        }
        public abstract Task<string> MainAsync();
        public IMyWork DoIndependetWork { get; private set; }
        protected IMyWorkASync MyWork { get {  return (IMyWorkASync)(DoIndependetWork); } }
    }


    public class MethodAsyncWithNext : MethodAsync, IGetStringAsyncWithNext
    {
        public ICallNextAsyncStrategy NextCallStrategy { get; private set; }

        public MethodAsyncWithNext(IMyWorkASync pMyWork, ICallNextAsyncStrategy pNextCallStrategy, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, gen, pMethod)
        {
            NextCallStrategy = pNextCallStrategy;
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            if (NextCallStrategy.HaveToWaitPre())
            {
                await NextCallStrategy.Pre();
            }
            else
            {
                NextCallStrategy.Pre();
            }


            var currentResult = await MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);

            if (NextCallStrategy.HaveToWaitPost())
            {
                if (!NextCallStrategy.IsCompleted())
                {
                    GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }

                await NextCallStrategy.Post();
            }
            else
            {
                NextCallStrategy.Post();
            }

            var resultNextStrings = NextCallStrategy.Result;

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }

        public string PreDescription()
        {
            return NextCallStrategy.PreDescription();
        }

        public string PostDescription()
        {
            return NextCallStrategy.PostDescription();
        }

    }


    public class MainAsyncFinal : MethodAsync
    {

        public MainAsyncFinal(IMyWorkASync pMyWork, IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod)
            : base(pMyWork, gen, pMethod)
        {
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var result = "";
            if (MyWork.HaveToWait())
            {
                result = await MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                result = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }


            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            return result;
        }
    }

}


using System.Reflection;
using System.Threading;

namespace HowToWorkAsync.ImpDynamic
{
    public abstract class MethodSync : ClassTemplateImpl, IGetString
    {
        
        public MethodSync(IMyWorkSync pMyWork, IGenerateSerie gen, IUseMethod pMethod)
            : base(gen, pMethod)
        {
            DoIndependetWork = pMyWork;
        }

        public IMyWork DoIndependetWork { get; private set; }


        public IMyWorkSync MyWork { get; private set; }
        

        public abstract string Main();
    }

    public class MethodSyncFinal : MethodSync
    {
        public CallNextSync NextCallStrategy { get; private set; }

        public MethodSyncFinal(IMyWorkSync pMyWork, IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod)
            : base(pMyWork, gen, pMethod)
        {
        }

        public override string Main()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            var result = "Body(nameReflection)";

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return result;
        }
    }

    public class MethodSyncWithNext : MethodSync, IGetStringWithNext
    {
        public ICallNextSyncStrategy NextCallStrategy { get; private set; }

        public MethodSyncWithNext(IMyWorkSync pMyWork, CallNextSync pNextCallStrategy, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, gen, pMethod)
        {
            NextCallStrategy = pNextCallStrategy;
        }

        public override string Main()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);

            NextCallStrategy.Pre();

            var currentResult = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);

            if (NextCallStrategy.HaveToWaitPost())
            {
                if (NextCallStrategy.IsCompleted())
                {
                    GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }
            }


            NextCallStrategy.Post();
            var resultNextStrings = NextCallStrategy.Result;

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

   
}

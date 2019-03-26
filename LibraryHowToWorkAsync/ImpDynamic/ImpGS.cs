using System.Reflection;
using System.Runtime.CompilerServices;
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


        public IMyWorkSync MyWork { get { return (IMyWorkSync)DoIndependetWork; } }


        public abstract string Main();

        public virtual string Validate()
        {
            if (DoIndependetWork == null)
                return "DoIndependetWork == null in Level " + Level;

            return null;
        }
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

            var result = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId); ;

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return result;
        }

        public override string Validate()
        {
            string result = base.Validate();

            if (!string.IsNullOrWhiteSpace(result))
                return result;

            if (NextCallStrategy == null)
                return "NextCallStrategy == null in Level " + Level;

            return null;
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
                if (!NextCallStrategy.IsCompleted())
                {
                    GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);
                }

            }
            NextCallStrategy.Post();
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

        public override string Validate()
        {
            string result = base.Validate();

            if (!string.IsNullOrWhiteSpace(result))
                return result;

            if (NextCallStrategy == null)
                return "NextCallStrategy == null in Level " + Level;

            string resultnext = NextCallStrategy.Validate(Level);
            if (!string.IsNullOrWhiteSpace(result))
                return resultnext;

            return null;
        }
    }

}

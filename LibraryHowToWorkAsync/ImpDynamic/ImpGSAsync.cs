using System.Reflection;
using System.Runtime.CompilerServices;
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
        protected IMyWorkASync MyWork { get { return (IMyWorkASync)(DoIndependetWork); } }

        public virtual string Validate()
        {
            if (DoIndependetWork == null)
                return "DoIndependetWork == null in Level " + Level;

            return null;
        }

        public bool PossibleDeadLockUsingAwaiter()
        {
            return (MyWork.HaveToWait());
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
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var result = "";
            if (MyWork.HaveToWait())
            {
                result = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                result = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return result;
        }

    }

    public abstract class MethodAsyncWithNext : MethodAsync, ICallNextDescription
    {
        public IGetBase Next { get; private set; }
        public abstract string PreDescription();
        public abstract string PostDescription();

        public virtual string Validate(uint Level)
        {
            string result = base.Validate();

            if (!string.IsNullOrWhiteSpace(result))
                return result;

            if (Next == null)
                return "Next == null in Level " + Level;

            return (Next.Validate());

        }

        public MethodAsyncWithNext(IMyWorkASync pMyWork, IGetBase pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, gen, pMethod)
        {
            Next = pNext;
        }

        public override string Validate()
        {
            string result = base.Validate();

            if (!string.IsNullOrWhiteSpace(result))
                return result;

            if (Next == null)
                return "Next == null in Level " + Level;

            string resultnext = Next.Validate();
            if (!string.IsNullOrWhiteSpace(result))
                return resultnext;


            return null;
        }

    }




}


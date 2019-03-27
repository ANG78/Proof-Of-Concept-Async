using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{
    #region WithNext

    public abstract class CallNextAsync : MethodAsyncWithNext
    {

        public CallNextAsync(IMyWorkASync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public new IGetStringAsync Next { get { return (IGetStringAsync)base.Next; } }
    }

    public class CallNextAsyncWaitAfter : CallNextAsync
    {
        public CallNextAsyncWaitAfter(IMyWorkASync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = Next.MainAsync();

            var currentResult = "";
            if (MyWork.HaveToWait())
            {
                currentResult = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                currentResult = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + await resultNextStrings;
        }

        public override string PreDescription()
        {
            return "Next.MainAsync()";
        }

        public override string PostDescription()
        {
            return "await Next";
        }


    }

    public class CallNextAsyncWaitFirst : CallNextAsync
    {

        public CallNextAsyncWaitFirst(IMyWorkASync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = await Next.MainAsync();

            var currentResult = "";
            if (MyWork.HaveToWait())
            {
                currentResult = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                currentResult = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }


        public override string PreDescription()
        {
            return " await Next.MainAsync()";
        }

        public override string PostDescription()
        {
            return "Next";
        }


    }

    public class CallNextAsyncNotWait : CallNextAsync
    {
        public CallNextAsyncNotWait(IMyWorkASync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = Next.MainAsync();

            var currentResult = "";
            if (MyWork.HaveToWait())
            {
                currentResult = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                currentResult = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }


        public override string PreDescription()
        {
            return "Next.MainAsync().GetAwaiter()";
        }

        public override string PostDescription()
        {
            return "Next";
        }

    }

    public class CallNextAsyncAwaiter : CallNextAsync
    {
        public CallNextAsyncAwaiter(IMyWorkASync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            dynamic resultNextStrings = null;

            if (Next.PossibleDeadLockUsingAwaiter())
            {
                resultNextStrings = Task.Run(() =>
                {
                    return (Next).MainAsync().GetAwaiter().GetResult();
                }).GetAwaiter();
            }
            else
            {
                resultNextStrings = Next.MainAsync().GetAwaiter();
            }


            var currentResult = "";
            if (MyWork.HaveToWait())
            {
                currentResult = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                currentResult = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings.GetResult();

        }
       
        public override string PreDescription()
        {
            if (Next.PossibleDeadLockUsingAwaiter())
            {
                return "Task.Run(() => {return Next.MainAsync().GetAwaiter().GetResult();}).GetAwaiter(); ";
            }
            else
            {
                return "Next.MainAsync().GetAwaiter()";
            }

        }

        public override string PostDescription()
        {
            return "Next.GetResult()";
        }
    }

    public class CallNextAsyncToAsync : CallNextAsync
    {
        public new IGetString Next { get; set; }


        public CallNextAsyncToAsync(IMyWorkASync pMyWork, IGetString pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, null, gen, pMethod)
        {
            Next = pNext;
        }

        public override async Task<string> MainAsync()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = Next.Main();

            var currentResult = "";
            if (MyWork.HaveToWait())
            {
                currentResult = await MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                currentResult = MyWork.GetStringAsync(nameReflection, Thread.CurrentThread.ManagedThreadId).Result;
            }

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }


        public override string PreDescription()
        {
            return "Next.Main()";
        }

        public override string PostDescription()
        {
            return "Next";
        }


    }

    #endregion
}

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public class CallNextSyncWaitFirstIfNextMethodIsAsync : MethodSyncWithNext
    {
        public new IGetStringAsync Next { get { return (IGetStringAsync)base.Next; } }

        public CallNextSyncWaitFirstIfNextMethodIsAsync(IMyWorkSync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
            : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override string Main()
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
                resultNextStrings = Next.MainAsync().GetAwaiter().GetResult();
            }

            var currentResult = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }

        public override string PreDescription()
        {
            return "Next.MainAsync().GetAwaiter().GetResult()";
        }

        public override string PostDescription()
        {
            return "Next";
        }

        /*
       public override void Pre()
       {
           Result = Task.Run(() =>
           {
               return ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();
           }).GetAwaiter().GetResult();
       }
       */
    }

    public class CallNextSyncWaitAfterIfNextMethodIsAsync : MethodSyncWithNext
    {
        public new IGetStringAsync Next { get { return (IGetStringAsync)base.Next; } }

        public CallNextSyncWaitAfterIfNextMethodIsAsync(IMyWorkSync pMyWork, IGetStringAsync pNext, IGenerateSerie gen, IUseMethod pMethod)
           : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override string Main()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            dynamic resultNextStrings = null;

            if (Next.PossibleDeadLockUsingAwaiter())
            {
                resultNextStrings = Task.Run(() =>
                {
                    return Next.MainAsync().GetAwaiter().GetResult();
                }).GetAwaiter();
            }
            else
            {
                resultNextStrings = Next.MainAsync().GetAwaiter();
            }

            var currentResult = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings.GetResult();
        }

        public override string PreDescription()
        {
            if (Next.PossibleDeadLockUsingAwaiter())
            {
                return "Next.MainAsync().GetAwaiter()";
            }
            else
            {
                return "Task.Run(() => { return Next.MainAsync().GetAwaiter().GetResult(); }).GetAwaiter(); ";
            }
        }

        public override string PostDescription()
        {
            return "Next.GetResult()";
        }

    }

    public class CallNextSyncIfNextMethodIsSync : MethodSyncWithNext
    {
        public new IGetString Next { get { return (IGetString)base.Next; } }

        public CallNextSyncIfNextMethodIsSync(IMyWorkSync pMyWork, IGetString pNext, IGenerateSerie gen, IUseMethod pMethod)
           : base(pMyWork, pNext, gen, pMethod)
        {
        }

        public override string PreDescription()
        {
            return "Next.Main()";
        }

        public override string PostDescription()
        {
            return "Next";
        }

        public override string Main()
        {
            var nameReflection = GenerateHeaderAndFoot(MethodBase.GetCurrentMethod(), Thread.CurrentThread.ManagedThreadId);

            var resultNextStrings = Next.Main();

            var currentResult = MyWork.GetString(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateLostPoint(nameReflection, Thread.CurrentThread.ManagedThreadId);

            GenerateHeaderAndFoot(nameReflection, Thread.CurrentThread.ManagedThreadId);
            return currentResult + resultNextStrings;
        }

    }

}

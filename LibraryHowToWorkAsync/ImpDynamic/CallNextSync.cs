using System.Runtime.CompilerServices;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class CallNextSync : ICallNextSyncStrategy
    {
        public IGetBase Next { get; set; }
        public string Result { get; protected set; }

        public abstract bool HaveToWaitPost();
        public abstract bool IsCompleted();
        public abstract void Pre();
        public abstract void Post();
        public abstract string PreDescription();
        public abstract string PostDescription();

        public CallNextSync(IGetBase pNext)
        {
            Next = pNext;
        }

    }

    public class CallNextSyncWaitFirstIfNextMethodIsAsync : CallNextSync
    {

        public CallNextSyncWaitFirstIfNextMethodIsAsync(IGetStringAsync pNext)
               : base(pNext)
        {
        }

        public override void Pre()
        {
            Result = ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();
        }

        public override void Post()
        {
        }

        public override string PreDescription()
        {
            return "((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult()";
        }

        public override string PostDescription()
        {
            return "";
        }

        public override bool HaveToWaitPost()
        {
            return false;
        }

        public override bool IsCompleted()
        {
            return true;
        }
    }

    public class CallNextSyncWaitAfterIfNextMethodIsAsync : CallNextSync
    {
        TaskAwaiter<string> Task;

        public CallNextSyncWaitAfterIfNextMethodIsAsync(IGetStringAsync pNext)
              : base(pNext)
        {
        }

        public override void Pre()
        {
            Task = ((IGetStringAsync)Next).MainAsync().GetAwaiter();
        }

        public override void Post()
        {
            Result = Task.GetResult();
        }


        public override string PreDescription()
        {
            return "((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult()";
        }

        public override string PostDescription()
        {
            return "";
        }

        public override bool HaveToWaitPost()
        {
            return true;
        }

        public override bool IsCompleted()
        {
            return Task.IsCompleted;
        }
    }

    public class CallNextSyncIfNextMethodIsSync : CallNextSync
    {
        public CallNextSyncIfNextMethodIsSync(IGetString pNext)
              : base(pNext)
        {
        }

        public override void Pre()
        {
            Result = ((IGetString)Next).Main();
        }

        public override void Post()
        {
        }

        public override string PreDescription()
        {
            return "((IGetString)Next).Main()";
        }

        public override string PostDescription()
        {
            return "";
        }

        public override bool HaveToWaitPost()
        {
            return true;
        }

        public override bool IsCompleted()
        {
            return true;
        }
    }

}

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{
    #region WithNext

    public abstract class CallNextAsync : ICallNextAsyncStrategy
    {
        public IGetBase Next { get; set; }

        public CallNextAsync(IGetBase pNext)
        {
            Next = pNext;
        }

        public abstract bool HaveToWaitPre();
        public abstract bool HaveToWaitPost();
        public abstract bool IsCompleted();
        public abstract Task Pre();
        public abstract Task Post();
        public string Result { get; protected set; }

        public abstract string PreDescription();
        public abstract string PostDescription();

        public virtual string Validate(uint Level)
        {
            if (Next == null)
                return "Next == null in Level " + Level;

            return (Next.Validate());

        }
    }

    public class CallNextAsyncWaitAfter : CallNextAsync
    {
        private Task<string> myTask;
        public CallNextAsyncWaitAfter(IGetBase pNext)
            : base(pNext)
        {
        }

        public override Task Pre()
        {
            myTask = ((IGetStringAsync)Next).MainAsync();
            return myTask;
        }

        public override async Task Post()
        {
            Result = await myTask;
        }

        public override string PreDescription()
        {
            return "Next.MainAsync()";
        }

        public override string PostDescription()
        {
            return "await Next";
        }

        public override bool HaveToWaitPre()
        {
            return false;
        }

        public override bool HaveToWaitPost()
        {
            return true;
        }

        public override bool IsCompleted()
        {
            return myTask.IsCompleted;

        }
    }

    public class CallNextAsyncWaitFirst : CallNextAsync
    {

        public CallNextAsyncWaitFirst(IGetBase pNext)
           : base(pNext)
        {
        }

        public override async Task Pre()
        {
            var result = await ((IGetStringAsync)Next).MainAsync();
        }

        public override Task Post()
        {
            return null;
        }

        public override string PreDescription()
        {
            return " await Next.MainAsync()";
        }

        public override string PostDescription()
        {
            return "";
        }

        public override bool HaveToWaitPre()
        {
            return true;
        }

        public override bool HaveToWaitPost()
        {
            return false;
        }

        public override bool IsCompleted()
        {
            return false;
        }


    }

    public class CallNextAsyncNotWait : CallNextAsync
    {
        private Task<string> myTask;
        public CallNextAsyncNotWait(IGetBase pNext)
          : base(pNext)
        {
        }

        public override async Task Pre()
        {
            myTask = Task.Run(() => { return ((IGetStringAsync)Next).MainAsync(); });
        }

        public async override Task Post()
        {
            Result = myTask.Result;
        }

        public override string PreDescription()
        {
            return " Next.MainAsync()";
        }

        public override string PostDescription()
        {
            return "Next.Result";
        }

        public override bool HaveToWaitPre()
        {
            return false;
        }

        public override bool HaveToWaitPost()
        {
            return false;
        }

        public override bool IsCompleted()
        {
            return false;
        }
    }

    public class CallNextAsyncAwaiter : CallNextAsync
    {
        private TaskAwaiter<string> myTask;
        public CallNextAsyncAwaiter(IGetBase pNext)
          : base(pNext)
        {
        }

        public override async Task Pre()
        {
            myTask = Task.Run(() =>
            {
                return ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();
            }).GetAwaiter();

        }


        public override async Task Post()
        {
            Result = myTask.GetResult();
            // return null;
        }

        public override string PreDescription()
        {
            return "Next.MainAsync().GetAwaiter()";
        }

        public override string PostDescription()
        {
            return "Next.GetResult()";
        }

        public override bool HaveToWaitPre()
        {
            return false;
        }

        public override bool HaveToWaitPost()
        {
            return false;
        }

        public override bool IsCompleted()
        {
            return false;
        }
    }

    public class CallNextAsyncToAsync : CallNextAsync
    {
        public CallNextAsyncToAsync(IGetBase pNext)
          : base(pNext)
        {
        }

        public override async Task Pre()
        {
            Result = ((IGetString)Next).Main();
        }

        public override Task Post()
        {
            return null;
        }

        public override string PreDescription()
        {
            return "Next.Main()";
        }

        public override string PostDescription()
        {
            return "";
        }

        public override bool HaveToWaitPre()
        {
            return false;
        }

        public override bool HaveToWaitPost()
        {
            return false;
        }

        public override bool IsCompleted()
        {
            return false;
        }
    }

    #endregion
}

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class CallNextSync : ICallNextSyncStrategy
    {
        public IGetBase Next { get; set; }
        protected string result;
        public virtual string Result { get { return result; } protected set { result = value; } }

        public abstract bool HaveToWaitPost();
        public abstract bool IsCompleted();
        public abstract void Pre();
        public abstract string PreDescription();
        public abstract string PostDescription();

        public virtual string Validate(uint Level)
        {
            if (Next == null)
                return "Next == null in Level " + Level;

            return (Next.Validate());

        }

        public abstract void Post();


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
            Result = Task.Run(() =>
            {
                return ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();
            }).GetAwaiter().GetResult();
        }

      

        public override string PreDescription()
        {
            return "Next.MainAsync().GetAwaiter().GetResult()";
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

        public override void Post()
        {

        }

       

    }

    public class CallNextSyncWaitAfterIfNextMethodIsAsync : CallNextSync
    {
        TaskAwaiter<string> TaskWaiter;

        public CallNextSyncWaitAfterIfNextMethodIsAsync(IGetStringAsync pNext)
              : base(pNext)
        {
        }

        public override void Pre()
        {
             TaskWaiter = Task.Run(() =>
            {
                return ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();
            }).GetAwaiter();

        }

        public override string PreDescription()
        {
            return "Next.MainAsync().GetAwaiter()";
        }

        public override string PostDescription()
        {
            return "Next.GetResult()";
        }

        public override bool HaveToWaitPost()
        {
            return true;
        }

        public override bool IsCompleted()
        {
            return TaskWaiter.IsCompleted;
        }

        public override void Post()
        {
            Result = TaskWaiter.GetResult();
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


        public override string PreDescription()
        {
            return "Next.Main()";
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

        public override void Post()
        {
          
        }
    }

}

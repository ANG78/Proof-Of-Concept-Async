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

    }

    public abstract class MethodSyncWithNext : MethodSync, ICallNextDescription
    {
        public IGetBase Next { get; private set; }

        public MethodSyncWithNext(IMyWorkSync pMyWork, IGetBase pNext, IGenerateSerie gen, IUseMethod pMethod)
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

        public abstract string PreDescription();
        public abstract string PostDescription();

    }

}

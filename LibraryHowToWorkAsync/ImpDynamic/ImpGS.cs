using System.Reflection;

namespace HowToWorkAsync.ImpDynamic
{
    public abstract class ImpGS : ClassTemplateImpl, IGetString
    {
        protected abstract string Body(string nameReflection);

        public ImpGS(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        public string Main()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection);

            var result = Body(nameReflection);

            GenerateHeaderAndFoot(nameReflection);
            return result;
        }

        public override string MyWorkDescription()
        {
            return "MyWork()";
        }


    }

    public class MainNextAsync_NW : ImpGS
    {
        public MainNextAsync_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {

            var nextResult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = nextResult;

            return currentResult + resultNextStrings.Result;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }

    public class MainNextAsync_AWAITER : ImpGS
    {
        public MainNextAsync_AWAITER(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {

            var nextResult = ((IGetStringAsync)Next).MainAsync().GetAwaiter();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = nextResult.GetResult();

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync().GetAwaiter()";
        }

        public override string HowToGetResultNextDescription()
        {
            return ".GetResult()";
        }
    }

    public class MainNextSync_WA : ImpGS
    {
        public MainNextSync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {
            var nextResult = (IGetString)Next;

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = nextResult.Main();

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "(IGetString)Next";
        }

        public override string HowToGetResultNextDescription()
        {
            return ".Main()";
        }
    }


    public class MainNextAsync_WF : ImpGS
    {
        public MainNextAsync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {
            var nextResult = ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = nextResult;

            return currentResult + resultNextStrings;

        }

        public override string CallNextDescription()
        {
            return "MainAsync().GetAwaiter().GetResult()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }

    public class MainNextSync_WF : ImpGS
    {

        public MainNextSync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {
            var nextResult = ((IGetString)Next).Main();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = nextResult;
            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "Main()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }
    }

    public class MainFinal : ImpGS
    {
        public MainFinal(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {
            return MyWork(nameReflection);
        }

        public override string CallNextDescription()
        {
            return MyWorkDescription();
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }

    }


}

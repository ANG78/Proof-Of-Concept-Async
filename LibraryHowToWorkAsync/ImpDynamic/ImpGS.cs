using System.Reflection;

namespace HowToWorkAsync.ImpDynamic
{
    public abstract class ImpGS : ClassTemplateImpl, IGetString, IGetLevel
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

            return currentResult + resultNextStrings;
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
    }

    public class MainNextSync_WA : ImpGS
    {
        public MainNextSync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
           : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        protected override string Body(string nameReflection)
        {
            var nextResult = ((IGetString)Next);

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = ((IGetString)nextResult).Main();

            return currentResult + resultNextStrings;
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
    }
    

}

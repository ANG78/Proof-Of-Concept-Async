using System.Reflection;

namespace HowToWorkAsync.ImpDynamic
{
    public class ImpGS : ClassTemplateImpl, IGetString, IGetLevel
    {
        private delegate string MethodDelegate(string nameReflection);
        private MethodDelegate algorith;

        public ImpGS(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {
            if (pNext == null)
            {
                algorith += MainFinal;
            }
            else
            {
                switch (pMethod.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainNextAsync_WF;
                        }
                        else
                        {
                            algorith += MainNextSync_WF;
                        }
                        break;
                    case ECallNext.WAIT_AFTER:
                    case ECallNext.AWAITER_AFTER:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainNextAsync_AWAITER;
                        }
                        else
                        {
                            algorith += MainNextSync_WA;
                        }
                        break;
                    case ECallNext.NOT_WAIT:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainNextAsync_NW;
                        }
                        else
                        {
                            algorith += MainNextSync_WA;
                        }
                        break;
                }
            }
        }

        public string Main()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod());
            GenerarCabeceraYPie(nameReflection);

            var result = algorith.Invoke(nameReflection);

            GenerarCabeceraYPie(nameReflection);
            return result;
        }

        public string MainNextAsync_NW(string nameReflection)
        {

            var nextResult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = nextResult;

            return currentResult + resultNextStrings;
        }

        public string MainNextAsync_AWAITER(string nameReflection)
        {

            var nextResult = ((IGetStringAsync)Next).MainAsync().GetAwaiter();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = nextResult.GetResult();

            return currentResult + resultNextStrings;
        }

        public string MainNextSync_WA(string nameReflection)
        {
            var nextResult = ((IGetString)Next);

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = ((IGetString)nextResult).Main();

            return currentResult + resultNextStrings;
        }

        public string MainNextAsync_WF(string nameReflection)
        {
            var nextResult = ((IGetStringAsync)Next).MainAsync().GetAwaiter().GetResult();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = nextResult;

            return currentResult + resultNextStrings;

        }

        public string MainNextSync_WF(string nameReflection)
        {
            var nextResult = ((IGetString)Next).Main();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = nextResult;
            return currentResult + resultNextStrings;
        }

        public string MainFinal(string nameReflection)
        {
            return MyWork(nameReflection);
        }
    }
}

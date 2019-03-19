using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public class ImpGSAsync : ClassTemplateImpl, IGetStringAsync, IGetLevel
    {
        private delegate Task<string> MethodDelegate(string literal);
        private MethodDelegate algorith;


        public ImpGSAsync(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {
            Method = pMethod;

            if (pNext == null)
            {
                switch (Method.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                    case ECallNext.WAIT_AFTER:
                        algorith += MainAsyncFinal;
                        break;
                    case ECallNext.AWAITER_AFTER:
                        algorith += MainAsyncFinal_Awaiter;
                        break;
                    case ECallNext.NOT_WAIT:
                        algorith += MainAsyncFinal_NW;
                        break;
                }

            }

            else
            {
                switch (Method.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainAsyncNextAsync_WF;
                        }
                        else
                        {
                            algorith += MainAsyncNextSync_WF;
                        }
                        break;
                    case ECallNext.WAIT_AFTER:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainAsyncNextAsync_WA;
                        }
                        else
                        {
                            algorith += MainAsyncNextSync_WA;
                        }
                        break;
                    case ECallNext.NOT_WAIT:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainAsyncNextAsync_NW;
                        }
                        else
                        {
                            algorith += MainAsyncNextSync_NW;
                        }
                        break;
                    case ECallNext.AWAITER_AFTER:
                        if (Next is IGetStringAsync)
                        {
                            algorith += MainAsyncNextAsync_AWAITER;
                        }
                        else
                        {
                            algorith += MainAsyncNextSync_NW;
                        }
                        break;
                }
            }
        }

        public virtual async Task<string> MainAsync()
        {
            var nameReflection = Adaptador(MethodBase.GetCurrentMethod(), true);
            GenerarCabeceraYPie(nameReflection);

            var res = await algorith.Invoke(nameReflection);

            GenerarCabeceraYPie(nameReflection);
            return res;
        }

        #region NW
        protected async Task<string> MainAsyncNextAsync_NW(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
        protected async Task<string> MainAsyncNextAsync_AWAITER(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = auxREsult.GetAwaiter().GetResult();

            return currentResult + resultNextStrings;
        }
        protected async Task<string> MainAsyncNextSync_NW(string nameReflection)
        {

            var auxREsult = ((IGetString)Next).Main();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
        #endregion

        #region WF
        protected async Task<string> MainAsyncNextAsync_WF(string nameReflection)
        {
            var auxREsult = await ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = auxREsult;
            return currentResult + resultNextStrings;
        }
        protected async Task<string> MainAsyncNextSync_WF(string nameReflection)
        {
            var auxREsult = "";
            await Task.Run(() =>
            {
                auxREsult = (((IGetString)Next).Main());
            });

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
        #endregion

        #region WA
        protected async Task<string> MainAsyncNextAsync_WA(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = await auxREsult;

            return currentResult + resultNextStrings;
        }
        protected async Task<string> MainAsyncNextSync_WA(string nameReflection)
        {

            var auxREsult = ((IGetString)Next);

            var currentResult = MyWork(nameReflection);
            GenerarLost(nameReflection);

            var resultNextStrings = "";
            await Task.Run(() =>
            {
                resultNextStrings = (auxREsult.Main());
            });

            GenerarCabeceraYPie(nameReflection);
            return currentResult + resultNextStrings;
        }
        #endregion

        #region Final
        public async Task<string> MainAsyncFinal(string nameReflection)
        {
            string currentResult = await Task<string>.Run(() =>
            {
                return MyWork(nameReflection);
            });

            return currentResult;
        }

        public async Task<string> MainAsyncFinal_Awaiter (string nameReflection)
        {
            string currentResult =  Task<string>.Run(() =>
            {
                return MyWork(nameReflection);
            }).GetAwaiter().GetResult();

            return currentResult;
        }

        

        public async Task<string> MainAsyncFinal_NW(string nameReflection)
        {
            return MyWork(nameReflection);
        }
        #endregion 
    }

}

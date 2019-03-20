using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ImpGSAsync : ClassTemplateImpl, IGetStringAsync, IGetLevel
    {
        protected abstract Task<string> Body(string literal);

        public ImpGSAsync(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        public virtual async Task<string> MainAsync()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod(), true);
            GenerateHeaderAndFoot(nameReflection);

            var res = await Body(nameReflection);

            GenerateHeaderAndFoot(nameReflection);
            return res;
        }

    }


    #region NW
    public class MainAsyncNextAsync_NW : ImpGSAsync
    {
        public MainAsyncNextAsync_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
    }

    public class MainAsyncNextAsync_AWAITER : ImpGSAsync
    {
        public MainAsyncNextAsync_AWAITER(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult.GetAwaiter().GetResult();

            return currentResult + resultNextStrings;
        }
    }

    public class MainAsyncNextSync_NW : ImpGSAsync
    {
        public MainAsyncNextSync_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {

            var auxREsult = ((IGetString)Next).Main();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
    }
    #endregion

    #region WF
    public class MainAsyncNextAsync_WF : ImpGSAsync
    {
        public MainAsyncNextAsync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = await ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;
            return currentResult + resultNextStrings;
        }
    }

    public class MainAsyncNextSync_WF : ImpGSAsync
    {

        public MainAsyncNextSync_WF(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = "";
            await Task.Run(() =>
            {
                auxREsult = (((IGetString)Next).Main());
            });

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }
    }
    #endregion

    #region WA
    public class MainAsyncNextAsync_WA : ImpGSAsync
    {

        public MainAsyncNextAsync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = await auxREsult;

            return currentResult + resultNextStrings;
        }
    }

    public class MainAsyncNextSync_WA : ImpGSAsync
    {
        public MainAsyncNextSync_WA(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetString)Next);

            var currentResult = MyWork(nameReflection);
            GenerateLostPoint(nameReflection);

            var resultNextStrings = "";
            await Task.Run(() =>
            {
                resultNextStrings = (auxREsult.Main());
            });

            GenerateHeaderAndFoot(nameReflection);
            return currentResult + resultNextStrings;
        }
    }
    #endregion

    #region Final

    public class MainAsyncFinal : ImpGSAsync
    {

        public MainAsyncFinal(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            string currentResult = await Task<string>.Run(() =>
            {
                return MyWork(nameReflection);
            });

            return currentResult;
        }
    }

    public class MainAsyncFinal_Awaiter : ImpGSAsync
    {
        public MainAsyncFinal_Awaiter(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            string currentResult = Task<string>.Run(() =>
            {
                return MyWork(nameReflection);
            }).GetAwaiter().GetResult();

            return currentResult;
        }
    }

    public class MainAsyncFinal_NW : ImpGSAsync
    {
        public MainAsyncFinal_NW(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }
        protected override async Task<string> Body(string nameReflection)
        {
            return MyWork(nameReflection);
        }
    }
    #endregion


}

using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ImpGSAsync : ClassTemplateImpl, IGetStringAsync
    {
        protected abstract Task<string> Body(string literal);

        public ImpGSAsync(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
        : base(gen, pProcesamiento, pMethod, pNext)
        {
        }

        public virtual async Task<string> MainAsync()
        {
            var nameReflection = GetNameMethod(MethodBase.GetCurrentMethod());
            GenerateHeaderAndFoot(nameReflection);

            var res = await Body(nameReflection);

            GenerateHeaderAndFoot(nameReflection);
            return res;
        }

        public override string MyWorkDescription()
        {
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                return "MyWork()";
            }
            else
            {
                return " await Task.Run(() => {   return MyWork(); })";
            }
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

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            var resultNextStrings = auxREsult;
            GenerateLostPoint(nameReflection);

            return currentResult + resultNextStrings;
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

    public class MainAsyncNextAsync_AWAITER : ImpGSAsync
    {
        public MainAsyncNextAsync_AWAITER(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento, pMethod, pNext)
        {

        }

        protected override async Task<string> Body(string nameReflection)
        {
            var auxREsult = ((IGetStringAsync)Next).MainAsync();

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult.GetAwaiter().GetResult();

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "GetAwaiter().GetResult()";
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

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetString)Next).Main()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
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

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;
            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "await ((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
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
            var auxREsult = await Task.Run(() =>
            {
                return ((IGetString)Next).Main();
            });

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            GenerateLostPoint(nameReflection);

            var resultNextStrings = auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "await Task.Run(() =>  {  return ((IGetString)Next).Main();}); ";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
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

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }

            GenerateLostPoint(nameReflection);

            var resultNextStrings = await auxREsult;

            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "((IGetStringAsync)Next).MainAsync()";
        }

        public override string HowToGetResultNextDescription()
        {
            return "await";
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
            var auxREsult = (IGetString)Next;

            var currentResult = "";
            if (Method.MyImpl == ETypeImpl.SYNC)
            {
                currentResult = MyWork(nameReflection);
            }
            else
            {
                currentResult = await Task.Run(() =>
                {
                    return MyWork(nameReflection);
                });
            }
            GenerateLostPoint(nameReflection);

            var resultNextStrings = await Task.Run(() =>
           {
               return auxREsult.Main();
           });

            GenerateHeaderAndFoot(nameReflection);
            return currentResult + resultNextStrings;
        }

        public override string CallNextDescription()
        {
            return "(IGetString)Next";
        }

        public override string HowToGetResultNextDescription()
        {
            return "await Task.Run(() => {   return auxREsult.Main(); }) ";
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
            string currentResult = await Task.Run(() =>
            {
                return MyWork(nameReflection);
            });

            return currentResult;
        }

        public override string CallNextDescription()
        {
            return "await Task.Run(() => {return " + MyWorkDescription()  + ";  }); ";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }

        public override string MyWorkDescription()
        {
            return "MyWork()";
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
            string currentResult = Task.Run(() =>
            {
                return MyWork(nameReflection);
            }).GetAwaiter().GetResult();

            return currentResult;
        }

        public override string CallNextDescription()
        {
            return "Task.Run(() =>  {  return " +MyWorkDescription() + " ; }).GetAwaiter().GetResult(); ";
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }

        public override string MyWorkDescription()
        {
            return "MyWork()";
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

        public override string CallNextDescription()
        {
            return MyWorkDescription(); 
        }

        public override string HowToGetResultNextDescription()
        {
            return "";
        }

        public override string MyWorkDescription()
        {
            return "MyWork()";
        }
    }
    #endregion


}

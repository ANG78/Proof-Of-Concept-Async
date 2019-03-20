namespace HowToWorkAsync.ImpDynamic
{
    public class FactoryImpl
    {

        public static IGetBase GetInstance(IUseMethod method, IGenerateSerie reporter)
        {
            if (method == null)
                return null;


            var lastClass = GetInstance(method.Next, reporter);
            if (method.TypeNextImpl == ETypeImpl.ASYNC)
            {
                return GetInstanceAsync(method, reporter, lastClass);
            }
            else if (method.TypeNextImpl == ETypeImpl.SYNC)
            {
                return GetInstanceNoAsync(method, reporter, lastClass);
            }


            return null;
        }


        private static IGetString GetInstanceNoAsync(IUseMethod method, IGenerateSerie reporter, IGetBase lastClass)
        {
            if (method == null)
                return null;

            var processing = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);

            if (method.Next == null)
            {
                return new MainFinal(reporter, processing, method, lastClass);
            }
            else
            {
                switch (method.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainNextAsync_WF(reporter, processing, method, lastClass);
                        }
                        else
                        {
                            return new MainNextSync_WF(reporter, processing, method, lastClass);
                        }
                    case ECallNext.WAIT_AFTER:
                    case ECallNext.AWAITER_AFTER:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainNextAsync_AWAITER(reporter, processing, method, lastClass);
                        }
                        else
                        {
                            return new MainNextSync_WA(reporter, processing, method, lastClass);
                        }
                    case ECallNext.NOT_WAIT:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainNextAsync_NW(reporter, processing, method, lastClass);
                        }
                        else
                        {
                            return new MainNextSync_WA(reporter, processing, method, lastClass);
                        }
                }
            }

            return null;
        }



        private static IGetStringAsync GetInstanceAsync(IUseMethod method, IGenerateSerie reporter, IGetBase lastClass)
        {
            if (method == null)
                return null;

            var procesamiento = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);


            if (method.Next == null)
            {
                switch (method.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                    case ECallNext.WAIT_AFTER:
                        return new MainAsyncFinal(reporter, procesamiento, method, lastClass); ;
                    case ECallNext.AWAITER_AFTER:
                        return new MainAsyncFinal_Awaiter(reporter, procesamiento, method, lastClass); ;
                    case ECallNext.NOT_WAIT:
                        return new MainAsyncFinal_NW(reporter, procesamiento, method, lastClass);
                }
            }

            else
            {
                switch (method.CallNext)
                {
                    case ECallNext.WAIT_FIRST:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainAsyncNextAsync_WF(reporter, procesamiento, method, lastClass);
                        }
                        else
                        {
                            return new MainAsyncNextSync_WF(reporter, procesamiento, method, lastClass);
                        }
                    case ECallNext.WAIT_AFTER:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainAsyncNextAsync_WA(reporter, procesamiento, method, lastClass);
                        }
                        else
                        {
                            return new MainAsyncNextSync_WA(reporter, procesamiento, method, lastClass);
                        }
                    case ECallNext.NOT_WAIT:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainAsyncNextAsync_NW(reporter, procesamiento, method, lastClass);
                        }
                        else
                        {
                            return new MainAsyncNextSync_NW(reporter, procesamiento, method, lastClass);
                        }
                    case ECallNext.AWAITER_AFTER:
                        if (lastClass is IGetStringAsync)
                        {
                            return new MainAsyncNextAsync_AWAITER(reporter, procesamiento, method, lastClass);
                        }
                        else
                        {
                            return new MainAsyncNextSync_WA(reporter, procesamiento, method, lastClass);
                        }
                }
            }
            //                return new ImpGSAsync(reporter, procesamiento, method, lastClass);
            return null;
        }


    }
}
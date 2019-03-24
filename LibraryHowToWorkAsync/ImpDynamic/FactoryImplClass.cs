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

            IStrategyTodo todo = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);

            MyWorkSync myW = new MyWorkSync(todo, reporter);

            if (method.Next == null)
            {
                return new MethodSyncFinal(myW, reporter, processing, method);
            }
            else
            {
                CallNextSync callNext = null;
                if (lastClass is IGetString)
                {
                    callNext = new CallNextSyncIfNextMethodIsSync((IGetString)lastClass);
                }
                else if (lastClass is IGetStringAsync)
                {
                    switch (method.CallNext)
                    {
                        case ECallNext.WAIT_FIRST:
                            callNext = new CallNextSyncWaitFirstIfNextMethodIsAsync((IGetStringAsync)lastClass);
                            break;
                        case ECallNext.WAIT_AFTER:
                        case ECallNext.AWAITER_AFTER:
                        case ECallNext.NOT_WAIT:
                            callNext = new CallNextSyncWaitAfterIfNextMethodIsAsync((IGetStringAsync)lastClass);
                            break;

                    }

                }

                return new MethodSyncWithNext(myW, callNext, reporter, method);
            }


        }



        private static IGetStringAsync GetInstanceAsync(IUseMethod method, IGenerateSerie reporter, IGetBase lastClass)
        {
            if (method == null)
                return null;

            var processing = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);


            IStrategyTodo todo = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);

            MyWorkAsync myW = null;
            switch (method.MyImpl)
            {
                case EMyTypeImpl.ASYNC:
                    myW = new MyWorkAsyncWait(todo, reporter);
                    break;
                case EMyTypeImpl.AWAITER:
                    myW = new MyWorkAsyncAwaiter(todo, reporter);
                    break;
                case EMyTypeImpl.SYNC:
                    myW = new MyWorkAsyncNotWait(todo, reporter);
                    break;
            }


            if (method.Next == null)
            {
                return new MainAsyncFinal(myW, reporter, processing, method);
            }
            else
            {

                ICallNextAsyncStrategy callNext = null;

                if (lastClass is IGetStringAsync)
                {
                    switch (method.CallNext)
                    {
                        case ECallNext.WAIT_FIRST:
                            callNext = new CallNextAsyncWaitFirst(lastClass);
                            break;
                        case ECallNext.WAIT_AFTER:
                            callNext = new CallNextAsyncWaitAfter(lastClass);
                            break;
                        case ECallNext.AWAITER_AFTER:
                            callNext = new CallNextAsyncAwaiter(lastClass);
                            break;
                        case ECallNext.NOT_WAIT:
                            callNext = new CallNextAsyncNotWait(lastClass);
                            break;
                    }
                }
                else
                {
                    callNext = new CallNextAsyncToAsync(lastClass);
                }
                return new MethodAsyncWithNext(myW, callNext, reporter, method);
            }

        }
    }
}
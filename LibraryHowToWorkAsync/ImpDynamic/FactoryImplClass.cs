namespace HowToWorkAsync.ImpDynamic
{
    public class FactoryImpl
    {

        public static IGetBase GetInstance(IUseMethod method, IGenerateSerie reporter)
        {
            if (method == null)
                return null;


            IGetBase result = null;
            var lastClass = GetInstance(method.Next, reporter);
            if (method.TypeNextImpl == ETypeImpl.ASYNC)
            {
                result =  GetInstanceAsync(method, reporter, lastClass);
            }
            else if (method.TypeNextImpl == ETypeImpl.SYNC)
            {
                result = GetInstanceNoAsync(method, reporter, lastClass);
            }


            return result;
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
                IGetString result = null;
                if (lastClass is IGetString)
                {
                    result = new CallNextSyncIfNextMethodIsSync(myW,(IGetString)lastClass,reporter, method);
                }
                else if (lastClass is IGetStringAsync)
                {
                    switch (method.CallNext)
                    {
                        case ECallNext.WAIT_FIRST:
                            result = new CallNextSyncWaitFirstIfNextMethodIsAsync(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;
                        case ECallNext.WAIT_AFTER:
                        case ECallNext.AWAITER_AFTER:
                        case ECallNext.NOT_WAIT:
                            result = new CallNextSyncWaitAfterIfNextMethodIsAsync(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;

                    }

                }

                return result;
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

                IGetStringAsync result = null;
                if (lastClass is IGetStringAsync)
                {
                    switch (method.CallNext)
                    {
                        case ECallNext.WAIT_FIRST:
                            result = new CallNextAsyncWaitFirst(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;
                        case ECallNext.WAIT_AFTER:
                            result = new CallNextAsyncWaitAfter(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;
                        case ECallNext.AWAITER_AFTER:
                            result = new CallNextAsyncAwaiter(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;
                        case ECallNext.NOT_WAIT:
                            result = new CallNextAsyncNotWait(myW, (IGetStringAsync)lastClass, reporter, method);
                            break;
                    }
                }
                else
                {
                    result = new CallNextAsyncToAsync(myW, (IGetString)lastClass, reporter, method);
                }
                return result;
            }

        }
    }
}
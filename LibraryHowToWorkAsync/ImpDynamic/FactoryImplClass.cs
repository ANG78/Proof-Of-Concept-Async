namespace HowToWorkAsync.ImpDynamic
{
    public class FactoryImplClass
    {

        public static IGetBase GetInstance(IUseMethod method, IGenerateSerie reporter)
        {
            if (method == null)
                return null;

            var procesamiento = StrategyTodoFactory.GetInstance(method.TypeWork, reporter, method.NumSteps);


            var lastClass = GetInstance(method.Next, reporter);
            if (method.TypeImplementation == ETypeImpl.ASYNC)
            {
                return new ImpGSAsync(reporter, procesamiento, method, lastClass);
            }
            else if (method.TypeImplementation == ETypeImpl.SYNC)
            {
                return new ImpGS(reporter, procesamiento, method, lastClass);
            }


            return null;
        }

    }
}
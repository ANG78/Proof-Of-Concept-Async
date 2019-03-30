namespace HowToWorkAsync
{
    public interface IUseMethod
    {
        IUseMethod Next { get; set; }
        
        int NumSteps { get; set; }
        int Level { get; set; }
        EventNextMethodWasChanged EventChange { get; set; }
        string IdMethod { get; }
        string ValidateConfigurations();
        IGetBase Implementation { get; set; }
        ETypeImpl TypeImpl { get; set; }
        ETypeDoIndependentWork TypeDoIndependentWork { get; set; }
        EStrategyDoIndependentWork StrategyDoIndependentWork { get; set; }
        ECallNext CallNext { get; set; }
    }
}

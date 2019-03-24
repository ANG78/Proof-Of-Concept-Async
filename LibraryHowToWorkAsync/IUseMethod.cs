namespace HowToWorkAsync
{
    public interface IUseMethod
    {
        IUseMethod Next { get; set; }
        ETypeWork TypeWork { get; set; }
        int NumSteps { get; set; }
        int Level { get; set; }
        EventNextMethodWasChanged EventChange { get; set; }
        string IdMethod { get; }
        string ValidateConfigurations();
        IGetBase Implementation { get; set; }
        ECallNext CallNext { get; set; }
        ETypeImpl TypeNextImpl { get; set; }
        EMyTypeImpl MyImpl { get; set; }
    }
}

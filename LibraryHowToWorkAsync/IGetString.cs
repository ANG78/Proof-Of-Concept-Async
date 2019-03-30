using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public interface IGetBase
    {
        uint Level { get; }
        string Ident();
        IMyWork DoIndependetWork { get; }
        string Validate();
    }

    public interface IGetString : IGetBase
    {
        string Main();
    }


    public interface IGetStringAsync : IGetBase
    {
        Task<string> MainAsync();
        bool PossibleDeadLockUsingAwaiter();
    }

}

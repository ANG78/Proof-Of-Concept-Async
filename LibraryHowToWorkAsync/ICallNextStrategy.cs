
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public interface ICallNextAsyncStrategy : ICallNextStrategyDescription
    {
        bool HaveToWaitPre();
        Task Pre();
        Task Post();
        string Result { get; }
    }

    public interface ICallNextSyncStrategy : ICallNextStrategyDescription
    {
        void Pre();
        void Post();
        string Result { get; }
    }

    public interface ICallNextStrategyDescription :  ICallNextValidate
    {
        bool HaveToWaitPost();
        bool IsCompleted();
        string PreDescription();
        string PostDescription();
    }

    public interface ICallNextValidate
    {
        string Validate(uint Level);
    }

    public interface ICallNextDescription
    {
        string PreDescription();
        string PostDescription();
    }

    public interface IGetStringAsyncWithNext : ICallNextDescription
    {
        ICallNextAsyncStrategy NextCallStrategy { get; }
    }

    public interface IGetStringWithNext : ICallNextDescription
    {
        ICallNextSyncStrategy NextCallStrategy { get; }
    }
}

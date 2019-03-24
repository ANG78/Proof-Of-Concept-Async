
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

    public interface ICallNextStrategyDescription
    {
        bool HaveToWaitPost();
        bool IsCompleted();
        string PreDescription();
        string PostDescription();
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

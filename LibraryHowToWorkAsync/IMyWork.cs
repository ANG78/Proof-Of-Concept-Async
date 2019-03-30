using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public interface IMyWork
    {
        string Description();
        IStrategyTodo StrategyTodo { get; }
    }

    public interface IMyWorkSync : IMyWork
    {
        string GetString(string name, int idThread);
    }

    public interface IMyWorkASync : IMyWork
    {
        bool HaveToWait();
        Task<string> GetStringAsync(string name, int idThread);
    }

}

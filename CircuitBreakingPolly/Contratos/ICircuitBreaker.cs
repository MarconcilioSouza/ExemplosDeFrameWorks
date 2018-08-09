using System;
using System.Threading.Tasks;

namespace CircuitBreakingPolly.Contratos
{
    public interface ICircuitBreaker
    {
        void Execute(Action action);
        T Execute<T>(Func<T> func);
        TResult Execute<TResult, TReset>(Func<TResult> action, Func<TReset> actionReset);
        Task ExecuteAsync(Func<Task> func);
        Task<T> ExecuteAsync<T>(Func<Task<T>> func);
    }
}

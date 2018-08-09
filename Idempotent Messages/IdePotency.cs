using System;
using System.Threading;
using System.Threading.Tasks;

namespace Idempotent_Messages
{
    public abstract class IdePotency<TData, TResult>
    {
        public IdePotency(TData data)
        {
        }

        private IdePotency(TData data, Func<TResult> _f, Object state, CancellationToken ct, TaskCreationOptions opts)
        {
            _f = this.function;
        }

        protected abstract TResult function();
    }
}

using CircuitBreakingPolly.Contratos;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CircuitBreakingPolly
{

    public class PoliticasManipulacaoPolly //: ICircuitBreaker
    {
        public static RetryPolicy getPolicyRetry()
        {
            return Policy.Handle<SqlException>()
                .Or<Exception>()
                .Retry(2, (exception, retryCount, context) =>
                {
                    RepositorioProdutos.sql = "SELECT * FROM [Produtos].[dbo].[Produtos]";
                    Log(exception, context, retryCount);
                });
        }

        public static RetryPolicy getPolicyWaitAndRetry()
        {
            return Policy.Handle<Exception>()
              .WaitAndRetry(new[] { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(8) },
                     (exception, timeSpan, context) =>
                     {
                         Log(exception, context, timeSpan);
                     });
        }

        public static RetryPolicy getPolicyWaitAndRetryAlterNumaro()
        {
            return Policy.Handle<DivideByZeroException>()
                .Or<Exception>()
              .WaitAndRetry(new[] { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(8) },
                     (exception, timeSpan, context) =>
                     {
                         Program.n2++;
                         Log(exception, context, timeSpan);
                     });
        }

        public static RetryPolicy getPolicyWaitAndRetryCallMethod()
        {

            return Policy.Handle<DivideByZeroException>()
               .Or<Exception>()
               .Retry(2, onRetry: (e, i) => Program.AtualizarNumero(e, i));
        }

        public TResult Execute<TResult>(Func<TResult> action)
        {
            // Break the circuit after the specified number of consecutive exceptions
            // and keep circuit broken for the specified duration,
            // calling an action on change of circuit state.
            Action<Exception, TimeSpan> onBreak = (exception, timespan) =>
            {
                Log(exception, timespan);
            };

            Action onReset = () =>
            {
                Program.n2 = 2;
            };

            TResult result = Policy
                .Handle<DivideByZeroException>()
                .CircuitBreaker(5, TimeSpan.FromMinutes(2), onBreak, onReset)
                .Execute(action);

            return result;
        }

        public TResult Execute<TResult, TReset>(Func<TResult> action, Func<TResult> actionReset)
        {
            Action<Exception, TimeSpan> onBreak = (exception, timespan) =>
            {
                Log(exception, timespan);
            };

            Action onReset = () =>
            {
                actionReset.Invoke();
            };

            return Policy
                .Handle<DivideByZeroException>()
                .CircuitBreaker(2, TimeSpan.FromMinutes(1)) //, onBreak, onReset)
                .Execute(action);
        }

        public Task ExecuteCircuitBreaker(Func<Task> action)
        {
            //Program.programStartTime = DateTime.Now;
            // simular o erro
            Program.programStartTime = DateTime.Now.AddSeconds(10);

            int totalvezes = 2;
            Task result2 = null;

            //Thread safety
            //PolicyWrap is thread - safe: multiple calls may safely be placed concurrently through a policy instance.
            //Policy reuse
            //PolicyWrap instances may be re - used across multiple call sites.
            //When reusing policies, use an ExecutionKey to distinguish different call - site usages within logging and metrics.

            Policy retryPolicy = Policy.Handle<TimeoutException>().RetryAsync(totalvezes);

            Policy circuitBreakerPolicy = Policy
                .Handle<TimeoutException>()
                .CircuitBreakerAsync(totalvezes, TimeSpan.FromSeconds(2));

            try
            {
                // you need to combine retry policy with circuit breaker policy. One way to do that would be:
                result2 = retryPolicy.ExecuteAsync(() => circuitBreakerPolicy.ExecuteAsync(action, true));

                // or Polly has also now added PolicyWrap which can make the syntax for
                //combining policies even more concise:

                //result2 = retryPolicy.WrapAsync(circuitBreakerPolicy).ExecuteAsync(action, true);
            }
            catch (AggregateException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }

            return result2;
        }


        // nomtei mas não entendi direito ver o metodo abaixo
        public Task ExecuteFallback(Func<Task> action, Func<Task> fallbackAction)
        {
            Program.programStartTime = DateTime.Now;
            Task result = null;

            Policy fallback = Policy
                .Handle<TimeoutException>()
                .Or<Exception>()
                .Fallback(() => action(), onFallback: (exception, context) =>
                {
                    Log(exception);
                    fallbackAction();
                });

            try
            {
                result = fallback.Execute(action);
            }
            catch (AggregateException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }

            return result;
        }

        public Task ExecuteFallbackWrap(Func<Task> action, Func<Task> fallbackAction)
        {
            Program.programStartTime = DateTime.Now;
            Task result = null;

            var politicaWithFallback = Policy
                .Handle<Exception>()
                .Fallback(() => fallbackAction());

            var politicaRetry = Policy
                .Handle<Exception>()
                .Retry(2);
            try
            {
                result = politicaWithFallback.Execute(action);

                // ou combinado
                var mixedPolicy = Policy.Wrap(politicaWithFallback, politicaRetry).Execute(action);
            }
            catch (AggregateException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }

            return result;
        }


        Exception LancaExcepcion()
        {
            throw new Exception();
        }

        #region Logs

        static void Log(Exception e, int intentos)
        {
            Console.WriteLine($"Intento: {intentos:00}\tTiempo: {DateTime.Now}\nError: {e.Message}");
        }

        public static void Log(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void Log(Exception exception, Context context, int retryCount)
        {
            Console.WriteLine("Erro: " + exception.Message + " Total : " + retryCount.ToString()
                    + " context : " + context.PolicyKey);
        }

        private void Log(Exception exception, TimeSpan timeSpan)
        {
            Console.WriteLine("Erro: " + exception.Message + " Tempo : " + timeSpan.ToString());
        }

        public static void Log(Exception exception, Context context, TimeSpan timeSpan)
        {
            Console.WriteLine("Erro: " + exception.Message + " Tempo : " + timeSpan.ToString()
                    + " context : " + context.PolicyKey);
        }

        #endregion
    }
}

using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CircuitBreakingPolly
{
    public class Program
    {
        public static int n1 = 2;
        public static int n2 = 0;
        public static DateTime programStartTime;
        private static List<Produtos> produtos = new List<Produtos>();

        static void Main(string[] args)
        {
            //RetryPolicy WaitRetry = PoliticasManipulacaoPolly.getPolicyRetry();
            //produtos = WaitRetry.Execute(() => RepositorioProdutos.GetProdutos(), new Context("Teste"));
            //Console.WriteLine("");
            //foreach (var p in produtos)
            //{
            //    Console.WriteLine("Id {0} - Nome{1} - Descrição{2}", p.ProdutoId, p.Nome, p.Decricao);
            //}

            //RetryPolicy WaitAndRetryAlterNumaro = PoliticasManipulacaoPolly.getPolicyWaitAndRetryAlterNumaro();
            //int result = WaitAndRetryAlterNumaro.Execute(() => Divisao(n1, n2));
            //Console.WriteLine(result.ToString());
            //Console.WriteLine("");

            //n2 = 0;

            //RetryPolicy WaitAndRetryCallMethod = PoliticasManipulacaoPolly.getPolicyWaitAndRetryCallMethod();
            //result = WaitAndRetryCallMethod.Execute(() => Divisao(n1, n2));
            //Console.WriteLine(result.ToString());

            n2 = 0;

            var pmp = new PoliticasManipulacaoPolly();
            //Int32 result = pmp.Execute(() => Divisao(n1, n2));
            //Console.WriteLine(result.ToString());
            //Console.WriteLine("");

            // var result = pmp.Execute(() => Divisao(n1, n2), () => Divisao(2, 2));

            //Task result = pmp.ExecuteCircuitBreaker(() => CallTask());

            //Task result = pmp.ExecuteFallback(() => CallTask(), () => CallTask2());
            Task result = pmp.ExecuteFallbackWrap(() => CallTask(), () => CallTask2());

            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }

        public static Task CallTask()
        {
            Console.WriteLine("Task falhou.");
            throw new TimeoutException();
        }

        public static Task CallTask2()
        {
            Console.WriteLine("Task completa.");
            throw new Exception();
        }


        private static int Divisao(int n1, int n2)
        {
            return n1 / n2;
        }


        internal static void AtualizarNumero(Exception e, int n)
        {
            n2 = n;
            PoliticasManipulacaoPolly.Log(e);
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace Idempotent_Messages
{
    public class Program
    {
        static void Main(string[] args)
        {
            var produto = new Produtos();
            produto.ProdutoId = 100;
            produto.Titulo = "Tela Led";
            produto.Descricao = "Tela de monito Led";


            var command1 = new CqrsBase<int, string>();
            // var id = command1.CriaTarefa();

            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {
                var id = r.Next(1, 20);
                var TReturn = command1.IdPotency((f) =>
                {
                    return command1.VerificaTarefa(id);
                },
                (x) => { return Convert.ToInt32(x); }, id.ToString());
            }


            //var command = new CqrsBase<Produtos, Produtos>();

            //var TReturn = command.IdPotency((f) =>
            //{
            //    return command.VerificaTarefa(f.ProdutoId);
            //},
            //(x) => { return x; } , produto);
        }
    }

    public class Produtos
    {
        public int ProdutoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }

    public class CqrsBase<T, TArgs>
    {
        private Dictionary<string, Task<int>> tarefas = new Dictionary<string, Task<int>>();
        private Random r = new Random();

        private async Task<int> Tarefa()
        {
            return r.Next(1, 20);
        }

        public T CriaTarefa(TArgs args)
        {
            var TipoArgs = args.GetType();
            var propValue = string.Empty;

            if (TipoArgs == typeof(string))
            {
                propValue = (string)Convert.ChangeType(args, typeof(TArgs));
            }

            tarefas.Add(propValue, Tarefa());
            T value = (T)Convert.ChangeType(propValue, typeof(T));
            return value;
        }

        public bool VerificaTarefa(int id)
        {
            Task<int> tarefa;
            if (tarefas.TryGetValue(id.ToString(), out tarefa))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Execute(TArgs args)
        {
            T value = CriaTarefa(args);// IdePotencyHelper.ExecuteSave(args);
                                       // T value = (T)Convert.ChangeType(id, typeof(T));

            return value;
        }


    }

    public static class CqrsBase
    {
        public static TReturn IdPotency<TReturn, TArgs>(this CqrsBase<TReturn, TArgs> cqrs, Func<TArgs, bool> verify,
            Func<TArgs, TReturn> existsFunc, TArgs args)
        {
            var existe = verify(args);
            if (existe)
            {
                return existsFunc(args);
            }
            else
            {
                return cqrs.Execute(args);
            }
        }
    }
}

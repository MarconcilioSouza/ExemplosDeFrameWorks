using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManipulandoTasks
{
    class Program
    {
        public int contador = 0;
        Task[] tarafas;
        static void Main(string[] args)
        {
            var filas = new List<Filas>()
            {
                new Filas(){id=2},
                new Filas(){id=3},
                new Filas(){id=4},
                new Filas(){id=5},
                new Filas(){id=6},
                new Filas(){id=7}
            };
            var fila = new Filas()
            {
                id = 1,
                FilasSecundarias = filas,
            };

            new Program().executar(fila);
        }

        public void executar(Filas fila)
        {
            var total = totalDeFilas(fila, 1);
            contador = total;

            Task.Factory.StartNew(async () =>
            {
                while (contador > 1) { }
                Console.WriteLine("Teste fim  eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee2");
                Console.ReadKey();
            });

            tarafas = new Task[total];



            for (int i = 0; i < tarafas.Length; i++)
            {
                tarafas[i] = new Task(() => { });
            }

            foreach (var item in tarafas)
            {
                Console.WriteLine(item.Status);
            }

            tarafas[1] = new Task(() => { Console.WriteLine("Teste t1"); }, TaskCreationOptions.AttachedToParent);
            tarafas[1].Start();
            contador--;

            tarafas[2] = new Task(() => { Console.WriteLine("Teste t2"); });
            tarafas[2].RunSynchronously();
            tarafas[2].ContinueWith(t => { contador--; });
           

            Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < tarafas.Length; i++)
                {
                    if (tarafas[i].Status == TaskStatus.Created)
                    {
                        var id = tarafas[i].Id;
                        tarafas[i] = new Task(() => { Console.WriteLine("Teste t{0}", id); });
                        tarafas[i].RunSynchronously();
                        tarafas[i].ContinueWith(t => { contador--; });
                    }
                }
            });

           

        }

        public int totalDeFilas(Filas fila, int n)
        {
            if (fila.FilasSecundarias != null)
                foreach (var item in fila.FilasSecundarias)
                {
                    n++;
                    if (item.FilasSecundarias != null)
                        totalDeFilas(item, n);
                }

            return n;
        }
    }

    public class Filas
    {
        public int id { get; set; }
        public List<Filas> FilasSecundarias { get; set; }
    }
}

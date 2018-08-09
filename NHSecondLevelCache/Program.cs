using NHibernate;
using NHSecondLevelCache.Code;
using System;
using System.Threading;

namespace NHSecondLevelCache
{
    class Program
    {
        //https://www.codeproject.com/Articles/529016/NHibernate-Second-Level-Caching-Implementation

        static void Main(string[] args)
        {
            //DbHelper.ExecuteSql("INSERT INTO [Produtos].[dbo].[Produtos] ([Titulo],[Descricao]) VALUES ('Computadores','teste Marconcilio');");
            //DbHelper.ExecuteSql("INSERT INTO [Produtos].[dbo].[Produtos] ([Titulo],[Descricao]) VALUES ('TVs','teste Marconcilio');");
            //DbHelper.ExecuteSql("INSERT INTO [Produtos].[dbo].[Produtos] ([Titulo],[Descricao]) VALUES ('Camas','teste Marconcilio');");
            //DbHelper.ExecuteSql("INSERT INTO [Produtos].[dbo].[Produtos] ([Titulo],[Descricao]) VALUES ('Mesas','teste Marconcilio');");

            Produtos produtos = null;
            using (ISession session = SessionHelper.CreateSession())
            {
                produtos = session.Get<Produtos>(9);
            }

            Console.WriteLine(produtos.Titulo);

            DbHelper.ExecuteSql("UPDATE [Produtos] SET Titulo = 'Carro' WHERE ProdutoId = 9");

            Thread.Sleep(6000);
            Produtos produtos2 = null;
            using (ISession session = SessionHelper.CreateSession())
            {
                produtos2 = session.Get<Produtos>(9);
            }
            Console.WriteLine(produtos2.Titulo);

            Console.ReadKey();
        }
    }
}

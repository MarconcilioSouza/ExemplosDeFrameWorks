using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Collections.Generic;

namespace CacheTests
{
    [TestClass]
    public class CacheTests
    {
        ICacheProvider _cacheProvider;

        [TestInitialize]
        public void Initialize()
        {
            _cacheProvider = new RedisCacheProvider();
        }

        [TestMethod]
        public void Test_SetValue()
        {
            List<Produtos> produto = new List<Produtos>()
            {
                new Produtos(1, "TV", "TV Led", new List<Itens>()
                {
                    new Itens(1, "123456789"),
                    new Itens(2, "234567890")
                })
            };

            _cacheProvider.Set("Produtos", produto);
        }

        [TestMethod]
        public void Test_GetValue()
        {
            var itens = _cacheProvider.Get<List<Itens>>("Produtos");

            Assert.IsNotNull(itens);
            Assert.AreEqual(2, itens.Count);
        }
    }
}

using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace NHSecondLevelCache.Code
{
    public class Produtos
    {
        public virtual int ProdutoId { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual IList<Itens> Itens { get; set; }

        public Produtos()
        {
            Itens = new List<Itens>();
        }
    }

    public class ProdutosMap : ClassMap<Produtos>
    {
        public ProdutosMap()
        {
            base.Cache.NonStrictReadWrite().Region("TableBasedDependency");

            base.Id(c => c.ProdutoId).Column("ProdutoId");
            base.Map(c => c.Titulo).Column("Titulo");
            base.Map(c => c.Descricao).Column("Descricao");
            base.HasMany(c => c.Itens).Table("Itens").KeyColumn("ItemId");
            base.Table("Produtos");
        }
    }

    public class Itens
    {
        public virtual int ItemId { get; set; }
        public virtual string NomeItem { get; set; }
        public virtual Produtos Produtos { get; set; }
        public Itens()
        {
        }
    }

    public class ItensMap : ClassMap<Itens>
    {
        public ItensMap()
        {            
            base.Cache.NonStrictReadWrite().Region("FiveSecondTimeInterval");
            base.Table("Itens");
            base.Id(c => c.ItemId).Column("ItemId");
            base.Map(c => c.NomeItem).Column("NomeItem");
            base.References(c => c.Produtos).Column("ProdutoId");           
        }
    }

}

using System.Collections.Generic;

namespace CacheTests
{
    public class Produtos
    {
        public virtual int ProdutoId { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual IList<Itens> Itens { get; set; }

        public Produtos(int produtoId, string titulo, string descricao, List<Itens> itens)
        {
            ProdutoId = produtoId;
            Titulo = titulo;
            Descricao = descricao;
            Itens = itens;
        }
    }

    public class Itens
    {
        public virtual int ItemId { get; set; }
        public virtual string NomeItem { get; set; }
        public Itens(int itemId, string nomeItem)
        {
            ItemId = itemId;
            NomeItem = nomeItem;
        }
    }

    public class Contact
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public Contact(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }

    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        public Person(long id, string name, List<Contact> contacts)
        {
            this.Id = id;
            this.Name = name;
            this.Contacts = contacts;
        }
    }
}

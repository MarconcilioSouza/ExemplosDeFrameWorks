using FluentValidation.Results;
using System;

namespace TesteDryIoC.Generic
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Observacao { get; set; }
        public DateTime DtInclusao { get; set; }
        public bool Ativo { get; set; }
        public string Senha { get; set; }
    }
}

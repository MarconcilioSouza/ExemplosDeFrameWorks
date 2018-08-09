using DryIoc;
using FluentValidation;
using System;
using System.Linq;
using TesteDryIoC.Generic.Validacoes;

namespace TesteDryIoC.Generic
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerRegister.getContainer();
            //
            var validadorDefout = ProvadersValidator<Produto>.GetValidators(container);

            // buscar a validador especifico por propriedade
            var validadorPropriedade = ProvadersValidator<Cliente>.GetValidators(container, (validadores) =>
            {
                var validadorEncontrado = validadores.FirstOrDefault(x => x.CreateDescriptor().GetRulesForMember("Observacao").Any());
                return validadorEncontrado ?? validadores.First();
            });

            var validadorRule = ProvadersValidator<Cliente>.GetValidators(container, (validadores) =>
            {
                var validadorEncontrado = validadores.FirstOrDefault(x => x. CreateDescriptor().GetValidatorsForMember("ClienteValidatorRuleSet").Any());
                return validadorEncontrado ?? validadores.First();
            });


            Console.WriteLine("cliente1");
            Cliente cliente = new Cliente();
            cliente.Nome = "Marconcilio";
            cliente.SobreNome = "Souza";
            cliente.Observacao = "teste";
            cliente.Email = "marconcili@hotmail.com";
            cliente.Idade = 32;
            cliente.DtInclusao = DateTime.Now;
            cliente.Ativo = true;
            cliente.Senha = "";

            var resultad = validadorPropriedade.Validate(cliente);

            foreach (var item in resultad.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }

           // validadorRule2.Validate(cliente, ruleSet: "Names");

            //var resultad1 = validador.Validate(cliente);

            //foreach (var item in resultad1.Errors)
            //{
            //    Console.WriteLine(item.ErrorMessage);
            //}

            //Console.WriteLine("cliente2");

            //Cliente cliente2 = new Cliente();
            //cliente2.Nome = "Marconcilio";
            //cliente2.SobreNome = "Souza";
            //cliente2.Observacao = "teste";
            //cliente2.Email = "marconcili@hotmail.com";
            //cliente2.Idade = 32;
            //cliente2.DtInclusao = DateTime.Now;
            //cliente2.Ativo = true;
            //cliente2.Senha = "12345";

            //resultad = validador.Validate(cliente2);

            //foreach (var item in resultad.Errors)
            //{
            //    Console.WriteLine(item.ErrorMessage);
            //}

            Console.WriteLine("produto");

            Produto produto = new Produto();
            produto.Nome = "teste";

            resultad = validadorDefout.Validate(produto);

            foreach (var item in resultad.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }

            Console.WriteLine("produto1");

            ProdutoItens produtoiten = new ProdutoItens();
            produtoiten.NomeItem = "";
            produtoiten.Descricao = "";

            try
            {
                var validadorprodutoitem = ProvadersValidator<ProdutoItens>.GetValidators(container);

                var teste = new ProdutoValidator();

                teste.Validate(produto, ruleSet : "");

                resultad = validadorprodutoitem. Validate(produtoiten);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            foreach (var item in resultad.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
            }

            Console.ReadKey();
        }
    }
}

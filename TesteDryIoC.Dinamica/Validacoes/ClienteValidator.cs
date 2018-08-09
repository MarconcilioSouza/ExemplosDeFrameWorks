using FluentValidation;
using System;

namespace TesteDryIoC.Generic.Validacoes
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
               // Garante que a propriedade especificada não seja nula                            
               .NotNull().WithMessage("{PropertyName} deve ser preenchido")
               //  Garante que a propriedade especificada não seja nula, uma string vazia ou espaço em branco (ou o valor padrão para tipos de valor, por exemplo, 0 para int
               .NotEmpty().WithMessage("{PropertyName} não pode estar em branco")
               // Especificar o tamanho minimo para o nome
               .MinimumLength(2).WithMessage("{PropertyName} não pode ter menos que 2 caracteres")
               // especificar o tamanho máximo para o nome
               .MaximumLength(200).WithMessage("{PropertyName} não pode ser maior que 200")
               // Garante que o valor da propriedade especificada não seja igual a um valor específico 
               .NotEqual("Carlos").WithMessage("{PropertyName} não pode igual a Carlos")
               //(ou não igual ao valor de outra propriedade)
               .NotEqual(c => c.SobreNome).WithMessage("{PropertyName} não pode igual ao sobrenome");

            RuleFor(x => x.Idade)
                .NotNull()
                .NotEmpty()
                // Garante que o valor da propriedade especificada seja maior do que um valor específico(ou maior que o valor de outra propriedade)
                .GreaterThan(18).WithMessage("{PropertyName} Minima é 18 anos!");

            // Garante que o comprimento de uma propriedade de string específica esteja dentro do intervalo especificado.
            RuleFor(x => x.Observacao)
                .Length(10, 250)
                .WithMessage("{PropertyName} tem que ter entre 10 e 250 caracteres");

            // Garante que o valor da propriedade especificada seja um formato de endereço de e-mail válido.
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("{ PropertyName} é inválido");

            //  Passa o valor da propriedade especificada para um delegate personalizado que pode executar a lógica de validação personalizada no valor
            RuleFor(x => x.DtInclusao).Must(ClienteValidatorDate).WithMessage("{ PropertyName} é inválido");

            //  Garante que o valor da propriedade especificada corresponda à expressão regular dada
            RuleFor(x => x.Senha)
                .Matches("").WithMessage("{ PropertyName} é inválido");

        }

        private bool ClienteValidatorDate(DateTime date)
        {
            if (date == default(DateTime))
                return false;
            return true;
        }
        private bool ClienteValidatorDate(DateTime? date)
        {
            if (date == default(DateTime))
                return false;
            return true;
        }
    }
}

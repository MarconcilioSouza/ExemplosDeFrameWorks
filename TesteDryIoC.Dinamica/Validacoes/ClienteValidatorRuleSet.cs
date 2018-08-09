using FluentValidation;
using System;

namespace TesteDryIoC.Generic.Validacoes
{
    public class ClienteValidatorRuleSet : AbstractValidator<Cliente>
    {
        public ClienteValidatorRuleSet()
        {
            // Usando 
            /*Os RuleSets permitem agrupar as regras de validação, que podem ser executadas
             * juntas como um grupo, ignorando outras regras:
             */

            RuleSet("Names", () =>
            {
                RuleFor(x => x.Nome).NotNull().WithMessage("{PropertyName} deve ser preenchido");
                RuleFor(x => x.SobreNome).NotNull().WithMessage("{PropertyName} deve ser preenchido");
            });

            RuleFor(x => x.Email)
           // Garante que a propriedade especificada não seja nula                            
           .NotNull().WithMessage("{PropertyName} deve ser preenchido");

            RuleFor(x => x.Nome)
          // Garante que a propriedade especificada não seja nula                            
          .NotNull().WithMessage("{PropertyName} deve ser preenchido ** sem rule");

            RuleFor(x => x.Email)
          // Garante que a propriedade especificada não seja nula                            
          .NotNull().WithMessage("{PropertyName} deve ser preenchido")
          .EmailAddress().WithMessage("{PropertyName} é invalido");

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

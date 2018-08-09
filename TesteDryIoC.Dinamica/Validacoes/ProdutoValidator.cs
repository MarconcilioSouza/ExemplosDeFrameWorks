using FluentValidation;

namespace TesteDryIoC.Generic.Validacoes
{
    class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                  .NotEmpty().WithMessage("{PropertyName} não pode estar em branco")
               // Garante que a propriedade especificada não seja nula                            
               .NotNull().WithMessage("{PropertyName} deve ser preenchido");
        }
    }
}

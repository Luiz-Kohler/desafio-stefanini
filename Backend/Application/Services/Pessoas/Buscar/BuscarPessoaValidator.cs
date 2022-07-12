using FluentValidation;

namespace Application.Services.Pessoas.Buscar
{
    public class BuscarPessoaValidator : AbstractValidator<BuscarPessoaRequest>
    {
        public BuscarPessoaValidator()
        {
            RuleFor(pessoa => pessoa.Id).GreaterThan(0).WithMessage("Cidade deve ser valida.");
        }
    }
}

using FluentValidation;

namespace Application.Services.Pessoas.Excluir
{
    public class ExcluirPessoaValidator : AbstractValidator<ExcluirPessoaRequest>
    {
        public ExcluirPessoaValidator()
        {
            RuleFor(pessoa => pessoa.Id).GreaterThan(0).WithMessage("Pessoa deve ser valida.");
        }
    }
}

using FluentValidation;

namespace Application.Services.Cidades.Excluir
{
    public class ExcluirCidadeValidator : AbstractValidator<ExcluirCidadeRequest>
    {
        public ExcluirCidadeValidator()
        {
            RuleFor(cidade => cidade.Id).GreaterThan(0).WithMessage("Cidade deve ser valida.");
        }
    }
}

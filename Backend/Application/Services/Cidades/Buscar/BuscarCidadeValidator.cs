using FluentValidation;

namespace Application.Services.Cidades.Buscar
{
    public class BuscarCidadeValidator : AbstractValidator<BuscarCidadeRequest>
    {
        public BuscarCidadeValidator()
        {
            RuleFor(cidade => cidade.Id).GreaterThan(0).WithMessage("Cidade deve ser valida.");
        }
    }
}

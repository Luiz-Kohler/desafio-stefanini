using FluentValidation;

namespace Application.Services.Cidades.Criar
{
    public class CriarCidadeValidator : AbstractValidator<CriarCidadeRequest>
    {
        public CriarCidadeValidator()
        {
            RuleFor(cidade => cidade.Nome).NotEmpty().Length(2, 200).WithMessage("Nome deve conter entre 2 a 200 caracteres.");
            RuleFor(cidade => cidade.Uf.Trim()).NotEmpty().Length(2).WithMessage("UF deve ser valida.");
        }
    }
}

using FluentValidation;

namespace Application.Services.Pessoas.Atualizar
{
    public class AtualizarPessoaValidator : AbstractValidator<AtualizarPessoaRequest>
    {
        public AtualizarPessoaValidator()
        {
            RuleFor(pessoa => pessoa.Id).GreaterThan(0).WithMessage("Pessoa deve ser valida.");
            RuleFor(pessoa => pessoa.Nome).NotEmpty().Length(2, 300).WithMessage("Nome deve conter entre 2 a 300 caracteres.");
            RuleFor(pessoa => pessoa.DataNascimento).InclusiveBetween(DateTime.MinValue, DateTime.UtcNow).WithMessage("Data de nascimento deve ser valida.");
            RuleFor(pessoa => pessoa.CidadeId).GreaterThan(0).WithMessage("Cidade deve ser valida.");
        }
    }
}

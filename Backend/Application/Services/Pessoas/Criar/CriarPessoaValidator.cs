using Domain.Common.Extensios;
using FluentValidation;

namespace Application.Services.Pessoas.Criar
{
    public class CriarPessoaValidator : AbstractValidator<CriarPessoaRequest>
    {
        public CriarPessoaValidator()
        {
            RuleFor(pessoa => pessoa.Nome).NotEmpty().Length(2, 300).WithMessage("Nome deve conter entre 2 a 300 caracteres.");
            RuleFor(pessoa => pessoa.Cpf.FormatCpf()).Must(cpf => cpf.IsCpf()).WithMessage("CPF deve ser valido.");
            RuleFor(pessoa => pessoa.DataNascimento).InclusiveBetween(DateTime.MinValue, DateTime.UtcNow).WithMessage("Data de nascimento deve ser valida.");
            RuleFor(pessoa => pessoa.CidadeId).GreaterThan(0).WithMessage("Cidade deve ser valida.");
        }
    }
}

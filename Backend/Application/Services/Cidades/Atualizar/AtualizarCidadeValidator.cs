﻿using FluentValidation;

namespace Application.Services.Cidades.Atualizar
{
    public class AtualizarCidadeValidator : AbstractValidator<AtualizarCidadeRequest>
    {
        public AtualizarCidadeValidator()
        {
            RuleFor(cidade => cidade.Id).GreaterThan(0).WithMessage("Cidade deve ser valida.");
            RuleFor(cidade => cidade.Nome).NotEmpty().Length(2, 200).WithMessage("Nome deve conter entre 2 a 200 caracteres.");
            RuleFor(cidade => cidade.Uf.Trim()).NotEmpty().Length(2).WithMessage("UF deve ser valida.");
        }
    }
}

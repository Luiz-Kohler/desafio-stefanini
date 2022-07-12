using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Pessoas.Atualizar
{
    public class AtualizarPessoaHandler : IRequestHandler<AtualizarPessoaRequest, AtualizarPessoaResponse>
    {
        private readonly IPessoasRepository _pessoasRepository;
        private readonly ICidadesRepository _cidadesRepository;
        private readonly ILogger<AtualizarPessoaHandler> _logger;

        public AtualizarPessoaHandler(
            IPessoasRepository pessoasRepository,
            ICidadesRepository cidadesRepository,
            ILogger<AtualizarPessoaHandler> logger)
        {
            _pessoasRepository = pessoasRepository;
            _cidadesRepository = cidadesRepository;
            _logger = logger;
        }

        public async Task<AtualizarPessoaResponse> Handle(AtualizarPessoaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando atualização de pessoa.");

            var cidade = await _cidadesRepository.SelecionarUmaPor(c => c.Id == request.CidadeId && c.EhAtivo);

            if (cidade is null)
            {
                _logger.LogInformation("Nenhuma cidade encontrada com o id informado.");
                throw new NotFoundException("Nenhuma cidade encontrada com o id informado.");
            }

            var pessoa = await _pessoasRepository.SelecionarUmaPor(p => p.Id == request.Id && p.EhAtivo);

            if (pessoa is null)
            {
                _logger.LogInformation("Nenhuma pessoa encontrada com o id informado.");
                throw new NotFoundException("Nenhuma pessoa encontrada com o id informado.");
            }

            pessoa.Atualizar(request.Nome, request.DataNascimento, cidade);

            _logger.LogInformation("Atualizando pessoa.");
            await _pessoasRepository.Atualizar(pessoa);
            _logger.LogInformation("Pessoa atualizada.");

            return default;
        }
    }
}

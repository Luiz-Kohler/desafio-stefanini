using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Pessoas.Excluir
{
    public class ExcluirPessoaHandler : IRequestHandler<ExcluirPessoaRequest, ExcluirPessoaResponse>
    {
        private readonly IPessoasRepository _repository;
        private readonly ILogger<ExcluirPessoaHandler> _logger;

        public ExcluirPessoaHandler(IPessoasRepository repository, ILogger<ExcluirPessoaHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ExcluirPessoaResponse> Handle(ExcluirPessoaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão de pessoa.");

            var pessoa = await _repository.SelecionarUmaPor(p => p.Id == request.Id && p.EhAtivo);

            if (pessoa is null)
            {
                _logger.LogInformation($"Nenhuma pessoa encontrada com o Id: {request.Id}.");
                throw new NotFoundException($"Nenhuma pessoa encontrada com o Id: {request.Id}.");
            }

            _logger.LogInformation("Excluindo pessoa.");
            await _repository.Excluir(pessoa);
            _logger.LogInformation("Pessoa excluida.");

            return default;
        }
    }
}

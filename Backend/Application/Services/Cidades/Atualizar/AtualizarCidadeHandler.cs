using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Cidades.Atualizar
{
    public class AtualizarCidadeHandler : IRequestHandler<AtualizarCidadeRequest, AtualizarCidadeResponse>
    {
        private readonly ICidadesRepository _repository;
        private readonly ILogger<AtualizarCidadeHandler> _logger;

        public AtualizarCidadeHandler(ICidadesRepository repository, ILogger<AtualizarCidadeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AtualizarCidadeResponse> Handle(AtualizarCidadeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando atualização de cidade.");

            var cidade = await _repository.SelecionarUmaPor(c => c.Id == request.Id && c.EhAtivo);

            if (cidade is null)
            {
                _logger.LogInformation($"Nenhuma cidade encontrada com o Id: {request.Id}.");
                throw new NotFoundException($"Nenhuma cidade encontrada com o Id: {request.Id}.");
            }

            cidade.Atualizar(request.Nome, request.Uf);

            _logger.LogInformation("Atualizando cidade.");
            await _repository.Atualizar(cidade);
            _logger.LogInformation("Cidade atualizada.");

            return default;
        }
    }
}

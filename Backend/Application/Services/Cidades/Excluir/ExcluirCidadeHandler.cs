using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Cidades.Excluir
{
    public class ExcluirCidadeHandler : IRequestHandler<ExcluirCidadeRequest, ExcluirCidadeResponse>
    {
        private readonly ICidadesRepository _repository;
        private readonly IPessoasRepository _pessoasRepository;
        private readonly ILogger<ExcluirCidadeHandler> _logger;

        public ExcluirCidadeHandler(ICidadesRepository repository, IPessoasRepository pessoasRepository, ILogger<ExcluirCidadeHandler> logger)
        {
            _repository = repository;
            _pessoasRepository = pessoasRepository;
            _logger = logger;
        }

        public async Task<ExcluirCidadeResponse> Handle(ExcluirCidadeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão de cidade.");

            var cidade = await _repository.SelecionarUmaPorIncluindoPessoas(c => c.Id == request.Id && c.EhAtivo);

            if (cidade is null)
            {
                _logger.LogInformation($"Nenhuma cidade encontrada com o Id: {request.Id}.");
                throw new NotFoundException($"Nenhuma cidade encontrada com o Id: {request.Id}.");
            }

            _logger.LogInformation("Excluindo pessoas relacionadas a cidade que vai ser excluida.");
            var pessoasRelacionadas = cidade.Pessoas;
            await _pessoasRepository.ExcluirVarios(pessoasRelacionadas);
            _logger.LogInformation("Pessoas  excluidas relacionadas a cidade que vai ser excluida.");

            _logger.LogInformation("Excluindo cidade.");
            await _repository.Excluir(cidade);
            _logger.LogInformation("Cidade excluida.");

            return default;
        }
    }
}

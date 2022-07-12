using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Cidades.Buscar
{
    public class BuscarCidadeHandler : IRequestHandler<BuscarCidadeRequest, BuscarCidadeResponse>
    {
        private readonly ICidadesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarCidadeHandler> _logger;

        public BuscarCidadeHandler(ICidadesRepository repository, IMapper mapper, ILogger<BuscarCidadeHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarCidadeResponse> Handle(BuscarCidadeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando busca de cidade.");

            var cidade = await _repository.SelecionarUmaPorIncluindoPessoas(c => c.Id == request.Id && c.EhAtivo);

            if (cidade is null)
            {
                _logger.LogInformation($"Nenhuma cidade encontrada com o Id: {request.Id}.");
                throw new NotFoundException($"Nenhuma cidade encontrada com o Id: {request.Id}.");
            }

            return _mapper.Map<BuscarCidadeResponse>(cidade);
        }
    }
}

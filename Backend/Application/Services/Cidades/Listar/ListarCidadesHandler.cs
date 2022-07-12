using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Cidades.Listar
{
    public class ListarCidadesHandler : IRequestHandler<ListarCidadesRequest, ListarCidadesResponse>
    {
        private readonly ICidadesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarCidadesHandler> _logger;

        public ListarCidadesHandler(ICidadesRepository repository, IMapper mapper, ILogger<ListarCidadesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ListarCidadesResponse> Handle(ListarCidadesRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando listagem de cidades.");

            var cidades = await _repository.SelecionarVariasPor(c => c.EhAtivo);

            return _mapper.Map<ListarCidadesResponse>(cidades);
        }
    }
}

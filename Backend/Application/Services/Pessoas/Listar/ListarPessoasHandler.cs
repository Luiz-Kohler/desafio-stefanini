using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Pessoas.Listar
{
    public class ListarPessoasHandler : IRequestHandler<ListarPessoasRequest, ListarPessoasResponse>
    {
        private readonly IPessoasRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarPessoasHandler> _logger;

        public ListarPessoasHandler(IPessoasRepository repository, IMapper mapper, ILogger<ListarPessoasHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ListarPessoasResponse> Handle(ListarPessoasRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando listagem de pessoas.");

            var pessoas = await _repository.SelecionarVariasPorIncluindoCidades(c => c.EhAtivo);

            return _mapper.Map<ListarPessoasResponse>(pessoas);
        }
    }
}

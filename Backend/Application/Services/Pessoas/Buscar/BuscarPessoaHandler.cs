using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Pessoas.Buscar
{
    public class BuscarPessoaHandler : IRequestHandler<BuscarPessoaRequest, BuscarPessoaResponse>
    {
        private readonly IPessoasRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarPessoaHandler> _logger;

        public BuscarPessoaHandler(
            IPessoasRepository repository,
            IMapper mapper,
            ILogger<BuscarPessoaHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarPessoaResponse> Handle(BuscarPessoaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando busca de pessoa.");

            var pessoa = await _repository.SelecionarUmaPorIncluindoCidade(c => c.Id == request.Id && c.EhAtivo);

            if (pessoa is null)
            {
                _logger.LogInformation($"Nenhuma pessoa encontrada com o Id: {request.Id}.");
                throw new NotFoundException($"Nenhuma pessoa encontrada com o Id: {request.Id}.");
            }

            return _mapper.Map<BuscarPessoaResponse>(pessoa);
        }
    }
}

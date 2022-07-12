using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Cidades.Criar
{
    public class CriarCidadeHandler : IRequestHandler<CriarCidadeRequest, CriarCidadeResponse>
    {
        private readonly ICidadesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CriarCidadeHandler> _logger;

        public CriarCidadeHandler(ICidadesRepository repository, IMapper mapper, ILogger<CriarCidadeHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CriarCidadeResponse> Handle(CriarCidadeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de cidade.");

            var cidade = await _repository.SelecionarUmaPor(c => c.UF == request.Uf && c.Nome == request.Nome && c.EhAtivo);

            if (cidade is not null)
            {
                _logger.LogInformation("Já existe uma cidade com esse nome cadastrada nessa UF.");
                throw new DuplicateValueException("Já existe uma cidade com esse nome cadastrada nessa UF.");
            }

            cidade = _mapper.Map<Cidade>(request);

            _logger.LogInformation("Inserindo nova cidade.");
            await _repository.Inserir(cidade);
            _logger.LogInformation("Nova cidade inserida.");

            return default;
        }
    }
}

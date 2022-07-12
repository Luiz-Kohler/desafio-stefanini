using Application.Common.Exceptions;
using AutoMapper;
using Domain.Common.Extensios;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Pessoas.Criar
{
    public class CriarPessoaHandler : IRequestHandler<CriarPessoaRequest, CriarPessoaResponse>
    {
        private readonly IPessoasRepository _pessoasRepository;
        private readonly ICidadesRepository _cidadesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CriarPessoaHandler> _logger;

        public CriarPessoaHandler(
            IPessoasRepository pessoasRepository,
            ICidadesRepository cidadesRepository,
            IMapper mapper,
            ILogger<CriarPessoaHandler> logger)
        {
            _pessoasRepository = pessoasRepository;
            _cidadesRepository = cidadesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CriarPessoaResponse> Handle(CriarPessoaRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de pessoa.");

            var cidade = await _cidadesRepository.SelecionarUmaPor(c => c.Id == request.CidadeId && c.EhAtivo);

            if (cidade is null)
            {
                _logger.LogInformation("Nenhuma cidade encontrada com o id informado.");
                throw new NotFoundException("Nenhuma cidade encontrada com o id informado.");
            }

            var pessoa = await _pessoasRepository.SelecionarUmaPor(p => p.Cpf == request.Cpf.FormatCpf() && p.EhAtivo);

            if (pessoa is not null)
            {
                _logger.LogInformation("Já existe uma pessoa com esse CPF.");
                throw new DuplicateValueException("Já existe uma pessoa com esse CPF.");
            }

            pessoa = _mapper.Map<Pessoa>(request);

            _logger.LogInformation("Inserindo nova pessoa.");
            await _pessoasRepository.Inserir(pessoa);
            _logger.LogInformation("Nova pessoa inserida.");

            return default;
        }
    }
}

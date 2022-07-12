using MediatR;

namespace Application.Services.Pessoas.Criar
{
    public class CriarPessoaRequest : IRequest<CriarPessoaResponse>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CidadeId { get; set; }
    }
}

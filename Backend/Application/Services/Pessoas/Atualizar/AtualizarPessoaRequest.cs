using MediatR;

namespace Application.Services.Pessoas.Atualizar
{
    public class AtualizarPessoaRequest : IRequest<AtualizarPessoaResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CidadeId { get; set; }
    }
}

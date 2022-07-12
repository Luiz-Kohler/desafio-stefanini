using MediatR;

namespace Application.Services.Pessoas.Excluir
{
    public class ExcluirPessoaRequest : IRequest<ExcluirPessoaResponse>
    {
        public int Id { get; set; }
    }
}

using MediatR;

namespace Application.Services.Cidades.Excluir
{
    public class ExcluirCidadeRequest : IRequest<ExcluirCidadeResponse>
    {
        public int Id { get; set; }
    }
}

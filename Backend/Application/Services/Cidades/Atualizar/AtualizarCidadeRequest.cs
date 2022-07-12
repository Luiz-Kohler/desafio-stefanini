using MediatR;

namespace Application.Services.Cidades.Atualizar
{
    public class AtualizarCidadeRequest : IRequest<AtualizarCidadeResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
    }
}

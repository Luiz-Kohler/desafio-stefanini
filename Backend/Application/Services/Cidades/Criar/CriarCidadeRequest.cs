using MediatR;

namespace Application.Services.Cidades.Criar
{
    public class CriarCidadeRequest : IRequest<CriarCidadeResponse>
    {
        public string Nome { get; set; }
        public string Uf { get; set; }
    }
}

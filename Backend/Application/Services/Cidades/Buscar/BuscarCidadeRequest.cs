using MediatR;

namespace Application.Services.Cidades.Buscar
{
    public class BuscarCidadeRequest : IRequest<BuscarCidadeResponse>
    {
        public int Id { get; set; }
    }
}

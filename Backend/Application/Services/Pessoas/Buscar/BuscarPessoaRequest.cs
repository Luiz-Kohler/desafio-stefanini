using MediatR;

namespace Application.Services.Pessoas.Buscar
{
    public class BuscarPessoaRequest : IRequest<BuscarPessoaResponse>
    {
        public int Id { get; set; }
    }
}

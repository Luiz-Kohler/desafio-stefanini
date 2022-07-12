using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface ICidadesRepository : IBaseRepository<Cidade>
    {
        Task<IEnumerable<Cidade>> SelecionarVariasPorIncluindoPessoas(Expression<Func<Cidade, bool>> filtro = null);
        Task<Cidade> SelecionarUmaPorIncluindoPessoas(Expression<Func<Cidade, bool>> filtro);
    }
}

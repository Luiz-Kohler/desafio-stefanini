using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IPessoasRepository : IBaseRepository<Pessoa>
    {
        Task<IList<Pessoa>> SelecionarVariasPorIncluindoCidades(Expression<Func<Pessoa, bool>> filtro = null);
        Task<Pessoa> SelecionarUmaPorIncluindoCidade(Expression<Func<Pessoa, bool>> filtro);
    }
}

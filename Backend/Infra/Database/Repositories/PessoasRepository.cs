using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Database.Repositories
{
    public class PessoasRepository : BaseRepository<Pessoa>, IPessoasRepository
    {
        public PessoasRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Pessoa> SelecionarUmaPorIncluindoCidade(Expression<Func<Pessoa, bool>> filtro)
        {
            return await Entity
                .Include(p => p.Cidade)
                .FirstOrDefaultAsync(filtro);
        }

        public async Task<IList<Pessoa>> SelecionarVariasPorIncluindoCidades(Expression<Func<Pessoa, bool>> filtro = null)
        {
            return await Entity
               .Include(p => p.Cidade)
               .Where(filtro)
               .ToListAsync();
        }
    }
}

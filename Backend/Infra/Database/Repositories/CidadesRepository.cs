using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Database.Repositories
{
    internal class CidadesRepository : BaseRepository<Cidade>, ICidadesRepository
    {
        public CidadesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Cidade> SelecionarUmaPorIncluindoPessoas(Expression<Func<Cidade, bool>> filtro)
        {
            return await Entity
                .Include(c => c.Pessoas)
                .FirstOrDefaultAsync(filtro);
        }

        public async Task<IEnumerable<Cidade>> SelecionarVariasPorIncluindoPessoas(Expression<Func<Cidade, bool>> filtro = null)
        {
            return await Entity
               .Include(c => c.Pessoas)
               .Where(filtro)
               .ToListAsync();
        }
    }
}

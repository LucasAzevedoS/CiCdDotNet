using Microsoft.EntityFrameworkCore;
using ProjetoColeta.Data.Contexts;
using ProjetoColeta.Models;

namespace ProjetoColeta.Repository
{
    public class PontoRepository : IPontoRepository
    {
        private readonly DatabaseContext _dbContext;

        public PontoRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PontoModel>> GetAllAsync()
        {
            return await _dbContext.Pontos.ToListAsync();
        }

        public async Task<PontoModel> GetByIdAsync(int id)
        {
            return await _dbContext.Pontos
                .FirstOrDefaultAsync(p => p.IdPonto == id);
        }

        public async Task AddAsync(PontoModel ponto)
        {
            await _dbContext.Pontos.AddAsync(ponto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PontoModel ponto)
        {
            _dbContext.Pontos.Update(ponto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ponto = await GetByIdAsync(id);
            if (ponto != null)
            {
                _dbContext.Pontos.Remove(ponto);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
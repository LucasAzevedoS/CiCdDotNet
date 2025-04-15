using Microsoft.EntityFrameworkCore;
using ProjetoColeta.Data;
using ProjetoColeta.Data.Contexts;
using ProjetoColeta.Models;

namespace ProjetoColeta.Repository
{
    public class ResiduoRepository : IResiduoRepository
    {
        private readonly DatabaseContext _context;

        public ResiduoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ResiduoModel> CreateResiduoAsync(ResiduoModel residuo)
        {
            _context.Residuos.Add(residuo);
            await _context.SaveChangesAsync();
            return residuo;
        }

        public async Task<IEnumerable<ResiduoModel>> GetAllResiduosAsync()
        {
            return await _context.Residuos.ToListAsync();
        }

        public async Task<ResiduoModel> GetResiduoByIdAsync(int id)
        {
            return await _context.Residuos.FindAsync(id);
        }

        public async Task UpdateResiduoAsync(ResiduoModel residuo)
        {
            _context.Entry(residuo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResiduoAsync(int id)
        {
            var residuo = await _context.Residuos.FindAsync(id);
            if (residuo != null)
            {
                _context.Residuos.Remove(residuo);
                await _context.SaveChangesAsync();
            }
        }
        

        public async Task<List<ResiduoModel>> GetPagedResiduosAsync(int pageNumber, int pageSize)
        {
            return await _context.Residuos
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)                   
                .ToListAsync();                   
        }

    }
}
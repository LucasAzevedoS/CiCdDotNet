using ProjetoColeta.Models;

namespace ProjetoColeta.Repository
{
    public interface IPontoRepository
    {
        Task<IEnumerable<PontoModel>> GetAllAsync();
        Task<PontoModel> GetByIdAsync(int id);
        Task AddAsync(PontoModel ponto);
        Task UpdateAsync(PontoModel ponto);
        Task DeleteAsync(int id);
    }
}
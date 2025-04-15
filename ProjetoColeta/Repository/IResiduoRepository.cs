using ProjetoColeta.Models;

namespace ProjetoColeta.Repository
{
    public interface IResiduoRepository
    {
        Task<ResiduoModel> CreateResiduoAsync(ResiduoModel residuo);
        Task<IEnumerable<ResiduoModel>> GetAllResiduosAsync();
        Task<ResiduoModel> GetResiduoByIdAsync(int id);
        Task UpdateResiduoAsync(ResiduoModel residuo);
        Task DeleteResiduoAsync(int id);
        Task<List<ResiduoModel>> GetPagedResiduosAsync(int pageNumber, int pageSize);


    }
}
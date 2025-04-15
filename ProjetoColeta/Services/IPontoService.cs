using ProjetoColeta.Models;

namespace ProjetoColeta.Services
{
    public interface IPontoService
    {
        Task<IEnumerable<PontoModel>> GetPontosAsync();
        Task<PontoModel> GetPontoByIdAsync(int id);
        Task CreatePontoAsync(PontoModel ponto);
        Task UpdatePontoAsync(PontoModel ponto);
        Task DeletePontoAsync(int id);
    }
}
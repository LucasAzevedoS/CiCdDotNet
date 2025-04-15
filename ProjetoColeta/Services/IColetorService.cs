using ProjetoColeta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoColeta.Services
{
    public interface IColetorService
    {
        Task<IEnumerable<ColetorModel>> GetColetorsAsync();
        Task<ColetorModel> GetColetorByIdAsync(int id);
        Task CreateColetorAsync(ColetorModel coletor);
        Task UpdateColetorAsync(ColetorModel coletor);
        Task DeleteColetorAsync(int id);
    }
}
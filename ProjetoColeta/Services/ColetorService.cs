using ProjetoColeta.Models;
using ProjetoColeta.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoColeta.Services
{
    public class ColetorService : IColetorService
    {
        private readonly IRepository<ColetorModel> _repository;

        public ColetorService(IRepository<ColetorModel> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ColetorModel>> GetColetorsAsync() =>
            await _repository.GetAllAsync();

        public async Task<ColetorModel> GetColetorByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task CreateColetorAsync(ColetorModel coletor) =>
            await _repository.AddAsync(coletor);

        public async Task UpdateColetorAsync(ColetorModel coletor) =>
            await _repository.UpdateAsync(coletor);

        public async Task DeleteColetorAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
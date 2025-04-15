using ProjetoColeta.Models;
using ProjetoColeta.Repository;

namespace ProjetoColeta.Services
{
    public class PontoService : IPontoService
    {
        private readonly IPontoRepository _repository;

        public PontoService(IPontoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PontoModel>> GetPontosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PontoModel> GetPontoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreatePontoAsync(PontoModel ponto)
        {
            await _repository.AddAsync(ponto);
        }

        public async Task UpdatePontoAsync(PontoModel ponto)
        {
            await _repository.UpdateAsync(ponto);
        }

        public async Task DeletePontoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
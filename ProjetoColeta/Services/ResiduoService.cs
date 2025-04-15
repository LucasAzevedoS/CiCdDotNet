using ProjetoColeta.Models;
using ProjetoColeta.Repository;

namespace ProjetoColeta.Services
{
    public class ResiduoService : IResiduoService
    {
        private readonly IResiduoRepository _residuoRepository;

        

        public ResiduoService(IResiduoRepository residuoRepository)
        {
            _residuoRepository = residuoRepository;
        }
        


        public async Task<ResiduoModel> CreateResiduoAsync(ResiduoModel residuo)
        {
            return await _residuoRepository.CreateResiduoAsync(residuo);
        }

        public async Task<IEnumerable<ResiduoModel>> GetAllResiduosAsync()
        {
            return await _residuoRepository.GetAllResiduosAsync();
        }

        public async Task<ResiduoModel> GetResiduoByIdAsync(int id)
        {
            return await _residuoRepository.GetResiduoByIdAsync(id);
        }

        public async Task UpdateResiduoAsync(ResiduoModel residuo)
        {
            await _residuoRepository.UpdateResiduoAsync(residuo);
        }

        public async Task DeleteResiduoAsync(int id)
        {
            await _residuoRepository.DeleteResiduoAsync(id);
        }
        
        
        public async Task<List<ResiduoModel>> GetPagedResiduosAsync(int pageNumber, int pageSize)
        {
           
            var residuos = await _residuoRepository.GetPagedResiduosAsync(pageNumber, pageSize);

            
            return residuos;
        }

    }
}
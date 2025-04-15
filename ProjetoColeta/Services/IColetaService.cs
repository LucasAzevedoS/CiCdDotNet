using ProjetoColeta.Repository;
using ProjetoColetaModels;

namespace ProjetoColeta.Services;

public interface IColetaService
{
    Task<IEnumerable<ColetaModel>> GetColetasAsync();
    Task<ColetaModel> GetColetaByIdAsync(int id);
    Task CreateColetaAsync(ColetaModel coleta);
}

public class ColetaService : IColetaService
{
    private readonly IRepository<ColetaModel> _repository;

    public ColetaService(IRepository<ColetaModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ColetaModel>> GetColetasAsync() => await _repository.GetAllAsync();
    public async Task<ColetaModel> GetColetaByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task CreateColetaAsync(ColetaModel coleta) => await _repository.AddAsync(coleta);
}

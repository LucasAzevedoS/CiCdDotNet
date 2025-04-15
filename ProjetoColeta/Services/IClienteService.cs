using ProjetoColeta.Models;
using ProjetoColeta.Repository;

namespace ProjetoColeta.Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteModel>> GetClientesAsync();
    Task<ClienteModel> GetClienteByIdAsync(int id);
    Task CreateClienteAsync(ClienteModel cliente);
    Task UpdateClienteAsync(ClienteModel cliente);
    Task DeleteClienteAsync(int id);
}

public class ClienteService : IClienteService
{
    private readonly IRepository<ClienteModel> _repository;

    public ClienteService(IRepository<ClienteModel> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClienteModel>> GetClientesAsync() => await _repository.GetAllAsync();
    public async Task<ClienteModel> GetClienteByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task CreateClienteAsync(ClienteModel cliente) => await _repository.AddAsync(cliente);
    public async Task UpdateClienteAsync(ClienteModel cliente) => await _repository.UpdateAsync(cliente);
    public async Task DeleteClienteAsync(int id) => await _repository.DeleteAsync(id);
}

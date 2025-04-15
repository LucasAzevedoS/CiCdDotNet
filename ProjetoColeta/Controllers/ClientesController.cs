using Microsoft.AspNetCore.Mvc;
using ProjetoColeta.Models;
using ProjetoColeta.Services;

namespace ProjetoColeta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    // Método POST para criar um cliente
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteModel cliente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.CreateClienteAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
    }

    // Método GET para buscar cliente pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _service.GetClienteByIdAsync(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // Método GET para listar todos os clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _service.GetClientesAsync();
        return Ok(clientes);
    }

    // Método PUT para atualizar um cliente
    // Veficicar depois pq nao esta funcionando
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(int id, [FromBody] ClienteModel cliente)
    // {
    //     if (id != cliente.IdCliente)
    //         return BadRequest("ID da URL não coincide com o ID do cliente.");
    //
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);
    //
    //     var clienteExistente = await _service.GetClienteByIdAsync(id);
    //     if (clienteExistente == null)
    //         return NotFound();
    //
    //     await _service.UpdateClienteAsync(cliente);
    //     return NoContent();
    // }

    // Método DELETE para remover um cliente
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _service.GetClienteByIdAsync(id);
        if (cliente == null)
            return NotFound();

        await _service.DeleteClienteAsync(id);
        return NoContent();
    }
}

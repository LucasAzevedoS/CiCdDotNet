using Microsoft.AspNetCore.Mvc;
using ProjetoColeta.Services;
using ProjetoColetaModels;

namespace ProjetoColeta.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class ColetasController : ControllerBase
{
    private readonly IColetaService _service;

    public ColetasController(IColetaService service)
    {
        _service = service;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     var coletas = await _service.GetColetasAsync();
    //     return Ok(coletas);
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var coleta = await _service.GetColetaByIdAsync(id);
        if (coleta == null)
            return NotFound();

        return Ok(coleta);
    }

    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] ColetaModel coleta)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);
    //
    //     await _service.CreateColetaAsync(coleta);
    //     return CreatedAtAction(nameof(GetById), new { id = coleta.IdColeta }, coleta);
    // }
}

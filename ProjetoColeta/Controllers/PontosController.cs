using Microsoft.AspNetCore.Mvc;
using ProjetoColeta.Models;
using ProjetoColeta.Services;

namespace ProjetoColeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontosController : ControllerBase
    {
        private readonly IPontoService _service;

        public PontosController(IPontoService service)
        {
            _service = service;
        }

        // GET: api/Pontos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pontos = await _service.GetPontosAsync();
            return Ok(pontos);
        }

        // GET: api/Pontos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ponto = await _service.GetPontoByIdAsync(id);
            if (ponto == null)
                return NotFound();

            return Ok(ponto);
        }

        // POST: api/Pontos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PontoModel ponto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreatePontoAsync(ponto);
            return CreatedAtAction(nameof(Get), new { id = ponto.IdPonto }, ponto);
        }

        // PUT: api/Pontos/{id}
        // Nao esta funcionando
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PontoModel ponto)
        {
            if (id != ponto.IdPonto)
                return BadRequest("ID da URL não coincide com o ID do ponto.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pontoExistente = await _service.GetPontoByIdAsync(id);
            if (pontoExistente == null)
                return NotFound();

            await _service.UpdatePontoAsync(ponto);
            return NoContent();
        }

        // DELETE: api/Pontos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pontoExistente = await _service.GetPontoByIdAsync(id);
            if (pontoExistente == null)
                return NotFound();

            await _service.DeletePontoAsync(id);
            return NoContent();
        }
    }
}

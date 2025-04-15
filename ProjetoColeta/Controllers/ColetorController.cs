using Microsoft.AspNetCore.Mvc;
using ProjetoColeta.Services;
using ProjetoColeta.Models;

namespace ProjetoColeta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetorController : ControllerBase
    {
        private readonly IColetorService _service;

        // Construtor com injeção de dependência para o serviço de coletor
        public ColetorController(IColetorService service)
        {
            _service = service;
        }

        // Método GET para listar todos os coletores
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coletores = await _service.GetColetorsAsync();
            if (coletores == null || !coletores.Any())
                return NotFound("Nenhum coletor encontrado.");

            return Ok(coletores);
        }

        // Método GET para buscar um coletor por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coletor = await _service.GetColetorByIdAsync(id);
            if (coletor == null)
                return NotFound("Coletor não encontrado.");

            return Ok(coletor);
        }

        // Método POST para criar um novo coletor
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColetorModel coletor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateColetorAsync(coletor);
            return CreatedAtAction(nameof(GetById), new { id = coletor.IdColetor }, coletor);
        }

        // Método PUT para atualizar um coletor existente
        // Nao esta funcionando
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ColetorModel coletor)
        {
            if (id != coletor.IdColetor)
                return BadRequest("ID da URL não coincide com o ID do coletor.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coletorExistente = await _service.GetColetorByIdAsync(id);
            if (coletorExistente == null)
                return NotFound("Coletor não encontrado.");

            await _service.UpdateColetorAsync(coletor);
            return NoContent();
        }

        // Método DELETE para remover um coletor
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coletorExistente = await _service.GetColetorByIdAsync(id);
            if (coletorExistente == null)
                return NotFound("Coletor não encontrado.");

            await _service.DeleteColetorAsync(id);
            return NoContent();
        }
    }
}

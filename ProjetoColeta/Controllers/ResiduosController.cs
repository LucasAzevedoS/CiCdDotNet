using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoColeta.Models;
using ProjetoColeta.Services;

namespace ProjetoColeta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResiduoController : ControllerBase
    {
        private readonly IResiduoService _residuoService;
        
        public ResiduoController(IResiduoService residuoService)
        {
            _residuoService = residuoService;
        }

        [HttpPost]
        [Authorize(Roles = "gerente, operador, analista")]
        public async Task<IActionResult> Create([FromBody] ResiduoModel residuo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var novoResiduo = await _residuoService.CreateResiduoAsync(residuo);
                return CreatedAtAction(nameof(GetById), new { id = novoResiduo.IdResiduo }, novoResiduo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "gerente, operador, analista")]
        public async Task<IActionResult> GetAll()
        {
            var residuos = await _residuoService.GetAllResiduosAsync();
            return Ok(residuos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "gerente, operador, analista")]
        public async Task<IActionResult> GetById(int id)
        {
            var residuo = await _residuoService.GetResiduoByIdAsync(id);
            if (residuo == null)
                return NotFound();

            return Ok(residuo);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public async Task<IActionResult> Update(int id, [FromBody] ResiduoModel residuo)
        {
            if (id != residuo.IdResiduo)
                return BadRequest("ID da URL não coincide com o ID do resíduo.");

            try
            {
                await _residuoService.UpdateResiduoAsync(residuo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _residuoService.DeleteResiduoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedResiduos(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Número da página e tamanho da página devem ser maiores que 0.");
            }

            var result = await _residuoService.GetPagedResiduosAsync(pageNumber, pageSize);
            return Ok(result);
        }

    }
}

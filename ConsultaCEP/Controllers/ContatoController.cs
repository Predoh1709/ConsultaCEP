using AutoMapper;
using ConsultaCEP.DTOs;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaCEP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly IMapper _mapper;

        public ContatoController(IContatoService contatoService, IMapper mapper)
        {
            _contatoService = contatoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ContatoDTO>> CreateContato(int clienteId, [FromBody] ContatoCreateDTO dto)
        {
            var contato = _mapper.Map<Contato>(dto);
            var created = await _contatoService.CreateContato(clienteId, contato);
            return CreatedAtAction(nameof(GetContato), new { clienteId, id = created.Id }, _mapper.Map<ContatoDTO>(created));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDTO>>> GetContatos(int clienteId)
        {
            var contatos = await _contatoService.GetContatosByClienteId(clienteId);
            return Ok(_mapper.Map<IEnumerable<ContatoDTO>>(contatos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDTO>> GetContato(int clienteId, int id)
        {
            var contato = await _contatoService.GetContatoById(id);
            if (contato == null || contato.ClienteId != clienteId) return NotFound();
            return Ok(_mapper.Map<ContatoDTO>(contato));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContatoDTO>> UpdateContato(int clienteId, int id, [FromBody] ContatoUpdateDTO dto)
        {
            var contatoExistente = await _contatoService.GetContatoById(id);
            if (contatoExistente == null || contatoExistente.ClienteId != clienteId)
                return NotFound();

            contatoExistente.Tipo = dto.Tipo;
            contatoExistente.Texto = dto.Texto;

            var atualizado = await _contatoService.UpdateContato(id, contatoExistente);
            var contatoDTO = _mapper.Map<ContatoDTO>(atualizado);

            return Ok(contatoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int clienteId, int id)
        {
            var contato = await _contatoService.GetContatoById(id);
            if (contato == null || contato.ClienteId != clienteId) return NotFound();
            try
            {
                await _contatoService.DeleteContato(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

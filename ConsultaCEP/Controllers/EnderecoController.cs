using AutoMapper;
using ConsultaCEP.DTOs;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaCEP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoService enderecoService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<EnderecoDTO>> CreateEndereco(int clienteId, [FromBody] EnderecoCreateDTO dto)
        {
            var endereco = _mapper.Map<Endereco>(dto);
            var created = await _enderecoService.CreateEndereco(clienteId, endereco);
            return CreatedAtAction(nameof(GetEndereco), new { clienteId, id = created.Id }, _mapper.Map<EnderecoDTO>(created));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDTO>>> GetEnderecos(int clienteId)
        {
            var enderecos = await _enderecoService.GetEnderecosByClienteId(clienteId);
            return Ok(_mapper.Map<IEnumerable<EnderecoDTO>>(enderecos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoDTO>> GetEndereco(int clienteId, int id)
        {
            var endereco = await _enderecoService.GetEnderecoById(id);
            if (endereco == null || endereco.ClienteId != clienteId) return NotFound();
            return Ok(_mapper.Map<EnderecoDTO>(endereco));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnderecoDTO>> UpdateEndereco(int clienteId, int id, [FromBody] EnderecoUpdateDTO dto)
        {
            var enderecoExistente = await _enderecoService.GetEnderecoById(id);
            if (enderecoExistente == null || enderecoExistente.ClienteId != clienteId)
                return NotFound();

            enderecoExistente.CEP = dto.CEP;
            enderecoExistente.Numero = dto.Numero;
            enderecoExistente.Complemento = dto.Complemento;

            var atualizado = await _enderecoService.UpdateEndereco(id, enderecoExistente);
            var enderecoDTO = _mapper.Map<EnderecoDTO>(atualizado);

            return Ok(enderecoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int clienteId, int id)
        {
            var endereco = await _enderecoService.GetEnderecoById(id);
            if (endereco == null || endereco.ClienteId != clienteId) return NotFound();
            try
            {
                await _enderecoService.DeleteEndereco(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

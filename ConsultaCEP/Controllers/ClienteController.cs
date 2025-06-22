using AutoMapper;
using ConsultaCEP.DTOs;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaCEP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> CreateCliente([FromBody] ClienteCreateDTO clienteCreateDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteCreateDTO);

            try
            {
                var created = await _clienteService.CreateCliente(cliente);
                var clienteDTO = _mapper.Map<ClienteDTO>(created);
                return CreatedAtAction(nameof(GetCliente), new { id = created.Id }, clienteDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientes();
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            return Ok(clientesDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteById(id);
            if (cliente == null)
                return NotFound();

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            return Ok(clienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> UpdateCliente(int id, [FromBody] ClienteUpdateDTO clienteUpdateDTO)
        {
            var clienteExistente = await _clienteService.GetClienteById(id);
            if (clienteExistente == null)
                return NotFound();

            clienteExistente.Nome = clienteUpdateDTO.Nome;

            var atualizado = await _clienteService.UpdateCliente(id, clienteExistente);
            var clienteDTO = _mapper.Map<ClienteDTO>(atualizado);

            return Ok(clienteDTO); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var deleted = await _clienteService.DeleteCliente(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

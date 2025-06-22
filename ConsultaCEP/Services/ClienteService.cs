using AutoMapper;
using ConsultaCEP.Data;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCEP.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ConsultaCEPDbContext _context;
        private readonly IViaCEPService _viaCEPService;
        private readonly IMapper _mapper;

        public ClienteService(ConsultaCEPDbContext context, IViaCEPService viaCEPService, IMapper mapper)
        {
            _context = context;
            _viaCEPService = viaCEPService;
            _mapper = mapper;
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            if (cliente.Contatos == null || !cliente.Contatos.Any())
                throw new ArgumentException("O cliente deve ter pelo menos um contato.");
            if (cliente.Enderecos == null || !cliente.Enderecos.Any())
                throw new ArgumentException("O cliente deve ter pelo menos um endereço.");

            foreach (var contato in cliente.Contatos)
            {
                contato.Cliente = cliente;
            }

            foreach (var endereco in cliente.Enderecos)
            {
                var viaCepData = await _viaCEPService.ConsultarCEP(endereco.CEP);
                if (viaCepData != null)
                {
                    _mapper.Map(viaCepData, endereco);
                }
                else
                {
                    throw new ArgumentException($"CEP '{endereco.CEP}' não encontrado ou inválido para o endereço.");
                }
                endereco.Cliente = cliente;
            }

            cliente.DataCadastro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _context.Clientes
                .Include(c => c.Contatos)
                .Include(c => c.Enderecos)
                .ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes
                .Include(c => c.Contatos)
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> UpdateCliente(int id, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clienteExistente == null)
                return null;

            clienteExistente.Nome = cliente.Nome;

            await _context.SaveChangesAsync();
            return clienteExistente;
        }

        public async Task<bool> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Contatos)
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

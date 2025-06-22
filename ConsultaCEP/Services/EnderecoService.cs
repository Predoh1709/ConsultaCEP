using AutoMapper;
using ConsultaCEP.Data;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCEP.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly ConsultaCEPDbContext _context;
        private readonly IViaCEPService _viaCEPService;
        private readonly IMapper _mapper;

        public EnderecoService(ConsultaCEPDbContext context, IViaCEPService viaCEPService, IMapper mapper)
        {
            _context = context;
            _viaCEPService = viaCEPService;
            _mapper = mapper;
        }

        public async Task<Endereco> CreateEndereco(int clienteId, Endereco endereco)
        {
            var viaCepData = await _viaCEPService.ConsultarCEP(endereco.CEP);
            if (viaCepData != null)
            {
                var complementoUsuario = endereco.Complemento;
                _mapper.Map(viaCepData, endereco);
                endereco.Complemento = complementoUsuario;
            }
            else
            {
                throw new ArgumentException($"CEP '{endereco.CEP}' não encontrado ou inválido para o endereço.");
            }
            endereco.ClienteId = clienteId;
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> UpdateEndereco(int id, Endereco endereco)
        {
            var existing = await _context.Enderecos.FindAsync(id);
            if (existing == null) return null;

            var viaCepData = await _viaCEPService.ConsultarCEP(endereco.CEP);
            if (viaCepData != null)
            {
                existing.CEP = endereco.CEP;
                var complementoUsuario = endereco.Complemento;
                _mapper.Map(viaCepData, existing);
                existing.Complemento = complementoUsuario;
            }
            else
            {
                throw new ArgumentException($"CEP '{endereco.CEP}' não encontrado ou inválido para o endereço.");
            }

            existing.Numero = endereco.Numero;
            existing.Complemento = endereco.Complemento;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return false;

            var totalEnderecos = await _context.Enderecos.CountAsync(e => e.ClienteId == endereco.ClienteId);
            if (totalEnderecos <= 1)
                throw new InvalidOperationException("O cliente deve ter pelo menos um endereço.");

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        public async Task<IEnumerable<Endereco>> GetEnderecosByClienteId(int clienteId)
        {
            return await _context.Enderecos.Where(e => e.ClienteId == clienteId).ToListAsync();
        }
    }
}

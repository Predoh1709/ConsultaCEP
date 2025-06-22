using ConsultaCEP.Data;
using ConsultaCEP.Entities;
using ConsultaCEP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCEP.Services
{
    public class ContatoService : IContatoService
    {
        private readonly ConsultaCEPDbContext _context;

        public ContatoService(ConsultaCEPDbContext context)
        {
            _context = context;
        }

        public async Task<Contato> CreateContato(int clienteId, Contato contato)
        {
            contato.ClienteId = clienteId;
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
            return contato;
        }

        public async Task<Contato> UpdateContato(int id, Contato contato)
        {
            var existing = await _context.Contatos.FindAsync(id);
            if (existing == null) return null;
            existing.Tipo = contato.Tipo;
            existing.Texto = contato.Texto;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteContato(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null) return false;

            var totalContatos = await _context.Contatos.CountAsync(c => c.ClienteId == contato.ClienteId);
            if (totalContatos <= 1)
                throw new InvalidOperationException("O cliente deve ter pelo menos um contato.");

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Contato> GetContatoById(int id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task<IEnumerable<Contato>> GetContatosByClienteId(int clienteId)
        {
            return await _context.Contatos.Where(c => c.ClienteId == clienteId).ToListAsync();
        }
    }
}

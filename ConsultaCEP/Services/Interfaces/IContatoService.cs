using ConsultaCEP.Entities;

namespace ConsultaCEP.Services.Interfaces
{
    public interface IContatoService
    {
        Task<Contato> CreateContato(int clienteId, Contato contato);
        Task<Contato> UpdateContato(int id, Contato contato);
        Task<bool> DeleteContato(int id);
        Task<Contato> GetContatoById(int id);
        Task<IEnumerable<Contato>> GetContatosByClienteId(int clienteId);
    }
}

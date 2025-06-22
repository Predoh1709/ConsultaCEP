using ConsultaCEP.Entities;

namespace ConsultaCEP.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> GetClienteById(int id);
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(int id, Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}

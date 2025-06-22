using ConsultaCEP.Entities;

namespace ConsultaCEP.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> CreateEndereco(int clienteId, Endereco endereco);
        Task<Endereco> UpdateEndereco(int id, Endereco endereco);
        Task<bool> DeleteEndereco(int id);
        Task<Endereco> GetEnderecoById(int id);
        Task<IEnumerable<Endereco>> GetEnderecosByClienteId(int clienteId);
    }
}

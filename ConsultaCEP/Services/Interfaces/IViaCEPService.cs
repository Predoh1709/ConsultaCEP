using ConsultaCEP.DTOs;

namespace ConsultaCEP.Services.Interfaces
{
    public interface IViaCEPService
    {
        Task<ViaCEPResponseDTO> ConsultarCEP(string cep);
    }
}

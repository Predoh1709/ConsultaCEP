using ConsultaCEP.DTOs;
using ConsultaCEP.Services.Interfaces;
using System.Text.Json;

namespace ConsultaCEP.Services
{
    public class ViaCEPService : IViaCEPService
    {
        private readonly HttpClient _httpClient;

        public ViaCEPService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCEPResponseDTO> ConsultarCEP(string cep)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var viaCepResponse = JsonSerializer.Deserialize<ViaCEPResponseDTO>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (viaCepResponse != null && string.IsNullOrEmpty(viaCepResponse.Cep))
                {
                    return null;
                }

                return viaCepResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao consultar ViaCEP para {cep}: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erro de desserialização JSON do ViaCEP para {cep}: {ex.Message}");
                return null;
            }
        }
    }
}

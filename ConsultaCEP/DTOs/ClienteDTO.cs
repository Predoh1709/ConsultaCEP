namespace ConsultaCEP.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataCadastro { get; set; }
        public ICollection<ContatoDTO> Contatos { get; set; }
        public ICollection<EnderecoDTO> Enderecos { get; set; }
    }

    public class ClienteCreateDTO
    {
        public string Nome { get; set; }
        public ICollection<ContatoCreateDTO> Contatos { get; set; }
        public ICollection<EnderecoCreateDTO> Enderecos { get; set; }
    }

    public class ClienteUpdateDTO
    {
        public string Nome { get; set; }
    }
}

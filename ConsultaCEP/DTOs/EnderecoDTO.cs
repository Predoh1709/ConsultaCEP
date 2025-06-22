namespace ConsultaCEP.DTOs
{
    public class EnderecoDTO
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }

    public class EnderecoCreateDTO
    {
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }

    public class EnderecoUpdateDTO
    {
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}

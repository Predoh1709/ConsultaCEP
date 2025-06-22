namespace ConsultaCEP.DTOs
{
    public class ContatoDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Texto { get; set; }
    }

    public class ContatoCreateDTO
    {
        public string Tipo { get; set; }
        public string Texto { get; set; }
    }

    public class ContatoUpdateDTO
    {
        public string Tipo { get; set; }
        public string Texto { get; set; }
    }
}

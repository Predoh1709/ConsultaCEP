﻿namespace ConsultaCEP.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}

using AutoMapper;
using ConsultaCEP.DTOs;
using ConsultaCEP.Entities;

namespace ConsultaCEP.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<ClienteUpdateDTO, Cliente>();

            CreateMap<Contato, ContatoDTO>().ReverseMap();
            CreateMap<ContatoCreateDTO, Contato>();
            CreateMap<ContatoUpdateDTO, Contato>();

            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<EnderecoCreateDTO, Endereco>();
            CreateMap<EnderecoUpdateDTO, Endereco>();

            CreateMap<ViaCEPResponseDTO, Endereco>()
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Cep.Replace("-", "")))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

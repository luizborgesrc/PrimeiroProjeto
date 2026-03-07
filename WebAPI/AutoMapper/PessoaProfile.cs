using AutoMapper;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.AutoMapper;

public class PessoaProfile : Profile
{
    public PessoaProfile()
    {
        CreateMap<PessoaDto, PessoaModel>() .ReverseMap();
        CreateMap<PessoaCreateDto, PessoaModel>();
    }
}
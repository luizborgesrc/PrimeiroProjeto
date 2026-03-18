using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Repository;

namespace WebAplicationPessoa.WebAPI.Services;

public class PessoaService : IPessoaService
{
    private readonly IMapper _mapper;
    private readonly  IPessoaRepository _pessoaRepository;
    
    public PessoaService(IPessoaRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _pessoaRepository =  repository;
    }
    
    public async Task<IEnumerable<PessoaDto>> MostrarPessoas()
    {
        var pessoas = await _pessoaRepository.ListarPessoas();
        return _mapper.Map<IEnumerable<PessoaDto>>(pessoas);
    }

    public async Task<PessoaDto> MostrarPessoaPorId(int id)
    {
        var pessoaId = await _pessoaRepository.ObterPessoaPorId(id);
        return _mapper.Map<PessoaDto>(pessoaId);
    }

    public async Task<PessoaDto> CriarPessoa(PessoaCreateDto pessoaCreateDto, [FromServices] IValidator<PessoaCreateDto> validator)
    {
        var validationResult = await validator.ValidateAsync(pessoaCreateDto);
        if (!validationResult.IsValid)
        {
            throw new Exception("Usuario não passou na validação.");
        }

        var novaPessoa = _mapper.Map<PessoaModel>(pessoaCreateDto);
        
        await _pessoaRepository.InserirPessoa(novaPessoa);
        return _mapper.Map<PessoaDto>(novaPessoa);
    }

    public async Task<PessoaDto> AtualizarPessoa(int id, PessoaDto novaPessoaDto)
    {
        var pessoaNoBanco = await _pessoaRepository.ObterPessoaPorId(id);

        _mapper.Map(novaPessoaDto, pessoaNoBanco);
        await _pessoaRepository.AtualizarPessoa(pessoaNoBanco, id);
        
        return _mapper.Map<PessoaDto>(pessoaNoBanco);
    }

    public async Task<PessoaDto> DeletarPessoa(int id)
    {
        var pessoa = await _pessoaRepository.ExcluirPessoa(id);
        return _mapper.Map<PessoaDto>(pessoa);
    }
}
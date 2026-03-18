using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Services;

public interface IPessoaService
{
    public Task<IEnumerable<PessoaDto>> MostrarPessoas();

    public Task<PessoaDto> MostrarPessoaPorId(int id);
    
    public Task<PessoaDto> CriarPessoa(PessoaCreateDto pessoaCreateDto, [FromServices] IValidator<PessoaCreateDto> validator);


    public Task<PessoaDto> AtualizarPessoa(int id, PessoaCreateDto novaPessoaDto);


    public Task<PessoaDto> DeletarPessoa(int id);

}
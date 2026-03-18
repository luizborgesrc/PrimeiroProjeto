using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Repository;
using WebAplicationPessoa.WebAPI.Services;

namespace WebAplicationPessoa.WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;
    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> MostrarPessoas ()
    {
        var pessoas = await _pessoaService.MostrarPessoas();
        
        return Ok(pessoas);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaDto>> MostrarPessoaPorId (int id)
    {
        var pessoa = await _pessoaService.MostrarPessoaPorId(id);
        
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<ActionResult<PessoaDto>> CriarPessoa (PessoaCreateDto dadosNovaPessoa, [FromServices] IValidator<PessoaCreateDto> validator)
    {
        var pessoa = await _pessoaService.CriarPessoa(dadosNovaPessoa, validator);
        return Ok(pessoa);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PessoaDto>> AtualizarPessoa([FromRoute] int id, [FromBody] PessoaCreateDto dadosPessoaNova)
    {
        
        var pessoaNova = await _pessoaService.AtualizarPessoa(id, dadosPessoaNova);
        
        return Ok(pessoaNova);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PessoaDto>> ExcluirPessoa(int id)
    {
        await _pessoaService.DeletarPessoa(id);
        return NoContent();
    }
}

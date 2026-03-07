using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Repository;

namespace WebAplicationPessoa.WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _repository;
    
    public PessoaController(IPessoaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> MostrarPessoas ()
    {
        var pessoas = await _repository.ListarPessoas();
        
        return Ok(pessoas);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaDto>> MostrarPessoaPorId (int id)
    {
        var pessoaPorId = await _repository.ObterPessoaPorId(id);
        
        return Ok(pessoaPorId);
    }

    [HttpPost]
    public async Task<ActionResult<PessoaDto>> CriarPessoa (PessoaCreateDto dadosNovaPessoa, [FromServices] IValidator<PessoaCreateDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dadosNovaPessoa);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var criarPessoa = _mapper.Map<PessoaModel>(dadosNovaPessoa);
        
        await _repository.InserirPessoa(criarPessoa);
        
        var pessoaCriadaDto = _mapper.Map<PessoaDto>(criarPessoa);
        
        return CreatedAtAction(nameof(MostrarPessoaPorId), new { id = pessoaCriadaDto.Id }, pessoaCriadaDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PessoaDto>> AtualizarPessoa([FromRoute] int id, [FromBody] PessoaDto dadosPessoaNova)
    {
        var pessoaNoBanco =  await _repository.ObterPessoaPorId(id);

        _mapper.Map(dadosPessoaNova, pessoaNoBanco );
        await _repository.AtualizarPessoa(pessoaNoBanco, id);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PessoaDto>> ExcluirPessoa(int id)
    {
        await _repository.ExcluirPessoa(id);

        return NoContent();
    }
}

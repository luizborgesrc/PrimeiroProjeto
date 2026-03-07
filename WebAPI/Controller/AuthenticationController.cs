using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Repository;
using WebAplicationPessoa.WebAPI.Services;
using WebAplicationPessoa.WebAPI.Validators;

namespace WebAplicationPessoa.WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserRepository _repository;
    
    private readonly IAuthService _authService;

    public AuthenticationController(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;

    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserCadastroDto>> CriarUser(UserCadastroDto userDto, [FromServices] IValidator<UserCadastroDto> validator)
    {
        var validationResult = await validator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        try
        {
            await _repository.CriarUsuario(userDto);
            return Ok(userDto);
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginDto>> Login (UserLoginDto userDto)
    {
        var login = await _repository.Login(userDto);

        if (login == null)
        {
            return BadRequest("Usuário ou senha incorretos!");
        }
        var token = _authService.CriarToken(login);
        
        return Ok(token);
    }
}
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Repository;

public interface IUserRepository
{
    public Task<UserModel> CriarUsuario(UserCadastroDto userDto);
    
    public Task<UserModel?> Login(UserLoginDto loginDto);

}
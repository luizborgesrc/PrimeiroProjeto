using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.DTOs;
using WebAplicationPessoa.WebAPI.Model;
using WebAplicationPessoa.WebAPI.Services;

namespace WebAplicationPessoa.WebAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;
    
    public UserRepository(AppDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<UserModel> CriarUsuario(UserCadastroDto userDto)
    {
        _authService.CriarSenhaHash(userDto.Senha, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = new UserModel()
        {
            Nome = userDto.Nome,
            Email = userDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
        
        await _context.Users.AddAsync(newUser);
        
        await _context.SaveChangesAsync();
        
        return newUser;
    }

    public async Task<UserModel?> Login(UserLoginDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == userDto.Email);
        
        if (user == null) return null;

        if (!_authService.VerificarSenhaHash(user.Senha, user.PasswordHash, user.PasswordSalt))
        {
            return null;
        }

        return user;
    }
}
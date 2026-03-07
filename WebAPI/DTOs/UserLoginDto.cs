namespace WebAplicationPessoa.WebAPI.DTOs;

public class UserLoginDto
{
    public string Email { get; set; } = string.Empty;
    
    public string Senha { get; set; } = string.Empty;
}
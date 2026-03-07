namespace WebAplicationPessoa.WebAPI.Model;

public class UserModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Senha { get; set; } = string.Empty;
    
    public byte[] PasswordHash { get; set; } = new byte[0];
    
    public byte[] PasswordSalt { get; set; } = new byte[0];
    
    public string Rule {get ; set;} = "User";
}
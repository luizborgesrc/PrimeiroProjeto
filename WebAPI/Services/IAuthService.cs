using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Services;

public interface IAuthService
{
    public void CriarSenhaHash(string senha, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerificarSenhaHash(string senha, byte[] passwordHash, byte[] passwordSalt);
    public string CriarToken(UserModel usuario);
}
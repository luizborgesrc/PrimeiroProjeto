using System.ComponentModel.DataAnnotations;

namespace WebAplicationPessoa.WebAPI.Model;

public class PessoaModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; } = string.Empty;
    
    public string Cpf { get; set; } = string.Empty;
    
    public string  Telefone { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

}
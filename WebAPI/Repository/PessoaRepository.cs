using Microsoft.EntityFrameworkCore;
using WebAplicationPessoa.WebAPI.Data;
using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Repository;

public class PessoaRepository : IPessoaRepository
{
    private AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<PessoaModel>> ListarPessoas()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        return pessoas;
    }

    public async Task<PessoaModel> ObterPessoaPorId(int id)
    {
        var idPessoa  = await _context.Pessoas.FindAsync(id);
        
        return idPessoa;
    }

    public async Task<PessoaModel> AtualizarPessoa(PessoaModel pessoaNova, int id)
    {
        var pessoaNoBanco = await _context.Pessoas.FindAsync(id);
        
        pessoaNoBanco.Id = id;
        _context.Pessoas.Entry(pessoaNoBanco).CurrentValues.SetValues(pessoaNova);
        await _context.SaveChangesAsync();
        
        return pessoaNoBanco;
    }

    public async Task<PessoaModel> ExcluirPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        
        return pessoa;
    }

    public async Task<PessoaModel> InserirPessoa(PessoaModel pessoaNova)
    {
       await _context.Pessoas.AddAsync(pessoaNova);
       await _context.SaveChangesAsync();
        return pessoaNova;
    }
}
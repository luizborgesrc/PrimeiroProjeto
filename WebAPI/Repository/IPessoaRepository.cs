using WebAplicationPessoa.WebAPI.Model;

namespace WebAplicationPessoa.WebAPI.Repository;

public interface IPessoaRepository
{
    Task <IEnumerable<PessoaModel>> ListarPessoas();
    Task <PessoaModel> ObterPessoaPorId(int id);
    Task <PessoaModel> AtualizarPessoa(PessoaModel pessoa, int id);
    Task <PessoaModel> ExcluirPessoa(int id);
    Task <PessoaModel> InserirPessoa(PessoaModel pessoa);
}

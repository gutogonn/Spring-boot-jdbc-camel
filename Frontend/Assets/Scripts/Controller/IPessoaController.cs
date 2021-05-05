using System.Collections.Generic;

public interface IPessoaController
{
    List<Pessoa> Listar();
    Pessoa Buscar(long id);
    Pessoa Cadastrar(Pessoa pessoa);
    Pessoa Editar(Pessoa pessoa);
}

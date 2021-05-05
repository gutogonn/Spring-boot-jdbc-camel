using System.Collections.Generic;

public interface IServicoController
{
    List<Servico> Listar();
    Servico Buscar(long id);
    Servico Cadastrar(Servico pessoa);
    Servico Editar(Servico pessoa);
}

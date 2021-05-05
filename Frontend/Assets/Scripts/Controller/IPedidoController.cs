using System.Collections.Generic;

public interface IPedidoController
{
    List<Pedido> Listar();
    Pedido Buscar(long id);
    Pedido Cadastrar(Pedido pessoa);
    Pedido Editar(Pedido pessoa);
}

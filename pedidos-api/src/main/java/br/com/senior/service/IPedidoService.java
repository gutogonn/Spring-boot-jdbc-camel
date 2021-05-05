package br.com.senior.service;

import java.io.Serializable;
import java.util.List;

import br.com.senior.domain.Pedido;

public interface IPedidoService extends Serializable {
	
	public Pedido buscarPor(long codigo);
	public List<Pedido> listar();
	public Pedido inserir(Pedido pedido);
	public Pedido alterar(Pedido pedido);
}

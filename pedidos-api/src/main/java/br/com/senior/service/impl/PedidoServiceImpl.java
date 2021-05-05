package br.com.senior.service.impl;

import java.io.Serializable;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.senior.domain.Pedido;
import br.com.senior.repository.PedidoRepository;
import br.com.senior.service.IPedidoService;

@Service
public class PedidoServiceImpl implements Serializable, IPedidoService {

	private static final long serialVersionUID = 1L;
	
	@Autowired
	private PedidoRepository pedidoRepository;
	
	public Pedido buscarPor(long codigo) {
		return pedidoRepository.buscar(codigo);
	}
	
	public List<Pedido> listar() {
		return pedidoRepository.listar();
	}
	
	public Pedido inserir(Pedido pedido){
		return pedidoRepository.salvar(pedido);
	}
	
	public Pedido alterar(Pedido pedido) {
		return pedidoRepository.update(pedido);
	}
}

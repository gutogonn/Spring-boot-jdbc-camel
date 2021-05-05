package br.com.senior.controller;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import br.com.senior.domain.Pedido;
import br.com.senior.middleware.template.PedidoTemplate;
import br.com.senior.service.IPedidoService;

@RestController
@RequestMapping("/pedido")
public class PedidoController {
	@Autowired
	private IPedidoService pedidoService;
	
	@Autowired
	private PedidoTemplate pedidoTemplate;
	
	@GetMapping	
	public ResponseEntity<List<Map<String, Object>>> listarTodos(){
		List<Map<String, Object>> listagem = new ArrayList<Map<String,Object>>();
		for (Pedido p : pedidoService.listar()){
			Map<String, Object> pedido = pedidoTemplate.buscarPedidoRoute(pedidoService.buscarPor(p.getCodigo()));
			listagem.add(pedido);	
		}
		return ResponseEntity.ok(listagem);
	}
	
	@GetMapping("/{codigo}")	
	public ResponseEntity<Object> buscar(@PathVariable Long codigo) throws InterruptedException {
		Map<String, Object> pedido = pedidoTemplate.buscarPedidoRoute(pedidoService.buscarPor(codigo));
		return ResponseEntity.ok(pedido);
	}
	
	@PostMapping
	public ResponseEntity<Pedido> criar(@RequestBody Pedido pedido) {
		return ResponseEntity.ok(pedidoService.inserir(pedido));
	}
	
	@PutMapping
	public ResponseEntity<Pedido> atualizar(@RequestBody Pedido pedido) {
		return ResponseEntity.ok(pedidoService.alterar(pedido));
	}
}

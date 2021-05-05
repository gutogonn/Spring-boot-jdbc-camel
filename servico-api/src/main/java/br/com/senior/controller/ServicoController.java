package br.com.senior.controller;

import java.util.List;
import java.util.concurrent.ExecutionException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import br.com.senior.domain.Servico;
import br.com.senior.service.IServicoService;

@RestController
@RequestMapping("/servico")
public class ServicoController {
	
	@Autowired
	private IServicoService servicoService;	
	
	@GetMapping	
	public ResponseEntity<List<Servico>> listar() throws InterruptedException {		
		return ResponseEntity.ok(servicoService.listar());
	}
	
	@GetMapping("/{codigo}")	
	public ResponseEntity<Servico> buscar(@PathVariable long codigo) throws InterruptedException, ExecutionException {		
		return ResponseEntity.ok(servicoService.buscarPor(codigo));
	}
	
	@PostMapping
	public ResponseEntity<Servico> criar(@RequestBody Servico servico) throws InterruptedException, ExecutionException {
		//if (servicoService.existe(servico.getNome())) return ResponseEntity.status(HttpStatus.FOUND).body("Já existe um serviço com esse nome");
		return ResponseEntity.ok(servicoService.inserir(servico));
	}
	
	@PutMapping
	public ResponseEntity<Servico> atualizar(@RequestBody Servico servico) throws InterruptedException, ExecutionException {
		return ResponseEntity.ok(servicoService.alterar(servico));
	}
}

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

import br.com.senior.domain.Pessoa;
import br.com.senior.service.IPessoaService;

@RestController
@RequestMapping("/pessoa")
public class PessoaController {
	
	@Autowired
	private IPessoaService pessoaService;	
	
	@GetMapping	
	public ResponseEntity<List<Pessoa>> listar() throws InterruptedException {		
		return ResponseEntity.ok(pessoaService.listar());
	}
	
	@GetMapping("/{codigo}")	
	public ResponseEntity<Pessoa> buscar(@PathVariable long codigo) throws InterruptedException, ExecutionException {		
		return ResponseEntity.ok(pessoaService.buscarPor(codigo));
	}
	
	@PostMapping
	public ResponseEntity<Pessoa> criar(@RequestBody Pessoa pessoa) throws InterruptedException, ExecutionException {
		return ResponseEntity.ok(pessoaService.inserir(pessoa));
	}
	
	@PutMapping
	public ResponseEntity<Pessoa> atualizar(@RequestBody Pessoa pessoa) throws InterruptedException, ExecutionException {
		return ResponseEntity.ok(pessoaService.alterar(pessoa));
	}
	
}

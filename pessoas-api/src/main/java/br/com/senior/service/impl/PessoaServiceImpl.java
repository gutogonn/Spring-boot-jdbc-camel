package br.com.senior.service.impl;

import java.io.Serializable;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.senior.domain.Pessoa;
import br.com.senior.repository.PessoaRepository;
import br.com.senior.service.IPessoaService;

@Service
public class PessoaServiceImpl implements Serializable, IPessoaService {
	
	private static final long serialVersionUID = 1L;
	
	@Autowired
	private PessoaRepository pessoaRepository;

	@Override
	public Pessoa buscarPor(long codigo) {
		return pessoaRepository.buscar(codigo);
	}

	@Override
	public List<Pessoa> listar() {
		return pessoaRepository.listar();
	}

	@Override
	public Pessoa inserir(Pessoa pessoa) {
		return pessoaRepository.salvar(pessoa);
	}

	@Override
	public Pessoa alterar(Pessoa pessoa) {
		return pessoaRepository.update(pessoa);
	}

}

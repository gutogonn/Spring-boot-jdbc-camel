package br.com.senior.service;

import java.io.Serializable;
import java.util.List;

import br.com.senior.domain.Pessoa;

public interface IPessoaService extends Serializable {
	
	public Pessoa buscarPor(long codigo);
	public List<Pessoa> listar();
	public Pessoa inserir(Pessoa pessoa);
	public Pessoa alterar(Pessoa pessoa);

}

package br.com.senior.service;

import java.io.Serializable;
import java.util.List;

import br.com.senior.domain.Servico;

public interface IServicoService extends Serializable {
	
	public Servico buscarPor(long codigo);
	public List<Servico> listar();
	public Servico inserir(Servico servico);
	public Servico alterar(Servico servico);
	public boolean existe(String nome);
	
}

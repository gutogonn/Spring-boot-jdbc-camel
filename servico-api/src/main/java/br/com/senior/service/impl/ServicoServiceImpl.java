package br.com.senior.service.impl;

import java.io.Serializable;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import br.com.senior.domain.Servico;
import br.com.senior.repository.ServicoRepository;
import br.com.senior.service.IServicoService;

@Service
public class ServicoServiceImpl implements Serializable, IServicoService {
	
	private static final long serialVersionUID = 1L;
	
	@Autowired
	private ServicoRepository servicoRepository;
	
	public Servico buscarPor(long codigo) {
		return servicoRepository.buscar(codigo);
	}
	
	public List<Servico> listar() {
		return servicoRepository.listar();
	}
	
	public Servico inserir(Servico servico){
		return servicoRepository.salvar(servico);
	}
	
	public Servico alterar(Servico servico) {
		return servicoRepository.update(servico);
	}
	
	public boolean existe(String nome) {
		return servicoRepository.existe(nome);
	}
}

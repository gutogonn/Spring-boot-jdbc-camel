package br.com.senior.repository;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import br.com.senior.domain.Servico;
import br.com.senior.repository.rowmapper.ServicoRowMapper;

@Repository
public class ServicoRepository {
		
	@Autowired
	JdbcTemplate jdbcTemplate;
	
	public List<Servico> listar() {		
		StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, nome, valorHora FROM servicos");
		return jdbcTemplate.query(sql.toString(), new ServicoRowMapper());
	}

    public Servico salvar(Servico servico)  {    	
		StringBuilder sql = new StringBuilder();
		sql.append("INSERT INTO servicos ");
		sql.append("(nome, valorHora) ");
		sql.append("VALUES ");
		sql.append("(?, ?)");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				servico.getNome(), 
				servico.getValorHora()
		});
		
        return servico;
    }

	public Servico buscar(long codigo) {
    	StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, nome, valorHora FROM servicos WHERE codigo = ?");
		return jdbcTemplate.queryForObject(sql.toString(), new Object[] {codigo}, new ServicoRowMapper());
    }

    public Servico update(Servico servico) {
		StringBuilder sql = new StringBuilder();
		sql.append("UPDATE servicos SET ");
		sql.append("nome = ?, ");
		sql.append("valorHora = ? ");
		sql.append("WHERE codigo = ?");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				servico.getNome(), 
				servico.getValorHora(),
				servico.getCodigo()
		});
		
        return servico;
    }
    
    public boolean existe(String nome) {
    	StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo FROM servicos WHERE nome = ?");
		return jdbcTemplate.queryForObject(sql.toString(), new Object[] {nome}, new ServicoRowMapper()) != null;
    }

}

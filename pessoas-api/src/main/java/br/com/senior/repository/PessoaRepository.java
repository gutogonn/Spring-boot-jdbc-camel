package br.com.senior.repository;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import br.com.senior.domain.Pessoa;
import br.com.senior.repository.rowmapper.PessoaRowMapper;

@Repository
public class PessoaRepository {
	
	@Autowired
	JdbcTemplate jdbcTemplate;
	
	public List<Pessoa> listar() {		
		StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, nome FROM pessoas");
		return jdbcTemplate.query(sql.toString(), new PessoaRowMapper());
	}

    public Pessoa salvar(Pessoa pessoa)  {    	
		StringBuilder sql = new StringBuilder();
		sql.append("INSERT INTO pessoas ");
		sql.append("(nome) ");
		sql.append("VALUES ");
		sql.append("(?)");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				pessoa.getNome(), 
		});
		
        return pessoa;
    }

	public Pessoa buscar(long codigo) {
    	StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, nome FROM pessoas WHERE codigo = ?");
		return jdbcTemplate.queryForObject(sql.toString(), new Object[] {codigo}, new PessoaRowMapper());
    }

    public Pessoa update(Pessoa pessoa) {
		StringBuilder sql = new StringBuilder();
		sql.append("UPDATE pessoas SET ");
		sql.append("nome = ? ");
		sql.append("WHERE codigo = ?");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				pessoa.getNome(), 
				pessoa.getCodigo()
		});
		
        return pessoa;
    }
}

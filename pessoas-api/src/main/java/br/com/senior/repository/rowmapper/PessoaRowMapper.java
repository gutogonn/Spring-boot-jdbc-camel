package br.com.senior.repository.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import br.com.senior.domain.Pessoa;

public class PessoaRowMapper implements RowMapper<Pessoa>{
	@Override
	public Pessoa mapRow(ResultSet rs, int rowNum) throws SQLException {
		Pessoa pessoa = new Pessoa();
		pessoa.setCodigo(rs.getLong("codigo"));
		pessoa.setNome(rs.getString("nome"));
		return pessoa;
	}

}

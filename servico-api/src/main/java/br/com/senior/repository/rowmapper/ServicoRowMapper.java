package br.com.senior.repository.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import br.com.senior.domain.Servico;

public class ServicoRowMapper implements RowMapper<Servico>{
	@Override
	public Servico mapRow(ResultSet rs, int rowNum) throws SQLException {
		Servico servico = new Servico();
		servico.setCodigo(rs.getLong("codigo"));
		servico.setNome(rs.getString("nome"));
		servico.setValorHora(rs.getFloat("valorHora"));
		return servico;
	}

}

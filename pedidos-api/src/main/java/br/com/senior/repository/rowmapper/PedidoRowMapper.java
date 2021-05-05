package br.com.senior.repository.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import br.com.senior.domain.Pedido;

public class PedidoRowMapper implements RowMapper<Pedido> {
	@Override
	public Pedido mapRow(ResultSet rs, int rowNum) throws SQLException {
		Pedido pedido = new Pedido();
		pedido.setCodigo(rs.getLong("codigo"));
		pedido.setServicoId(rs.getLong("servicoId"));
		pedido.setPessoaId(rs.getLong("pessoaId"));
		pedido.setImposto(rs.getDouble("pessoaId"));
		pedido.setHorasAlocada(rs.getInt("horasAlocada"));
		return pedido;
	}
}

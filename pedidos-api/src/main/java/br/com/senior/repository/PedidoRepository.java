package br.com.senior.repository;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import br.com.senior.domain.Pedido;
import br.com.senior.repository.rowmapper.PedidoRowMapper;

@Repository
public class PedidoRepository {
	
	@Autowired
	JdbcTemplate jdbcTemplate;
	
	public List<Pedido> listar() {		
		StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, servicoId, pessoaId, horasAlocada, imposto FROM pedidos");
		return jdbcTemplate.query(sql.toString(), new PedidoRowMapper());
	}

    public Pedido salvar(Pedido pedido)  {    	
		StringBuilder sql = new StringBuilder();
		sql.append("INSERT INTO pedidos ");
		sql.append("(servicoId, pessoaId, horasAlocada, imposto) ");
		sql.append("VALUES ");
		sql.append("(?, ?, ?, ?)");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				pedido.getServicoId(),
				pedido.getPessoaId(),
				pedido.getHorasAlocada(), 
				pedido.getImposto(),
		});
		
        return pedido;
    }

	public Pedido buscar(long codigo) {
    	StringBuilder sql = new StringBuilder();
		sql.append("SELECT codigo, servicoId, pessoaId, horasAlocada, imposto FROM pedidos WHERE codigo = ?");
		return jdbcTemplate.queryForObject(sql.toString(), new Object[] { codigo }, new PedidoRowMapper());
    }

    public Pedido update(Pedido pedido) {
		StringBuilder sql = new StringBuilder();
		sql.append("UPDATE pedidos SET ");
		sql.append("servicoId = ?, ");
		sql.append("pessoaId = ?, ");
		sql.append("horasAlocada = ?, ");
		sql.append("imposto = ? ");
		sql.append("WHERE codigo = ?");
				
		jdbcTemplate.update(sql.toString(), new Object[] {
				pedido.getServicoId(),
				pedido.getPessoaId(),
				pedido.getHorasAlocada(), 
				pedido.getImposto(),
				pedido.getCodigo()
		});
		
        return pedido;
    }
}

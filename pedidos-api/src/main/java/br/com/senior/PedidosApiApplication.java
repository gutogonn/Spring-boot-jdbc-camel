package br.com.senior;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

@SpringBootApplication
public class PedidosApiApplication implements CommandLineRunner {

	@Autowired
	private JdbcTemplate jdbcTemplate;
	
	public static void main(String[] args) {
		SpringApplication.run(PedidosApiApplication.class, args);
	}
	
	@Override
	public void run(String... args) throws Exception {
		jdbcTemplate.execute("CREATE TABLE IF NOT EXISTS pedidos(codigo INTEGER PRIMARY KEY, servicoId INTEGER, horasAlocada INTEGER, pessoaId INTEGER, imposto FLOAT)");
	}

}

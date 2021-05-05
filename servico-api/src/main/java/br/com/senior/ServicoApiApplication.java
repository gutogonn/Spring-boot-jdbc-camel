package br.com.senior;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

@SpringBootApplication
public class ServicoApiApplication implements CommandLineRunner {

	@Autowired
	private JdbcTemplate jdbcTemplate;
	
	public static void main(String[] args) {
		SpringApplication.run(ServicoApiApplication.class, args);
	}
	
	@Override
	public void run(String... args) throws Exception {
		jdbcTemplate.execute("CREATE TABLE IF NOT EXISTS servicos(codigo INTEGER PRIMARY KEY, nome VARCHAR(100), valorHora FLOAT)");
	}

}

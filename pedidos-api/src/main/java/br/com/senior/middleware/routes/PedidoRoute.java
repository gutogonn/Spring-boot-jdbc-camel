package br.com.senior.middleware.routes;

import org.apache.camel.Exchange;
import org.apache.camel.builder.RouteBuilder;
import org.apache.camel.component.http.HttpMethods;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import br.com.senior.middleware.processor.PedidoProcessor;
import br.com.senior.middleware.processor.PessoaProcessor;

@Component
public class PedidoRoute extends RouteBuilder {
	
	@Value("${farol.zuul.route}")
	private String zuulRoute;
	
	PedidoProcessor pedidoProcessor = new PedidoProcessor();
	PessoaProcessor pessoaProcessor = new PessoaProcessor();
	
	@Override
	public void configure() throws Exception {
		from("direct:buscarPedido")				
			.id("transform-buscarPedido")
			
			.setProperty("servicoId").jsonpath("servicoId")
			.setProperty("pessoaId").jsonpath("pessoaId")
			.setProperty("pedidoBody").body()
			
			.setHeader(Exchange.HTTP_METHOD, HttpMethods.GET)
			.toD(zuulRoute + "/api/pessoa/${exchangeProperty[pessoaId]}")
			.process(pessoaProcessor)
			.marshal().json()
						
			.setHeader(Exchange.HTTP_METHOD, HttpMethods.GET)
			.toD(zuulRoute + "/api/servico/${exchangeProperty[servicoId]}")
			.process(pedidoProcessor)
				
		.end();
	}

}

package br.com.senior.middleware.processor;

import java.util.Map;

import org.apache.camel.Exchange;
import org.apache.camel.Processor;
import org.json.JSONObject;

public class PessoaProcessor implements Processor{

	@Override
	public void process(Exchange exchange) throws Exception {
		String body = exchange.getIn().getBody(String.class);
				   		
		JSONObject pessoa = new JSONObject(body);
		
		JSONObject response = new JSONObject((Map<String, Object>) exchange.getProperty("pedidoBody"));
		response.remove("pessoaId");
		response.put("pessoa", pessoa);
		
		exchange.setProperty("responseBody", response.toMap());
		exchange.getIn().setBody(response.toMap());
	}

}

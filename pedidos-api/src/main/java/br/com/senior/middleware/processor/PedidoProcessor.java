package br.com.senior.middleware.processor;

import java.util.Map;

import org.apache.camel.Exchange;
import org.apache.camel.Processor;
import org.json.JSONObject;

public class PedidoProcessor implements Processor{

	@Override
	public void process(Exchange exchange) throws Exception {
		String body = exchange.getIn().getBody(String.class);	
				   
		JSONObject response = new JSONObject((Map<String, Object>)exchange.getProperty("responseBody"));
		JSONObject servicoJson = new JSONObject(body);
		   
		response.remove("servicoId");
		response.put("servico", servicoJson);
		
		exchange.setProperty("responseBody", response.toMap());
		exchange.getIn().setBody(response.toMap());	
	}

}

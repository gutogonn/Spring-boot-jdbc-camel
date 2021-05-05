package br.com.senior.middleware.template;

import java.util.Map;

import org.apache.camel.Produce;
import org.apache.camel.ProducerTemplate;
import org.json.JSONObject;
import org.springframework.stereotype.Service;

import br.com.senior.domain.Pedido;

@Service
public class PedidoTemplate {
	
	@Produce("direct:buscarPedido")
	private ProducerTemplate buscarPedido;
	
	public Map<String, Object> buscarPedidoRoute (Pedido pedido) {
		try {
			Map<String, Object> fromCamel = (Map<String, Object>) buscarPedido.requestBody(buscarPedido.getDefaultEndpoint(), new JSONObject(pedido).toMap());
			JSONObject json = new JSONObject(fromCamel);
			return json.toMap();
		}catch(Exception e) {
			System.out.println(e.getMessage());
			return null;
		}
	}
}

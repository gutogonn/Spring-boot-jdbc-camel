package br.com.senior.domain;

import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Pedido implements Serializable{
	
	private static final long serialVersionUID = 1L;
	
	private long codigo;
	private long servicoId;
	private int horasAlocada;
	private long pessoaId;
	private double imposto;
	
	public Pedido() {
		
	}
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + (int) (codigo ^ (codigo >>> 32));
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Pedido other = (Pedido) obj;
		if (codigo != other.codigo)
			return false;
		return true;
	}
	
	public long getCodigo() {
		return codigo;
	}
	public void setCodigo(long codigo) {
		this.codigo = codigo;
	}
	public long getServicoId() {
		return servicoId;
	}
	public void setServicoId(long servicoId) {
		this.servicoId = servicoId;
	}
	public int getHorasAlocada() {
		return horasAlocada;
	}
	public void setHorasAlocada(int horasAlocada) {
		this.horasAlocada = horasAlocada;
	}
	public long getPessoaId() {
		return pessoaId;
	}
	public void setPessoaId(long pessoaId) {
		this.pessoaId = pessoaId;
	}
	public double getImposto() {
		return imposto;
	}
	public void setImposto(double imposto) {
		this.imposto = imposto;
	}
	
}	

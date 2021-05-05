using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pedido
{
    public int codigo;
	public Servico servico;
	public int horasAlocada;
	public Pessoa pessoa;
	public double imposto;
}

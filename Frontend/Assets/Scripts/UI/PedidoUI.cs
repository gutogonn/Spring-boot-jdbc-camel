using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PedidoUI : MonoBehaviour
{
    private IPessoaController pessoaController;
    private IPedidoController pedidoController;
    private IServicoController servicoController;

    [SerializeField] private List<Transform> telas;

    private List<Servico> servicos = new List<Servico>();
    private List<Pessoa> pessoas = new List<Pessoa>();

    [Header("Cadastro")]
    [SerializeField] private TMP_InputField horasAlocadaInput;
    [SerializeField] private TMP_InputField impostoInput;
    [SerializeField] private TMP_Dropdown pessoaDropdown;
    [SerializeField] private TMP_Dropdown servicoDropdown;
    [SerializeField] private Button cadastrarButton;
    [SerializeField] private Button editarButton;
    [SerializeField] private Button cancelarButton;
    [SerializeField] private Pedido pedidoSelecionado;
    private bool editando;

    [Header("Listagem")]
    [SerializeField] private Button filtroButton;
    [SerializeField] private TMP_InputField pesquisaInput;
    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject itemListaPrefab;

    void Start()
    {
        pessoaController = new PessoaController();
        pedidoController = new PedidoController();
        servicoController = new ServicoController();

        pessoas = pessoaController.Listar();
        servicos = servicoController.Listar();

        List<TMP_Dropdown.OptionData> pessoasData = new List<TMP_Dropdown.OptionData>();
        foreach(Pessoa p in pessoas) {
            TMP_Dropdown.OptionData op = new TMP_Dropdown.OptionData();
            op.text = p.codigo + " - " + p.nome;
            pessoasData.Add(op);
        }
        pessoaDropdown.AddOptions(pessoasData);

        List<TMP_Dropdown.OptionData> servicosData = new List<TMP_Dropdown.OptionData>();
        foreach(Servico s in servicos) {
            TMP_Dropdown.OptionData op = new TMP_Dropdown.OptionData();
            op.text = s.codigo + " - " + s.nome;
            servicosData.Add(op);
        }
        servicoDropdown.AddOptions(servicosData);

        TrocarTela(0);

        cadastrarButton.onClick.AddListener(() => Cadastrar());
        editarButton.onClick.AddListener(() => Editar());

        filtroButton.onClick.AddListener(() => Filtro(pesquisaInput.text));
    }

    public void TrocarTela(int id)
    {
        foreach (Transform t in telas)
        {
            t.GetComponent<CanvasGroup>().alpha = 0;
            t.GetComponent<CanvasGroup>().interactable = false;
            t.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        telas[id].GetComponent<CanvasGroup>().alpha = 1;
        telas[id].GetComponent<CanvasGroup>().interactable = true;
        telas[id].GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void Cadastrar()
    {
        if (horasAlocadaInput.text.Equals("")) return;
        Pedido novoPedido = new Pedido();
        novoPedido.imposto = double.Parse(impostoInput.text);
        novoPedido.horasAlocada = int.Parse(horasAlocadaInput.text);
        novoPedido.servico = servicos[servicoDropdown.value];
        novoPedido.pessoa = pessoas[pessoaDropdown.value];

        pedidoController.Cadastrar(novoPedido);
        LimparCampos();
    }

    public void Editar()
    {
        if (horasAlocadaInput.text.Equals("")) return;
        Pedido editPedido = new Pedido();
        editPedido.imposto = double.Parse(impostoInput.text);
        editPedido.horasAlocada = int.Parse(horasAlocadaInput.text);
        editPedido.servico = servicos[servicoDropdown.value];
        editPedido.pessoa = pessoas[pessoaDropdown.value];
        pedidoController.Editar(editPedido);
        LimparCampos();
    }

    public void LimparCampos()
    {
        impostoInput.text = "";
        horasAlocadaInput.text = "";
        servicoDropdown.value = 0;
        pessoaDropdown.value = 0;
        pedidoSelecionado = null;
    }

    public void Listagem()
    {
        cadastrarButton.gameObject.SetActive(true);
        editando = false;
        pesquisaInput.text = "";
        DestruirLista();
        List<Pedido> pedidos = pedidoController.Listar();
        foreach (Pedido p in pedidos)
        {
            GameObject itemLista = Instantiate(itemListaPrefab);
            itemLista.transform.SetParent(listContent);
            itemLista.name = p.codigo + " - " + p.servico.nome + " - " + p.pessoa.nome + " - " + p.horasAlocada + " Hrs - Valor: R$" + (p.servico.valorHora + (p.servico.valorHora * p.imposto)) ;
            itemLista.GetComponent<Button>().onClick.AddListener(() => ColocarEmEdicao(p.codigo));
        }
    }

    private void DestruirLista()
    {
        foreach (Transform p in listContent)
        {
            Destroy(p.gameObject);
        }
    }

    public void ColocarEmEdicao(int id)
    {
        cadastrarButton.gameObject.SetActive(false);
        pedidoSelecionado = pedidoController.Buscar(id);
        impostoInput.text = pedidoSelecionado.imposto.ToString();
        horasAlocadaInput.text = pedidoSelecionado.horasAlocada.ToString();
        servicoDropdown.value = 0;
        pessoaDropdown.value = 0;
        editando = true;
        TrocarTela(1);
    }

    private void Filtro(string filtro)
    {
        foreach (Transform child in listContent)
        {
            child.gameObject.SetActive(true);
            if (!child.name.Contains(filtro) && !filtro.Equals(""))
            {
                child.gameObject.SetActive(false);
            }

        }
    }
}

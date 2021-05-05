using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ServicoUI : MonoBehaviour
{
    private IServicoController servicoController;

    [SerializeField] private List<Transform> telas;

    [Header("Cadastro")]
    [SerializeField] private TMP_InputField nomeInput;
    [SerializeField] private TMP_InputField valorHoraInput;
    [SerializeField] private Button cadastrarButton;
    [SerializeField] private Button editarButton;
    [SerializeField] private Button cancelarButton;
    [SerializeField] private Servico servicoSelecionado;
    private bool editando;

    [Header("Listagem")]
    [SerializeField] private Button filtroButton;
    [SerializeField] private TMP_InputField pesquisaInput;
    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject itemListaPrefab;

    void Start()
    {
        servicoController = new ServicoController();

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
        if (nomeInput.text.Equals("")) return;
        Servico novoServico = new Servico();
        novoServico.nome = nomeInput.text;
        novoServico.valorHora = double.Parse(valorHoraInput.text);
        servicoController.Cadastrar(novoServico);
        LimparCampos();
    }

    public void Editar()
    {
        if (nomeInput.text.Equals("")) return;
        Servico editServico = new Servico();
        editServico.codigo = servicoSelecionado.codigo;
        editServico.nome = nomeInput.text;
        editServico.valorHora = double.Parse(valorHoraInput.text);
        servicoController.Editar(editServico);
        LimparCampos();
    }

    public void LimparCampos()
    {
        nomeInput.text = "";
        valorHoraInput.text = "";
        servicoSelecionado = null;
    }

    public void Listagem()
    {
        cadastrarButton.gameObject.SetActive(true);
        editando = false;
        pesquisaInput.text = "";
        DestruirLista();
        List<Servico> servicos = servicoController.Listar();
        foreach (Servico s in servicos)
        {
            GameObject itemLista = Instantiate(itemListaPrefab);
            itemLista.transform.SetParent(listContent);
            itemLista.name = s.codigo + " - " + s.nome + " - R$" + s.valorHora;
            itemLista.GetComponent<Button>().onClick.AddListener(() => ColocarEmEdicao(s.codigo));
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
        servicoSelecionado = servicoController.Buscar(id);
        nomeInput.text = servicoSelecionado.nome;
        valorHoraInput.text = servicoSelecionado.valorHora.ToString();
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

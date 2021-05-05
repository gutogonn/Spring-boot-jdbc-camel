using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PessoaUI : MonoBehaviour
{
    private IPessoaController pessoaController;

    [SerializeField] private List<Transform> telas;

    [Header("Cadastro")]
    [SerializeField] private TMP_InputField nomeInput;
    [SerializeField] private Button cadastrarButton;
    [SerializeField] private Button editarButton;
    [SerializeField] private Button cancelarButton;
    [SerializeField] private Pessoa pessoaSelecionada;
    private bool editando;

    [Header("Listagem")]
    [SerializeField] private Button filtroButton;
    [SerializeField] private TMP_InputField pesquisaInput;
    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject itemListaPrefab;

    void Start()
    {
        pessoaController = new PessoaController();

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
        Pessoa novaPessoa = new Pessoa();
        novaPessoa.nome = nomeInput.text;
        pessoaController.Cadastrar(novaPessoa);
        LimparCampos();
    }

    public void Editar()
    {
        if (nomeInput.text.Equals("")) return;
        Pessoa editPessoa = new Pessoa();
        editPessoa.codigo = pessoaSelecionada.codigo;
        editPessoa.nome = nomeInput.text;
        pessoaController.Editar(editPessoa);
        LimparCampos();
    }

    public void LimparCampos()
    {
        nomeInput.text = "";
        pessoaSelecionada = null;
    }

    public void Listagem()
    {
        cadastrarButton.gameObject.SetActive(true);
        editando = false;
        pesquisaInput.text = "";
        DestruirLista();
        List<Pessoa> pessoas = pessoaController.Listar();
        foreach (Pessoa p in pessoas)
        {
            GameObject itemLista = Instantiate(itemListaPrefab);
            itemLista.transform.SetParent(listContent);
            itemLista.name = p.codigo + " - " + p.nome;
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
        pessoaSelecionada = pessoaController.Buscar(id);
        nomeInput.text = pessoaSelecionada.nome;
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

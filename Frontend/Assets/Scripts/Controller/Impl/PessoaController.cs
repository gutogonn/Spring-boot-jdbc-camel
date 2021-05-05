using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Utility;

public class PessoaController : IPessoaController
{
    protected string url = "http://localhost:5555/api/pessoa";

    public List<Pessoa> Listar()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false) Task.Delay(1000 / 30);

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                string jsonFormat = "{\"pessoas\": " + www.downloadHandler.text + "}";
                PessoaWrapper pessoaWrapper = JsonUtility.FromJson<PessoaWrapper>(jsonFormat);
                List<Pessoa> pessoas = pessoaWrapper.pessoas;
                return pessoas;
            }
        }
    }

    public Pessoa Buscar(long id)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "/" + id))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false) Task.Delay(1000 / 30);

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                return JsonUtility.FromJson<Pessoa>(www.downloadHandler.text);
            }
        }
    }

    public Pessoa Cadastrar(Pessoa pessoa)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("nome", pessoa.nome);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] body = Encoding.UTF8.GetBytes(JSONHelper.jsonFy(request));
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false) Task.Delay(1000 / 30);

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                return null;
            }
            else
            {
                ManagerUI.Instance().CreateMessage("Cadastrado com Sucesso!");
                return JsonUtility.FromJson<Pessoa>(www.downloadHandler.text);
            }
        }
    }

    public Pessoa Editar(Pessoa pessoa)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("codigo", pessoa.codigo);
        request.Add("nome", pessoa.nome);

        using (UnityWebRequest www = new UnityWebRequest(url, "PUT"))
        {
            byte[] body = Encoding.UTF8.GetBytes(JSONHelper.jsonFy(request));
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false) Task.Delay(1000 / 30);

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                ManagerUI.Instance().CreateMessage("Alterado com Sucesso!");
                return JsonUtility.FromJson<Pessoa>(www.downloadHandler.text);
            }
        }
    }

}

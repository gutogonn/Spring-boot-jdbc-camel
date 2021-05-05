using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Utility;

public class ServicoController : IServicoController
{
    protected string url = "http://localhost:5555/api/servico";

    public List<Servico> Listar()
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
                string jsonFormat = "{\"servicos\": " + www.downloadHandler.text + "}";
                ServicoWrapper servicoWrapper = JsonUtility.FromJson<ServicoWrapper>(jsonFormat);
                List<Servico> servicos = servicoWrapper.servicos;
                return servicos;
            }
        }
    }

    public Servico Buscar(long id)
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
                return JsonUtility.FromJson<Servico>(www.downloadHandler.text);
            }
        }
    }

    public Servico Cadastrar(Servico servico)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("nome", servico.nome);
        request.Add("valorHora", servico.valorHora);

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
                Debug.Log(www.error);
                return null;
            }
            else
            {
                ManagerUI.Instance().CreateMessage("Cadastrado com Sucesso!");
                return JsonUtility.FromJson<Servico>(www.downloadHandler.text);
            }
        }
    }

    public Servico Editar(Servico servico)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("codigo", servico.codigo);
        request.Add("nome", servico.nome);
        request.Add("valorHora", servico.valorHora);

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
                return JsonUtility.FromJson<Servico>(www.downloadHandler.text);
            }
        }
    }

}

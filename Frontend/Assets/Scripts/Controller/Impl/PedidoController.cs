using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Utility;

public class PedidoController : IPedidoController
{
    protected string url = "http://localhost:5555/api/pedido";

    public List<Pedido> Listar()
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
                string jsonFormat = "{\"pedidos\": " + www.downloadHandler.text + "}";
                PedidoWrapper pedidoWrapper = JsonUtility.FromJson<PedidoWrapper>(jsonFormat);
                List<Pedido> pedidos = pedidoWrapper.pedidos;
                return pedidos;
            }
        }
    }

    public Pedido Buscar(long id)
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
                return JsonUtility.FromJson<Pedido>(www.downloadHandler.text);
            }
        }
    }

    public Pedido Cadastrar(Pedido pedido)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("servicoId", pedido.servico.codigo);
        request.Add("horasAlocada", pedido.horasAlocada);
        request.Add("pessoaId", pedido.pessoa.codigo);
        request.Add("imposto", pedido.imposto);

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
                return JsonUtility.FromJson<Pedido>(www.downloadHandler.text);
            }
        }
    }

    public Pedido Editar(Pedido pedido)
    {
        Dictionary<string, object> request = new Dictionary<string, object>();
        request.Add("codigo", pedido.codigo);
        request.Add("servicoId", pedido.servico.codigo);
        request.Add("horasAlocada", pedido.horasAlocada);
        request.Add("pessoaId", pedido.pessoa.codigo);
        request.Add("imposto", pedido.imposto);

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
                return JsonUtility.FromJson<Pedido>(www.downloadHandler.text);
            }
        }
    }

}

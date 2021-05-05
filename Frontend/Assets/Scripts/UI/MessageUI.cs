using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public Button closeButton;
    public TextMeshProUGUI messageText;

    public void Create(string msg)
    {
        messageText.text = msg.Replace("\"", "");
        closeButton.onClick.AddListener(() => Close());
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }
}

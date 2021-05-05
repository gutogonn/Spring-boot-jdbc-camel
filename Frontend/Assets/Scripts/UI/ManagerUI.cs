using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private List<Transform> menus;
    [SerializeField] private GameObject messagePrefab;

    private static ManagerUI instance = null;
    public static ManagerUI Instance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;
    }

    public void OpenMenu(int id)
    {
        foreach (Transform t in menus)
        {
            t.GetComponent<CanvasGroup>().alpha = 0;
            t.GetComponent<CanvasGroup>().interactable = false;
            t.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        menus[id].GetComponent<CanvasGroup>().alpha = 1;
        menus[id].GetComponent<CanvasGroup>().interactable = true;
        menus[id].GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CreateMessage(string msg)
    {
        GameObject obj = Instantiate(messagePrefab);
        obj.name = "MESSAGE_" + msg;
        obj.transform.SetParent(gameObject.transform);
        obj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        obj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        obj.GetComponent<MessageUI>().Create(msg);
    }
}

using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start(){
        text.text = gameObject.name;
    }
 
}

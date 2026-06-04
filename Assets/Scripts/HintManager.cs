using UnityEngine;
using UnityEngine.UI;
public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public Text hintText;
    public GameObject hintPanel;
    void Awake()
    {
        Instance = this;
        hintPanel.SetActive(false);
    }
    public void ShowHint(string text, float duration)
    {
        hintText.text = text;
        hintPanel.SetActive(true);
        Invoke("HideHint", duration);
    }
    void HideHint()
    {
        hintPanel.SetActive(false);
    }
}

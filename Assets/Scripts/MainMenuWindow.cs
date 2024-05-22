using CodeMonkey.Utils;
using UnityEngine;
public class MainMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("HowtoPlay").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        Debug.Log("tobbe");
        GameObject.Find("Playbutton").GetComponent<Button_UI>().ClickFunc = () => Loader.Load(Loader.Scene.SampleScene);
      


    }
}

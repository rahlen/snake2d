using CodeMonkey.Utils;
using UnityEngine;
public class MainMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("tobbe");
        GameObject.Find("Playbutton").GetComponent<Button_UI>().ClickFunc = () => Loader.Load(Loader.Scene.SampleScene);
      


    }
}

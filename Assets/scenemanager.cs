using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public string scenename;
    public Button changescene;
    public Button Quit;
    public Button howtoplay;
    public GameObject help;


    private enum Sub
    {
        Main,
        HowToPlay,
    }
    void Start()
    {
        changescene.onClick.AddListener(Changescene);
        //GameObject.Find("HowtoPlaySub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //GameObject.Find("Mainsub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        Quit.onClick.AddListener(Application.Quit);
        //howtoplay.onClick.AddListener("MainSub").ShowSub(Sub.HowToPlay);
        howtoplay.onClick.AddListener(tutorialwindow);

        ShowSub(Sub.Main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void tutorialwindow()
    {
        help.SetActive(true);
    }
    public void Changescene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void ShowSub(Sub sub)
    {
        GameObject.Find("Mainsub").gameObject.SetActive(false);
        GameObject.Find("Howtoplaysub").gameObject.SetActive(false);

        switch (sub)
        {
            case Sub.Main:
            GameObject.Find("Mainsub").gameObject.SetActive(true);
                break;
            case Sub.HowToPlay:
               GameObject.Find("HowtoPlaysub").gameObject.SetActive(true);
                break;


        }
    }
}

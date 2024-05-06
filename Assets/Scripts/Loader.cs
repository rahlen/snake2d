using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Loader
{
  public enum Scene
    {
        SampleScene,
        Loading,
    }

    private static Action loaderCallbackAction;
    public static void Load(Scene scene) {
        {
            loaderCallbackAction = () => {
                SceneManager.LoadScene(scene.ToString());

            };
        }
        
        
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadCallback()
    {
        if (loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
}

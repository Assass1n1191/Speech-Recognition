using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour 
{




	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		
	}

    public void Play()
    {
        iTween.CameraFadeAdd();
        iTween.CameraFadeTo(iTween.Hash("amount", 1f, 
                                        "time", 2f, 
                                        "oncompletetarget", gameObject, 
                                        "oncomplete", "LoadScene", 
                                        "oncompleteparams", "Game Screen"));

        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, 
                                               "to", 0f,
                                               "onupdate", "OnUiColorFade"));
    }

    private void OnUiColorFade(float alphaValue)
    {
        GameObject.Find("Canvas").GetComponent<CanvasGroup>().alpha = alphaValue;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

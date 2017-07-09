using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private void Start ()
	{
        iTween.CameraFadeAdd();
        iTween.CameraFadeFrom(iTween.Hash("amount", 1f, "time", 2f, "oncompletetarget", gameObject, "oncomplete", "StartGame"));
    }
	
	public void StartGame()
    {
        iTween.CameraFadeDestroy();
    }
}
